using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace GestionLocation
{
    public partial class AjoutModifLocations : Form
    {
        private readonly Locations fenLocation;
        private readonly string typeReq;
        private readonly int id;
        private string req;
        private MySqlCommand command;
        private readonly string[] rubLocations = { "idlocation", "idbien", "idcaution", "idlocataire", "debutlocation", "finlocation", "depotgarantie", "locationarchivee", "numcontratvisale" };
        private readonly Dictionary<string, string> datas;

        /// <summary>
        /// Constructeur de AjoutModifLocations
        /// </summary>
        /// <param name="fenLocations">Fenêtre de Locations ayant créé l'instance de AjoutModifLocations</param>
        /// <param name="typeReq">Type de requête</param>
        /// <param name="id">id de la location</param>
        public AjoutModifLocations(Locations fenLocation, string typeReq, int id = 0)
        {
            InitializeComponent();
            this.datas = new Dictionary<string, string>();
            this.fenLocation = fenLocation;
            this.typeReq = typeReq;
            this.id = id;
            // Remplit les listes des biens, des locataires et des cautions
            AfficheLesListes();
            // Si c'est une modification que l'on souhaite faire
            if (this.id != 0)
            {
                SelectionnerElements();
            }
            // Sinon, si c'est un ajout
            else
            {
                this.req = "SELECT MAX(idlocation) + 1 FROM location";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Read();
                this.id = reader.GetInt32(0);
                reader.Close();
            }
            lblID.Text = $"ID : {this.id}";
        }


        /// <summary>
        /// Remplit les listes des biens, locataires et cautions
        /// </summary>
        private void AfficheLesListes()
        {
            RemplirLstBiens();
            RemplirLstLocataires();
            RemplirLstCautions();
        }


        /// <summary>
        /// Gère le remplissage de la liste des biens
        /// </summary>
        private void RemplirLstBiens()
        {
            lstBiens.Items.Clear();
            this.req = "SELECT nombien FROM bien WHERE bienarchive = 0 ORDER BY nombien";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstBiens.Items.Add($"{reader["nombien"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }


        /// <summary>
        /// Gère le remplissage de la liste des locataires
        /// </summary>
        private void RemplirLstLocataires()
        {
            lstLocataires.Items.Clear();
            this.req = "SELECT nomcompletlocataire FROM locataire WHERE locatairearchive = 0 ORDER BY nomcompletlocataire";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            /* lecture de la première ligne du curseur (finCurseur passe à false en fin de
            curseur) */
            bool finCurseur = !reader.Read();
            // boucle tant que la ligne lue contient quelque chose
            // (donc tant que la fin du curseur n'est pas atteinte)
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstLocataires.Items.Add($"{reader["nomcompletlocataire"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }


        /// <summary>
        /// Gère le remplissage de la liste des locataires
        /// </summary>
        private void RemplirLstCautions()
        {
            lstCautions.Items.Clear();
            this.req = "SELECT nomcompletcaution FROM caution WHERE cautionarchivee = 0 ORDER BY nomcompletcaution";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                // affichage des champs récupérés dans la ligne
                lstCautions.Items.Add($"{reader["nomcompletcaution"]}");
                // lecture de la ligne suivante dans le curseur
                finCurseur = !reader.Read();
            }
            // fermeture du curseur
            reader.Close();
        }


        /// <summary>
        /// Sélectionne le bien, le locataire et la caution de la location sélectionnée
        /// </summary>
        private void SelectionnerElements()
        {
            lstBiens.SelectedIndex = lstBiens.Items.IndexOf(RetrouveBien());
            lstLocataires.SelectedIndex = lstLocataires.Items.IndexOf(RetrouveLocataire());
            lstCautions.SelectedIndex = lstCautions.Items.IndexOf(RetrouveCaution());
            this.req = $"SELECT * FROM location WHERE idlocation = {this.id}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            datDebut.Value = reader.GetDateTime(4);
            datFin.Value = reader.GetDateTime(5);
            txtDepotGarantie.Text = reader.GetString(6);
            txtContratVisale.Text = reader["numcontratvisale"].ToString();
            if ((bool)reader["locationarchivee"])
            {
                cbxArchive.Checked = true;
            }
            else
            {
                cbxArchive.Checked = false;
            }
            reader.Close();
        }


        /// <summary>
        /// Retrouve le bien à partir de l'id d'une location
        /// </summary>
        private string RetrouveBien()
        {
            this.req = $"SELECT nombien FROM bien WHERE idbien = (SELECT idbien FROM location WHERE idlocation = {this.id})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string nombien = ($"{reader["nombien"]}");
            reader.Close();
            return nombien;
        }


        /// <summary>
        /// Récupère les données sur un bien à partir de son id
        /// </summary>
        /// <param name="id">Identifiant du bien</param>
        /// <returns>Données sur le bien</returns>
        public void RecupBien(string id)
        {
            this.req = $"SELECT * FROM bien WHERE idbien = {id}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            this.datas.Add("NomBien", $"{reader["nombien"]}");
            this.datas.Add("LoyerHC", $"{reader["loyerHC"]}");
            this.datas.Add("Charges", $"{reader["charges"]}");
            this.datas.Add("LoyerCC", $"{reader["loyerCC"]}");
            this.datas.Add("AdresseBien", $"{reader["adressebien"]}");
            this.datas.Add("CPBien", $"{reader["cpbien"]}");
            this.datas.Add("VilleBien", $"{ reader["villebien"]}");
            this.datas.Add("TypeHabitat", $"{ reader["typehabitat"]}");
            this.datas.Add("RegJuriImmeuble", $"{ reader["regimejuridique"]}");
            this.datas.Add("PeriodeConstruc", $"{ reader["periodeconstruction"]}");
            this.datas.Add("superficie", $"{ reader["superficie"]}");
            this.datas.Add("NbPiece", $"{ reader["nbpiece"]}");
            this.datas.Add("DescriLogement", $"{ reader["description"]}");
            this.datas.Add("ElementEquip", $"{ reader["elementequip"]}");
            this.datas.Add("AutrePartieLog", $"{ reader["autre"]}");
            this.datas.Add("ModProdChauff", $"{ reader["prodchauff"]}");
            this.datas.Add("ModProdEauChaude", $"{ reader["prodeauchaude"]}");
            reader.Close();
        }


        /// <summary>
        /// Retrouve le locataire à partir de l'id d'une location
        /// </summary>
        private string RetrouveLocataire()
        {
            this.req = $"SELECT nomcompletlocataire FROM locataire WHERE idlocataire = (SELECT idlocataire FROM location WHERE idlocation = {this.id})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string nomLocataire = ($"{reader["nomcompletlocataire"]}");
            reader.Close();
            return nomLocataire;
        }


        /// <summary>
        /// Retrouve la caution à partir de l'id d'une location
        /// </summary>
        private string RetrouveCaution()
        {
            this.req = $"SELECT nomcompletcaution FROM caution WHERE idcaution = (SELECT idcaution FROM location WHERE idlocation = {this.id})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string nomCaution = ($"{reader["nomcompletcaution"]}");
            reader.Close();
            return nomCaution;
        }


        /// <summary>
        /// Enregistre la location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValider_Click(object sender, EventArgs e)
        {
            if (ChampsRenseignes())
            {
                string[] lesId = RecupLesId();
                if (this.typeReq.Equals("UPDATE"))
                {
                    // Construit la requête de modification
                    ConstruitReqModif(lesId);
                }
                else
                {
                    // Construit la requête d'ajout
                    ConstruitReqAjout(lesId);
                }
                // Exécute la requête d'enregistrement de location
                this.command = new MySqlCommand(this.req, Global.Connexion);
                this.command.Prepare();
                this.command.ExecuteNonQuery();
                // Ajoute/modifie les enregistrements de la table Paiement pour cette location
                MajTablePaiement(this.id);
                // Met à jour les champs de la fenêtre de Locations
                this.fenLocation.AfficherBiens();
                this.fenLocation.AfficherLocations();
                this.fenLocation.GetFenAccueil().AfficherLocations();
                if (this.typeReq.Equals("INSERT INTO"))
                {
                    // Génère le bail de location
                    GenererBail(lesId);
                }
                // Ferme la fenêtre
                this.Dispose();
            }
        }


        /// <summary>
        /// Génère le bail de location
        /// </summary>
        public void GenererBail(string[] lesId)
        {
            // Crée le fichier
            string cheminModele = Environment.CurrentDirectory + "/Baux/Bail.doc";
            string cheminDestination = $"C:\\Users\\Don_F\\OneDrive\\Bureau\\Bail {lstLocataires.SelectedItem}.doc";
            File.Copy(cheminModele, cheminDestination, true);

            // Remplit le bail
            // Création d'une application Word
            Word.Application wordApp = new Word.Application
            {
                Visible = false
            };

            // Ouverture du document
            Word.Document bail = wordApp.Documents.Open(cheminDestination);

            // Remplacement du texte
            RecupDatas(lesId);
            Word.Find find = wordApp.Selection.Find;
            foreach (KeyValuePair<string, string> data in datas)
            {
                find.Text = $"%{data.Key}%";
                find.Replacement.Text = data.Value;
                find.Execute(Replace: Word.WdReplace.wdReplaceAll);
            }
            find.Text = "%DateDuJour%";
            find.Replacement.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            find.Execute(Replace: Word.WdReplace.wdReplaceAll);
            find.Text = "%NbExemplaire%";
            if (datas["NomCaution"].Equals("VISALE"))
            {
                find.Replacement.Text = "2";
            }
            else
            {
                find.Replacement.Text = "3";
            }
            find.Execute(Replace: Word.WdReplace.wdReplaceAll);
            find.Text = "%MentionCaution%";
            if (!datas["NomCaution"].Equals("VISALE"))
            {
                find.Replacement.Text = "LE(LA) CAUTION";
            }
            else
            {
                find.Replacement.Text = "";
            }
            find.Execute(Replace: Word.WdReplace.wdReplaceAll);
            find.Text = "%MentionLuApprouve%";
            if (!datas["NomCaution"].Equals("VISALE"))
            {
                find.Replacement.Text = "Lu et approuvé";
            }
            else
            {
                find.Replacement.Text = "";
            }
            find.Execute(Replace: Word.WdReplace.wdReplaceAll);

            // Sauvegarde des modifications
            bail.Save();

            // Fermeture du document et de l'application Word
            bail.Close();
            MessageBox.Show("Le contrat de location a été généré.\nPensez à vérifier les champs et à remplir l'IRL.");
            wordApp.Quit();
        }


        /// <summary>
        /// Récupère toutes les infos pour construire le bail
        /// </summary>
        /// <returns>Dictionnaire contenant toutes les données</returns>
        public Dictionary<string, string> RecupDatas(string[] lesId)
        {
            // Données du locataire
            RecupLocataire(lesId[1]);
            // Données du bien
            RecupBien(lesId[0]);
            // Données de la caution
            RecupCaution(lesId[2]);
            // Données sur la location
            RecupLocation();
            RecupAssurance();
            return this.datas;
        }


        /// <summary>
        /// Récupère les dates de début et de fin d'assurance du logement
        /// </summary>
        public void RecupAssurance()
        {
            DateAssurance fenDateAssur = new DateAssurance();
            fenDateAssur.ShowDialog();
            this.datas.Add("DateSousAssur", fenDateAssur.lesDates[0]);
            this.datas.Add("DateFinAssur", fenDateAssur.lesDates[1]);
        }


        /// <summary>
        /// Récupère les infos sur le locataire
        /// </summary>
        public void RecupLocataire(string id)
        {
            this.req = $"SELECT * FROM locataire WHERE idlocataire = {id}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            this.datas.Add("PrenomLocat", $"{reader["prenomlocataire"]}");
            this.datas.Add("NomLocat", $"{reader["nomlocataire"]}");
            this.datas.Add("NomPrenomLocat", $"{reader["prenomlocataire"]} {reader["nomlocataire"]}");
            this.datas.Add("AdresseLocat", $"{reader["adresselocataire"]} {reader["cplocataire"]} {reader["villelocataire"]}");
            this.datas.Add("DaNaisLocat", $"{reader["datenaissancelocataire"]}");
            this.datas.Add("LieuNaisLocat", $"{reader["lieunaissancelocataire"].ToString().ToUpper()}");
            this.datas.Add("TelLocat", $"{reader["telephonelocataire"]}");
            this.datas.Add("EmailLocat", $"{reader["emailocataire"]}");
            reader.Close();
        }


        /// <summary>
        /// Récupère les infos sur la caution
        /// </summary>
        public void RecupCaution(string id)
        {
            this.req = $"SELECT * FROM caution WHERE idcaution = {id}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            this.datas.Add("PrenomCaution", $"{reader["prenomcaution"]}");
            this.datas.Add("NomCaution", $"{reader["nomcaution"]}");
            this.datas.Add("NomPrenomCaution", $"{reader["prenomcaution"]} {reader["nomcaution"]}");
            this.datas.Add("AdresseCaution", $"{reader["adressecaution"]} {reader["cpcaution"]} {reader["villecaution"].ToString().ToUpper()}");
            this.datas.Add("TelCaution", $"{reader["telephonecaution"]}");
            this.datas.Add("EmailCaution", $"{reader["emailcaution"]}");
            reader.Close();
            if (this.datas["NomCaution"].Equals("VISALE"))
            {
                this.datas.Add("InfoCaution1", $"N° de contrat :");
                this.datas.Add("InfoCaution2", txtContratVisale.Text);
            }
            else
            {
                this.datas.Add("InfoCaution1", $"Adresse de la caution :");
                this.datas.Add("InfoCaution2", this.datas["AdresseCaution"]);
            }
        }


        /// <summary>
        /// Récupère les informations sur la location
        /// </summary>
        public void RecupLocation()
        {
            if (lstCautions.SelectedItem.Equals("VISALE (Action Logement"))
            {
                this.datas.Add("DepotGarantie", "0");
            }
            else
            {
                this.datas.Add("DepotGarantie", this.datas["LoyerHC"]);
            }
            this.datas.Add("DebLoc", $"{datDebut.Value:dd/MM/yyyy}");
            this.datas.Add("FinLoc", $"{datFin.Value:dd/MM/yyyy}");
            double diffDate = (datFin.Value - datDebut.Value).TotalDays + 1;
            if (diffDate < 365)
            {
                this.datas.Add("DuréeContrat", $"{Math.Round((diffDate / 31.417), 1)} mois");
            }
            else
            {
                this.datas.Add("DuréeContrat", "1 année reconductible par tacite reconduction par période de : 1 an"); ;
            }
        }


        /// <summary>
        /// Vérifie si tous les champs ont été renseignés
        /// </summary>
        /// <returns>Vrai si tous les champs sont remplis, Faux dans le cas contraire</returns>
        private bool ChampsRenseignes()
        {
            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un bien.");
                return false;
            }
            else if (lstLocataires.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un locataire.");
                return false;
            }
            else if (lstCautions.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une caution.");
                return false;
            }
            else if (datDebut.Value.Equals(""))
            {
                MessageBox.Show("Veuillez sélectionner une date de début.");
                return false;
            }
            else if (datFin.Value.Equals(""))
            {
                MessageBox.Show("Veuillez sélectionner une date de fin.");
                return false;
            }
            else if (datFin.Value < datDebut.Value)
            {
                MessageBox.Show("Veuillez sélectionner des dates cohérentes.");
                return false;
            }
            else if (lstCautions.SelectedItem.Equals("VISALE (Action Logement)") && txtContratVisale.Text.Equals(""))
            {
                MessageBox.Show("Veuillez renseigner le numéro de contrat Visale.");
                return false;
            }
            else
            {
                if (txtDepotGarantie.Text.Equals(""))
                {
                    txtDepotGarantie.Text = "0";
                }
                return true;
            }
        }


        /// <summary>
        /// Récupère l'id du bien sélectionné ainsi que celui du locataire et de la caution
        /// </summary>
        /// <returns></returns>
        private string[] RecupLesId()
        {
            string[] lesId = { "", "", "" };
            // Récupère l'ID du bien
            this.req = $"SELECT idbien FROM bien WHERE nombien = \"{lstBiens.SelectedItem}\"";
            lesId[0] = ReqSelectUnElement();
            // Récupère l'ID du locataire
            this.req = $"SELECT idlocataire FROM locataire WHERE nomcompletlocataire = \"{lstLocataires.SelectedItem}\"";
            lesId[1] = ReqSelectUnElement();
            // Récupère l'ID de la caution
            this.req = $"SELECT idcaution FROM caution WHERE nomcompletcaution = \"{lstCautions.SelectedItem}\"";
            lesId[2] = ReqSelectUnElement();
            return lesId;
        }


        /// <summary>
        /// Construit la requête de modification
        /// </summary>
        private void ConstruitReqModif(string[] lesId)
        {   
            this.req = $"{this.typeReq} location SET ";
            this.req += $"idlocation = {this.id}, idbien = \"{lesId[0]}\", idcaution = \"{lesId[2]}\", idlocataire = \"{lesId[1]}\", " +
                $"debutlocation = \"{datDebut.Value:yyyy-MM-dd}\", finlocation = \"{datFin.Value:yyyy-MM-dd}\", " +
                $"depotgarantie = \"{txtDepotGarantie.Text}\", locationarchivee = {cbxArchive.Checked}, numcontratvisale = \'{txtContratVisale.Text}\' WHERE idlocation = {this.id}";
        }


        /// <summary>
        /// Construit la requête d'ajout
        /// </summary>
        private void ConstruitReqAjout(string[] lesId)
        {
            this.req = $"{this.typeReq} location (";
            for (int i = 0; i < this.rubLocations.Length - 1; i++)
            {
                this.req += $"{rubLocations[i]}, ";
            }
            this.req += $"{rubLocations[rubLocations.Length - 1]}) VALUES ({this.id}, {lesId[0]}, {lesId[2]}, " +
                $"{lesId[1]}, \"{datDebut.Value:yyyy-MM-dd}\", \"{datFin.Value:yyyy-MM-dd}\", {txtDepotGarantie.Text}, " +
                $"{cbxArchive.Checked}, \'{txtContratVisale.Text}\')";
        }


        /// <summary>
        /// Retourne le résultat unique d'une requête select
        /// </summary>
        /// <returns>Valeur retournée par la requête</returns>
        private string ReqSelectUnElement()
        {
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string resultat = reader.GetString(0);
            reader.Close();
            return resultat;
        }


        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Gère les procédures de mise à jour et d'ajout des enregistrements de la table Paiement
        /// </summary>
        /// <param name="id">id de la location sur laquelle porte les Paiements</param>
        public void MajTablePaiement(int id)
        {
            // Déclaration/affectation des variables
            List<string[]> lesMensualites = new List<string[]>();
            int jour;
            string debutLoc = $"{datDebut.Value:yyyy-MM-dd}";
            string finLoc = $"{datFin.Value:yyyy-MM-dd}";
            string periodeFacturee, moisDebutLoc, anneeDebutLoc, moisFinLoc, anneeFinLoc;
            DateTime dateCpt = datDebut.Value;
            string date = "01" + dateCpt.ToString().Substring(2);
            dateCpt = DateTime.Parse(date);
            anneeFinLoc = finLoc.Substring(0, 4);
            moisFinLoc = finLoc.Substring(5, 2);
            anneeDebutLoc = debutLoc.Substring(0, 4);
            moisDebutLoc = debutLoc.Substring(5, 2);
            // Parcourt tous les mois de la date de début à la date de fin
            while (dateCpt <= datFin.Value)
            {
                // Détermine le jour de la mensualité (jour de début, jour de fin de contrat ou 1er jour du mois)
                if (dateCpt.ToString().Substring(3, 2).Equals(moisDebutLoc) && dateCpt.ToString().Substring(6, 4).Equals(anneeDebutLoc))
                {
                    jour = int.Parse(debutLoc.Substring(8, 2));
                }
                else if (dateCpt.ToString().Substring(3, 2).Equals(moisFinLoc) && dateCpt.ToString().Substring(6, 4).Equals(anneeFinLoc))
                {
                    jour = int.Parse(finLoc.Substring(8, 2));
                }
                else
                {
                    jour = 1;
                }

                // Enregistre la période facturée de la mensualité dans la liste lesMensualités
                periodeFacturee = dateCpt.ToString().Substring(6, 4) + "-" + dateCpt.ToString().Substring(3, 2) + "-" + jour.ToString("D2");
                string[] mensualite = { periodeFacturee, id.ToString() };
                lesMensualites.Add(mensualite);
                dateCpt = dateCpt.AddMonths(1);
            }

            // Recherche si des enregistrements de Paiement existent déjà pour cette location
            int i = 0;
            this.req = $"SELECT * FROM paiement WHERE idlocation = {id}";
            List<string[]> resBdd = new List<string[]>();
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                DateTime laDate = reader.GetDateTime(4);
                string[] tab = { laDate.ToString("yyyy-MM-dd"), reader.GetString(0) };
                resBdd.Add(tab);
                finCurseur = !reader.Read();
            }
            reader.Close();

            // Si la requête n'a pas trouvé d'enregistrements
            if (resBdd.Count() == 0)
            {
                // Crée un nouvel id de paiement
                this.req = "SELECT MAX(idpaiement) FROM (SELECT idpaiement FROM paiement) AS req";
                this.command = new MySqlCommand(this.req, Global.Connexion);
                reader = this.command.ExecuteReader();
                reader.Read();
                int idPaiement = int.Parse(reader.GetString(0)) + 1;
                reader.Close();
                while (i <= lesMensualites.Count() - 1)
                {
                    // Ajouter le nouvel enregistrement dans la table Paiement
                    AjoutePaiement(lesMensualites[i], idPaiement);
                    i++;
                    idPaiement++;
                }
            }
            // Si la requête a trouvé des enregistrements correspondant à la location dans la table Paiement
            else
            {
                while (i < lesMensualites.Count())
                {
                    bool trouve = false;
                    // Vérifie si un enregistrement existe pour cette location, ce mois et cette année
                    foreach (string[] enr in resBdd)
                    {
                        // Si un enregistrement existe avec la même année et le même mois
                        if (enr[0].Substring(0, 7).Equals(lesMensualites[i][0].Substring(0, 7)))
                        {
                            // Si l'enregistrement n'a pas le même jour
                            if (!enr[0].Substring(8, 2).Equals(lesMensualites[i][0].Substring(8, 2)))
                            {
                                float montantDu = CalculeMontantDu(lstBiens.SelectedItem.ToString(), lesMensualites[i][0]);
                                ModifiePaiement(enr[1], montantDu, lesMensualites[i][0]);
                            }
                            trouve = true;
                            break;
                        }
                    }
                    // Si aucun enregistrement n'a été trouvé pour cette location et pour ce mois + année
                    if (trouve == false)
                    {
                        // Récupère l'id du paiement
                        int idPaiement;
                        this.req = "SELECT MAX(idpaiement) FROM (SELECT idpaiement FROM paiement) AS req";
                        this.command = new MySqlCommand(this.req, Global.Connexion);
                        reader = this.command.ExecuteReader();
                        reader.Read();
                        idPaiement = int.Parse(reader.GetString(0)) + 1;
                        reader.Close();
                        AjoutePaiement(lesMensualites[i], idPaiement);
                    }
                    i++;
                }
                // Vérifie si des enregistrements doivent être supprimés
                // Crée le tableau des dates de paiement de la location
                string[] lesMensu = new string[lesMensualites.Count()];
                for (int j = 0; j < lesMensu.Length; j++)
                {
                    lesMensu[j] = lesMensualites[j][0].Substring(0, 7);
                }
                // Parcourt les enregistrements qui étaient déjà présents dans la BDD
                string dateAChercher;
                for (int k = 0; k < resBdd.Count(); k++)
                {
                    // Extrait annee + mois de l'enregistrement
                    dateAChercher = resBdd[k][0].Substring(0, 7);
                    // Si la date issue de la BDD n'est pas dans le tableau des dates de paiement de la location
                    if (!lesMensu.Contains(dateAChercher))
                    {
                        this.req = $"DELETE FROM paiement WHERE idpaiement = {resBdd[k][1]}";
                        ExecuteReqCUD();
                    }
                }
            }
        }


        /// <summary>
        /// Ajoute un enregistrement dans la table Paiement
        /// </summary>
        /// <param name="laMensualite">Mensualité à ajouter à la table</param>
        /// <param name="idPaiement">id du paiement à ajouter</param>
        public void AjoutePaiement(string[] laMensualite, int idPaiement)
        {
            float montantDu = CalculeMontantDu(lstBiens.SelectedItem.ToString(), laMensualite[0]);
            string montantD = montantDu.ToString().Replace(',', '.');
            this.req = $"INSERT INTO paiement (idpaiement, idlocation, datepaiement, montantpaye, periodefacturee, montantdu, resteapayer, loyerregle)" +
                $" VALUES ({idPaiement}, {laMensualite[1]}, \'0000-00-00\', 0, \'{laMensualite[0]}\', \'{montantD}\', \'{montantD}\', false)";
            ExecuteReqCUD();
        }


        /// <summary>
        /// Calcule le montant dû pour la mensualité concernée
        /// </summary>
        /// <returns>Montant dû pour la période</returns>
        public float CalculeMontantDu(string leBien, string laMensualite)
        {
            // Récupère le loyer charges comprises à partir du nom du bien
            this.req = $"SELECT loyercc FROM bien WHERE nombien = \'{leBien}\'";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            float loyercc = (float) reader["loyercc"];
            reader.Close();

            // Détermine si c'est un mois "entrant", "sortant", "entier" ou "partiel" (mois = premier et dernier de la location)
            string type = "entier";
            // Si le mois est à la fois premier et dernier de la location
            if ($"{datDebut.Value:yyyy-MM-dd}".Substring(0, 7).Equals($"{datFin.Value:yyyy-MM-dd}".Substring(0, 7)))
            {
                type = "partiel";
            }
            // Si le mois est le premier de la location
            else if (laMensualite.Substring(0, 7).Equals($"{datDebut.Value:yyyy-MM-dd}".Substring(0, 7)))
            {
                type = "entrant";
            }
            // Si le mois est le dernier de la location
            else if (laMensualite.Substring(0, 7).Equals($"{datFin.Value:yyyy-MM-dd}".Substring(0, 7)))
            {
                type = "sortant";
            }

            // Récupère le nombre de jour dans le mois
            int nbDeJoursMax = DateTime.DaysInMonth(int.Parse(laMensualite.Substring(0, 4)), int.Parse(laMensualite.Substring(5, 2)));

            // Calcule le prorata du montant dû
            float montantDu;
            int jourEntree = int.Parse(laMensualite.Substring(8, 2));
            switch (type)
            {
                case "entrant":
                    montantDu = ((float)loyercc / nbDeJoursMax) * (nbDeJoursMax - jourEntree + 1);
                    break;
                case "sortant":
                    montantDu = ((float)loyercc / nbDeJoursMax) * jourEntree;
                    break;
                case "partiel":
                    int jourFin = int.Parse($"{datFin.Value:yyyy-MM-dd}".Substring(8, 2));
                    int nbDeJours = jourFin - jourEntree + 1;
                    montantDu = ((float) loyercc / nbDeJoursMax) * nbDeJours;
                    break;
                default:
                    montantDu = loyercc;
                    break;
            }
            return (float) Math.Round(montantDu, 2);
        }


        /// <summary>
        /// Gère la requête de modification de l'enregistrement de Paiement
        /// </summary>
        /// <param name="idPaiement">id du paiement à modifier</param>
        /// <param name="montantDu">nouveau montant dû à mettre à jour dans la table</param>
        public void ModifiePaiement(string idPaiement, float montantDu, string periodeFacturee)
        {
            string resteAPayer = CalculerResteAPayer(idPaiement, montantDu);
            bool regle;
            if (float.Parse(resteAPayer) <= 0)
            {
                regle = true;
            }
            else
            {
                regle = false;
            }
            resteAPayer = resteAPayer.Replace(',', '.');
            string montantD = montantDu.ToString().Replace(',', '.');
            this.req = $"UPDATE paiement SET periodefacturee = \'{periodeFacturee}\', montantdu = \'{montantD}\', resteapayer = \'{resteAPayer}\', " +
                $"loyerregle = {regle} WHERE idpaiement = {idPaiement}";
            ExecuteReqCUD();
        }


        /// <summary>
        /// Gère le calcul du reste à payer
        /// </summary>
        /// <param name="idPaiement">id du paiement concerné</param>
        /// <param name="montantDu">montant dû pour ce paiement</param>
        /// <returns>Reste à payer</returns>
        public string CalculerResteAPayer(string idPaiement, float montantDu)
        {
            float resteAPayer;
            this.req = $"SELECT montantpaye FROM paiement WHERE idpaiement = {idPaiement}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            float montantpaye = float.Parse(reader["montantpaye"].ToString());
            reader.Close();
            resteAPayer = montantDu - montantpaye;
            return resteAPayer.ToString();
        }


        /// <summary>
        /// Exécute une requête de création, modification ou suppression
        /// </summary>
        public void ExecuteReqCUD()
        {
            // Exécute la requête
            this.command = new MySqlCommand(this.req, Global.Connexion);
            // préparation de la requête
            this.command.Prepare();
            // exécution de la requête
            this.command.ExecuteNonQuery();
        }


        /// <summary>
        /// Gère la sélection d'une caution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstCautions_SelectedIndexChanged(object sender, EventArgs e)
        {
             lblContratVisale.Visible = lstCautions.SelectedItem.Equals("VISALE (Action Logement)");
             txtContratVisale.Visible = lstCautions.SelectedItem.Equals("VISALE (Action Logement)");
            if (!lstCautions.SelectedItem.Equals("VISALE (Action Logement)"))
            {
                txtContratVisale.Text = "";
            }
        }
    }
}
