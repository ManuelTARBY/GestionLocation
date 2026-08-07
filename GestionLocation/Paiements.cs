using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MimeKit;
using MailKit.Security;
using Developpez.Dotnet;
using iTextFont = iTextSharp.text.Font;
using iTextBaseColor = iTextSharp.text.BaseColor;

namespace GestionLocation
{
    public partial class Paiements : Form
    {
        private string req, laPeriode, leLocataire, leBailleur;
        private readonly string idUser;
        private string emailLocataire;
        private int idLocation;
        private readonly Dictionary<string, string> lesPaiements;
        private readonly Dictionary<string, int> lesId;
        private readonly Accueil fenAccueil;
        private readonly Locations fenLocation;

        /// <summary>
        /// Constructeur de la fenêtre Paiements
        /// </summary>
        public Paiements(object fenetre, int idLocation = 0)
        {
            InitializeComponent();

            if (fenetre is Accueil accueil)
            {
                this.fenAccueil = accueil;
                this.idUser = this.fenAccueil.GetIdUser();
            }
            else if (fenetre is Locations location)
            {
                this.fenLocation = location;
                this.idUser = this.fenLocation.GetIdUser();
            }

            this.idLocation = idLocation;
            this.lesPaiements = new Dictionary<string, string>();
            this.lesId = new Dictionary<string, int>();

            AfficherLocations();
            RemplirListePaiements();
            SelectionnerLocation();
        }

        /// <summary>
        /// Liste tous les paiements
        /// </summary>
        public void RemplirListePaiements()
        {
            this.req = "SELECT nombien, periodefacturee, montantdu, montantpaye, datepaiement, resteapayer, idpaiement, idlocation, " +
                       "CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', SUBSTRING_INDEX(nomlocataire, ',', 1)) AS Locataire " +
                       "FROM paiement " +
                       "NATURAL JOIN location " +
                       "NATURAL JOIN bien " +
                       "NATURAL JOIN locataire " +
                       "WHERE locationarchivee = @locArchiv ";

            if (this.idLocation != 0)
            {
                this.req += "AND idlocation = @idLocation ";
            }
            else
            {
                this.req += "AND loyerregle = False ";
                if (lstLocations.SelectedItem != null && this.lesId.TryGetValue(lstLocations.SelectedItem.ToString(), out int selectedId))
                {
                    this.idLocation = selectedId;
                    this.req += "AND idlocation = @idLocation ";
                }
            }

            this.req += "ORDER BY periodefacturee, nombien";

            EnvoiReqSelectPaiements();
        }

