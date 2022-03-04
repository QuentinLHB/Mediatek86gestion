using Mediatek86.controleur;
using Mediatek86.metier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediatek86.vue
{
    internal partial class FrmExemplaires : Form
    {
        private readonly Document document;
        private readonly Controle controle;
        private readonly BindingSource bdgExemplairesListe = new BindingSource();

        private readonly BindingSource bdgEtatsListe = new BindingSource();

        public FrmExemplaires(Document document, Controle controle)
        {
            InitializeComponent();
            this.document = document;
            this.controle = controle;
            Init();
        }

        /// <summary>
        /// Initialise les objets graphiques.
        /// </summary>
        private void Init()
        {
            bdgEtatsListe.DataSource = controle.GetEtats();
            cbxEtat.DataSource = bdgEtatsListe;

            bdgExemplairesListe.DataSource = controle.GetExemplaires();
            dgvExemplaires.DataSource = bdgExemplairesListe;
            dgvExemplaires.Columns["IdDocument"].Visible = false;
            dgvExemplaires.Columns["Photo"].Visible = false;

            lblDocument.Text = "Exemplaires de : " + document.Titre;
            txbIdDocument.Text =  document.Id;
            txbTitreDocument.Text = document.Titre;
            refreshButtonAccess();
        }

        /// <summary>
        /// Gère la propriété Enabled des objets graphiques selon l'état de la liste des exemplaires.
        /// </summary>
        private void refreshButtonAccess()
        {
            bool shouldEnable = controle.GetExemplaires().Count != 0;
            btnMaJExemplaire.Enabled = shouldEnable;
            btnSupprExemplaire.Enabled = shouldEnable;
            cbxEtat.Enabled = shouldEnable;
        }

        /// <summary>
        /// Affiche le détail de l'exemplaire sélectionné dans la liste.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplaires_SelectionChanged(object sender, EventArgs e)
        {
            Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
            txbNumExemplaire.Text = exemplaire.Numero.ToString();
            cbxEtat.SelectedItem = exemplaire.Etat;
            
        }

        /// <summary>
        /// Met à jour l'état d'un exemplaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaJExemplaire_Click(object sender, EventArgs e)
        {
            controle.ModifierExemplaire(GetExemplaireSelectionne(), GetEtatSelectionne());
            bdgExemplairesListe.ResetBindings(false);
        }

        /// <summary>
        /// Supprime un exemplaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprExemplaire_Click(object sender, EventArgs e)
        {
            controle.SupprimerExemplaire(document, GetExemplaireSelectionne());
            bdgExemplairesListe.ResetBindings(false);
            refreshButtonAccess();
        }

        /// <summary>
        /// Récupère l'exemplaire sélectionné dans la liste.
        /// </summary>
        /// <returns></returns>
        private Exemplaire GetExemplaireSelectionne()
        {
            return (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
        }

        /// <summary>
        /// Récupère l'état sélectionné dans la liste.
        /// </summary>
        /// <returns></returns>
        private Etat GetEtatSelectionne()
        {
            return (Etat)bdgEtatsListe.List[bdgEtatsListe.Position];
        }
    }
}
