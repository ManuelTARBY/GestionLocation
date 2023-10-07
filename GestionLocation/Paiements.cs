using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using MimeKit;
using MailKit.Security;
using Developpez.Dotnet;

namespace GestionLocation
{
    public partial class Paiements : Form
    {
        
        private MySqlCommand command;
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
        /// <param name="connexion">Connexion à la base de données</param>
        public Paiements(Object fenetre, int idLocation = 0)
        {
            InitializeComponent();
            if (typeof(Accueil).IsInstanceOfType(fenetre))
            {
                this.fenAccueil = fenetre as Accueil;
                this.idUser = this.fenAccueil.GetIdUser();

            }
            else
            {
                this.fenLocation = fenetre as Locations;
                this.idUser = this.fenLocation.GetIdUser();
            }
            this.idLocation = idLocation;
            // Instancie le dictionnaire contenant le détail du paiement en clé et son id en valeur
            this.lesPaiements = new Dictionary<string, string>();
            // Instancie le dictionnaire contenant le détail de la location en clé et son id en valeur
            this.lesId = new Dictionary<string, int>();
            AfficherLocations(false);
            RemplirListePaiements();
            SelectionnerLocation();
        }


        /// <summary>
        /// Liste tous les paiements
        /// </summary>
        public void RemplirListePaiements()
        {
            // Détermination de la requête
            if (this.idLocation != 0)
            {
                this.req = $"SELECT * FROM paiement WHERE idlocation = {this.idLocation}";
            }
            else
            {
                if (lstLocations.SelectedItem == null)
                {
                    this.req = $"SELECT * FROM paiement WHERE loyerregle = False";
                }
                else
                {
                    this.idLocation = this.lesId[lstLocations.SelectedItem.ToString()];
                    this.req = $"SELECT * FROM paiement WHERE loyerregle = False AND idlocation = {this.idLocation}";
                }
            }
            this.req += " ORDER BY periodefacturee";
            // Affiche les enregistrements de la table Paiement et récupère les idpaiement et idlocation dans un dictionnaire
            EnvoiReqSelectPaiements();
        }


        /// <summary>
        /// Lance la requête, affiche les enregistrements de paiements et enregistre les id
        /// </summary>
        public void EnvoiReqSelectPaiements()
        {
            this.lesPaiements.Clear();
            lstPaiements.Items.Clear();
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                /*string ligne = $"Location n°{reader.GetString(1)} || Période : {reader.GetDateTime(4):MMMM yyyy} || " +
                    $"Montant dû : {reader.GetString(5)} || Montant payé : {reader.GetString(3)} || Restant dû : {reader.GetString(6)}";*/
                String dateRegle = $"{reader.GetDateTime(2):d}";
                if (dateRegle.Equals("01/01/0001"))
                {
                    dateRegle = "-";
                }
                string ligne = $"Location n°{reader.GetString(1)} || Période : {reader.GetDateTime(4):MMMM yyyy} || " +
                    $"Montant dû : {reader.GetString(5)} || Montant payé : {reader.GetString(3)} || Date : {dateRegle}" +
                    $" || Restant dû : {reader.GetString(6)}";
                lstPaiements.Items.Add(ligne);
                this.lesPaiements.Add(ligne, reader["idpaiement"].ToString());
                finCurseur = !reader.Read();
            }
            reader.Close();
        }


