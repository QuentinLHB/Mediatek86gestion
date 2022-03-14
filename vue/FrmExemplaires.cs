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
            this.StartPosition = FormStartPosition.CenterScreen;
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

            RemplirListe(controle.GetExemplaires());

            lblDocument.Text = "Exemplaires de : " + document.Titre;
            txbIdDocument.Text =  document.Id;
            txbTitreDocument.Text = document.Titre;
            refreshButtonAccess();
        }

        /// <summary>
        /// Remplit la Data GridView avec la liste spécifiée
        /// </summary>
        /// <param name="exemplaires">Liste des exemplaires à afficher.</param>
        private void RemplirListe(List<Exemplaire> exemplaires)
        {
            bdgExemplairesListe.DataSource = exemplaires;
            dgvExemplaires.DataSource = bdgExemplairesListe;
            dgvExemplaires.Columns["IdDocument"].Visible = false;
            dgvExemplaires.Columns["Photo"].Visible = false;
            dgvExemplaires.Columns["DateAchat"].HeaderText = "Date d'achat";
        }

        /// <summary>
        /// Gère la propriété Enabled des objets graphiques selon l'état de la liste des exemplaires.
        /// </summary>
        private void refreshButtonAccess()
        {
            bool shouldEnable = controle.GetExemplaires().Count != 0;
            if (!controle.PeutModifier()) shouldEnable = false;
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
            if(bdgExemplairesListe.Count != 0)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                txbNumExemplaire.Text = exemplaire.Numero.ToString();
                cbxEtat.SelectedItem = exemplaire.Etat;
            }            
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

        /// <summary>
        /// Trie la data grid view selon la colonne sélectionnée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplaires_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvExemplaires.Columns[e.ColumnIndex].HeaderText;
            controle.SortExemplaires(titreColonne);
            bdgExemplairesListe.ResetBindings(false);
        }
    }
}
