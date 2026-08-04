using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace GestionLocation
{
    public partial class AjoutModifLocations : Form
    {
        private readonly Locations fenLocation;
        private readonly string typeReq;
        private int id;
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

            // Définition du titre de la fenêtre (ex: "Ajout d'une location" ou "Modification d'une location")
            this.Text = $"{this.typeReq} d'une location";

            // 1. Remplissage des listes déroulantes (Biens, Locataires, Cautions)
            AfficheLesListes();

            // 2. Traitement selon le mode
            if (this.id > 0)
            {
                // Mode Modification : pré-sélectionne les données existantes
                SelectionnerElements();
                lblID.Text = $"ID : {this.id}";
            }
            else
            {
                // Mode Ajout : l'ID sera attribué par MySQL lors de la validation
                lblID.Text = "ID : (Nouveau)";
            }
        }

        public class ListItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; }

            // Indispensable : WinForms utilise ToString() pour afficher l'élément dans la ListBox
            public override string ToString()
            {
                return DisplayText;
            }
        }


        /// <summary>
        /// Remplit les 3 listes en réutilisant la méthode générique (avec gestion des ID)
        /// </summary>
        private void AfficheLesListes()
        {
            RemplirListe(lstBiens,
                         "SELECT idbien, nombien FROM bien WHERE bienarchive = 0 ORDER BY nombien",
                         "idbien",
                         "nombien");

            RemplirListe(lstLocataires,
                         "SELECT idlocataire, nomcompletlocataire FROM locataire WHERE locatairearchive = 0 ORDER BY nomcompletlocataire",
                         "idlocataire",
                         "nomcompletlocataire");

            RemplirListe(lstCautions,
                         "SELECT idcaution, nomcompletcaution FROM caution WHERE cautionarchivee = 0 ORDER BY nomcompletcaution",
                         "idcaution",
                         "nomcompletcaution");
        }

        /// <summary>
        /// Méthode générique pour remplir n'importe quel ListBox avec un objet ListItem (Id + Texte)
        /// </summary>
        private void RemplirListe(ListBox listBox, string reqSql, string colId, string colTexte)
        {
            listBox.Items.Clear();

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(reqSql, Global.Connexion))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader[colTexte] != DBNull.Value && reader[colId] != DBNull.Value)
                            {
                                int id = Convert.ToInt32(reader[colId]);
                                string texte = reader[colTexte].ToString();

                                // On ajoute l'objet complet dans la ListBox
                                listBox.Items.Add(new ListItem { Id = id, DisplayText = texte });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement de la liste : {ex.Message}", "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Charge les données de la location sélectionnée et pré-sélectionne les éléments dans l'IHM
        /// </summary>
        private void SelectionnerElements()
        {
            // Requête SQL avec jointures pour récupérer directement les noms associés à la location
            string req = @"
                SELECT b.nombien, 
                       l.nomcompletlocataire, 
                       c.nomcompletcaution,
                       loc.debutlocation,
                       loc.finlocation,
                       loc.depotgarantie,
                       loc.numcontratvisale
                FROM location loc
                INNER JOIN bien b ON loc.idbien = b.idbien
                INNER JOIN locataire l ON loc.idlocataire = l.idlocataire
                LEFT JOIN caution c ON loc.idcaution = c.idcaution
                WHERE loc.idlocation = @id";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(req, Global.Connexion))
                {
                    // 1. Passage de l'ID sous forme de paramètre sécurisé
                    cmd.Parameters.AddWithValue("@id", this.id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 2. Extraction sécurisée avec gestion du NULL
                            string nomBien = reader["nombien"] != DBNull.Value ? reader["nombien"].ToString() : "";
                            string nomLocataire = reader["nomcompletlocataire"] != DBNull.Value ? reader["nomcompletlocataire"].ToString() : "";
                            string nomCaution = reader["nomcompletcaution"] != DBNull.Value ? reader["nomcompletcaution"].ToString() : "";

                            // 3. Pré-sélection dans les ListBox via la méthode helper
                            SelectionnerDansListBox(lstBiens, nomBien);
                            SelectionnerDansListBox(lstLocataires, nomLocataire);
                            SelectionnerDansListBox(lstCautions, nomCaution);

                            // 4. Remplissage du dictionnaire datas et/ou des champs du formulaire
                            if (reader["debutlocation"] != DBNull.Value)
                            {
                                string dateDeb = Convert.ToDateTime(reader["debutlocation"]).ToString("dd/MM/yyyy");
                                this.datas["DebLoc"] = dateDeb;
                                datDebut.Value = Convert.ToDateTime(reader["debutlocation"]);
                            }

                            if (reader["finlocation"] != DBNull.Value)
                            {
                                string dateFin = Convert.ToDateTime(reader["finlocation"]).ToString("dd/MM/yyyy");
                                this.datas["FinLoc"] = dateFin;
                                datFin.Value = Convert.ToDateTime(reader["finlocation"]);
                            }

                            if (reader["depotgarantie"] != DBNull.Value)
                            {
                                this.datas["DepotGarantie"] = reader["depotgarantie"].ToString();
                                txtDepotGarantie.Text = reader["depotgarantie"].ToString();
                            }

                            if (reader["numcontratvisale"] != DBNull.Value)
                            {
                                txtContratVisale.Text = reader["numcontratvisale"].ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show(
                                "Impossible de trouver les détails de cette location dans la base de données.",
                                "Location introuvable",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur lors du chargement de la location : {ex.Message}",
                    "Erreur BDD",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Recherche et sélectionne un élément dans une ListBox sans risquer d'exception si l'élément n'existe pas.
        /// </summary>
        private void SelectionnerDansListBox(ListBox listBox, string valeur)
        {
            if (string.IsNullOrWhiteSpace(valeur))
            {
                listBox.SelectedIndex = -1;
                return;
            }

            // FindStringExact cherche la correspondance exacte dans la liste
            int index = listBox.FindStringExact(valeur);

            if (index != ListBox.NoMatches)
            {
                listBox.SelectedIndex = index;
            }
            else
            {
                // L'élément n'est plus dans la liste (ex: bien ou locataire archivé)
                listBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Récupère toutes les infos pour construire le bail
        /// </summary>
        public async Task RecupDatasAsync(string[] lesId)
        {
            if (lesId == null || lesId.Length < 3)
            {
                MessageBox.Show("Les identifiants fournis pour la génération du bail sont invalides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1. Données du locataire (lesId[1])
            RecupLocataire(lesId[1]);

            // 2. Données du bien (lesId[0])
            RecupBien(lesId[0]);

            // 3. Données de la caution (lesId[2])
            RecupCaution(lesId[2]);

            // 4. Données sur la location
            RecupLocation();

            // 5. Récupère le dernier indice IRL
            await RecupIRLAsync();

            // 6. Récupère la date de souscription d'assurance si c'est une chambre
            if (this.datas.TryGetValue("NomBien", out string nomBien) && nomBien.StartsWith("Chambre", StringComparison.OrdinalIgnoreCase))
            {
                RecupAssurance();
            }
        }

        /// <summary>
        /// Récupère les infos sur le locataire
        /// </summary>
        public void RecupLocataire(string id)
        {
            string req = "SELECT * FROM locataire WHERE idlocataire = @id";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(req, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime dateNaissLoc = reader["datenaissancelocataire"] != DBNull.Value
                                ? Convert.ToDateTime(reader["datenaissancelocataire"])
                                : DateTime.MinValue;

                            this.datas["DaNaisLocat"] = dateNaissLoc != DateTime.MinValue ? dateNaissLoc.ToString("dd/MM/yyyy") : "";
                            this.datas["PrenomLocat"] = reader["prenomlocataire"]?.ToString() ?? "";
                            this.datas["NomLocat"] = reader["nomlocataire"]?.ToString() ?? "";
                            this.datas["NomPrenomLocat"] = $"{this.datas["PrenomLocat"]} {this.datas["NomLocat"]}";
                            this.datas["AdresseLocat"] = $"{reader["adresselocataire"]} {reader["cplocataire"]} {reader["villelocataire"]}";
                            this.datas["LieuNaisLocat"] = reader["lieunaissancelocataire"]?.ToString().ToUpper() ?? "";
                            this.datas["TelLocat"] = reader["telephonelocataire"]?.ToString() ?? "";
                            this.datas["EmailLocat"] = reader["emailocataire"]?.ToString() ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération du locataire : {ex.Message}", "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Récupère les données sur un bien à partir de son id
        /// </summary>
        public void RecupBien(string id)
        {
            string req = "SELECT * FROM bien WHERE idbien = @id";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(req, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.datas["NomBien"] = reader["nombien"]?.ToString() ?? "";
                            this.datas["LoyerHC"] = reader["loyerHC"]?.ToString() ?? "0";
                            this.datas["Charges"] = reader["charges"]?.ToString() ?? "0";
                            this.datas["LoyerCC"] = reader["loyerCC"]?.ToString() ?? "0";
                            this.datas["AdresseBien"] = reader["adressebien"]?.ToString() ?? "";
                            this.datas["CPBien"] = reader["cpbien"]?.ToString() ?? "";
                            this.datas["VilleBien"] = reader["villebien"]?.ToString() ?? "";
                            this.datas["NumFiscal"] = reader["numerofiscal"]?.ToString() ?? "";
                            this.datas["ClasseEnergie"] = reader["classeDPE"]?.ToString() ?? "";
                            this.datas["EstimationCoutElec"] = reader["estimationconsommation"]?.ToString() ?? "";
                            this.datas["AnneeReference"] = reader["anneereference"]?.ToString() ?? "";
                            this.datas["TypeHabitat"] = reader["typehabitat"]?.ToString() ?? "";
                            this.datas["RegJuriImmeuble"] = reader["regimejuridique"]?.ToString() ?? "";
                            this.datas["PeriodeConstruc"] = reader["periodeconstruction"]?.ToString() ?? "";
                            this.datas["superficie"] = reader["superficie"]?.ToString() ?? "";
                            this.datas["NbPiece"] = reader["nbpiece"]?.ToString() ?? "";
                            this.datas["DescriLogement"] = reader["description"]?.ToString() ?? "";
                            this.datas["ElementEquip"] = reader["elementequip"]?.ToString() ?? "";
                            this.datas["AutrePartieLog"] = reader["autre"]?.ToString() ?? "";
                            this.datas["ModProdChauff"] = reader["prodchauff"]?.ToString() ?? "";
                            this.datas["ModProdEauChaude"] = reader["prodeauchaude"]?.ToString() ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération du bien : {ex.Message}", "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Récupère les infos sur la caution
        /// </summary>
        public void RecupCaution(string id)
        {
            // Gestion du cas où il n'y a pas de caution (ID vide ou "0")
            if (string.IsNullOrWhiteSpace(id) || id == "0")
            {
                this.datas["NomCaution"] = "";
                this.datas["PrenomCaution"] = "";
                this.datas["NomPrenomCaution"] = "Aucune caution";
                this.datas["AdresseCaution"] = "";
                this.datas["TelCaution"] = "";
                this.datas["EmailCaution"] = "";
                this.datas["InfoCaution1"] = "";
                this.datas["InfoCaution2"] = "";
                this.datas["SignatureCaution"] = "";
                this.datas["DonneesCaution"] = "Sans garant";
                return;
            }

            string req = "SELECT * FROM caution WHERE idcaution = @id";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(req, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.datas["PrenomCaution"] = reader["prenomcaution"]?.ToString() ?? "";
                            this.datas["NomCaution"] = reader["nomcaution"]?.ToString() ?? "";
                            this.datas["NomPrenomCaution"] = $"{this.datas["PrenomCaution"]} {this.datas["NomCaution"]}";
                            this.datas["AdresseCaution"] = $"{reader["adressecaution"]} {reader["cpcaution"]} {reader["villecaution"]}".ToUpper();
                            this.datas["TelCaution"] = reader["telephonecaution"]?.ToString() ?? "";
                            this.datas["EmailCaution"] = reader["emailcaution"]?.ToString() ?? "";
                        }
                    }
                }

                string nomCaution = this.datas.TryGetValue("NomCaution", out string nc) ? nc : "";

                if (nomCaution.Equals("VISALE", StringComparison.OrdinalIgnoreCase))
                {
                    this.datas["InfoCaution1"] = "N° de contrat :";
                    this.datas["InfoCaution2"] = txtContratVisale.Text;
                    this.datas["SignatureCaution"] = "";
                    this.datas["DonneesCaution"] = $"Garantie VISALE. Contrat numéro: {txtContratVisale.Text}";
                }
                else
                {
                    this.datas["InfoCaution1"] = "Adresse de la caution :";
                    this.datas["InfoCaution2"] = this.datas["AdresseCaution"];
                    this.datas["SignatureCaution"] = "Signature de la caution";
                    this.datas["DonneesCaution"] = $"{this.datas["NomPrenomCaution"]}, résidant {this.datas["AdresseCaution"]}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération de la caution : {ex.Message}", "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Récupère les informations sur la location et calcule le dépôt de garantie
        /// </summary>
        public void RecupLocation()
        {
            this.datas["DebLoc"] = datDebut.Value.ToString("dd/MM/yyyy");
            this.datas["FinLoc"] = datFin.Value.ToString("dd/MM/yyyy");

            double diffDate = (datFin.Value.Date - datDebut.Value.Date).TotalDays + 1;

            if (diffDate < 365)
            {
                // Durée calculée en mois
                double nbMois = Math.Round(diffDate / 30.4375, 1); // 30.4375 = moyenne exacte de jours par mois (365 / 12)
                this.datas["DuréeContrat"] = $"{nbMois} mois";
                this.datas["NbMoisDepGarantie"] = "0";
            }
            else
            {
                this.datas["DuréeContrat"] = "1 année reconductible par tacite reconduction par période de : 1 an";

                string nomCaution = this.datas.TryGetValue("NomCaution", out string nc) ? nc : "";

                if (nomCaution.Equals("VISALE", StringComparison.OrdinalIgnoreCase))
                {
                    this.datas["NbMoisDepGarantie"] = "0";
                }
                else
                {
                    this.datas["NbMoisDepGarantie"] = "1";
                }
            }

            // Calcul du dépôt de garantie avec decimal (précision financière)
            if (this.datas.TryGetValue("LoyerHC", out string strLoyerHC) &&
                decimal.TryParse(strLoyerHC, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal loyerHC) &&
                this.datas.TryGetValue("NbMoisDepGarantie", out string strNbMois) &&
                decimal.TryParse(strNbMois, out decimal nbMoisDepot))
            {
                decimal depotGarantie = loyerHC * nbMoisDepot;
                this.datas["DepotGarantie"] = depotGarantie.ToString("F2");
            }
            else
            {
                this.datas["DepotGarantie"] = "0";
                MessageBox.Show("Le montant du dépôt de garantie n'a pas pu être calculé en raison d'un loyer ou d'un nombre de mois invalide.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private async void btnValider_Click(object sender, EventArgs e)
        {
            string[] lesIds = { "", "", "" };
            bool creation = true;
            // 1. Validation des saisies
            // Récupération de l'ID du bien
            if (!(lstBiens.SelectedItem is ListItem bienSelectionne))
            {
                MessageBox.Show("Veuillez sélectionner un bien dans la liste.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idBien = bienSelectionne.Id;

            // Récupération de l'ID du locataire
            if (!(lstLocataires.SelectedItem is ListItem locataireSelectionne))
            {
                MessageBox.Show("Veuillez sélectionner un locataire dans la liste.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idLocataire = locataireSelectionne.Id;

            // Récupération de l'ID de la caution
            if (!(lstCautions.SelectedItem is ListItem cautionSelectionnee))
            {
                MessageBox.Show("Veuillez sélectionner une caution / garant dans la liste.", "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idCaution = cautionSelectionnee.Id;

            if (lstBiens.SelectedIndex == -1 || lstLocataires.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner un bien et un locataire.", "Saisie incomplète", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (datFin.Value.Date <= datDebut.Value.Date)
            {
                MessageBox.Show("La date de fin du bail doit être postérieure à la date de début.", "Dates invalides", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime dateDebut = datDebut.Value.Date;
            DateTime dateFin = datFin.Value.Date;

            // Utilisation d'une transaction MySQL pour garantir la cohérence des données
            using (MySqlTransaction transaction = Global.Connexion.BeginTransaction())
            {
                try
                {
                    if (this.id == 0)
                    {
                        // ==========================================
                        // MODE AJOUT (INSERTION DE LOCATION)
                        // ==========================================
                        lesIds[0] = idBien.ToString();
                        lesIds[1] = idLocataire.ToString();
                        lesIds[2] = idCaution.ToString();
                        string reqInsert = @"INSERT INTO location 
                            (idbien, idlocataire, idcaution, debutlocation, finlocation, locationarchivee, numcontratvisale, depotgarantie) 
                            VALUES 
                            (@idBien, @idLocataire, @idCaution, @dateDebut, @dateFin, @locationArchivee, @numContratVisale, @depotGarantie)";

                        using (MySqlCommand cmd = new MySqlCommand(reqInsert, Global.Connexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idBien", idBien);
                            cmd.Parameters.AddWithValue("@idLocataire", idLocataire);
                            cmd.Parameters.AddWithValue("@idCaution", idCaution);
                            cmd.Parameters.AddWithValue("@dateDebut", dateDebut.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@dateFin", dateFin.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@locationArchivee", cbxArchive.Checked);
                            cmd.Parameters.AddWithValue("@numContratVisale", txtContratVisale.Text);
                            cmd.Parameters.AddWithValue("@depotGarantie", txtDepotGarantie.Text);

                            cmd.ExecuteNonQuery();

                            // Récupération de l'ID AUTO_INCREMENT généré par MySQL
                            this.id = Convert.ToInt32(cmd.LastInsertedId);
                        }
                    }
                    else
                    {
                        // ==========================================
                        // MODE MODIFICATION (MISE À JOUR DE LOCATION)
                        // ==========================================
                        creation = false;
                        string reqUpdate = @"UPDATE location 
                            SET idbien = @idBien, 
                                idlocataire = @idLocataire, 
                                idcaution = @idCaution,
                                debutlocation = @dateDebut, 
                                finlocation = @dateFin,
                                locationarchivee = @locationArchivee,
                                numcontratvisale = @numContratVisale,
                                depotgarantie = @depotGarantie
                            WHERE idlocation = @idLocation";

                        using (MySqlCommand cmd = new MySqlCommand(reqUpdate, Global.Connexion, transaction))
                        {
                            cmd.Parameters.AddWithValue("@idLocation", this.id);
                            cmd.Parameters.AddWithValue("@idBien", idBien);
                            cmd.Parameters.AddWithValue("@idLocataire", idLocataire);
                            cmd.Parameters.AddWithValue("@idCaution", idCaution);
                            cmd.Parameters.AddWithValue("@dateDebut", dateDebut.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@dateFin", dateFin.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@locationArchivee", cbxArchive.Checked);
                            cmd.Parameters.AddWithValue("@numContratVisale", txtContratVisale.Text);
                            cmd.Parameters.AddWithValue("@depotGarantie", txtDepotGarantie.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Validation de la transaction pour la table Location
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Annulation des modifications en cas d'erreur
                    transaction.Rollback();
                    MessageBox.Show($"Une erreur est survenue lors de l'enregistrement de la location : {ex.Message}", "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 2. Synchronisation de la table Paiement pour cette location
            try
            {
                MajTablePaiement(this.id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"La location a été enregistrée, mais la mise à jour des paiements a échoué : {ex.Message}", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 3. Génération des documents Word (Bail, acte de caution, etc.)
            try
            {
                if (creation)
                {
                    await GenererBailAsync(lesIds);
                    await GenererEtatDesLieuxAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"La location a été enregistrée, mais la génération des documents a échoué : {ex.Message}", "Avertissement Document", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 4. Notification, rafraîchissement de la grille parente et fermeture
            MessageBox.Show("Enregistrement effectué avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Rafraîchit la liste dans la fenêtre d'origine (si la méthode existe)
            if (this.fenLocation != null)
            {
                fenLocation.AfficherLocations();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        /// <summary>
        /// Prépare la commande SQL d'insertion d'une nouvelle location avec requêtes paramétrées.
        /// </summary>
        private void PreparerCommandeAjout(MySqlCommand cmd, string[] lesId)
        {
            cmd.CommandText = @"
            INSERT INTO location (idlocation, idbien, idlocataire, idcaution, debutlocation, finlocation, numcontratvisale)
            VALUES (@idlocation, @idbien, @idlocataire, @idcaution, @debloc, @finloc, @numcontratvisale)";

            // 1. Clé primaire
            cmd.Parameters.AddWithValue("@idlocation", this.id);

            // 2. Clés étrangères (Bien et Locataire)
            cmd.Parameters.AddWithValue("@idbien", Convert.ToInt32(lesId[0]));
            cmd.Parameters.AddWithValue("@idlocataire", Convert.ToInt32(lesId[1]));

            // 3. Gestion de la caution optionnelle (si id == "0" ou vide -> DBNull.Value)
            if (int.TryParse(lesId[2], out int idCaution) && idCaution > 0)
            {
                cmd.Parameters.AddWithValue("@idcaution", idCaution);
            }
            else
            {
                cmd.Parameters.AddWithValue("@idcaution", DBNull.Value);
            }

            // 4. Dates (passage d'objets DateTime directs, MySQL gère le format automatiquement)
            cmd.Parameters.AddWithValue("@debloc", datDebut.Value.Date);
            cmd.Parameters.AddWithValue("@finloc", datFin.Value.Date);

            // 5. Numéro de contrat Visale
            cmd.Parameters.AddWithValue("@numcontratvisale", txtContratVisale.Text);
        }


        /// <summary>
        /// Prépare la commande SQL de mise à jour d'une location existante avec requêtes paramétrées.
        /// </summary>
        private void PreparerCommandeModif(MySqlCommand cmd, string[] lesId)
        {
            cmd.CommandText = @"
            UPDATE location 
            SET idbien = @idbien, 
                idlocataire = @idlocataire, 
                idcaution = @idcaution, 
                debutlocation = @debloc, 
                finlocation = @finloc, 
                numcontratvisale = @numcontratvisale
            WHERE idlocation = @idlocation";

            // Clause WHERE
            cmd.Parameters.AddWithValue("@idlocation", this.id);

            // Clés étrangères
            cmd.Parameters.AddWithValue("@idbien", Convert.ToInt32(lesId[0]));
            cmd.Parameters.AddWithValue("@idlocataire", Convert.ToInt32(lesId[1]));

            // Caution optionnelle
            if (int.TryParse(lesId[2], out int idCaution) && idCaution > 0)
            {
                cmd.Parameters.AddWithValue("@idcaution", idCaution);
            }
            else
            {
                cmd.Parameters.AddWithValue("@idcaution", DBNull.Value);
            }

            // Dates et Montants
            cmd.Parameters.AddWithValue("@debloc", datDebut.Value.Date);
            cmd.Parameters.AddWithValue("@finloc", datFin.Value.Date);

            // Numéro de contrat Visale
            cmd.Parameters.AddWithValue("@numcontratvisale", txtContratVisale.Text);
        }

        /// <summary>
        /// Génère les états des lieux d'entrée et l'inventaire du mobilier dans une seule session Word
        /// </summary>
        public async Task GenererEtatDesLieuxAsync()
        {
            if (lstLocataires.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un locataire.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!datas.TryGetValue("NomBien", out string nomBien) || string.IsNullOrWhiteSpace(nomBien))
            {
                MessageBox.Show("Le nom du bien est introuvable.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string locataire = lstLocataires.SelectedItem.ToString();
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string bureauPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Modèles
            string etatModele = Path.Combine(baseDir, "Baux", $"État des lieux {nomBien.ToLower()}.docx");
            string invModele = Path.Combine(baseDir, "Baux", $"Inventaire mobilier {nomBien.ToLower()}.docx");

            // Destinations sur le Bureau
            string etatDest = Path.Combine(bureauPath, $"État des lieux {nomBien} - {locataire}.docx");
            string invDest = Path.Combine(bureauPath, $"Inventaire du mobilier {nomBien} - {locataire}.docx");

            // Vérification de l'existence des modèles
            if (!File.Exists(etatModele) || !File.Exists(invModele))
            {
                MessageBox.Show("L'un des fichiers modèles (État des lieux ou Inventaire) est introuvable.", "Erreur Fichier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Copie des fichiers modèles
            try
            {
                File.Copy(etatModele, etatDest, true);
                File.Copy(invModele, invDest, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la préparation des fichiers : {ex.Message}", "Erreur I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Traitement dans un seul processus Word en arrière-plan
            bool succes = await Task.Run(() =>
            {
                Word.Application wordApp = null;

                try
                {
                    // 1. Démarrage d'UNE SEULE instance de Word pour les deux documents
                    wordApp = new Word.Application { Visible = false };

                    // --- DOCUMENT 1 : ÉTAT DES LIEUX ---
                    Word.Document docEtat = wordApp.Documents.Open(etatDest);
                    try
                    {
                        Word.Find findEtat = wordApp.Selection.Find;
                        foreach (KeyValuePair<string, string> data in datas)
                        {
                            RemplacerTexteWord(findEtat, $"%{data.Key}%", data.Value ?? "");
                        }
                        docEtat.Save();
                    }
                    finally
                    {
                        docEtat.Close();
                        Marshal.ReleaseComObject(docEtat);
                    }

                    // --- DOCUMENT 2 : INVENTAIRE DU MOBILIER ---
                    Word.Document docInv = wordApp.Documents.Open(invDest);
                    try
                    {
                        Word.Find findInv = wordApp.Selection.Find;

                        // Remplacement de toutes les données du dictionnaire (si présentes dans l'inventaire)
                        foreach (KeyValuePair<string, string> data in datas)
                        {
                            RemplacerTexteWord(findInv, $"%{data.Key}%", data.Value ?? "");
                        }

                        // Remplacement spécifique si besoin
                        if (datas.TryGetValue("DebLoc", out string debutlocation))
                        {
                            RemplacerTexteWord(findInv, "%DebLoc%", debutlocation);
                        }

                        docInv.Save();
                    }
                    finally
                    {
                        docInv.Close();
                        Marshal.ReleaseComObject(docInv);
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    // Fermeture garantie de l'application Word
                    if (wordApp != null)
                    {
                        wordApp.Quit();
                        Marshal.ReleaseComObject(wordApp);
                    }
                }
            });

            if (succes)
            {
                MessageBox.Show(
                    "L'état des lieux et l'inventaire du mobilier ont été générés sur votre Bureau.",
                    "Génération réussie",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Une erreur est survenue lors de la génération des documents Word.",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Génère le bail de location au format Word (.docx)
        /// </summary>
        public async Task GenererBailAsync(string[] lesId)
        {
            // 1. Récupération des données
            await RecupDatasAsync(lesId);

            if (lstLocataires.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un locataire dans la liste.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Détermination des chemins
            string type = (this.datas.TryGetValue("TypeHabitat", out string typeHab) && typeHab == "Chambre en colocation")
                ? " colocation"
                : "";

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string cheminModele = Path.Combine(baseDir, "Baux", "Contrat-type de location meublée version 2025.docx");

            if (!File.Exists(cheminModele))
            {
                MessageBox.Show($"Le fichier modèle est introuvable :\n{cheminModele}", "Erreur fichier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Destination dynamique sur le Bureau de l'utilisateur connecté
            string bureauPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string nomFichier = $"Bail{type} {lstLocataires.SelectedItem}.docx";
            string cheminDestination = Path.Combine(bureauPath, nomFichier);

            try
            {
                File.Copy(cheminModele, cheminDestination, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Impossible de copier le fichier modèle : {ex.Message}", "Erreur I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Remplissage du document Word hors du thread UI pour éviter le gel de la fenêtre
            bool succes = await Task.Run(() =>
            {
                Word.Application wordApp = null;
                Word.Document bail = null;

                try
                {
                    wordApp = new Word.Application { Visible = false };
                    bail = wordApp.Documents.Open(cheminDestination);
                    Word.Find find = wordApp.Selection.Find;

                    // Remplacement des variables génériques
                    foreach (KeyValuePair<string, string> data in datas)
                    {
                        RemplacerTexteWord(find, $"%{data.Key}%", data.Value ?? "");
                    }

                    // Remplacement des variables spécifiques
                    RemplacerTexteWord(find, "%DateDuJour%", DateTime.Now.ToString("dd/MM/yyyy"));

                    bool isVisale = datas.TryGetValue("NomCaution", out string nomCaution) && nomCaution.Equals("VISALE", StringComparison.OrdinalIgnoreCase);

                    RemplacerTexteWord(find, "%NbExemplaire%", isVisale ? "2" : "3");
                    RemplacerTexteWord(find, "%MentionCaution%", isVisale ? "" : "LE(LA) CAUTION");
                    RemplacerTexteWord(find, "%MentionLuApprouve%", isVisale ? "" : "Lu et approuvé");

                    // Enregistrement
                    bail.Save();
                    return true;
                }
                catch (Exception ex)
                {
                    // Enregistrement de l'erreur / log si nécessaire
                    MessageBox.Show($"Impossible de copier le fichier modèle : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    // Nettoyage impératif des objets COM
                    if (bail != null)
                    {
                        bail.Close();
                        Marshal.ReleaseComObject(bail);
                    }
                    if (wordApp != null)
                    {
                        wordApp.Quit();
                        Marshal.ReleaseComObject(wordApp);
                    }
                }
            });

            if (succes)
            {
                MessageBox.Show("Votre contrat de location a été généré sur votre Bureau.\nPensez à vérifier tous les champs.", "Génération réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Une erreur est survenue lors de la génération du document Word.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Remplace toutes les occurrences d'une balise par une chaîne de texte dans le document Word.
        /// </summary>
        /// <param name="find">L'objet Find de l'application Word (ex: wordApp.Selection.Find)</param>
        /// <param name="texteAChercher">Le texte/placeholder à remplacer (ex: "%DateDuJour%")</param>
        /// <param name="texteRemplacement">La valeur de remplacement</param>
        private void RemplacerTexteWord(Word.Find find, string texteAChercher, string texteRemplacement)
        {
            if (string.IsNullOrEmpty(texteAChercher)) return;

            // 1. Réinitialisation des formats pour éviter d'hériter de filtres de recherches précédents
            find.ClearFormatting();
            find.Replacement.ClearFormatting();

            // 2. Assignation des textes
            find.Text = texteAChercher;
            find.Replacement.Text = texteRemplacement ?? "";

            // 3. Configuration des paramètres de recherche Word
            object replaceAll = Word.WdReplace.wdReplaceAll;
            object wrapContinue = Word.WdFindWrap.wdFindContinue; // Continue la recherche dans tout le document

            // 4. Exécution du remplacement
            find.Execute(
                FindText: find.Text,
                MatchCase: false,
                MatchWholeWord: false,
                MatchWildcards: false,
                Wrap: wrapContinue,
                ReplaceWith: find.Replacement.Text,
                Replace: replaceAll
            );
        }


        /// <summary>
        /// Récupère le dernier indice IRL depuis l'API INSEE avec un timeout strict de 5 secondes.
        /// </summary>
        public async Task RecupIRLAsync()
        {
            bool jetonObtenu = await AssurerJetonAPIInseeValideAsync();

            if (!jetonObtenu)
            {
                MessageBox.Show(
                    "Impossible d'obtenir le jeton d'accès à l'API de l'INSEE.\nVous devrez renseigner la valeur de l'IRL manuellement.",
                    "Authentification INSEE échouée",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string uri = Global.IrlURI;
            string bearerToken = Global.bearerToken;

            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            using (HttpClient client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                // 1. Définition du timeout via CancellationTokenSource
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)))
                {
                    try
                    {
                        // 2. Passage du jeton d'annulation à la méthode HTTP
                        HttpResponseMessage httpResponse = await client.GetAsync(uri, cts.Token);

                        if (httpResponse.IsSuccessStatusCode)
                        {
                            string xmlContent = await httpResponse.Content.ReadAsStringAsync();
                            XDocument xmlDoc = XDocument.Parse(xmlContent);

                            bool indexTrouve = false;

                            foreach (XElement obs in xmlDoc.Descendants("Obs"))
                            {
                                string dateJo = (string)obs.Attribute("DATE_JO");

                                if (!string.IsNullOrEmpty(dateJo))
                                {
                                    string period = (string)obs.Attribute("TIME_PERIOD") ?? "";
                                    string valeur = (string)obs.Attribute("OBS_VALUE") ?? "";

                                    string periodFormatee = period.Replace("Q", "T");

                                    this.datas["IRL"] = $"{valeur} ({periodFormatee})";
                                    indexTrouve = true;
                                    break;
                                }
                            }

                            if (!indexTrouve)
                            {
                                this.datas["IRL"] = "";
                                MessageBox.Show("Aucun indice IRL valide n'a été trouvé dans le flux fourni.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            this.datas["IRL"] = "";
                            MessageBox.Show($"La requête permettant de récupérer l'IRL a échoué (Code HTTP : {httpResponse.StatusCode}). Pensez à le renseigner vous-même.", "Erreur API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (OperationCanceledException) when (cts.IsCancellationRequested)
                    {
                        // 3. Gestion spécifique du timeout
                        this.datas["IRL"] = "";
                        MessageBox.Show(
                            "L'API de l'INSEE n'a pas répondu dans le délai imparti (5 secondes).\nVeuillez saisir l'IRL manuellement.",
                            "Délai d'attente dépassé",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    catch (Exception err)
                    {
                        this.datas["IRL"] = "";
                        MessageBox.Show($"Une erreur réseau s'est produite lors de la récupération de l'IRL : {err.Message}", "Erreur Réseau", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        /// <summary>
        /// Récupère un jeton valide pour l'API de l'INSEE.
        /// Utilise le jeton en cache si celui-ci est encore valide.
        /// </summary>
        public static async Task<bool> AssurerJetonAPIInseeValideAsync()
        {
            // 1. Vérification du cache : si le token a moins de 6 jours, on le conserve
            if (!string.IsNullOrEmpty(Global.bearerToken) &&
                (DateTime.Now - Global.dateBearerToken).TotalDays < 6)
            {
                return true;
            }

            // 2. Préparation de la requête de jeton
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Global.consumerkey}:{Global.secretclient}"));

            using (var request = new HttpRequestMessage(HttpMethod.Post, "https://api.insee.fr/token"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                request.Content = requestContent;

                try
                {
                    using (HttpResponseMessage response = await new HttpClient().SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            JObject json = JObject.Parse(responseBody);

                            if (json.TryGetValue("access_token", out JToken tokenToken))
                            {
                                Global.bearerToken = tokenToken.ToString();
                                Global.dateBearerToken = DateTime.Now;
                                return true;
                            }
                        }

                        // Journalisation ou retour d'échec
                        return false;
                    }
                }
                catch (Exception)
                {
                    // En cas d'erreur réseau, on retourne false pour que le formulaire appelant gère la saisie manuelle
                    return false;
                }
            }
        }


        /// <summary>
        /// Récupère les dates de début et de fin d'assurance du logement
        /// </summary>
        public void RecupAssurance()
        {
            using (var fenAssurance = new DateAssurance())
            {
                if (fenAssurance.ShowDialog() == DialogResult.OK)
                {
                    // Récupération directe des données typées
                    string debut = fenAssurance.DateSouscription.ToString("dd/MM/yyyy");
                    string fin = fenAssurance.DateEcheance.ToString("dd/MM/yyyy");
                    string montant = fenAssurance.MontantAssurance.ToString();

                    this.datas.Add("DateSousAssur", debut);
                    this.datas.Add("DateFinAssur", fin);
                    this.datas.Add("MontantAssur", montant);
                }
            }
        }


        /// <summary>
        /// Vérifie la validité des champs de saisie avant enregistrement.
        /// </summary>
        /// <returns>True si le formulaire est valide, False sinon.</returns>
        private bool ChampsRenseignes()
        {
            // 1. Sélection des éléments obligatoires dans les listes
            if (lstBiens.SelectedItem == null)
            {
                AfficherAvertissement("Veuillez sélectionner un bien.", lstBiens);
                return false;
            }

            if (lstLocataires.SelectedItem == null)
            {
                AfficherAvertissement("Veuillez sélectionner un locataire.", lstLocataires);
                return false;
            }

            if (lstCautions.SelectedItem == null)
            {
                AfficherAvertissement("Veuillez sélectionner une caution.", lstCautions);
                return false;
            }

            // 2. Cohérence des dates (comparaison sur les dates seules, sans les heures)
            if (datFin.Value.Date < datDebut.Value.Date)
            {
                AfficherAvertissement("La date de fin de contrat ne peut pas être antérieure à la date de début.", datFin);
                return false;
            }

            // 3. Validation spécifique du contrat Visale
            string cautionSelectionnee = lstCautions.SelectedItem.ToString();
            if (cautionSelectionnee.StartsWith("VISALE", StringComparison.OrdinalIgnoreCase) &&
                string.IsNullOrWhiteSpace(txtContratVisale.Text))
            {
                AfficherAvertissement("Veuillez renseigner le numéro de contrat Visale.", txtContratVisale);
                return false;
            }

            // 4. Normalisation des valeurs par défaut
            if (string.IsNullOrWhiteSpace(txtDepotGarantie.Text))
            {
                txtDepotGarantie.Text = "0";
            }

            return true;
        }

        /// <summary>
        /// Centralise l'affichage des messages d'avertissement et repositionne le curseur sur le contrôle en erreur
        /// </summary>
        private void AfficherAvertissement(string message, Control controleAProbleme)
        {
            MessageBox.Show(
                message,
                "Saisie incomplète ou invalide",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            controleAProbleme?.Focus();
        }


        /// <summary>
        /// Récupère l'ID du bien, du locataire et de la caution sélectionnés.
        /// </summary>
        private string[] RecupLesId()
        {
            string[] lesId = new string[3];

            lesId[0] = ObtenirIdViaNom("bien", "idbien", "nombien", lstBiens.SelectedItem?.ToString());
            lesId[1] = ObtenirIdViaNom("locataire", "idlocataire", "nomcompletlocataire", lstLocataires.SelectedItem?.ToString());
            lesId[2] = ObtenirIdViaNom("caution", "idcaution", "nomcompletcaution", lstCautions.SelectedItem?.ToString());

            return lesId;
        }

        /// <summary>
        /// Helper sécurisé pour récupérer un ID à partir d'un libellé
        /// </summary>
        private string ObtenirIdViaNom(string table, string champId, string champNom, string valeurRecherchee)
        {
            if (string.IsNullOrWhiteSpace(valeurRecherchee))
                return "0";

            string req = $"SELECT {champId} FROM {table} WHERE {champNom} = @nom";

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(req, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@nom", valeurRecherchee);
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? result.ToString() : "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche de l'ID dans {table} : {ex.Message}", "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "0";
            }
        }


        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Structure légère pour lire et manipuler les paiements existants depuis la BDD
        /// </summary>
        private class PaiementExistant
        {
            public int IdPaiement { get; set; }
            public DateTime PeriodeFacturee { get; set; }
        }

        /// <summary>
        /// Synchronise les enregistrements de la table Paiement avec la période du bail
        /// </summary>
        /// <param name="idLocation">ID de la location concernée</param>
        public void MajTablePaiement(int idLocation)
        {
            DateTime dateDebut = datDebut.Value.Date;
            DateTime dateFin = datFin.Value.Date;

            // 1. Génération des échéances théoriques (DateTime)
            List<DateTime> lesMensualites = new List<DateTime>();
            DateTime dateCpt = new DateTime(dateDebut.Year, dateDebut.Month, 1);

            while (dateCpt <= dateFin)
            {
                int jour;
                if (dateCpt.Year == dateDebut.Year && dateCpt.Month == dateDebut.Month)
                    jour = dateDebut.Day;
                else if (dateCpt.Year == dateFin.Year && dateCpt.Month == dateFin.Month)
                    jour = dateFin.Day;
                else
                    jour = 1;

                lesMensualites.Add(new DateTime(dateCpt.Year, dateCpt.Month, jour));
                dateCpt = dateCpt.AddMonths(1);
            }

            // 2. Récupération sécurisée des paiements existants en BDD
            List<PaiementExistant> resBdd = new List<PaiementExistant>();
            string reqSelect = "SELECT idpaiement, periodefacturee FROM paiement WHERE idlocation = @idlocation";

            using (MySqlCommand cmd = new MySqlCommand(reqSelect, Global.Connexion))
            {
                cmd.Parameters.AddWithValue("@idlocation", idLocation);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("periodefacturee")))
                        {
                            resBdd.Add(new PaiementExistant
                            {
                                IdPaiement = reader.GetInt32("idpaiement"),
                                PeriodeFacturee = reader.GetDateTime("periodefacturee")
                            });
                        }
                    }
                }
            }

            // 3. Traitement des ajouts et mises à jour
            string bienSelectionne = lstBiens.SelectedItem.ToString();

            foreach (DateTime mensu in lesMensualites)
            {
                // Recherche si une mensualité existe déjà pour le même mois et la même année
                PaiementExistant paiementExistant = resBdd.FirstOrDefault(p =>
                    p.PeriodeFacturee.Year == mensu.Year &&
                    p.PeriodeFacturee.Month == mensu.Month);

                if (paiementExistant != null)
                {
                    // Si le jour de l'échéance a changé (ex: ajustement de début/fin de contrat)
                    if (paiementExistant.PeriodeFacturee.Day != mensu.Day)
                    {
                        decimal montantDu = CalculerMontantDu(bienSelectionne, mensu);
                        ModifierPaiement(paiementExistant.IdPaiement, montantDu, mensu);
                    }
                }
                else
                {
                    // Création d'un nouvel enregistrement (l'ID est auto-généré par MySQL)
                    decimal montantDu = CalculerMontantDu(bienSelectionne, mensu);
                    AjouterPaiement(idLocation, mensu, montantDu);
                }
            }

            // 4. Suppression des paiements devenus hors période (ex: contrat raccourci)
            foreach (PaiementExistant pBdd in resBdd)
            {
                bool estToujoursValide = lesMensualites.Any(m =>
                    m.Year == pBdd.PeriodeFacturee.Year &&
                    m.Month == pBdd.PeriodeFacturee.Month);

                if (!estToujoursValide)
                {
                    SupprimerPaiement(pBdd.IdPaiement);
                }
            }
        }


        /// <summary>
        /// Insère un nouvel enregistrement de paiement dans la base de données (AUTO_INCREMENT)
        /// </summary>
        /// <param name="idLocation">ID de la location rattachée</param>
        /// <param name="periodeFacturee">Date de l'échéance / mensualité</param>
        /// <param name="montantDu">Montant dû calculé pour cette mensualité</param>
        /// <param name="montantPaye">Montant déjà réglé (0 par défaut)</param>
        /// <param name="datePaiement">Date effective du règlement (null si non réglé)</param>
        /// <returns>L'idpaiement auto-généré par MySQL</returns>
        public int AjouterPaiement(int idLocation, DateTime periodeFacturee, decimal montantDu, decimal montantPaye = 0m, DateTime? datePaiement = null)
        {
            decimal resteAPayer = montantDu - montantPaye;
            bool loyerRegle = resteAPayer <= 0;

            string reqInsert = @"INSERT INTO paiement 
                                (idlocation, datepaiement, montantpaye, periodefacturee, montantdu, resteapayer, loyerregle) 
                                VALUES 
                                (@idLocation, @datePaiement, @montantPaye, @periodeFacturee, @montantDu, @resteAPayer, @loyerRegle)";

            using (MySqlCommand cmd = new MySqlCommand(reqInsert, Global.Connexion))
            {
                cmd.Parameters.AddWithValue("@idLocation", idLocation);
                cmd.Parameters.AddWithValue("@datePaiement", datePaiement.HasValue ? (object)datePaiement.Value.ToString("yyyy-MM-dd") : DBNull.Value);
                cmd.Parameters.AddWithValue("@montantPaye", montantPaye);
                cmd.Parameters.AddWithValue("@periodeFacturee", periodeFacturee.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@montantDu", montantDu);
                cmd.Parameters.AddWithValue("@resteAPayer", resteAPayer);
                cmd.Parameters.AddWithValue("@loyerRegle", loyerRegle);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }

        /// <summary>
        /// Supprime un enregistrement de la table Paiement par son ID
        /// </summary>
        /// <param name="idPaiement">Identifiant du paiement à supprimer</param>
        private void SupprimerPaiement(int idPaiement)
        {
            string reqDelete = "DELETE FROM paiement WHERE idpaiement = @idPaiement";

            using (MySqlCommand cmd = new MySqlCommand(reqDelete, Global.Connexion))
            {
                cmd.Parameters.AddWithValue("@idPaiement", idPaiement);
                cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Calcule le montant dû pour la mensualité concernée (gestion du prorata)
        /// </summary>
        /// <param name="leBien">Nom du bien sélectionné</param>
        /// <param name="laMensualite">Date de la mensualité à calculer</param>
        /// <returns>Montant dû sous forme de decimal</returns>
        public decimal CalculerMontantDu(string leBien, DateTime laMensualite)
        {
            decimal loyercc = 0m;

            // 1. Récupération sécurisée du loyer charges comprises
            string reqLoyer = "SELECT loyercc FROM bien WHERE nombien = @nombien";
            using (MySqlCommand cmd = new MySqlCommand(reqLoyer, Global.Connexion))
            {
                cmd.Parameters.AddWithValue("@nombien", leBien);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    loyercc = Convert.ToDecimal(result);
                }
            }

            DateTime dateDebut = datDebut.Value.Date;
            DateTime dateFin = datFin.Value.Date;

            // 2. Détermination du type de mois via l'objet DateTime
            bool estMoisDebut = (laMensualite.Year == dateDebut.Year && laMensualite.Month == dateDebut.Month);
            bool estMoisFin = (laMensualite.Year == dateFin.Year && laMensualite.Month == dateFin.Month);

            int nbDeJoursMax = DateTime.DaysInMonth(laMensualite.Year, laMensualite.Month);
            decimal montantDu;

            if (estMoisDebut && estMoisFin) // Mois partiel (entrée et sortie dans le même mois)
            {
                int nbDeJours = dateFin.Day - dateDebut.Day + 1;
                montantDu = (loyercc / nbDeJoursMax) * nbDeJours;
            }
            else if (estMoisDebut) // Mois entrant
            {
                int nbJoursOccupes = nbDeJoursMax - dateDebut.Day + 1;
                montantDu = (loyercc / nbDeJoursMax) * nbJoursOccupes;
            }
            else if (estMoisFin) // Mois sortant
            {
                int nbJoursOccupes = dateFin.Day;
                montantDu = (loyercc / nbDeJoursMax) * nbJoursOccupes;
            }
            else // Mois complet
            {
                montantDu = loyercc;
            }

            return Math.Round(montantDu, 2);
        }


        /// <summary>
        /// Gère le calcul du reste à payer pour un paiement donné
        /// </summary>
        /// <param name="idPaiement">ID du paiement concerné</param>
        /// <param name="montantDu">Montant dû pour ce paiement</param>
        /// <returns>Reste à payer sous forme de decimal</returns>
        public decimal CalculerResteAPayer(int idPaiement, decimal montantDu)
        {
            decimal montantPaye = 0m;

            // IFNULL permet de retourner 0 si le montantpaye est NULL en BDD
            string req = "SELECT IFNULL(montantpaye, 0) FROM paiement WHERE idpaiement = @idPaiement";

            using (MySqlCommand cmd = new MySqlCommand(req, Global.Connexion))
            {
                cmd.Parameters.AddWithValue("@idPaiement", idPaiement);
                object res = cmd.ExecuteScalar();

                if (res != null && res != DBNull.Value)
                {
                    montantPaye = Convert.ToDecimal(res);
                }
            }

            return montantDu - montantPaye;
        }


        /// <summary>
        /// Maintient à jour l'enregistrement d'un paiement en BDD
        /// </summary>
        /// <param name="idPaiement">ID du paiement à modifier</param>
        /// <param name="montantDu">Nouveau montant dû</param>
        /// <param name="periodeFacturee">Date de la période facturée</param>
        public void ModifierPaiement(int idPaiement, decimal montantDu, DateTime periodeFacturee)
        {
            decimal resteAPayer = CalculerResteAPayer(idPaiement, montantDu);
            bool regle = resteAPayer <= 0;

            string reqUpdate = @"UPDATE paiement 
                        SET periodefacturee = @periodeFacturee, 
                            montantdu = @montantDu, 
                            resteapayer = @resteAPayer, 
                            loyerregle = @loyerRegle 
                        WHERE idpaiement = @idPaiement";

            using (MySqlCommand cmd = new MySqlCommand(reqUpdate, Global.Connexion))
            {
                // Les paramètres C# s'occupent automatiquement du typage et du formatage SQL (virgules, dates, booléens)
                cmd.Parameters.AddWithValue("@periodeFacturee", periodeFacturee.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@montantDu", montantDu);
                cmd.Parameters.AddWithValue("@resteAPayer", resteAPayer);
                cmd.Parameters.AddWithValue("@loyerRegle", regle);
                cmd.Parameters.AddWithValue("@idPaiement", idPaiement);

                cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Gère la sélection d'une caution
        /// </summary>
        private void LstCautions_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool estVisale = false;

            // 1. On vérifie si un élément est sélectionné et qu'il s'agit bien d'un ListItem
            if (lstCautions.SelectedItem is ListItem cautionSelectionnee)
            {
                // On compare sur la propriété DisplayText de l'objet ListItem
                estVisale = cautionSelectionnee.DisplayText.Equals("VISALE (Action Logement)", StringComparison.OrdinalIgnoreCase);
            }

            // 2. Application de la visibilité sur le label et le champ texte
            lblContratVisale.Visible = estVisale;
            txtContratVisale.Visible = estVisale;

            // 3. Réinitialisation de la saisie si ce n'est pas Visale
            if (!estVisale)
            {
                txtContratVisale.Text = "";
            }
        }
    }
}
