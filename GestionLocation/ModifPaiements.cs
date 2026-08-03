using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class ModifPaiements : Form
    {
        private readonly string idPaiement;
        private readonly Paiements fenPaiements;

        /// <summary>
        /// Constructeur de la fenêtre ModifPaiement
        /// </summary>
        public ModifPaiements(Paiements fenPaiements)
        {
            InitializeComponent();
            this.fenPaiements = fenPaiements;
            this.idPaiement = this.fenPaiements.GetIdPaiement();
            RemplirChamps();
        }

        /// <summary>
        /// Remplit les différents champs de la fenêtre
        /// </summary>
        public void RemplirChamps()
        {
            try
            {
                string idLocation = "";
                string locataire = "";
                string bien = "";
                string periode = "";

                // 1. Récupération des informations du paiement
                string reqPaiement = "SELECT idlocation, datepaiement, montantpaye, periodefacturee, montantdu, resteapayer, loyerregle " +
                                     "FROM paiement WHERE idpaiement = @id";

                using (var cmdPaiement = new MySqlCommand(reqPaiement, Global.Connexion))
                {
                    cmdPaiement.Parameters.AddWithValue("@id", this.idPaiement);

                    using (var reader = cmdPaiement.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idLocation = reader["idlocation"].ToString();

                            // Vérification de la présence et de la validité de la date pour le DateTimePicker
                            if (reader["datepaiement"] != DBNull.Value)
                            {
                                DateTime dt = Convert.ToDateTime(reader["datepaiement"]);

                                // Si la date est valide et dans les bornes du contrôleur, on l'affecte
                                if (dt >= datPaiement.MinDate && dt <= datPaiement.MaxDate)
                                {
                                    datPaiement.Value = dt;
                                }
                                else
                                {
                                    // Date par défaut si 01/01/0001 ou hors limites
                                    datPaiement.Value = DateTime.Today;
                                }
                            }
                            else
                            {
                                datPaiement.Value = DateTime.Today;
                            }

                            txtMontantPaye.Text = reader["montantpaye"].ToString();

                            if (reader["periodefacturee"] != DBNull.Value)
                            {
                                periode = $"{Convert.ToDateTime(reader["periodefacturee"]):MMMM yyyy}";
                            }

                            txtMontantDu.Text = reader["montantdu"].ToString();
                            txtResteAPayer.Text = reader["resteapayer"].ToString();
                            cbxRegle.Checked = Convert.ToBoolean(reader["loyerregle"]);
                        }
                    }
                }

                // 2. Récupération du nom du locataire (ExecuteScalar est plus adapté pour une valeur unique)
                string reqLocataire = "SELECT nomcompletlocataire FROM locataire WHERE idlocataire = " +
                                      "(SELECT idlocataire FROM location WHERE idlocation = @idLocation)";
                using (var cmdLocataire = new MySqlCommand(reqLocataire, Global.Connexion))
                {
                    cmdLocataire.Parameters.AddWithValue("@idLocation", idLocation);
                    object resultLocataire = cmdLocataire.ExecuteScalar();
                    if (resultLocataire != null) locataire = resultLocataire.ToString();
                }

                // 3. Récupération du nom du bien
                string reqBien = "SELECT nombien FROM bien WHERE idbien = " +
                                 "(SELECT idbien FROM location WHERE idlocation = @idLocation)";
                using (var cmdBien = new MySqlCommand(reqBien, Global.Connexion))
                {
                    cmdBien.Parameters.AddWithValue("@idLocation", idLocation);
                    object resultBien = cmdBien.ExecuteScalar();
                    if (resultBien != null) bien = resultBien.ToString();
                }

                // 4. Affiche le locataire + bien
                string ligneUn = $"{locataire} - {bien.ToUpper()}";
                string separation = new string('-', (int)(ligneUn.Length * 1.2)); // Remplace la boucle For

                lblLocation.Text = $"{ligneUn}\n{separation}\n{periode.ToUpper()}";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ferme la fenêtre
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Gère l'enregistrement du Paiement
        /// </summary>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            // Modifie le format du montant payé pour le TryParse (tolérance de saisie)
            string strMontant = txtMontantPaye.Text.Replace('.', ',');

            // Si le contenu du champ du montant payé est incorrect
            if (!float.TryParse(strMontant, out float montant))
            {
                MessageBox.Show("Montant incorrect", "Erreur de saisie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMontantPaye.Focus();
                return; // Stoppe l'exécution si la saisie est mauvaise
            }

            // Calcule les valeurs de l'enregistrement à mettre à jour
            string strMontantDu = txtMontantDu.Text.Replace('.', ',');
            if (!float.TryParse(strMontantDu, out float montantDu))
            {
                montantDu = 0; // Sécurité si la base retourne un format inattendu
            }

            float resteAPayer = montantDu - montant;
            bool loyerregle = resteAPayer <= 0;

            try
            {
                // Construit la requête de mise à jour paramétrée
                string reqUpdate = "UPDATE paiement SET datepaiement = @datepaiement, montantpaye = @montantpaye, " +
                                   "resteapayer = @resteapayer, loyerregle = @loyerregle WHERE idpaiement = @id";

                using (var command = new MySqlCommand(reqUpdate, Global.Connexion))
                {
                    // L'avantage des paramètres : plus besoin de s'embêter avec les Replace(',', '.') pour la BDD !
                    // MySQL gérera tout seul la conversion du float en format base de données.
                    command.Parameters.AddWithValue("@datepaiement", datPaiement.Value.Date);
                    command.Parameters.AddWithValue("@montantpaye", montant);
                    command.Parameters.AddWithValue("@resteapayer", resteAPayer);
                    command.Parameters.AddWithValue("@loyerregle", loyerregle);
                    command.Parameters.AddWithValue("@id", this.idPaiement);

                    command.ExecuteNonQuery();
                }

                // Met à jour l'affichage de la fenêtre parente
                this.fenPaiements.RemplirListePaiements();
                this.fenPaiements.EnvoiReqSelectPaiements();

                // Si le loyer est réglé, demande s'il faut envoyer la quittance par mail au locataire
                if (loyerregle)
                {
                    /* // Demande pour envoi de la quittance de loyer
                    DialogResult result = MessageBox.Show($"Voulez-vous envoyer une quittance au locataire ?", "Envoi de quittance", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Préparation et envoi de la quittance
                        if (!fenPaiements.VerifMail().Equals(""))
                        {
                            this.fenPaiements.GestionQuittance(this.idPaiement);
                        }
                        else
                        {
                            MessageBox.Show("Impossible d'envoyer la quittance au locataire, vous n'avez pas renseigné son adresse mail.");
                        }
                    }*/
                }

                // Ferme la fenêtre
                this.Dispose();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement en base de données :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}