        /// <summary>
        /// Met à jour la liste des locations en fonction des critères sélectionnés par l'utilisateur
        /// </summary>
        public void AfficherLocations(bool etat)
        {
            // Vide le champ liste, paiements et le dictionnaire contenant les id
            lstLocations.Items.Clear();
            lstPaiements.Items.Clear();
            lesId.Clear();
            // Construit la requête
            StringBuilder req = new StringBuilder();
            req.AppendLine("SELECT nombien AS `Bien`, nomcompletlocataire AS `Locataire`, debutlocation AS `Début de location`, finlocation AS `Fin de location`, nomcompletcaution AS `Caution`, idlocation AS `id`");
            req.AppendLine("FROM location JOIN locataire USING(idlocataire) JOIN bien USING(idbien) JOIN caution USING(idcaution)");
            req.AppendLine($"WHERE locationarchivee = {etat} ORDER BY nombien");
            this.command = new MySqlCommand(req.ToString(), Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            bool finCurseur = !reader.Read();
            while (!finCurseur)
            {
                // Affichage des champs récupérés dans la ligne
                string item = $"{reader["Bien"]} || {reader["Locataire"]} || Du {reader.GetDateTime(2):d} au {reader.GetDateTime(3):d} || Caution : {reader["Caution"]}";
                lstLocations.Items.Add(item);
                lesId.Add(item, (int)(reader["id"]));
                finCurseur = !reader.Read();
            }
            reader.Close();
            // Paramètre le texte sur le messagePied du bouton afficher locations archivees/non archivées
            if (etat)
            {
                btnFiltreArchive.Text = "Afficher les locations non archivées";
            }
            else
            {
                btnFiltreArchive.Text = "Afficher les locations archivées";
            }
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
                    lstLocations.SelectedIndex = lstLocations.Items.IndexOf(paire.Key);
                }
            }
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
        /// Gère l'ouverture de la fenêtre de modification d'un enregistrement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaisirPaiement_Click(object sender, EventArgs e)
        {
            if (lstPaiements.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un paiement dans la liste pour pouvoir le modifier");
            }
            else
            {
                // Récupère l'id de la location
                //this.idLocation = ;
                ModifPaiements fenModifPaiement = new ModifPaiements(this);
                fenModifPaiement.ShowDialog();
            }
        }


