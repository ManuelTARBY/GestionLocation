using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifLocataires : Form
    {
        private readonly Locataires fenLocataire;
        private readonly string typeReq;
        private readonly int id;
        private string[] nomprenom;

        /// <summary>
        /// Constructeur de AjoutModifLocataires
        /// </summary>
        public AjoutModifLocataires(Locataires fenLocataire, string typeReq, int id = 0)
        {
            InitializeComponent();
            this.Text = "Ajout/Modification d'un locataire";
            this.fenLocataire = fenLocataire;
            this.typeReq = typeReq;
            this.id = id;

            if (this.id == 0)
            {
                // Calcul du nouvel ID si création
                const string reqMaxId = "SELECT IFNULL(MAX(idlocataire), 0) FROM locataire";
                using var command = new MySqlCommand(reqMaxId, Global.Connexion);
                object result = command.ExecuteScalar();
                this.id = Convert.ToInt32(result) + 1;
            }
            else
            {
                AfficheInfo();
            }

            lblID.Text = $"ID : {this.id}";
        }

        /// <summary>
        /// Remplit les champs du formulaire lors d'une modification
        /// </summary>
        private void AfficheInfo()
        {
            const string req = "SELECT prenomlocataire, nomlocataire, adresselocataire, cplocataire, " +
                               "villelocataire, datenaissancelocataire, lieunaissancelocataire, " +
                               "telephonelocataire, emailocataire, locatairearchive " +
                               "FROM locataire WHERE idlocataire = @id";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", this.id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // Récupération sécurisée par le nom des colonnes SQL
                txtPrenom.Text = reader["prenomlocataire"].ToString();
                txtNom.Text = reader["nomlocataire"].ToString();
                txtAdresse.Text = reader["adresselocataire"].ToString();
                txtCp.Text = reader["cplocataire"].ToString();
                txtVille.Text = reader["villelocataire"].ToString();

                if (reader["datenaissancelocataire"] != DBNull.Value)
                {
                    datDateNaissance.Value = Convert.ToDateTime(reader["datenaissancelocataire"]);
                }

                txtLieuNaissance.Text = reader["lieunaissancelocataire"].ToString();
                txtTelephone.Text = reader["telephonelocataire"].ToString();
                txtEmail.Text = reader["emailocataire"].ToString();
                cbxArchive.Checked = Convert.ToBoolean(reader["locatairearchive"]);
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton "Valider"
        /// </summary>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!ChampsRenseignes())
            {
                MessageBox.Show("Vous devez remplir les champs Prénom, Nom, Téléphone et saisir un Email valide.",
                                "Saisie incomplète", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.nomprenom = MiseEnFormeNomPrenom();

            string req = this.typeReq.Equals("UPDATE", StringComparison.OrdinalIgnoreCase)
                ? ObtenirReqModif()
                : ObtenirReqAjout();

            try
            {
                using (var command = new MySqlCommand(req, Global.Connexion))
                {
                    AjouterParametres(command);
                    command.ExecuteNonQuery();
                }

                this.fenLocataire.RemplirLstLocataires();
                this.Dispose();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement en base de données :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vérifie si tous les champs obligatoires sont renseignés et valides
        /// </summary>
        private bool ChampsRenseignes()
        {
            // Vérification basique des champs non vides
            if (string.IsNullOrWhiteSpace(txtNom.Text) ||
                string.IsNullOrWhiteSpace(txtTelephone.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                return false;
            }

            // Contrôle de cohérence sur le format de l'adresse email
            if (!txtEmail.Text.Contains("@"))
            {
                return false;
            }

            // Vérification que le prénom contient au moins un caractère valide hors virgules/espaces
            string prenomNettoye = txtPrenom.Text.Replace(",", "").Trim();
            return !string.IsNullOrWhiteSpace(prenomNettoye);
        }

        /// <summary>
        /// Retourne la requête d'ajout sous forme de chaîne paramétrée
        /// </summary>
        private string ObtenirReqAjout()
        {
            return "INSERT INTO locataire (idlocataire, prenomlocataire, nomlocataire, nomcompletlocataire, " +
                   "adresselocataire, cplocataire, villelocataire, datenaissancelocataire, lieunaissancelocataire, " +
                   "telephonelocataire, emailocataire, locatairearchive) " +
                   "VALUES (@id, @prenom, @nom, @nomcomplet, @adresse, @cp, @ville, @datenaissance, " +
                   "@lieunaissance, @telephone, @email, @archive)";
        }

        /// <summary>
        /// Retourne la requête de modification sous forme de chaîne paramétrée
        /// </summary>
        private string ObtenirReqModif()
        {
            return "UPDATE locataire SET " +
                   "prenomlocataire = @prenom, nomlocataire = @nom, nomcompletlocataire = @nomcomplet, " +
                   "adresselocataire = @adresse, cplocataire = @cp, villelocataire = @ville, " +
                   "datenaissancelocataire = @datenaissance, lieunaissancelocataire = @lieunaissance, " +
                   "telephonelocataire = @telephone, emailocataire = @email, locatairearchive = @archive " +
                   "WHERE idlocataire = @id";
        }

        /// <summary>
        /// Ajoute et type tous les paramètres de la requête SQL
        /// </summary>
        private void AjouterParametres(MySqlCommand command)
        {
            command.Parameters.AddWithValue("@id", this.id);
            command.Parameters.AddWithValue("@prenom", this.nomprenom[0]);
            command.Parameters.AddWithValue("@nom", this.nomprenom[1]);
            command.Parameters.AddWithValue("@nomcomplet", this.nomprenom[2]);
            command.Parameters.AddWithValue("@adresse", txtAdresse.Text.Trim());
            command.Parameters.AddWithValue("@cp", txtCp.Text.Trim());
            command.Parameters.AddWithValue("@ville", txtVille.Text.Trim().ToUpper());
            command.Parameters.AddWithValue("@datenaissance", datDateNaissance.Value.Date);
            command.Parameters.AddWithValue("@lieunaissance", txtLieuNaissance.Text.Trim().ToUpper());
            command.Parameters.AddWithValue("@telephone", EspacerNumTel());
            command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            command.Parameters.AddWithValue("@archive", cbxArchive.Checked);
        }

        /// <summary>
        /// Génère les espaces tous les deux chiffres pour le numéro de téléphone
        /// </summary>
        private string EspacerNumTel()
        {
            StringBuilder leNum = new StringBuilder(txtTelephone.Text.Trim());
            if (leNum.Length == 10)
            {
                int[] indices = { 2, 5, 8, 11 };
                foreach (int i in indices)
                {
                    leNum.Insert(i, " ");
                }
            }
            return leNum.ToString();
        }

        /// <summary>
        /// Récupère et met en forme le nom et les prénoms de manière tolérante à la saisie
        /// </summary>
        private string[] MiseEnFormeNomPrenom()
        {
            string[] result = { "", "", "" };

            // Découpage sur la virgule, puis suppression des espaces superflus autour de chaque prénom
            string[] lesPrenoms = txtPrenom.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lesPrenoms.Length; i++)
            {
                lesPrenoms[i] = lesPrenoms[i].Trim();
            }

            result[1] = txtNom.Text.Trim().ToUpper();

            if (lesPrenoms.Length > 0)
            {
                result[2] = result[1] + " " + Global.Capitalize(lesPrenoms[0]);

                for (int i = 0; i < lesPrenoms.Length - 1; i++)
                {
                    result[0] += Global.Capitalize(lesPrenoms[i]) + ", ";
                }
                result[0] += Global.Capitalize(lesPrenoms[lesPrenoms.Length - 1]);
            }
            else
            {
                result[2] = result[1];
            }

            return result;
        }
    }
}