using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class AjoutModifCautions : Form
    {
        private readonly Cautions fenCaution;
        private readonly string typeReq;
        private readonly int id;
        private string[] nomprenom;

        /// <summary>
        /// Constructeur de la fenêtre AjoutModifCautions
        /// </summary>
        public AjoutModifCautions(Cautions fenCaution, string typeReq, int id = 0)
        {
            InitializeComponent();
            this.Text = "Ajout/Modification d'une caution";
            this.fenCaution = fenCaution;
            this.typeReq = typeReq;
            this.id = id;

            if (this.id == 0)
            {
                // Calcul du nouvel ID si création
                const string reqMaxId = "SELECT IFNULL(MAX(idcaution), 0) FROM caution";
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
            const string req = "SELECT prenomcaution, nomcaution, adressecaution, cpcaution, " +
                               "villecaution, telephonecaution, emailcaution, cautionarchivee " +
                               "FROM caution WHERE idcaution = @id";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@id", this.id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                // Accès sécurisé par les noms de colonnes SQL
                txtPrenom.Text = reader["prenomcaution"].ToString();
                txtNom.Text = reader["nomcaution"].ToString();
                txtAdresse.Text = reader["adressecaution"].ToString();
                txtCp.Text = reader["cpcaution"].ToString();
                txtVille.Text = reader["villecaution"].ToString();
                txtTelephone.Text = reader["telephonecaution"].ToString();
                txtEmail.Text = reader["emailcaution"].ToString();
                cbxArchive.Checked = Convert.ToBoolean(reader["cautionarchivee"]);
            }
        }

        /// <summary>
        /// Vérifie si tous les champs obligatoires ont été renseignés et valides
        /// </summary>
        private bool ChampsRenseignes()
        {
            // Vérification que le prénom contient au moins un caractère valide hors virgules/espaces
            string prenomNettoye = txtPrenom.Text.Replace(",", "").Trim();

            if (string.IsNullOrWhiteSpace(prenomNettoye) || string.IsNullOrWhiteSpace(txtNom.Text))
            {
                return false;
            }

            bool emailRenseigne = !string.IsNullOrWhiteSpace(txtEmail.Text);
            bool telephoneRenseigne = !string.IsNullOrWhiteSpace(txtTelephone.Text);

            if (!emailRenseigne && !telephoneRenseigne)
            {
                return false;
            }

            // Contrôle de cohérence sur le format de l'adresse email si elle est renseignée
            if (emailRenseigne && !txtEmail.Text.Contains("@"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gère le clic sur le bouton Valider
        /// </summary>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (!ChampsRenseignes())
            {
                MessageBox.Show("Vous devez au moins remplir les champs Prénom, Nom et Téléphone ou saisir un Email valide pour pouvoir valider la saisie.",
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

                this.fenCaution.RemplirLstCautions();
                this.Dispose();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement en base de données :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Retourne la requête de modification sous forme de chaîne paramétrée
        /// </summary>
        private string ObtenirReqModif()
        {
            return "UPDATE caution SET " +
                   "prenomcaution = @prenom, nomcaution = @nom, nomcompletcaution = @nomcomplet, " +
                   "adressecaution = @adresse, cpcaution = @cp, villecaution = @ville, " +
                   "telephonecaution = @telephone, emailcaution = @email, cautionarchivee = @archive " +
                   "WHERE idcaution = @id";
        }

        /// <summary>
        /// Retourne la requête d'ajout sous forme de chaîne paramétrée
        /// </summary>
        private string ObtenirReqAjout()
        {
            return "INSERT INTO caution (idcaution, prenomcaution, nomcaution, nomcompletcaution, " +
                   "adressecaution, cpcaution, villecaution, telephonecaution, emailcaution, cautionarchivee) " +
                   "VALUES (@id, @prenom, @nom, @nomcomplet, @adresse, @cp, @ville, @telephone, @email, @archive)";
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
            command.Parameters.AddWithValue("@telephone", EspacerNumTel());
            command.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            command.Parameters.AddWithValue("@archive", cbxArchive.Checked);
        }

        /// <summary>
        /// Génère les espaces tous les deux chiffres pour les numéros de téléphone
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
        /// Récupère et met en forme les noms et prénoms du formulaire de manière tolérante à la saisie
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