        /// <summary>
        /// Gère la sélection d'une location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstLocations_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstLocations.SelectedItem != null)
            {
                // Récupère le nouvel id de location
                this.idLocation = this.lesId[lstLocations.SelectedItem.ToString()];
                // Met à jour la liste des paiements
                RemplirListePaiements();
            }
        }


        /// <summary>
        /// Affiche les locations archivées ou non archivées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFiltreArchive_Click(object sender, EventArgs e)
        {
            if (btnFiltreArchive.Text.Equals("Afficher les locations archivées"))
            {
                AfficherLocations(true);
            }
            else
            {
                AfficherLocations(false);
            }
        }


        /// <summary>
        /// Met à jour la liste des paiements pour n'afficher que ceux qui ne sont pas réglés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNonRegle_Click(object sender, EventArgs e)
        {
            if (lstLocations.SelectedItem != null)
            {
                this.req = $"SELECT * FROM paiement WHERE loyerregle = False AND idlocation = {this.lesId[lstLocations.SelectedItem.ToString()]} ORDER BY periodefacturee";
            }
            else
            {
                this.idLocation = 0;
                this.req = $"SELECT * FROM paiement WHERE loyerregle = False ORDER BY periodefacturee";
            }
            EnvoiReqSelectPaiements();
        }


        /// <summary>
        /// Permet d'obtenir l'id du paiement
        /// </summary>
        /// <returns>ID de la location</returns>
        public string GetIdPaiement()
        {
            return this.lesPaiements[lstPaiements.SelectedItem.ToString()];
        }


        /// <summary>
        /// Permet de récupérer la requête
        /// </summary>
        /// <returns>Requête</returns>
        public string GetRequete()
        {
            return this.req;
        }


        /// <summary>
        /// Retourne l'id de la location concernée
        /// </summary>
        /// <returns>Id de la location concernée</returns>
        public int GetIdLocation()
        {
            return this.idLocation;
        }


        /// <summary>
        /// Envoi une quittance par mail au locataire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnvoyerQuittance_Click(object sender, EventArgs e)
        {
            if (lstPaiements.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un paiement dans la liste pour pouvoir envoyer sa quittance.");
            }
            else
            {
                if (VerifMail() != "")
                {
                    GestionQuittance(GetIdPaiement());
                }
                else
                {
                    MessageBox.Show("Impossible d'envoyer la quittance au locataire, vous n'avez pas renseigné son adresse mail.");
                }
            }
        }


        /// <summary>
        /// Vérifie si une adresse mail est renseignée pour le locataire
        /// </summary>
        /// <returns>Contenu renseigné pour l'adresse mail</returns>
        public string VerifMail()
        {
            // Récupère l'id du locataire
            this.req = $"SELECT idlocataire FROM location WHERE idlocation = {this.idLocation}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            string idLocataire = reader["idlocataire"].ToString();
            reader.Close();

            // Récupère l'adresse mail du locataire
            this.req = $"SELECT emailocataire FROM locataire WHERE idlocataire = {idLocataire}";
            this.command = new MySqlCommand(req.ToString(), Global.Connexion);
            reader = this.command.ExecuteReader();
            string adresse;
            reader.Read();
            adresse = reader["emailocataire"].ToString();
            reader.Close();
            
            return adresse;
        }


        /// <summary>
        /// Gère la procédure de création et d'envoi par mail de la quittance de loyer
        /// </summary>
        public void GestionQuittance(string idPaiement)
        {
            GénérerQuittance(idPaiement);
            EnvoyerQuittance();
        }


        /// <summary>
        /// Génère la quittance
        /// </summary>
        public void GénérerQuittance(string idPaiement)
        {
            // Récupère les coordonnées sur l'utilisateur
            this.req = $"SELECT * FROM utilisateur WHERE iduser = {this.idUser}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            MySqlDataReader reader = this.command.ExecuteReader();
            reader.Read();
            this.leBailleur = $"{reader.GetString(3)} {reader.GetString(4)}";
            string adresseRue = reader.GetString(5);
            string adresseCp = $"{reader.GetString(6)}";
            string adresseVille = $"{reader.GetString(7)}";
            reader.Close();

            // Récupère les données sur la location
            this.req = $"SELECT * FROM location WHERE idlocation = {GetIdLocation()}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            DateTime datDebutLoc = reader.GetDateTime(4);
            DateTime datFinLoc = reader.GetDateTime(5);
            reader.Close();
            string debutLoc = datDebutLoc.ToString("d", CultureInfo.CreateSpecificCulture("fr-FR"));
            string finLoc = datFinLoc.ToString("d", CultureInfo.CreateSpecificCulture("fr-FR"));

            // Récupère les coordonnées sur le locataire
            this.req = $"SELECT * FROM locataire WHERE idlocataire = (SELECT idlocataire FROM location WHERE idlocation = {GetIdLocation()})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            this.leLocataire = $"{reader.GetString(3)}";
            this.emailLocataire = $"{reader.GetString(10)}";
            reader.Close();
            string[] prenomNomSep = this.leLocataire.Split(' ');
            this.leLocataire = prenomNomSep[1] + " " + prenomNomSep[0];

            // Récupère les données sur le bien
            this.req = $"SELECT * FROM bien WHERE idbien = (SELECT idbien FROM location WHERE idlocation = {GetIdLocation()})";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            string charges = $"{reader.GetString(3)}";
            string loyercc = $"{reader.GetString(4)}";
            string adresseRueBien = $"{reader.GetString(5)}";
            string adresseCpVilleBien = $"{reader.GetString(6)} {reader.GetString(7)}";
            reader.Close();

            // Récupère les données sur le paiement (montantdu, réglé, période facturée, date du paiement)
            this.req = $"SELECT * FROM paiement WHERE idpaiement = {idPaiement}";
            this.command = new MySqlCommand(this.req, Global.Connexion);
            reader = this.command.ExecuteReader();
            reader.Read();
            DateTime periodeFactureeComp = reader.GetDateTime(4);
            this.laPeriode = $"{reader.GetDateTime(4):MMMM yyyy}";
            DateTime datePaiement = reader.GetDateTime(2);
            string totalRecu = $"{reader.GetString(3)}";
            reader.Close();
            string strPeriodeFacturee = periodeFactureeComp.ToString("d", CultureInfo.CreateSpecificCulture("fr-FR"));
            string strDatePaiement = datePaiement.ToShortDateString();
            if (strDatePaiement.Equals("01/01/0001"))
            {
                strDatePaiement = "-";
            }

            // Construit la quittance au format pdf
            // Génère le chemin vers le fichier
            string cheminFichier = Environment.CurrentDirectory + $"/Quittances/{this.leLocataire} - {this.laPeriode}.pdf";
            // Crée le document
            Document quittance = new Document(PageSize.A4) ;
            // Permet de travailler sur le document
            PdfWriter.GetInstance(quittance, new FileStream(cheminFichier, FileMode.Create));
            quittance.Open();

            // Polices de caratère
            Font fTitre = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18f, 1, new BaseColor(0, 0, 0));
            Font fNormal = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));
            Font fItalique = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.ITALIC, new BaseColor(0, 0, 0));
            Font fPiedPage = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.ITALIC, new BaseColor(0, 0, 0));
            Font fPetitEspace = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 2f, iTextSharp.text.Font.ITALIC, new BaseColor(0, 0, 0));
            Font fGrasSouligne = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(0, 0, 0));
            Font fGras = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            // Crée les différents paragraphes du document
            // Titre
            Paragraph titre = new Paragraph($"QUITTANCE DE LOYER\n{this.laPeriode.ToUpper()}\n\n", fTitre)
            {
                Alignment = Element.ALIGN_CENTER
            };
            quittance.Add(titre);

            // Encart bailleur
            Paragraph enTeteBailleur = new Paragraph("Le bailleur :", fGrasSouligne)
            {
                Alignment = Element.ALIGN_LEFT
            };
            quittance.Add(enTeteBailleur);
            string blocBailleur = $"{this.leBailleur}\n{adresseRue}\n{adresseCp} {adresseVille}";
            Paragraph donneesBailleur = new Paragraph(blocBailleur, fItalique)
            {
                Alignment = Element.ALIGN_LEFT
            };
            quittance.Add(donneesBailleur);

            // Encart locataire
            Paragraph enTeteLocataire = new Paragraph("Le locataire :", fGrasSouligne)
            {
                Alignment = Element.ALIGN_RIGHT
            };
            quittance.Add(enTeteLocataire);
            string blocLocataire = $"{this.leLocataire}\n{adresseRueBien}\n{adresseCpVilleBien}\n\n";
            Paragraph donneesLocataire = new Paragraph(blocLocataire, fItalique)
            {
                Alignment = Element.ALIGN_RIGHT
            };
            quittance.Add(donneesLocataire);

            // Fait le / à
            string villeCapitalize = Global.Capitalize(adresseVille);
            Paragraph faitLe = new Paragraph($"Fait à {villeCapitalize}, le {DateTime.Today:dd MMMM yyyy}\n\n", fItalique)
            {
                Alignment = Element.ALIGN_RIGHT
            };
            quittance.Add(faitLe);

            // Encart objet
            string[] blocLocation = { $"Adresse de la location :\n", $"{adresseRueBien} {adresseCpVilleBien}" };
            Paragraph locationUn = new Paragraph(blocLocation[0], fGrasSouligne)
            {
                Alignment = Element.ALIGN_LEFT
            };
            quittance.Add(locationUn);
            Paragraph locationDeux = new Paragraph(blocLocation[1], fGras)
            {
                Alignment = Element.ALIGN_LEFT
            };
            quittance.Add(locationDeux);

            // Contenu de la quittance
            string[] laPeriode = strPeriodeFacturee.Split('/');
            int nbJours = DateTime.DaysInMonth(int.Parse(laPeriode[2]), int.Parse(laPeriode[1]));
            string periodeFin = $"{nbJours}/{laPeriode[1]}/{laPeriode[2]}";
            if (strPeriodeFacturee.Equals($"{debutLoc}"))
            {
                strPeriodeFacturee = debutLoc;
            }
            else if (strPeriodeFacturee.Substring(3).Equals($"{finLoc.Substring(3)}"))
            {
                periodeFin = finLoc;
            }
            // Séparer les euros et les centimes
            string[] recu = totalRecu.Split(',');
            string centimes = "";
            if (recu.Length > 1)
            {
            centimes = $" et {NumberConverter.Spell(int.Parse(recu[1]))} centimes";

            }
            string blocContenu = $"\nJe soussigné {this.leBailleur} propriétaire du logement désigné ci-dessus, déclare avoir reçu de " +
                $"{this.leLocataire} la somme de {totalRecu.Replace(',', '.')}€ ({NumberConverter.Spell(int.Parse(recu[0]))} euros" +
                $"{centimes}) au titre du paiement du loyer et des charges pour la " +
                $"période du {strPeriodeFacturee} au {periodeFin} et lui en donne quittance sous réserve de tous mes droits.\n\n";
            Paragraph contenu = new Paragraph(blocContenu, fItalique)
            {
                Alignment = Element.ALIGN_JUSTIFIED
            };
            quittance.Add(contenu);

            // Encart détails du règlement
            Paragraph detailsTitre = new Paragraph("Détails du règlement :", fGrasSouligne)
            {
                Alignment = Element.ALIGN_LEFT
            };
            quittance.Add(detailsTitre);
            quittance.Add(new Phrase("\n", fPetitEspace));
            // Calculs pour les détails
            float ratioChargeLoyer = (float)Math.Round(float.Parse(charges) / float.Parse(loyercc), 2);
            float chargesRecues = (float)Math.Round(float.Parse(totalRecu) * ratioChargeLoyer, 2);
            float loyerRecu = (float)Math.Round(float.Parse(totalRecu) - chargesRecues, 2);
            // Création du tableau contenant les détails du paiment
            PdfPTable tabDetails = new PdfPTable(2)
            {
                WidthPercentage = 40,
                HorizontalAlignment = 0
            };
            // Création des colonnes du tableau
            AddColumnToTab("Loyer hors charges :", fNormal, 0, tabDetails);
            AddColumnToTab($"{loyerRecu.ToString().Replace(',', '.')} euros", fNormal, 2, tabDetails);
            // Création du contenu des cellules
            string[] donnees = new string[6];
            donnees[0] = "Charges :";
            donnees[1] = $"{chargesRecues.ToString().Replace(',', '.')} euros";
            donnees[2] = "Total :";
            donnees[3] = $"{totalRecu.ToString().Replace(',', '.')} euros";
            donnees[4] = "Date du règlement :";
            donnees[5] = strDatePaiement;
            int i = 0;
            foreach (string donnee in donnees)
            {
                PdfPCell cell = new PdfPCell(new Phrase(donnee, fNormal));
                if (i == 4)
                {
                    i = 0;
                }
                cell.HorizontalAlignment = i;
                cell.BorderColor = BaseColor.WHITE;
                tabDetails.AddCell(cell);
                i += 2;
            };
            quittance.Add(tabDetails);

            quittance.Add(new Phrase("\n"));

            // Encart signature
            // Nom et Prénom
            Paragraph signNomPrenom = new Paragraph(this.leBailleur, fGras)
            {
                Alignment = 2
            };
            quittance.Add(signNomPrenom);
            // Signature
            string cheminSignature = Environment.CurrentDirectory + $"/Signature/{this.leBailleur}.png";
            Stream inputImageStream = new FileStream(cheminSignature, FileMode.Open, FileAccess.Read, FileShare.Read);
            Image signature = Image.GetInstance(inputImageStream);
            // Redimensionnement de la signature
            signature.ScalePercent(17);
            // Positionnement de la signature
            signature.SetAbsolutePosition(quittance.PageSize.Width - quittance.RightMargin - signature.ScaledWidth, signature.AbsoluteY);
            // Intégration de la signature dans un paragraphe
            Paragraph sign = new Paragraph()
            {
                signature
            };
            // Intégration du paragraphe contenant la signature à la fenêtre
            quittance.Add(sign);

            // Encart pied de page
            quittance.Add(new Phrase("\n\n\n\n\n\n\n\n"));
            string messagePied = "Cette quittance annule tous les reçus qui auraient pu être établis précédemment en cas de paiement partiel du " +
                "montant du présent terme. Elle est à conserver pendant trois ans par le locataire (loi n° 89-462 du 6 juillet 1989 : art. 7-1).";
            Paragraph piedPage = new Paragraph(messagePied, fPiedPage)
            {
                Alignment = Element.ALIGN_JUSTIFIED
            };
            quittance.Add(piedPage);

            // Ferme le document
            quittance.Close();
            
            // Ouvre le pdf dans le navigateur
            Process.Start(Environment.CurrentDirectory + $"/Quittances/{this.leLocataire} - {this.laPeriode}.pdf");
        }


        /// <summary>
        /// Envoi la quittance
        /// </summary>
        public void EnvoyerQuittance()
        {
            // Construit l'email
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(Global.User, Global.EmailUser));
            email.To.Add(new MailboxAddress(this.leLocataire, this.emailLocataire));
            email.Subject = $"Votre quittance de loyer de {laPeriode}";
            // Bailleur en Cci du mail
            email.Bcc.Add(new MailboxAddress(Global.User, Global.EmailUser));
            string de = "de ";
            // Si le mois concerné par la quittance commence par une voyelle
            if (laPeriode[0].Equals('a') || laPeriode[0].Equals('o'))
            {
                de = "d'";
            }
                var builder = new BodyBuilder
            {
                // Corps du message
                HtmlBody = "<p>Bonjour,<br /></p>" +
                $"<p>Veuillez trouver, ci-jointe, votre quittance de loyer {de}{laPeriode}.<br /><br /></p>" +
                $"<p>Cordialement,<br /><strong>{this.leBailleur}</strong></p>"
            };
            // Chemin de la pièce jointe
            string chemin = Environment.CurrentDirectory + $"/Quittances/{this.leLocataire} - {this.laPeriode}.pdf";
            // Crée la pièce jointe
            var pj = new MimePart()
            {
                Content = new MimeContent(File.OpenRead(chemin)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(chemin)
            };
            // Ajoute la pièce jointe
            builder.Attachments.Add(pj);
            email.Body = builder.ToMessageBody();

            // Crée la session SMTP pour l'envoi
            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(Global.ServeurSmtp, Global.PortEmail, SecureSocketOptions.StartTls);
            smtp.Authenticate(Global.EmailUser, Global.PwdUser);
            try
            {
                // Envoi le mail
                smtp.Send(email);
                // Envoi un message de confirmation à l'utilisateur
                MessageBox.Show("Quittance envoyée avec succès !", "Mail envoyée", MessageBoxButtons.OK);
            }
            // Si exception levée
            catch
            {
                var result = MessageBox.Show("Erreur lors de l'envoi de la quittance !", "Erreur lors de l'envoi du mail", MessageBoxButtons.AbortRetryIgnore);
                // Si l'utilisateur veut retenter l'envoi du mail
                if (result == DialogResult.Retry)
                {
                    smtp.Disconnect(true);
                    EnvoyerQuittance();
                }
            }
            pj.Dispose();
            // Fermeture de la session SMTP
            smtp.Disconnect(true);
        }


        /// <summary>
        /// Récupère l'id de la location dont le paiement est sélectionné
        /// </summary>
        public void RecupIdLocation()
        {
            string[] separatingStrings = { "Location n°", " || " };
            string texte = lstPaiements.SelectedItem.ToString();

            string[] lesMots = texte.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);
            this.idLocation = int.Parse(lesMots[0]);
        }


        /// <summary>
        /// Met à jour l'id de location du paiement sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstPaiements_Click(object sender, EventArgs e)
        {
            if (lstPaiements.SelectedItem != null)
            {
                RecupIdLocation();
            }
        }


    /// <summary>
    /// Ajoute une colonne à un tableau
    /// </summary>
    /// <param name="str">Titre de la colonne à ajouter</param>
    /// <param name="f">Police s'appliquant à la colonne</param>
    /// <param name="p">Indice de la position du texte</param>
    /// <param name="t">Tableau auquel il faut ajouter la colonne</param>
        public void AddColumnToTab(string str, Font f, int p, PdfPTable t)
        {
            PdfPCell cell = new PdfPCell(new Phrase(str, f))
            {
                HorizontalAlignment = p,
                BorderColor = BaseColor.WHITE
            };
            t.AddCell(cell);
        }
    }
}
