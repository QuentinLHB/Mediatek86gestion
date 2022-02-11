using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mediatek86.metier;
using Mediatek86.controleur;
using System.Drawing;
using System.Linq;
using Mediatek86.modele;

namespace Mediatek86.vue
{

    public enum Mode
    {
        Info,
        Modification,
        Ajout,
    }

    public partial class FrmMediatek : Form
    {

        #region Variables globales

        private readonly Controle controle;
        const string ETATNEUF = "00001";

        Mode modeActuel = Mode.Info;

        private readonly BindingSource bdgLivresListe = new BindingSource();
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();
        private List<Dvd> lesDvd = new List<Dvd>();
        private List<Revue> lesRevues = new List<Revue>();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();

        #endregion


        internal FrmMediatek(Controle controle)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.controle = controle;
        }


        #region modules communs

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories"></param>
        /// <param name="bdg"></param>
        /// <param name="cbx"></param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx, bool afficheValeurDefault)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                if (afficheValeurDefault) cbx.SelectedIndex = 0;
                else cbx.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Empêche la saisie de charactères non-alphabétiques.
        /// </summary>
        /// <param name="e">Evenement de type KeyPress</param>
        private void BloqueChracteresNonAlpha(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        #endregion


        #region Revues
        //-----------------------------------------------------------
        // ONGLET "Revues"
        //------------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controle.GetAllRevues();
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxRevuesGenres, false);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxRevuesPublics, false);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxRevuesRayons, false);

            // Combos du bloc information, dans le scénario d'un ajout.
            RemplirComboCategorie(controle.GetAllGenres(), new BindingSource(), cbxInfoGenreRevue, true);
            RemplirComboCategorie(controle.GetAllPublics(), new BindingSource(), cbxInfoPublicRevue, true);
            RemplirComboCategorie(controle.GetAllRayons(), new BindingSource(), cbxInfoRayonRevue, true);

            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["empruntable"].Visible = false;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>();
                    revues.Add(revue);
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue"></param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            chkRevuesEmpruntable.Checked = revue.Empruntable;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            chkRevuesEmpruntable.Checked = false;
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        private void txbRevuesDateMiseADispo_KeyPress(object sender, KeyPressEventArgs e)
        {
            BloqueChracteresNonAlpha(e);
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }


        private void btnAjoutRevue_Click(object sender, EventArgs e)
        {
            VideRevuesInfos();
            modeActuel = Mode.Ajout;
            ChangeModeOngletRevue(Mode.Ajout);
        }

        private void btnModifRevue_Click(object sender, EventArgs e)
        {
            modeActuel = Mode.Modification;
            ChangeModeOngletRevue(Mode.Modification);
        }

        private void btnSupprRevue_Click(object sender, EventArgs e)
        {
            Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
            if (controle.GetExemplairesDocument(revue.Id).Count == 0)
            {
                DialogResult choix = MessageBox.Show("Confirmer la suppression ?",
              "Confirmation", MessageBoxButtons.YesNo);
                if (choix == DialogResult.Yes)
                {
                    if (controle.SupprimerRevue(revue))
                    {
                        lesRevues.Remove(revue);
                        bdgRevuesListe.ResetBindings(false);
                    }
                }
            }
            else
            {
                MessageBox.Show("Impossible de supprimer une revue ayant des exemplaires répertoriés.", "Suppression impossible");
            }


        }

        private void btnValiderRevue_Click(object sender, EventArgs e)
        {
            bool ok = false;
            if (modeActuel == Mode.Ajout)
            {
                ok = ValiderAjoutRevue();
            }

            else if (modeActuel == Mode.Modification)
            {
                ok = ValiderModifRevue();
            }

            if (ok)
            {
                VideRevuesInfos();
                modeActuel = Mode.Info;
                ChangeModeOngletRevue(Mode.Info);
                bdgRevuesListe.ResetBindings(false);
                dgvRevuesListe_SelectionChanged(null, null);
            }
        }


        private bool ValiderAjoutRevue()
        {
            if (txbRevuesNumero.Text == "" || !controle.verifieIdentifiantUnique(txbRevuesNumero.Text))
            {
                MessageBox.Show("Veuillez renseigner un numéro de document unique.");
                return false;
            }
            if (!VerifieCompletionInfosRevue()) return false;

            Genre genre = (Genre)cbxInfoGenreRevue.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicRevue.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonRevue.SelectedItem;
            Revue revue = controle.AjouterRevue(txbRevuesNumero.Text, txbRevuesTitre.Text, txbRevuesImage.Text, genre.Id, genre.Libelle,
                lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle, chkRevuesEmpruntable.Checked, txbRevuesPeriodicite.Text, int.Parse(txbRevuesDateMiseADispo.Text));
            // Si non null, l'ajut à la BDD a bien été effectué.
            if (revue != null)
            {
                lesRevues.Add(revue);
                return true;
            }
            else return false;
        }

        private bool VerifieCompletionInfosRevue()
        {
            if (txbRevuesTitre.Text == "")
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires.");
                return false;
            }
            return true;
        }

        private bool ValiderModifRevue()
        {
            if (controle.verifieIdentifiantUnique(txbLivresNumero.Text))
            {
                MessageBox.Show($"L'entrée {txbLivresNumero.Text} n'existe pas dans la base de données.");
                return false;
            }
            if (!VerifieCompletionInfosLivre()) return false;
            Genre genre = (Genre)cbxInfoGenreRevue.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicRevue.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonRevue.SelectedItem;
            Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
            return controle.ModifierRevue(revue, txbRevuesTitre.Text, txbRevuesImage.Text, genre.Id, genre.Libelle,
                lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle, chkRevuesEmpruntable.Checked, txbRevuesPeriodicite.Text, int.Parse(txbRevuesDateMiseADispo.Text));
        }

        private void btnAnnulerRevue_Click(object sender, EventArgs e)
        {
            VideRevuesInfos();
            modeActuel = Mode.Info;
            ChangeModeOngletRevue(Mode.Info);
            dgvRevuesListe_SelectionChanged(null, null);
        }

        private void ChangeModeOngletRevue(Mode mode)
        {
            bool readOnlyChamps = mode == Mode.Info;
            bool readOnlyIdentifiant = mode == Mode.Info || mode == Mode.Modification;

            txbRevuesGenre.ReadOnly = readOnlyChamps;
            txbRevuesDateMiseADispo.ReadOnly = readOnlyChamps;
            txbRevuesPeriodicite.ReadOnly = readOnlyChamps;
            txbRevuesImage.ReadOnly = readOnlyChamps;
            txbRevuesTitre.ReadOnly = readOnlyChamps;
            dgvRevuesListe.Enabled = readOnlyChamps;
            chkRevuesEmpruntable.Enabled = !readOnlyChamps;
            txbRevuesNumero.ReadOnly = readOnlyIdentifiant;


            txbRevuesGenre.Visible = readOnlyChamps;
            txbRevuesPublic.Visible = readOnlyChamps;
            txbRevuesRayon.Visible = readOnlyChamps;

            cbxInfoGenreRevue.Visible = !readOnlyChamps;
            cbxInfoPublicRevue.Visible = !readOnlyChamps;
            cbxInfoRayonRevue.Visible = !readOnlyChamps;

            btnValiderRevue.Visible = !readOnlyChamps;
            btnAnnulerRevue.Visible = !readOnlyChamps;
        }

        #endregion


        #region Livres

        //-----------------------------------------------------------
        // ONGLET "LIVRES"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controle.GetAllLivres();

            // Combos du bloc recherche.
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxLivresGenres, false);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxLivresPublics, false);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxLivresRayons, false);

            // Combos du bloc information, dans le scénario d'un ajout.
            RemplirComboCategorie(controle.GetAllGenres(), new BindingSource(), cbxInfoGenreLivres, true);
            RemplirComboCategorie(controle.GetAllPublics(), new BindingSource(), cbxInfoPublicLivre, true);
            RemplirComboCategorie(controle.GetAllRayons(), new BindingSource(), cbxInfoRayonLivre, true);

            RemplirLivresListeComplete();

            VideLivresInfos();
            DgvLivresListe_SelectionChanged(null, null);
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>();
                    livres.Add(livre);
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre"></param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;

            Genre genre = (Genre)controle.trouveCategorie(controle.GetAllGenres(), livre.IdGenre);
            if (genre != null) cbxInfoGenreLivres.SelectedItem = genre;
            Public lePublic = (Public)controle.trouveCategorie(controle.GetAllPublics(), livre.IdPublic);
            if (genre != null) cbxInfoGenreLivres.SelectedItem = genre;
            Rayon rayon = (Rayon)controle.trouveCategorie(controle.GetAllRayons(), livre.IdRayon);
            if (genre != null) cbxInfoGenreLivres.SelectedItem = genre;

            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            resetCombobox(cbxInfoGenreLivres);
            resetCombobox(cbxInfoPublicLivre);
            resetCombobox(cbxInfoRayonLivre);
            pcbLivresImage.Image = null;
        }

        private void resetCombobox(ComboBox cbo)
        {
            if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
            else cbo.SelectedIndex = -1;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }


        private void btnAjoutLivre_Click(object sender, EventArgs e)
        {
            VideLivresInfos();
            modeActuel = Mode.Ajout;
            ChangeModeOngletLivre(Mode.Ajout);
        }

        private void btnModifLivre_Click(object sender, EventArgs e)
        {
            modeActuel = Mode.Modification;
            ChangeModeOngletLivre(Mode.Modification);
        }

        private void btnSupprLivre_Click(object sender, EventArgs e)
        {

            Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
            if (controle.GetExemplairesDocument(livre.Id).Count == 0)
            {
                DialogResult choix = MessageBox.Show("Confirmer la suppression ?",
                    "Confirmation", MessageBoxButtons.YesNo);
                if (choix == DialogResult.Yes)
                {
                    if (controle.SupprimerLive(livre))
                    {
                        lesLivres.Remove(livre);
                        bdgLivresListe.ResetBindings(false);
                    }
                }
            }
            else
            {
                MessageBox.Show("Impossible de supprimer un livre ayant des exemplaires répertoriés.", "Suppression impossible");
            }
        }


        /// <summary>
        /// Affiche la groupbox d'informations des livres selon le mode en cours.
        /// </summary>
        /// <param name="mode">Mode actuel.</param>
        private void ChangeModeOngletLivre(Mode mode)
        {
            bool readOnlyChamps = mode == Mode.Info;
            bool readOnlyIdentifiant = mode == Mode.Info || mode == Mode.Modification;

            txbLivresAuteur.ReadOnly = readOnlyChamps;
            txbLivresCollection.ReadOnly = readOnlyChamps;
            txbLivresGenre.ReadOnly = readOnlyChamps;
            txbLivresIsbn.ReadOnly = readOnlyChamps;
            txbLivresImage.ReadOnly = readOnlyChamps;
            txbLivresTitre.ReadOnly = readOnlyChamps;
            dgvLivresListe.Enabled = readOnlyChamps;
            txbLivresNumero.ReadOnly = readOnlyIdentifiant;


            txbLivresGenre.Visible = readOnlyChamps;
            txbLivresPublic.Visible = readOnlyChamps;
            txbLivresRayon.Visible = readOnlyChamps;

            cbxInfoGenreLivres.Visible = !readOnlyChamps;
            cbxInfoPublicLivre.Visible = !readOnlyChamps;
            cbxInfoRayonLivre.Visible = !readOnlyChamps;

            btnValiderLivre.Visible = !readOnlyChamps;
            btnAnnulerLivre.Visible = !readOnlyChamps;
        }

        /// <summary>
        /// Ajoute le livre saisi par par l'utilisateur dans la base de données.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValiderLivre_Click(object sender, EventArgs e)
        {
            bool ok = false;
            if (modeActuel == Mode.Ajout)
            {
                ok = ValiderAjoutLivre();
            }

            else if (modeActuel == Mode.Modification)
            {
                ok = ValiderModifLivre();
            }

            if (ok)
            {
                VideLivresInfos();
                modeActuel = Mode.Info;
                ChangeModeOngletLivre(Mode.Info);
                bdgLivresListe.ResetBindings(false);
                DgvLivresListe_SelectionChanged(null, null);
            }



        }

        private bool ValiderAjoutLivre()
        {
            if (txbLivresNumero.Text == "" || !controle.verifieIdentifiantUnique(txbLivresNumero.Text))
            {
                MessageBox.Show("Veuillez renseigner un numéro de document unique.");
                return false;
            }
            if (!VerifieCompletionInfosLivre()) return false;

            Genre genre = (Genre)cbxInfoGenreLivres.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicLivre.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonLivre.SelectedItem;
            Livre livre = controle.AjouterLivre(txbLivresNumero.Text, txbLivresTitre.Text, txbLivresImage.Text, txbLivresIsbn.Text,
                txbLivresAuteur.Text, txbLivresCollection.Text, genre.Id, genre.Libelle, lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle);
            // Si non null, l'ajut à la BDD a bien été effectué.
            if (livre != null)
            {
                lesLivres.Add(livre);
                return true;
            }
            else return false;
        }


        private bool ValiderModifLivre()
        {
            if (controle.verifieIdentifiantUnique(txbLivresNumero.Text))
            {
                MessageBox.Show($"L'entrée {txbLivresNumero.Text} n'existe pas dans la base de données.");
                return false;
            }
            if (!VerifieCompletionInfosLivre()) return false;
            Genre genre = (Genre)cbxInfoGenreLivres.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicLivre.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonLivre.SelectedItem;
            Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
            return controle.ModifierLivre(livre, txbLivresTitre.Text, txbLivresImage.Text, txbLivresIsbn.Text,
                txbLivresAuteur.Text, txbLivresCollection.Text, genre.Id, genre.Libelle, lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle);
        }

        private bool VerifieCompletionInfosLivre()
        {
            if (txbLivresTitre.Text == "" || txbLivresAuteur.Text == "")
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires.");
                return false;
            }
            return true;
        }



        private void btnAnnulerLivre_Click(object sender, EventArgs e)
        {
            VideLivresInfos();
            modeActuel = Mode.Info;
            ChangeModeOngletLivre(Mode.Info);
            DgvLivresListe_SelectionChanged(null, null);
        }

        private void btnCommandesLivre_Click(object sender, EventArgs e)
        {
            List<CommandeDocument> lst = controle.getCommandesLivres();
            foreach(CommandeDocument cde in lst)
            {
                Console.WriteLine($"id: {cde.Id} - titre : {cde.Titre} - qte: {cde.NbExemplaire}");
            }
        }

        #endregion


        #region Dvd
        //-----------------------------------------------------------
        // ONGLET "DVD"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controle.GetAllDvd();
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxDvdGenres, false);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxDvdPublics, false);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxDvdRayons, false);

            // Combos du bloc information, dans le scénario d'un ajout.
            RemplirComboCategorie(controle.GetAllGenres(), new BindingSource(), cbxInfoGenreDVD, true);
            RemplirComboCategorie(controle.GetAllPublics(), new BindingSource(), cbxInfoPublicDVD, true);
            RemplirComboCategorie(controle.GetAllRayons(), new BindingSource(), cbxInfoRayonDVD, true);

            RemplirDvdListeComplete();
            VideDvdInfos();
            dgvDvdListe_SelectionChanged(null, null);
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>();
                    Dvd.Add(dvd);
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd"></param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        private void txbDvdDuree_KeyPress(object sender, KeyPressEventArgs e)
        {
            BloqueChracteresNonAlpha(e);
        }


        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }

        private void btnAjoutDVD_Click(object sender, EventArgs e)
        {
            VideDvdInfos();
            modeActuel = Mode.Ajout;
            ChangemodeOngletDVD(Mode.Ajout);
        }

        private void btnModifDVD_Click(object sender, EventArgs e)
        {
            modeActuel = Mode.Modification;
            ChangemodeOngletDVD(Mode.Modification);
        }

        private void btnSupprDVD_Click(object sender, EventArgs e)
        {
            Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
            if (controle.GetExemplairesDocument(dvd.Id).Count == 0)
            {
                DialogResult choix = MessageBox.Show("Confirmer la suppression ?",
                "Confirmation", MessageBoxButtons.YesNo);
                if (choix == DialogResult.Yes)
                {
                    if (controle.SupprimerDVD(dvd))
                    {
                        lesDvd.Remove(dvd);
                        bdgDvdListe.ResetBindings(false);
                    }
                }
            }
            else
            {
                MessageBox.Show("Impossible de supprimer une revue ayant des exemplaires répertoriés.", "Suppression impossible");
            }

        }

        private void btnValiderDVD_Click(object sender, EventArgs e)
        {
            bool ok = false;
            if (modeActuel == Mode.Ajout)
            {
                ok = ValiderAjoutDVD();
            }

            else if (modeActuel == Mode.Modification)
            {
                ok = ValiderModifDVD();
            }

            if (ok)
            {
                VideLivresInfos();
                modeActuel = Mode.Info;
                ChangemodeOngletDVD(Mode.Info);
                bdgDvdListe.ResetBindings(false);
                dgvDvdListe_SelectionChanged(null, null);
            }
        }

        private void btnAnnulerDVD_Click(object sender, EventArgs e)
        {
            VideDvdInfos();
            modeActuel = Mode.Info;
            ChangemodeOngletDVD(Mode.Info);
            dgvDvdListe_SelectionChanged(null, null);
        }

        private bool ValiderAjoutDVD()
        {
            if (txbDvdNumero.Text == "" || !controle.verifieIdentifiantUnique(txbDvdNumero.Text))
            {
                MessageBox.Show("Veuillez renseigner un numéro de document unique.");
                return false;
            }
            if (!VerifieCompletionInfosDVD()) return false;

            Genre genre = (Genre)cbxInfoGenreDVD.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicDVD.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonDVD.SelectedItem;
            Dvd dvd = controle.AjouterDvd(txbDvdNumero.Text, txbDvdTitre.Text, txbDvdImage.Text, int.Parse(txbDvdDuree.Text), txbDvdRealisateur.Text, txbDvdSynopsis.Text, genre.Id, genre.Libelle, lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle);
            // Si non null, l'ajut à la BDD a bien été effectué.
            if (dvd != null)
            {
                lesDvd.Add(dvd);
                return true;
            }
            else return false;
        }

        private bool ValiderModifDVD()
        {
            if (controle.verifieIdentifiantUnique(txbDvdNumero.Text))
            {
                MessageBox.Show($"L'entrée {txbDvdNumero.Text} n'existe pas dans la base de données.");
                return false;
            }
            if (!VerifieCompletionInfosDVD()) return false;
            Genre genre = (Genre)cbxInfoGenreDVD.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicDVD.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonDVD.SelectedItem;
            Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
            return controle.ModifierDvd(dvd, txbDvdTitre.Text, txbDvdImage.Text, int.Parse(txbDvdDuree.Text), txbDvdRealisateur.Text, txbDvdSynopsis.Text, genre.Id, genre.Libelle, lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle);
        }

        private bool VerifieCompletionInfosDVD()
        {
            if (txbDvdDuree.Text == "" || txbDvdRealisateur.Text == "" || txbDvdTitre.Text == "")
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires.");
                return false;
            }
            return true;
        }

        private void ChangemodeOngletDVD(Mode mode)
        {
            bool readOnlyChamps = mode == Mode.Info;
            bool readOnlyIdentifiant = mode == Mode.Info || mode == Mode.Modification;

            txbDvdDuree.ReadOnly = readOnlyChamps;
            txbDvdImage.ReadOnly = readOnlyChamps;
            txbDvdRealisateur.ReadOnly = readOnlyChamps;
            txbDvdSynopsis.ReadOnly = readOnlyChamps;
            txbDvdTitre.ReadOnly = readOnlyChamps;
            dgvDvdListe.Enabled = readOnlyChamps;
            txbDvdNumero.ReadOnly = readOnlyIdentifiant;


            txbDvdGenre.Visible = readOnlyChamps;
            txbDvdPublic.Visible = readOnlyChamps;
            txbDvdRayon.Visible = readOnlyChamps;

            cbxInfoGenreDVD.Visible = !readOnlyChamps;
            cbxInfoPublicDVD.Visible = !readOnlyChamps;
            cbxInfoRayonDVD.Visible = !readOnlyChamps;

            btnValiderDVD.Visible = !readOnlyChamps;
            btnAnnulerDVD.Visible = !readOnlyChamps;
        }

        #endregion


        #region Réception Exemplaire de presse
        //-----------------------------------------------------------
        // ONGLET "RECEPTION DE REVUES"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet : blocage en saisie des champs de saisie des infos de l'exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controle.GetAllRevues();
            accesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            bdgExemplairesListe.DataSource = exemplaires;
            dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
            dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
            dgvReceptionExemplairesListe.Columns["idDocument"].Visible = false;
            dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
            dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    VideReceptionRevueInfos();
                }
            }
            else
            {
                VideReceptionRevueInfos();
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            accesReceptionExemplaireGroupBox(false);
            VideReceptionRevueInfos();
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue"></param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            chkReceptionRevueEmpruntable.Checked = revue.Empruntable;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            afficheReceptionExemplairesRevue();
            // accès à la zone d'ajout d'un exemplaire
            accesReceptionExemplaireGroupBox(true);
        }

        private void afficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controle.GetExemplairesDocument(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
        }

        /// <summary>
        /// Vide les zones d'affchage des informations de la revue
        /// </summary>
        private void VideReceptionRevueInfos()
        {
            txbReceptionRevuePeriodicite.Text = "";
            chkReceptionRevueEmpruntable.Checked = false;
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            lesExemplaires = new List<Exemplaire>();
            RemplirReceptionExemplairesListe(lesExemplaires);
            accesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de l'exemplaire
        /// </summary>
        private void VideReceptionExemplaireInfos()
        {
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces"></param>
        private void accesReceptionExemplaireGroupBox(bool acces)
        {
            VideReceptionExemplaireInfos();
            grpReceptionExemplaire.Enabled = acces;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controle.CreerExemplaire(exemplaire))
                    {
                        VideReceptionExemplaireInfos();
                        afficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// Sélection d'une ligne complète et affichage de l'image sz l'exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }







        #endregion

  
    }
}
