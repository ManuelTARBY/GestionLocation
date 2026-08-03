using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifChargeAnnuelle : Form
    {
        private readonly Dictionary<string, string> infoBien;
        private string idCharge;
        private readonly string typeReq;
        private readonly ListeCharges fenListeCharges;

        /// <summary>
        /// Constructeur de la fenêtre AjoutModifChargeAnnuelle
        /// </summary>
        /// <param name="fenListeCharges">Instance de la fenêtre ListeCharges</param>
        /// <param name="idCharge">Id de la charge annuelle ("0" pour une création)</param>
        public AjoutModifChargeAnnuelle(ListeCharges fenListeCharges, string idCharge = "0")
        {
            InitializeComponent();
            this.Text = "Ajout/Modification d'une charge";
            this.fenListeCharges = fenListeCharges;
            this.infoBien = fenListeCharges.GetLeBien();
            this.idCharge = idCharge;

            if (this.idCharge.Equals("0"))
            {
                this.typeReq = "INSERT";
                this.idCharge = ProchainIdCharge();
            }
            else
            {
                this.typeReq = "UPDATE";
                cobListeBien.Enabled = false;
            }

            RemplirComboListeBien();
            RemplirComboFreq();
            RemplirChamps();
        }

        /// <summary>
        /// Calcule le prochain id de charge disponible.
        /// IFNULL(...) évite un plantage sur la toute première charge créée
        /// (MAX() renvoie NULL sur une table vide, donc NULL+1 = NULL).
        /// </summary>
        private string ProchainIdCharge()
        {
            const string req = "SELECT IFNULL(MAX(idchargeannuelle), 0) + 1 FROM chargesannuelles";
            using var command = new MySqlCommand(req, Global.Connexion);
            return Convert.ToInt32(command.ExecuteScalar()).ToString();
        }

        /// <summary>
        /// Remplit la combo avec la liste des biens et des groupes de biens
        /// </summary>
        public void RemplirComboListeBien()
        {
            const string reqBiens = "SELECT nombien FROM bien WHERE bienarchive = 0 ORDER BY nombien";
            using (var command = new MySqlCommand(reqBiens, Global.Connexion))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cobListeBien.Items.Add(reader.GetString(0));
                }
            }

            const string reqGroupes = "SELECT nomdugroupe FROM grpedebiens ORDER BY nomdugroupe";
            using (var command = new MySqlCommand(reqGroupes, Global.Connexion))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cobListeBien.Items.Add(reader.GetString(0));
                }
            }
        }

        /// <summary>
        /// Remplit le champ combo avec les fréquences
        /// </summary>
        public void RemplirComboFreq()
        {
            const string req = "SELECT libelle FROM frequencepaiement";
            using var command = new MySqlCommand(req, Global.Connexion);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                cobFrequence.Items.Add(reader.GetString(0));
            }
        }

        /// <summary>
        /// Gère le remplissage des différents champs de la fenêtre
        /// </summary>
        public void RemplirChamps()
        {
            if (this.typeReq.Equals("UPDATE"))
            {
                RecupDonnees();
            }
        }

        /// <summary>
        /// Récupère les données de la charge en cours de modification
        /// </summary>
        public void RecupDonnees()
        {
            const string req =
                "SELECT libelle, montantcharge, refFrequence, imputable, annee, nombien " +
                "FROM chargesannuelles NATURAL JOIN bien WHERE idchargeannuelle = @id";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", int.Parse(this.idCharge));

            using var reader = command.ExecuteReader();
            reader.Read();

            txtLibelle.Text = reader.GetString("libelle");
            txtMontant.Text = reader["montantcharge"].ToString();
            txtAnnee.Text = reader["annee"].ToString();
            cbxImputable.Checked = reader.GetBoolean("imputable");
            cobFrequence.SelectedItem = reader.GetString("refFrequence");

            // NB : le JOIN se fait sur `bien`, donc `nombien` correspond toujours à UN SEUL
            // bien, même si la charge d'origine avait été créée pour un groupe (chaque bien
            // du groupe a alors sa propre ligne, avec son propre idchargeannuelle). Éditer
            // une charge de groupe ne permet donc de modifier que la ligne de ce bien précis
            // — comportement existant conservé tel quel, non modifié par cette correction.
            cobListeBien.SelectedItem = reader.GetString("nombien");
        }

        /// <summary>
        /// Valide l'enregistrement de la charge annuelle
        /// </summary>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!VerifChamps())
            {
                if (MontantVirg() == 0)
                {
                    MessageBox.Show("Veuillez remplir un montant correct.");
                }
                else if (!VerifAnnee())
                {
                    MessageBox.Show("Veuillez saisir une année correcte.");
                }
                else
                {
                    MessageBox.Show("Veuillez remplir tous les champs pour pouvoir valider la saisie.");
                }
                return;
            }

            List<int> lesId = RecupIdBiens(cobListeBien.SelectedItem.ToString());
            if (lesId.Count == 0)
            {
                MessageBox.Show("Bien ou groupe introuvable.");
                return;
            }

            float montantAnnuelTotal = CalculerMontantAnnuel();
            float montantAnnuelParBien = (float)Math.Round(montantAnnuelTotal / lesId.Count, 2);
            float montantChargeParBien = (float)Math.Round(MontantVirg() / lesId.Count, 2);

            foreach (int idBien in lesId)
            {
                if (this.typeReq.Equals("INSERT"))
                {
                    InsererCharge(idBien, montantChargeParBien, montantAnnuelParBien);
                    this.idCharge = (int.Parse(this.idCharge) + 1).ToString();
                }
                else
                {
                    MettreAJourCharge(idBien, montantChargeParBien, montantAnnuelParBien);
                }

                MajChargesDuBien(idBien);
            }

            this.fenListeCharges.RecupListeCharges();
            this.fenListeCharges.GetFenFicheBien()?.RemplirChamps();
            this.Dispose();
        }

        /// <summary>
        /// Récupère les id de bien concernés par le nom sélectionné (un bien, ou tous les
        /// biens d'un groupe). Met aussi à jour infoBien pour rester cohérent avec le bien
        /// réellement sélectionné.
        /// </summary>
        private List<int> RecupIdBiens(string nomSelectionne)
        {
            var ids = new List<int>();

            const string reqBien = "SELECT idbien FROM bien WHERE nombien = @nom";
            using (var command = new MySqlCommand(reqBien, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nom", nomSelectionne);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int idBien = reader.GetInt32(0);
                    ids.Add(idBien);
                    this.infoBien["id"] = idBien.ToString();
                    this.infoBien["nom"] = nomSelectionne;
                    return ids;
                }
            }

            const string reqGroupe =
                "SELECT idbien FROM lignegroupe WHERE idgroupe = " +
                "(SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = @nom)";
            using (var command = new MySqlCommand(reqGroupe, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nom", nomSelectionne);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ids.Add(reader.GetInt32(0));
                }
            }

            return ids;
        }

        /// <summary>
        /// Insère une nouvelle ligne de charge annuelle pour un bien
        /// </summary>
        private void InsererCharge(int idBien, float montantCharge, float montantAnnuel)
        {
            const string req =
                "INSERT INTO chargesannuelles (idchargeannuelle, idbien, libelle, refFrequence, annee, " +
                "montantcharge, chargeannuelle, imputable) " +
                "VALUES (@idCharge, @idBien, @libelle, @frequence, @annee, @montant, @montantAnnuel, @imputable)";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@idCharge", int.Parse(this.idCharge));
            command.Parameters.AddWithValue("@idBien", idBien);
            command.Parameters.AddWithValue("@libelle", txtLibelle.Text);
            command.Parameters.AddWithValue("@frequence", cobFrequence.SelectedItem.ToString());
            command.Parameters.AddWithValue("@annee", int.Parse(txtAnnee.Text));
            command.Parameters.AddWithValue("@montant", montantCharge);
            command.Parameters.AddWithValue("@montantAnnuel", montantAnnuel);
            command.Parameters.AddWithValue("@imputable", cbxImputable.Checked);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Met à jour une ligne de charge annuelle existante
        /// </summary>
        private void MettreAJourCharge(int idBien, float montantCharge, float montantAnnuel)
        {
            const string req =
                "UPDATE chargesannuelles SET idbien = @idBien, libelle = @libelle, refFrequence = @frequence, " +
                "annee = @annee, montantcharge = @montant, chargeannuelle = @montantAnnuel, imputable = @imputable " +
                "WHERE idchargeannuelle = @idCharge";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@idBien", idBien);
            command.Parameters.AddWithValue("@libelle", txtLibelle.Text);
            command.Parameters.AddWithValue("@frequence", cobFrequence.SelectedItem.ToString());
            command.Parameters.AddWithValue("@annee", int.Parse(txtAnnee.Text));
            command.Parameters.AddWithValue("@montant", montantCharge);
            command.Parameters.AddWithValue("@montantAnnuel", montantAnnuel);
            command.Parameters.AddWithValue("@imputable", cbxImputable.Checked);
            command.Parameters.AddWithValue("@idCharge", int.Parse(this.idCharge));
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Calcule le montant annuel de la charge en fonction du montant et de la fréquence renseignée
        /// </summary>
        private float CalculerMontantAnnuel()
        {
            const string req = "SELECT occurrence FROM frequencepaiement WHERE libelle = @libelle";
            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@libelle", cobFrequence.SelectedItem.ToString());

            using var reader = command.ExecuteReader();
            reader.Read();
            // Convert.ToSingle plutôt qu'un cast direct : évite un InvalidCastException
            // si le type exact de la colonne "occurrence" ne correspond pas pile à float.
            float occurrence = Convert.ToSingle(reader["occurrence"]);

            return (float)Math.Round(occurrence * MontantVirg(), 2);
        }

        /// <summary>
        /// Vérifie que les champs obligatoires soient remplis
        /// </summary>
        private bool VerifChamps()
        {
            bool annee = VerifAnnee();
            if (txtLibelle.Text.Equals("") || txtMontant.Text.Equals("") || cobFrequence.SelectedItem == null
                || MontantVirg() == 0 || !annee || cobListeBien.SelectedItem == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Met à jour la table bien au niveau du montant total des charges annuelles et des charges imputables.
        /// NB : logique quasi identique à ListeCharges.MajChargesDuBien — dupliquée ici faute de
        /// service partagé, à factoriser ensemble un jour si tu veux.
        /// </summary>
        public void MajChargesDuBien(int idBien)
        {
            int anneeCourante = DateTime.Now.Year;

            const string reqTotal = "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles WHERE idbien = @id AND annee = @annee";
            float charges;
            using (var command = new MySqlCommand(reqTotal, Global.Connexion))
            {
                command.Parameters.AddWithValue("@id", idBien);
                command.Parameters.AddWithValue("@annee", anneeCourante);
                charges = Convert.ToSingle(command.ExecuteScalar());
            }

            const string reqImputables =
                "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles " +
                "WHERE idbien = @id AND annee = @annee AND imputable = 1";
            float chImputables;
            using (var command = new MySqlCommand(reqImputables, Global.Connexion))
            {
                command.Parameters.AddWithValue("@id", idBien);
                command.Parameters.AddWithValue("@annee", anneeCourante);
                chImputables = Convert.ToSingle(command.ExecuteScalar());
            }

            const string reqMaj =
                "UPDATE bien SET chargeannuelles = @charges, chargesimputables = @chargesImputables WHERE idbien = @id";
            using (var command = new MySqlCommand(reqMaj, Global.Connexion))
            {
                command.Parameters.AddWithValue("@charges", (int)Math.Round(charges));
                command.Parameters.AddWithValue("@chargesImputables", Math.Round(chImputables / 12, 2));
                command.Parameters.AddWithValue("@id", idBien);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vérifie si l'année saisie est correcte
        /// </summary>
        public bool VerifAnnee()
        {
            return int.TryParse(txtAnnee.Text, out _);
        }

        /// <summary>
        /// Convertit le contenu de txtMontant en float, indépendamment du séparateur
        /// décimal saisi (virgule ou point) et de la culture du thread courant.
        /// </summary>
        public float MontantVirg()
        {
            string chaine = txtMontant.Text.Replace(',', '.');
            return float.TryParse(chaine, NumberStyles.Any, CultureInfo.InvariantCulture, out float result)
                ? result
                : 0;
        }

        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}