using System.Collections.Generic;
using System.Windows.Forms;
using Mediatek86.metier;
using Mediatek86.controleur;
using System.Drawing;
using Serilog;

namespace Mediatek86.vue
{

    /// <summary>
    /// Mode actuel du formulaire.
    /// </summary>
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
            this.controle = controle;
            Init();
        }

        /// <summary>
        /// Initialise les objets graphiques.
        /// </summary>
        private void Init()
        {
            // Accessibilité selon le service de l'utilisateur.
            bool peutModif = controle.PeutModifier();
            btnAjoutDVD.Enabled = peutModif;
            btnAjoutLivre.Enabled = peutModif;
            btnAjoutRevue.Enabled = peutModif;
            btnModifDVD.Enabled = peutModif;
            btnModifLivre.Enabled = peutModif;
            btnModifRevue.Enabled = peutModif;
            btnSupprDVD.Enabled = peutModif;
            btnSupprLivre.Enabled = peutModif;
            btnSupprRevue.Enabled = peutModif;
            btnCommandesLivre.Enabled = peutModif;
            btnCommandesDvd.Enabled = peutModif;
            btnCommandeRevues.Enabled = peutModif;
            grpReceptionExemplaire.Enabled = peutModif;

            controle.GetEtatsCommande();
        }

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
        /// Empêche la saisie de charactères alphabétiques pour ne garder que les chiffres.
        /// </summary>
        /// <param name="e">Evenement de type KeyPress</param>
        private void BloqueChracteresAlpha(KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Réinitialise une Combobox à sa première valeur.
        /// </summary>
        /// <param name="cbo"></param>
        private void resetCombobox(ComboBox cbo)
        {
            if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
            else cbo.SelectedIndex = -1;
        }

        /// <summary>
        /// Gère les actions effectuées lors de la fermeture de l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMediatek_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.Information("Fermeture de l'application par {0}. Déclencheur : {1}", controle.GetUserLogin(), e.CloseReason);
        }

        /// <summary>
        /// Ouvre une fenêtre de sélection d'image locale.
        /// </summary>
        /// <returns>Chemin de l'image sélectionnée par l'utilisateur.</returns>
        private string selectionneImageLocale()
        {
            return Outils.selectionnerImageLocale();
        }

        /// <summary>
        /// Affiche une image dans une PictureBox, et affiche le chemind dans une TextBox.
        /// </summary>
        /// <param name="img">Chemin de l'image à afficher.</param>
        /// <param name="pictureBox">PictureBox où afficher l'image.</param>
        /// <param name="textBox">TextBox où afficher le chemin.</param>
        private void afficheImage(string img, PictureBox pictureBox, TextBox textBox)
        {
            textBox.Text = img;
            try
            {
                pictureBox.Image = Image.FromFile(img);
            }
            catch
            {
                pictureBox.Image = null;
            }
        }

    
    }
}