        /// <summary>
        /// Lance la requête, affiche les enregistrements de paiements et enregistre les id
        /// </summary>
        public void EnvoiReqSelectPaiements()
        {
            this.lesPaiements.Clear();
            lstPaiements.Items.Clear();

            try
            {
                using (var cmd = new MySqlCommand(this.req, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@locArchiv", Global.LocArchiv);
                    cmd.Parameters.AddWithValue("@idLocation", this.idLocation);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dateRegle = "-";
                            if (reader["datepaiement"] != DBNull.Value)
                            {
                                DateTime dt = Convert.ToDateTime(reader["datepaiement"]);
                                if (dt != DateTime.MinValue && dt.Year > 1)
                                {
                                    dateRegle = dt.ToString("d");
                                }
                            }

                            DateTime periodeDate = Convert.ToDateTime(reader["periodefacturee"]);
                            string periodeStr = periodeDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture);

                            string ligne = $"{reader["nombien"]} ({reader["Locataire"]}) || {periodeStr} || " +
                                           $"Montant dû : {reader["montantdu"]} || Montant payé : {reader["montantpaye"]} || Date : {dateRegle} || Restant dû : {reader["resteapayer"]}";

                            lstPaiements.Items.Add(ligne);
                            this.lesPaiements[ligne] = reader["idpaiement"].ToString();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors du chargement de la liste des paiements :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Retrouve le nom du bien à partir d'un id de location
        /// </summary>
        public string RecupNomBien(string idloc)
        {
            const string reqNom = "SELECT nombien FROM bien NATURAL JOIN location WHERE idlocation = @idLoc";
            try
            {
                using (var cmd = new MySqlCommand(reqNom, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@idLoc", idloc);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? string.Empty;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de la récupération du bien :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Met à jour la liste des locations en fonction des critères sélectionnés par l'utilisateur
        /// </summary>
        public void AfficherLocations()
        {
            lstLocations.Items.Clear();
            lstPaiements.Items.Clear();
            lesId.Clear();

            const string reqLoc = "SELECT nombien AS Bien, " +
                                  "CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', SUBSTRING_INDEX(nomlocataire, ',', 1)) AS Locataire, " +
                                  "debutlocation, finlocation, nomcompletcaution AS Caution, idlocation AS id " +
                                  "FROM location " +
                                  "JOIN locataire USING(idlocataire) " +
                                  "JOIN bien USING(idbien) " +
                                  "JOIN caution USING(idcaution) " +
                                  "WHERE locationarchivee = @locArchiv ORDER BY nombien";

            try
            {
                using (var cmd = new MySqlCommand(reqLoc, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@locArchiv", Global.LocArchiv);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime debut = Convert.ToDateTime(reader["debutlocation"]);
                            DateTime fin = Convert.ToDateTime(reader["finlocation"]);

                            string item = $"{reader["Bien"]} || {reader["Locataire"]} || Du {debut:d} au {fin:d} || Caution : {reader["Caution"]}";
                            lstLocations.Items.Add(item);
                            this.lesId[item] = Convert.ToInt32(reader["id"]);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors du chargement des locations :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnFiltreArchive.Text = Global.LocArchiv ? "Afficher les locations non archivées" : "Afficher les locations archivées";
        }

        /// <summary>
        /// Sélectionne la location concernée par les paiements affichés
        /// </summary>
        public void SelectionnerLocation()
        {
            foreach (var paire in this.lesId)
            {
                if (paire.Value == this.idLocation)
                {
                    lstLocations.SelectedItem = paire.Key;
                    return;
                }
            }
        }

        /// <summary>
        /// Ferme la fenêtre
        /// </summary>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Gère l'ouverture de la fenêtre de modification d'un enregistrement
        /// </summary>
        private void BtnSaisirPaiement_Click(object sender, EventArgs e)
        {
            if (lstPaiements.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un paiement dans la liste pour pouvoir le modifier.",
                                "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (ModifPaiements fenModifPaiement = new ModifPaiements(this))
            {
                fenModifPaiement.ShowDialog();
            }
        }

        /// <summary>
        /// Affiche les locations archivées ou non archivées
        /// </summary>
        private void BtnFiltreArchive_Click(object sender, EventArgs e)
        {
            Global.LocArchiv = !Global.LocArchiv;
            AfficherLocations();
        }

        /// <summary>
        /// Met à jour la liste des paiements pour n'afficher que ceux qui ne sont pas réglés
        /// </summary>
        private void BtnNonRegle_Click(object sender, EventArgs e)
        {
            this.req = "SELECT nombien, periodefacturee, montantdu, montantpaye, datepaiement, resteapayer, idpaiement, idlocation, " +
                       "CONCAT(SUBSTRING_INDEX(prenomlocataire, ',', 1), ' ', SUBSTRING_INDEX(nomlocataire, ',', 1)) AS Locataire " +
                       "FROM paiement " +
                       "NATURAL JOIN location " +
                       "NATURAL JOIN bien " +
                       "NATURAL JOIN locataire " +
                       "WHERE loyerregle = False ";

            if (lstLocations.SelectedItem != null && this.lesId.TryGetValue(lstLocations.SelectedItem.ToString(), out int locId))
            {
                this.idLocation = locId;
                this.req += "AND idlocation = @idLocation ";
            }
            else
            {
                this.idLocation = 0;
            }

            this.req += "ORDER BY periodefacturee, nombien";
            EnvoiReqSelectPaiements();
        }

        /// <summary>
        /// Permet d'obtenir l'id du paiement sélectionné
        /// </summary>
        public string GetIdPaiement()
        {
            if (lstPaiements.SelectedItem != null && this.lesPaiements.TryGetValue(lstPaiements.SelectedItem.ToString(), out string id))
            {
                return id;
            }
            return string.Empty;
        }

        public string GetRequete() => this.req;

        public int GetIdLocation() => this.idLocation;

        /// <summary>
        /// Envoie une quittance par mail au locataire
        /// </summary>
        private void BtnEnvoyerQuittance_Click(object sender, EventArgs e)
        {
            if (lstPaiements.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un paiement dans la liste pour pouvoir envoyer sa quittance.",
                                "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string idPaiement = GetIdPaiement();
            if (!string.IsNullOrEmpty(idPaiement))
            {
                GestionQuittance(idPaiement);
            }
        }

        /// <summary>
        /// Vérifie si une adresse mail est renseignée pour le locataire (1 seule requête SQL optimisée)
        /// </summary>
        public string VerifMail()
        {
            const string reqMail = "SELECT emailocataire FROM locataire JOIN location USING(idlocataire) WHERE idlocation = @idLocation";
            try
            {
                using (var cmd = new MySqlCommand(reqMail, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@idLocation", this.idLocation);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? string.Empty;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de la vérification de l'email :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gère la procédure de création et d'envoi par mail de la quittance de loyer
        /// </summary>
        public void GestionQuittance(string idPaiement)
        {
            GenererQuittance(idPaiement);
            // EnvoyerQuittance();
        }

        /// <summary>
        /// Génère toutes les quittances d'une année civile
        /// </summary>
        public void GenererQuittanceAnnee(string annee)
        {
            const string reqAnnee = @"SELECT idpaiement, idlocation 
                                      FROM paiement 
                                      WHERE periodefacturee >= @debutperiode 
                                      AND periodefacturee <= @finperiode";

            List<int> idsPaiement = new List<int>();
            List<int> idsLocation = new List<int>();

            try
            {
                using (var cmd = new MySqlCommand(reqAnnee, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@debutperiode", $"{annee}0101");
                    cmd.Parameters.AddWithValue("@finperiode", $"{annee}1231");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idsPaiement.Add(reader.GetInt32("idpaiement"));
                            idsLocation.Add(reader.GetInt32("idlocation"));
                        }
                    }
                }

                if (idsPaiement.Count == 0)
                {
                    MessageBox.Show("Aucun paiement trouvé pour cette période.",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                for (int i = 0; i < idsPaiement.Count; i++)
                {
                    this.idLocation = idsLocation[i];
                    GenererQuittance(idsPaiement[i].ToString());
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de la génération des quittances annuelles :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Génère la quittance PDF en une seule requête SQL sécurisée
        /// </summary>
        public void GenererQuittance(string idPaiement)
        {
            string cheminLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "erreur_quittance.log");

            const string reqFull = @"
        SELECT 
            u.nomuser, u.prenomuser, u.adresseuser, u.cpuser, u.villeuser,
            loc.debutlocation, loc.finlocation,
            l.nomlocataire, l.prenomlocataire, l.emailocataire,
            b.charges, b.loyercc, b.adressebien, b.cpbien, b.villebien,
            p.montantpaye, p.datepaiement, p.periodefacturee
        FROM paiement p
        JOIN location loc ON p.idlocation = loc.idlocation
        JOIN locataire l ON loc.idlocataire = l.idlocataire
        JOIN bien b ON loc.idbien = b.idbien
        JOIN utilisateur u ON u.iduser = @idUser
        WHERE p.idpaiement = @idPaiement";

            try
            {
                using (var cmd = new MySqlCommand(reqFull, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@idPaiement", idPaiement);
                    cmd.Parameters.AddWithValue("@idUser", this.idUser);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Impossible de trouver le paiement ou les données associées en base de données.",
                                            "Aucune donnée", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // 1. Extraction sécurisée (protection contre les champs NULL en BDD)
                        this.leBailleur = $"{reader["prenomuser"]} {reader["nomuser"]}";
                        string adresseRueBailleur = reader["adresseuser"]?.ToString() ?? "";
                        string adresseCpBailleur = reader["cpuser"]?.ToString() ?? "";
                        string adresseVilleBailleur = reader["villeuser"]?.ToString() ?? "";

                        DateTime datDebutLoc = reader["debutlocation"] != DBNull.Value ? Convert.ToDateTime(reader["debutlocation"]) : DateTime.Today;
                        DateTime datFinLoc = reader["finlocation"] != DBNull.Value ? Convert.ToDateTime(reader["finlocation"]) : DateTime.Today;

                        string nomLoc = reader["nomlocataire"]?.ToString() ?? "";
                        string prenomLoc = reader["prenomlocataire"]?.ToString() ?? "";
                        this.leLocataire = $"{prenomLoc} {nomLoc}";
                        this.emailLocataire = reader["emailocataire"]?.ToString() ?? "";

                        decimal charges = reader["charges"] != DBNull.Value ? Convert.ToDecimal(reader["charges"]) : 0m;
                        decimal loyercc = reader["loyercc"] != DBNull.Value ? Convert.ToDecimal(reader["loyercc"]) : 0m;
                        string adresseRueBien = reader["adressebien"]?.ToString() ?? "";
                        string adresseCpVilleBien = $"{reader["cpbien"]} {reader["villebien"]}";

                        decimal totalRecu = reader["montantpaye"] != DBNull.Value ? Convert.ToDecimal(reader["montantpaye"]) : 0m;
                        DateTime datePaiement = reader["datepaiement"] != DBNull.Value ? Convert.ToDateTime(reader["datepaiement"]) : DateTime.MinValue;
                        DateTime periodeFactureeComp = reader["periodefacturee"] != DBNull.Value ? Convert.ToDateTime(reader["periodefacturee"]) : DateTime.Today;

                        this.laPeriode = periodeFactureeComp.ToString("MMMM yyyy", CultureInfo.CurrentCulture);

                        // Formatage des dates
                        string debutLoc = datDebutLoc.ToString("dd/MM/yyyy");
                        string finLoc = datFinLoc.ToString("dd/MM/yyyy");
                        string strPeriodeFacturee = periodeFactureeComp.ToString("dd/MM/yyyy");
                        string strDatePaiement = (datePaiement == DateTime.MinValue || datePaiement.Year <= 1) ? "-" : datePaiement.ToString("dd/MM/yyyy");

                        // 2. Préparation du chemin du fichier PDF
                        string dossierQuittances = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quittances");
                        Directory.CreateDirectory(dossierQuittances);

                        string nomFichierBrut = $"Quittance {this.leLocataire} - {this.laPeriode}.pdf";
                        string nomFichierSain = string.Join("_", nomFichierBrut.Split(Path.GetInvalidFileNameChars()));
                        string cheminFichier = Path.Combine(dossierQuittances, nomFichierSain);

                        // 3. Génération du PDF avec iTextSharp
                        using (var fs = new FileStream(cheminFichier, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (Document quittance = new Document(PageSize.A4))
                            {
                                PdfWriter.GetInstance(quittance, fs);
                                quittance.Open();

                                

                                // Polices iTextSharp
                                iTextFont fTitre = FontFactory.GetFont(FontFactory.HELVETICA, 18f, iTextFont.BOLD, iTextBaseColor.Black);
                                iTextFont fNormal = FontFactory.GetFont(FontFactory.HELVETICA, 11f, iTextFont.NORMAL, iTextBaseColor.Black);
                                iTextFont fItalique = FontFactory.GetFont(FontFactory.HELVETICA, 11f, iTextFont.ITALIC, iTextBaseColor.Black);
                                iTextFont fPiedPage = FontFactory.GetFont(FontFactory.HELVETICA, 8f, iTextFont.ITALIC, iTextBaseColor.Black);
                                iTextFont fPetitEspace = FontFactory.GetFont(FontFactory.HELVETICA, 2f, iTextFont.ITALIC, iTextBaseColor.Black);
                                iTextFont fGrasSouligne = FontFactory.GetFont(FontFactory.HELVETICA, 11f, iTextFont.BOLD | iTextFont.UNDERLINE, iTextBaseColor.Black);
                                iTextFont fGras = FontFactory.GetFont(FontFactory.HELVETICA, 11f, iTextFont.BOLD, iTextBaseColor.Black);
                                Paragraph titre = new Paragraph($"QUITTANCE DE LOYER\n{this.laPeriode.ToUpper()}\n\n", fTitre) { Alignment = Element.ALIGN_CENTER };
                                quittance.Add(titre);

                                Paragraph enTeteBailleur = new Paragraph("Le bailleur :", fGrasSouligne) { Alignment = Element.ALIGN_LEFT };
                                quittance.Add(enTeteBailleur);
                                quittance.Add(new Paragraph($"{this.leBailleur}\n{adresseRueBailleur}\n{adresseCpBailleur} {adresseVilleBailleur}", fItalique) { Alignment = Element.ALIGN_LEFT });

                                Paragraph enTeteLocataire = new Paragraph("Le locataire :", fGrasSouligne) { Alignment = Element.ALIGN_RIGHT };
                                quittance.Add(enTeteLocataire);
                                quittance.Add(new Paragraph($"{this.leLocataire}\n{adresseRueBien}\n{adresseCpVilleBien}\n\n", fItalique) { Alignment = Element.ALIGN_RIGHT });

                                string villeCapitalize = Global.Capitalize(adresseVilleBailleur);
                                quittance.Add(new Paragraph($"Fait à {villeCapitalize}, le {DateTime.Today:dd MMMM yyyy}\n\n", fItalique) { Alignment = Element.ALIGN_RIGHT });

                                quittance.Add(new Paragraph("Adresse de la location :", fGrasSouligne) { Alignment = Element.ALIGN_LEFT });
                                quittance.Add(new Paragraph($"{adresseRueBien} {adresseCpVilleBien}", fGras) { Alignment = Element.ALIGN_LEFT });

                                int nbJours = DateTime.DaysInMonth(periodeFactureeComp.Year, periodeFactureeComp.Month);
                                string periodeFin = $"{nbJours:D2}/{periodeFactureeComp.Month:D2}/{periodeFactureeComp.Year}";

                                if (strPeriodeFacturee.Equals(debutLoc))
                                {
                                    strPeriodeFacturee = debutLoc;
                                }
                                else if (periodeFactureeComp.Month == datFinLoc.Month && periodeFactureeComp.Year == datFinLoc.Year)
                                {
                                    periodeFin = finLoc;
                                }

                                long euros = (long)Math.Truncate(totalRecu);
                                long centimesVal = (long)Math.Round((totalRecu - euros) * 100);
                                string centimesText = centimesVal > 0 ? $" et {NumberConverter.Spell((int)centimesVal)} centimes" : string.Empty;

                                string blocContenu = $"\nJe soussigné {this.leBailleur} propriétaire du logement désigné ci-dessus, déclare avoir reçu de " +
                                    $"{this.leLocataire} la somme de {totalRecu:F2}€ ({NumberConverter.Spell((int)euros)} euros" +
                                    $"{centimesText}) au titre du paiement du loyer et des charges pour la " +
                                    $"période du {strPeriodeFacturee} au {periodeFin} et lui en donne quittance sous réserve de tous mes droits.\n\n";
                                quittance.Add(new Paragraph(blocContenu, fItalique) { Alignment = Element.ALIGN_JUSTIFIED });

                                quittance.Add(new Paragraph("Détails du règlement :", fGrasSouligne) { Alignment = Element.ALIGN_LEFT });
                                quittance.Add(new Phrase("\n", fPetitEspace));

                                decimal ratioChargeLoyer = loyercc > 0 ? (charges / loyercc) : 0;
                                decimal chargesRecues = Math.Round(totalRecu * ratioChargeLoyer, 2);
                                decimal loyerRecu = Math.Round(totalRecu - chargesRecues, 2);

                                PdfPTable tabDetails = new PdfPTable(2) { WidthPercentage = 40, HorizontalAlignment = 0 };
                                AddColumnToTab("Loyer hors charges :", fNormal, Element.ALIGN_LEFT, tabDetails);
                                AddColumnToTab($"{loyerRecu:F2} euros", fNormal, Element.ALIGN_RIGHT, tabDetails);

                                AddColumnToTab("Charges :", fNormal, Element.ALIGN_LEFT, tabDetails);
                                AddColumnToTab($"{chargesRecues:F2} euros", fNormal, Element.ALIGN_RIGHT, tabDetails);

                                AddColumnToTab("Total :", fNormal, Element.ALIGN_LEFT, tabDetails);
                                AddColumnToTab($"{totalRecu:F2} euros", fNormal, Element.ALIGN_RIGHT, tabDetails);

                                AddColumnToTab("Date du règlement :", fNormal, Element.ALIGN_LEFT, tabDetails);
                                AddColumnToTab(strDatePaiement, fNormal, Element.ALIGN_RIGHT, tabDetails);

                                quittance.Add(tabDetails);
                                quittance.Add(new Phrase("\n"));

                                quittance.Add(new Paragraph(this.leBailleur, fGras) { Alignment = Element.ALIGN_RIGHT });

                                string cheminSignature = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Signature", $"{this.leBailleur}.png");
                                if (File.Exists(cheminSignature))
                                {
                                    using (var imgStream = new FileStream(cheminSignature, FileMode.Open, FileAccess.Read, FileShare.Read))
                                    {
                                        Image signature = Image.GetInstance(imgStream);
                                        signature.ScalePercent(17);
                                        signature.SetAbsolutePosition(quittance.PageSize.Width - quittance.RightMargin - signature.ScaledWidth, signature.AbsoluteY);
                                        quittance.Add(signature);
                                    }
                                }

                                quittance.Add(new Phrase("\n\n\n\n\n\n\n\n"));
                                string messagePied = "Cette quittance annule tous les reçus qui auraient pu être établis précédemment en cas de paiement partiel du " +
                                    "montant du présent terme. Elle est à conserver pendant trois ans par le locataire (loi n° 89-462 du 6 juillet 1989 : art. 7-1).";
                                quittance.Add(new Paragraph(messagePied, fPiedPage) { Alignment = Element.ALIGN_JUSTIFIED });

                                quittance.Close();

                                MessageBox.Show($"La quittance a été générée avec succès et enregistrée dans le dossier :\n{dossierQuittances}",
                                                "Quittance générée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Capture absolue de TOUTE exception et écriture sur le disque
                File.WriteAllText(cheminLog, ex.ToString());

                MessageBox.Show($"Une erreur est survenue lors de la génération :\n\n{ex.Message}\n\nLe détail complet a été écrit dans :\n{cheminLog}",
                                "Erreur détectée", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Envoie la quittance par e-mail
        /// </summary>
        public void EnvoyerQuittance()
        {
            if (string.IsNullOrEmpty(this.emailLocataire))
            {
                MessageBox.Show("Le locataire ne possède pas d'adresse email valide.",
                                "Envoi impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dossierQuittances = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quittances");
            string chemin = Path.Combine(dossierQuittances, $"Quittance {this.leLocataire} - {this.laPeriode}.pdf");

            if (!File.Exists(chemin))
            {
                MessageBox.Show("Le fichier de quittance introuvable. Veuillez d'abord le générer.",
                                "Fichier manquant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(Global.User, Global.EmailUser));
            email.To.Add(new MailboxAddress(this.leLocataire, this.emailLocataire));
            email.Subject = $"Votre quittance de loyer de {this.laPeriode}";
            email.Bcc.Add(new MailboxAddress(Global.User, Global.EmailUser));

            string de = (!string.IsNullOrEmpty(this.laPeriode) && (this.laPeriode.StartsWith("a", StringComparison.OrdinalIgnoreCase) || this.laPeriode.StartsWith("o", StringComparison.OrdinalIgnoreCase))) ? "d'" : "de ";

            var builder = new BodyBuilder
            {
                HtmlBody = $"<p>Bonjour,<br /></p>" +
                           $"<p>Veuillez trouver, ci-jointe, votre quittance de loyer {de}{this.laPeriode}.<br /><br /></p>" +
                           $"<p>Cordialement,<br /><strong>{this.leBailleur}</strong></p>"
            };

            builder.Attachments.Add(chemin);
            email.Body = builder.ToMessageBody();

            bool succes = false;
            while (!succes)
            {
                try
                {
                    using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                    {
                        smtp.Connect(Global.ServeurSmtp, Global.PortEmail, SecureSocketOptions.StartTls);
                        smtp.Authenticate(Global.EmailUser, Global.PwdUser);
                        smtp.Send(email);
                        smtp.Disconnect(true);
                    }

                    MessageBox.Show("Quittance envoyée avec succès !", "Email envoyé", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    succes = true;
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show($"Erreur lors de l'envoi de la quittance :\n{ex.Message}",
                                                 "Erreur d'envoi", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);

                    if (result != DialogResult.Retry)
                    {
                        break; // Abort ou Ignore
                    }
                }
            }
        }

        /// <summary>
        /// Gère la sélection d'une location
        /// </summary>
        private void SelectLocation(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem != null && this.lesId.TryGetValue(lstLocations.SelectedItem.ToString(), out int idLoc))
            {
                this.idLocation = idLoc;
                RemplirListePaiements();
            }
        }

        /// <summary>
        /// Récupère l'id de la location dont le paiement est sélectionné
        /// </summary>
        public void RecupIdLocation()
        {
            if (lstPaiements.SelectedItem == null || !this.lesPaiements.TryGetValue(lstPaiements.SelectedItem.ToString(), out string strIdPaiement))
            {
                return;
            }

            const string reqPaiement = "SELECT idlocation FROM paiement WHERE idpaiement = @id";
            try
            {
                using (var cmd = new MySqlCommand(reqPaiement, Global.Connexion))
                {
                    cmd.Parameters.AddWithValue("@id", strIdPaiement);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        this.idLocation = Convert.ToInt32(result);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erreur lors de la récupération de la location :\n{ex.Message}",
                                "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Met à jour l'id de location du paiement sélectionné
        /// </summary>
        private void LstPaiements_Click(object sender, EventArgs e)
        {
            if (lstPaiements.SelectedItem != null)
            {
                RecupIdLocation();
            }
        }

        /// <summary>
        /// Ajoute une colonne/cellule à un tableau PdfPTable
        /// </summary>
        public void AddColumnToTab(string str, iTextFont f, int alignment, PdfPTable t)
        {
            PdfPCell cell = new PdfPCell(new Phrase(str, f))
            {
                HorizontalAlignment = alignment,
                BorderColor = BaseColor.White
            };
            t.AddCell(cell);
        }
    }
}