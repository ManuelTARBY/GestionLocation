using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class ListeCharges : Form
    {
        private string req;
        private MySqlCommand command;
        private readonly Dictionary<string, string> lesCharges, chargeBien, infoBien;
        private readonly FicheBien fenFicheBien;


        /// <summary>
        /// Constructeur de ListeCharges
        /// </summary>
        /// <param name="fenetre">Instance de la fenêtre ayant appelé le constructeur</param>
        public ListeCharges(Form fenetre)
        {
            InitializeComponent();
            this.infoBien = new Dictionary<string, string>();
            // Si le paramètre contient la fenêtre de type FicheBien
            if (typeof(FicheBien).IsInstanceOfType(fenetre))
            {
                this.fenFicheBien = fenetre as FicheBien;
                this.infoBien = this.fenFicheBien.GetInfoBien();
            }
            this.Text = "Liste des charges";
            this.lesCharges = new Dictionary<string, string>();
            this.chargeBien = new Dictionary<string, string>();
            RemplirListeBiens();
            RecupListeCharges();
            AfficheTitre();
        }


        /// <summary>
        /// Gère le titre à afficher
        /// </summary>
        public void AfficheTitre()
        {
            string titre = $"Liste des charges {cobAnnee.SelectedItem}";
            if (this.infoBien.ContainsKey("nom") && this.infoBien["nom"] != null)
            {
                titre += $" - {this.infoBien["nom"].ToUpper()}";
            }
            lblNomBien.Text = titre;
        }


        /// <summary>
        /// Récupère la liste des charges (les charges ponctuelles hors année en cours sont exclues) à partir de l'id du bien
        /// </summary>
        public void RecupListeCharges()
        {
            // Vide la liste
            lstCharges.Items.Clear();
            this.lesCharges.Clear();
            this.chargeBien.Clear();
            if (this.infoBien.ContainsKey("type") && this.infoBien["type"] != null)
            {
                // Si le bien est un bien
                if (this.infoBien["type"] == "bien")
                {
                    this.req = $"SELECT idchargeannuelle, nombien, libelle, montantcharge, refFrequence, annee " +
                        $"FROM chargesannuelles NATURAL JOIN bien WHERE annee = {cobAnnee.SelectedItem} ";
                     //$"FROM chargesannuelles NATURAL JOIN bien WHERE annee = YEAR(CURDATE()) ";
                    if (this.fenFicheBien != null || lstBiens.SelectedItem != null)
                    {
                        this.req += $"AND idbien={this.infoBien["id"]} ";
                    }
                }

                // Si le bien est un groupe de biens
                else if (this.infoBien["type"] == "groupe")
                {
                    // Récupère la liste des biens qui composent le groupe
                    List<int> lesIdBiens = new List<int>();
                    this.req = $"SELECT idbien FROM lignegroupe WHERE idgroupe = {this.infoBien["id"]}";
                    this.command = new MySqlCommand(this.req, Global.Connexion);
                    MySqlDataReader rdrGroupe = this.command.ExecuteReader();
                    if (rdrGroupe.HasRows)
                    {
                        while (rdrGroupe.Read())
                        {
                            lesIdBiens.Add(int.Parse(rdrGroupe["idbien"].ToString()));
                        }
                        rdrGroupe.Close();
                    }
                    // Récupère la liste des charges pour le groupe de bien
                    this.req = "SELECT idchargeannuelle, nombien, libelle, montantcharge, refFrequence, idchargeannuelle, annee " +
                        "FROM chargesannuelles NATURAL JOIN bien WHERE annee = YEAR(CURDATE()) ";
                    if (this.fenFicheBien != null || lstBiens.SelectedItem != null)
                    {
                        this.req += $"AND idbien IN ({string.Join(",", lesIdBiens.ConvertAll(v => v.ToString()))}) ";
                    }
                }
                this.req += "ORDER BY libelle, nombien, annee DESC";

                // Exécution de la requête
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                MySqlDataReader reader = this.command.ExecuteReader();
                string ligneCharge;
                while (reader.Read())
                {
                    // Remplit la liste des charges
                    // Ajoute le nom du bien si la liste concerne un groupe de bien
                    if (this.infoBien["type"] == "groupe")
                    {
                        ligneCharge = $"{reader["nombien"]} || ";
                    }
                    else
                    {
                        ligneCharge = "";
                    }
                    ligneCharge += $"{reader["libelle"]} || Montant : {reader["montantcharge"]} € || Fréquence : {reader["refFrequence"]}";
                    lstCharges.Items.Add(ligneCharge);

                    // Remplit le dictionnaire avec le contenu du listbox: idchargesannuelles
                    lesCharges.Add(ligneCharge, reader["idchargeannuelle"].ToString());
                    chargeBien.Add(ligneCharge, reader.GetString(1));
                }
                reader.Close();
            }
        }


        /// <summary>
        /// Gère le clic sur le bouton Ajouter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
/*            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner le bien concerné par la charge.");
                return;
            }*/
            // MajBienSelectionne();
            AjoutModifChargeAnnuelle fenCharge = new AjoutModifChargeAnnuelle(this);
            fenCharge.ShowDialog();
        }


        /// <summary>
        /// Permet de récupérer le bien
        /// </summary>
        /// <returns>Le bien</returns>
        public Dictionary<string, string> GetLeBien()
        {
            return this.infoBien;
        }


        /// <summary>
        /// Gère le clic sur le bouton modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (lstCharges.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une charge pour pouvoir la modifier.");
                return;
            }
            MajBienSelectionne();
            AjoutModifChargeAnnuelle fenCharge = new AjoutModifChargeAnnuelle(this, lesCharges[lstCharges.SelectedItem.ToString()]);
            fenCharge.ShowDialog();
        }


        /// <summary>
        /// Renvoie l'instance de FicheBien
        /// </summary>
        /// <returns>Instance de fichebien</returns>
        public FicheBien GetFenFicheBien()
        {
            return this.fenFicheBien;
        }


        /// <summary>
        /// Gère le demande de suppression d'une charge de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (lstCharges.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une charge dans la liste pour pouvoir la supprimer.");
            }
            else
            {
                // Demande confirmation de suppression du bien
                DialogResult result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer la charge : {lstCharges.SelectedItem} ?", "Confirmer suppression", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    this.req = $"DELETE FROM chargesannuelles WHERE idchargeannuelle = \"{lesCharges[lstCharges.SelectedItem.ToString()]}\"";
                    ExecuteReqIUD();
                    MajChargesDuBien();
                    RecupListeCharges();
                    if (this.fenFicheBien != null)
                    {
                        this.fenFicheBien.RemplirChamps();
                    }
                }
            }
        }


        /// <summary>
        /// Exécute une requête insert, update, delete
        /// </summary>
        private void ExecuteReqIUD()
        {
            this.command = new MySqlCommand(this.req, Global.Connexion);
            // préparation de la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }


        /// <summary>
        /// Met à jour le champ charges annuelles du bien
        /// </summary>
        public void MajChargesDuBien()
        {
            // Calcule la charge annuelle du bien
            float charges = 0;
            this.req = "SELECT SUM(chargeannuelle) AS 'TotalCharges' FROM chargesannuelles WHERE idbien = " +
                $"(SELECT idbien FROM bien WHERE nombien = '{chargeBien[lstCharges.SelectedItem.ToString()]}') " +
                "AND refFrequence != 'Ponctuelle'";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                charges = float.Parse(reader["TotalCharges"].ToString());
                reader.Close();
            }

            // Calcule la charge imputable au locataire du bien
            this.req += " AND imputable = True";
            float chImputables = 0;
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                chImputables = float.Parse(reader["TotalCharges"].ToString());
                reader.Close();
            }

            // Met à jour la table bien
            this.req = $"UPDATE bien SET chargeannuelles = \'{Math.Round(charges, 2)}\', " +
                $"chargesimputables = \'{Math.Round(chImputables / 12, 2)}\' WHERE idbien = {this.infoBien["id"]}";
            // Exécute la requête
            ExecuteReqIUD();
        }


        /// <summary>
        /// Gère la fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Remplit la liste des biens
        /// </summary>
        private void RemplirListeBiens()
        {
            List<string> listeBienGrpeDeBiens = new List<string>();
            // Récupère les biens
            this.req = "SELECT nombien FROM bien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                listeBienGrpeDeBiens.Add(reader["nombien"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();

            listeBienGrpeDeBiens.Sort();

            // Ajoute les biens dans la liste
            foreach (string bien in listeBienGrpeDeBiens)
            {
                lstBiens.Items.Add(bien);
            }

            // Si la fenêtre a été ouverte depuis la fiche d'un bien
            if (fenFicheBien != null)
            {
                // Positionne le focus sur le bien en question
                int index = lstBiens.FindString(this.infoBien["nom"]);
                if (index != -1)
                {
                    lstBiens.SetSelected(index, true);
                }
            }
        }


        /// <summary>
        /// Gère le filtrage des charges
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFiltrer_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un bien dans la liste pour pouvoir afficher ses charges.");
            }
            else
            {
                MajComboAnnee();
                MajBienSelectionne();
                RecupListeCharges();
                AfficheTitre();
            }
        }


        /// <summary>
        /// Met à jour la liste des charges lorsqu'on change l'année
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangerAnneeCharge(object sender, EventArgs e)
        {
            MajBienSelectionne();
            RecupListeCharges();
            AfficheTitre();
        }


        /// <summary>
        /// Met à jour la liste des années pour le bien sélectionné
        /// </summary>
        private void MajComboAnnee()
        {
            // Récupère les années de début et de fin d'exploitation
            int anneeMini, anneeMaxi;
            cobAnnee.Items.Clear();
            this.req = "SELECT MIN(YEAR(paiement.periodefacturee)) AS 'AnneeMini', " +
                "MAX(YEAR(paiement.periodefacturee)) AS 'AnneeMaxi' " +
                "FROM paiement NATURAL JOIN location NATURAL JOIN bien " +
                $"WHERE bien.nombien = \"{lstBiens.SelectedItem}\" and montantpaye != 0";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            anneeMini = reader.GetInt32(0);
            anneeMaxi = reader.GetInt32(1);
            reader.Close();

            // Remplit la combobox des années
            for (int i = anneeMini; i <= anneeMaxi; i++)
            {
                cobAnnee.Items.Add(i);
            }
            cobAnnee.SelectedIndex = cobAnnee.Items.Count - 1;
        }


        /// <summary>
        /// Met à jour les infos sur le bien sélectionné
        /// </summary>
        public void MajBienSelectionne()
        {
            this.infoBien.Clear();
            // Récupère les informations sur le bien/groupe sur lequel s'applique le filtre
            this.infoBien.Add("nom", lstBiens.SelectedItem.ToString());
            this.req = $"SELECT idbien FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            if (reader.HasRows)
            {
                this.infoBien.Add("type", "bien");
                reader.Read();
                this.infoBien.Add("id", reader["idbien"].ToString());
                reader.Close();
            }
            else
            {
                reader.Close();
                this.infoBien.Add("type", "groupe");
                this.req = $"SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = \"{lstBiens.SelectedItem}\"";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                reader = this.command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    this.infoBien.Add("id", reader["idgroupe"].ToString());
                    reader.Close();
                }
            }
        }
    }
}
