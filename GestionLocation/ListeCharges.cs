using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionLocation
{
    public partial class ListeCharges : Form
    {
        private readonly Dictionary<string, string> infoBien;
        private readonly FicheBien fenFicheBien;
        private bool initialisationEnCours = true;

        /// <summary>
        /// Représente une ligne de la liste des charges. Remplace l'ancien système de
        /// dictionnaires indexés par le texte affiché (fragile : deux charges avec le
        /// même libellé/montant/fréquence provoquaient un plantage par clé dupliquée).
        /// La ListBox affiche directement ToString() ; l'id et le nom du bien restent
        /// accessibles en propriétés sur l'objet sélectionné.
        /// </summary>
        private class LigneCharge
        {
            public int IdCharge { get; }
            public string NomBien { get; }
            public string Libelle { get; }
            public string Montant { get; }
            public string Frequence { get; }
            public bool AfficherNomBien { get; }

            public LigneCharge(int idCharge, string nomBien, string libelle, string montant, string frequence, bool afficherNomBien)
            {
                IdCharge = idCharge;
                NomBien = nomBien;
                Libelle = libelle;
                Montant = montant;
                Frequence = frequence;
                AfficherNomBien = afficherNomBien;
            }

            public override string ToString()
            {
                string prefixe = AfficherNomBien ? $"{NomBien} || " : "";
                return $"{prefixe}{Libelle} || Montant : {Montant} € || Fréquence : {Frequence}";
            }
        }

        /// <summary>
        /// Constructeur de ListeCharges
        /// </summary>
        /// <param name="fenetre">Instance de la fenêtre ayant appelé le constructeur</param>
        public ListeCharges(Form fenetre)
        {
            InitializeComponent();
            this.infoBien = new Dictionary<string, string>();

            if (typeof(FicheBien).IsInstanceOfType(fenetre))
            {
                this.fenFicheBien = fenetre as FicheBien;
                this.infoBien = this.fenFicheBien.GetInfoBien();
            }

            this.Text = "Liste des charges";
            RemplirListeBiens();

            // Peuple la combobox des années AVANT le premier appel à RecupListeCharges/AfficheTitre,
            // qui en ont besoin : sans ça, cobAnnee.SelectedItem est null à l'ouverture et la requête
            // de RecupListeCharges plantait (opérande manquant après "annee =").
            if (this.infoBien.ContainsKey("id"))
            {
                MajComboAnnee();
            }

            RecupListeCharges();
            AfficheTitre();

            this.initialisationEnCours = false;
        }

        /// <summary>
        /// Gère le titre à afficher, avec le total des charges pour l'année sélectionnée
        /// </summary>
        public void AfficheTitre()
        {
            string titre = "";
            if (this.infoBien.ContainsKey("nom") && this.infoBien["nom"] != null)
            {
                titre = $"{this.infoBien["nom"].ToUpper()} - ";
            }

            titre += $"Liste des charges {cobAnnee.SelectedItem}";

            if (this.infoBien.ContainsKey("id") && cobAnnee.SelectedItem != null)
            {
                // Pour un groupe, infoBien["id"] est un idgroupe, pas un idbien : il faut
                // passer par lignegroupe. L'ancien code utilisait cet id directement comme
                // idbien pour un groupe, ce qui donnait un total faux (ou toujours à 0).
                bool estGroupe = this.infoBien.ContainsKey("type") && this.infoBien["type"] == "groupe";
                string req = estGroupe
                    ? "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles " +
                      "WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = @id) AND annee = @annee"
                    : "SELECT COALESCE(SUM(chargeannuelle), 0) FROM chargesannuelles WHERE idbien = @id AND annee = @annee";

                using var command = new MySqlCommand(req, Global.Connexion);
                command.Parameters.AddWithValue("@id", this.infoBien["id"]);
                command.Parameters.AddWithValue("@annee", cobAnnee.SelectedItem);

                float total = Convert.ToSingle(command.ExecuteScalar());
                titre += " - Total = " + Math.Round(total, 2) + "€";
            }

            lblNomBien.Text = titre;
        }

        /// <summary>
        /// Récupère la liste des charges pour l'année sélectionnée
        /// </summary>
        public void RecupListeCharges()
        {
            lstCharges.Items.Clear();

            if (!this.infoBien.ContainsKey("type") || this.infoBien["type"] == null || cobAnnee.SelectedItem == null)
            {
                return;
            }

            bool estGroupe = this.infoBien["type"] == "groupe";

            // Le filtre par année était codé en dur sur l'année en cours pour un groupe
            // (YEAR(CURDATE())), ignorant la combobox : changer l'année n'avait alors
            // aucun effet sur l'affichage d'un groupe. Corrigé pour utiliser @annee
            // dans les deux cas, comme pour un bien seul.
            string req = estGroupe
                ? "SELECT idchargeannuelle, nombien, libelle, montantcharge, refFrequence " +
                  "FROM chargesannuelles NATURAL JOIN bien " +
                  "WHERE annee = @annee AND idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = @id) " +
                  "ORDER BY libelle, nombien"
                : "SELECT idchargeannuelle, nombien, libelle, montantcharge, refFrequence " +
                  "FROM chargesannuelles NATURAL JOIN bien " +
                  "WHERE annee = @annee AND idbien = @id " +
                  "ORDER BY libelle, nombien";

            using var command = new MySqlCommand(req, Global.Connexion);
            command.Parameters.AddWithValue("@annee", cobAnnee.SelectedItem);
            command.Parameters.AddWithValue("@id", this.infoBien["id"]);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                lstCharges.Items.Add(new LigneCharge(
                    reader.GetInt32("idchargeannuelle"),
                    reader.GetString("nombien"),
                    reader.GetString("libelle"),
                    reader["montantcharge"].ToString(),
                    reader.GetString("refFrequence"),
                    afficherNomBien: estGroupe));
            }
        }

        /// <summary>
        /// Ouvre la fenêtre d'AjoutModifChargeAnnuelle pour ajout
        /// </summary>
        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            AjoutModifChargeAnnuelle fenCharge = new AjoutModifChargeAnnuelle(this);
            fenCharge.ShowDialog();
        }

        /// <summary>
        /// Permet de récupérer le bien
        /// </summary>
        public Dictionary<string, string> GetLeBien()
        {
            return this.infoBien;
        }

        /// <summary>
        /// Gère le clic sur le bouton modifier
        /// </summary>
        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (!(lstCharges.SelectedItem is LigneCharge ligne))
            {
                MessageBox.Show("Veuillez sélectionner une charge pour pouvoir la modifier.");
                return;
            }

            MajBienSelectionne();
            AjoutModifChargeAnnuelle fenCharge = new AjoutModifChargeAnnuelle(this, ligne.IdCharge.ToString());
            fenCharge.ShowDialog();
        }

        /// <summary>
        /// Renvoie l'instance de FicheBien
        /// </summary>
        public FicheBien GetFenFicheBien()
        {
            return this.fenFicheBien;
        }

        /// <summary>
        /// Gère la demande de suppression d'une charge de la liste
        /// </summary>
        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (!(lstCharges.SelectedItem is LigneCharge ligne))
            {
                MessageBox.Show("Veuillez sélectionner une charge dans la liste pour pouvoir la supprimer.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer la charge : {ligne} ?",
                "Confirmer suppression", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            const string req = "DELETE FROM chargesannuelles WHERE idchargeannuelle = @id";
            using (var command = new MySqlCommand(req, Global.Connexion))
            {
                command.Parameters.AddWithValue("@id", ligne.IdCharge);
                command.ExecuteNonQuery();
            }

            MajChargesDuBien(ligne.NomBien);
            RecupListeCharges();
            this.fenFicheBien?.RemplirChamps();
            AfficheTitre();
        }

        /// <summary>
        /// Met à jour les champs charges annuelles / charges imputables du bien concerné.
        /// Reçoit le nom du bien en paramètre plutôt que de le retrouver via un dictionnaire
        /// indexé par texte affiché.
        /// </summary>
        public void MajChargesDuBien(string nomBien)
        {
            const string reqIdBien = "SELECT idbien FROM bien WHERE nombien = @nom";
            int idBien;
            using (var command = new MySqlCommand(reqIdBien, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nom", nomBien);
                idBien = Convert.ToInt32(command.ExecuteScalar());
            }

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
                // chargeannuelles est un INT en base : arrondi à l'entier le plus proche
                command.Parameters.AddWithValue("@charges", (int)Math.Round(charges));
                command.Parameters.AddWithValue("@chargesImputables", Math.Round(chImputables / 12, 2));
                command.Parameters.AddWithValue("@id", idBien);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gère la fermeture de la fenêtre
        /// </summary>
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Remplit la liste des biens (non archivés)
        /// </summary>
        private void RemplirListeBiens()
        {
            List<string> listeBiens = new List<string>();

            const string req = "SELECT nombien FROM bien WHERE bienarchive = 0 ORDER BY nombien";
            using (var command = new MySqlCommand(req, Global.Connexion))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listeBiens.Add(reader.GetString(0));
                }
            }

            foreach (string bien in listeBiens)
            {
                lstBiens.Items.Add(bien);
            }

            // Si la fenêtre a été ouverte depuis la fiche d'un bien (pas d'un groupe,
            // puisque cette liste ne contient que des biens individuels), positionne le focus
            if (this.fenFicheBien != null && this.infoBien.ContainsKey("nom"))
            {
                int index = lstBiens.FindString(this.infoBien["nom"]);
                if (index != -1)
                {
                    lstBiens.SetSelected(index, true);
                }
            }
        }

        /// <summary>
        /// Gère le filtrage des charges par bien sélectionné dans la liste
        /// </summary>
        private void BtnFiltrer_Click(object sender, EventArgs e)
        {
            if (lstBiens.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un bien dans la liste pour pouvoir afficher ses charges.");
                return;
            }

            // MajBienSelectionne() doit être appelée AVANT MajComboAnnee() : celle-ci se base
            // désormais sur infoBien (mis à jour par MajBienSelectionne), pas directement sur
            // lstBiens.SelectedItem.
            MajBienSelectionne();
            MajComboAnnee();
            RecupListeCharges();
            AfficheTitre();
        }

        /// <summary>
        /// Met à jour la liste des charges lorsqu'on change l'année sélectionnée
        /// </summary>
        private void ChangerAnneeCharge(object sender, EventArgs e)
        {
            // Évite un rechargement redondant (ou pire, un reader imbriqué) si cet évènement
            // se déclenche pendant la construction de la fenêtre, avant que celle-ci ait fini
            // son propre chargement initial.
            if (this.initialisationEnCours)
            {
                return;
            }

            // Ne réévalue le bien sélectionné que si un bien est effectivement choisi dans
            // la liste. Sans ce garde-fou, ouvrir la fiche d'un GROUPE de biens (lstBiens
            // reste alors sans sélection, puisque la liste ne contient que des biens seuls)
            // puis changer l'année provoquait un NullReferenceException.
            if (lstBiens.SelectedItem != null)
            {
                MajBienSelectionne();
            }
            RecupListeCharges();
            AfficheTitre();
        }

        /// <summary>
        /// Met à jour la liste des années disponibles pour le bien ou le groupe courant (infoBien).
        /// Affiche l'année en cours si aucune charge n'a encore été enregistrée, plutôt que de
        /// planter sur un MIN/MAX NULL.
        /// </summary>
        private void MajComboAnnee()
        {
            cobAnnee.Items.Clear();

            if (!this.infoBien.ContainsKey("type"))
            {
                return;
            }

            bool estGroupe = this.infoBien["type"] == "groupe";
            string req = estGroupe
                ? "SELECT MIN(annee), MAX(annee) FROM chargesannuelles " +
                  "WHERE idbien IN (SELECT idbien FROM lignegroupe WHERE idgroupe = @id)"
                : "SELECT MIN(annee), MAX(annee) FROM chargesannuelles WHERE idbien = @id";

            int? anneeMini = null;
            int? anneeMaxi = null;

            // Le reader est ouvert ET refermé (fin du bloc using) AVANT toute modification
            // de cobAnnee.SelectedIndex plus bas. Sans ça, modifier SelectedIndex déclenche
            // immédiatement SelectedIndexChanged -> ChangerAnneeCharge -> MajBienSelectionne,
            // qui tente d'ouvrir un second reader sur la même connexion pendant que celui-ci
            // est encore ouvert : "There is already an open DataReader...".
            using (var command = new MySqlCommand(req, Global.Connexion))
            {
                command.Parameters.AddWithValue("@id", this.infoBien["id"]);
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    if (!reader.IsDBNull(0) && !reader.IsDBNull(1))
                    {
                        anneeMini = reader.GetInt32(0);
                        anneeMaxi = reader.GetInt32(1);
                    }
                }
            }

            if (anneeMini == null || anneeMaxi == null)
            {
                cobAnnee.Items.Add(DateTime.Now.Year);
                cobAnnee.SelectedIndex = 0;
                return;
            }

            for (int i = anneeMini.Value; i <= anneeMaxi.Value; i++)
            {
                cobAnnee.Items.Add(i);
            }
            cobAnnee.SelectedIndex = cobAnnee.Items.Count - 1;
        }

        /// <summary>
        /// Met à jour les infos sur le bien/groupe sélectionné dans lstBiens
        /// </summary>
        public void MajBienSelectionne()
        {
            this.infoBien.Clear();
            string nomSelectionne = lstBiens.SelectedItem.ToString();
            this.infoBien.Add("nom", nomSelectionne);

            const string reqBien = "SELECT idbien FROM bien WHERE nombien = @nom";
            using (var command = new MySqlCommand(reqBien, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nom", nomSelectionne);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    this.infoBien.Add("type", "bien");
                    this.infoBien.Add("id", reader.GetInt32(0).ToString());
                    return;
                }
            }

            const string reqGroupe = "SELECT idgroupe FROM grpedebiens WHERE nomdugroupe = @nom";
            using (var command = new MySqlCommand(reqGroupe, Global.Connexion))
            {
                command.Parameters.AddWithValue("@nom", nomSelectionne);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    this.infoBien.Add("type", "groupe");
                    this.infoBien.Add("id", reader.GetInt32(0).ToString());
                }
            }
        }
    }
}