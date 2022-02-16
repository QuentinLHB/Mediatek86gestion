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

        private void Init()
        {
            bdgEtatsListe.DataSource = controle.GetEtats();
            cbxEtat.DataSource = bdgEtatsListe;
            bdgExemplairesListe.DataSource = controle.GetExemplaires();
            dgvExemplaires.DataSource = bdgExemplairesListe;
            txbIdDocument.Text = document.Id;
            txbTitreDocument.Text = document.Titre;
        }

        private void dgvExemplaires_SelectionChanged(object sender, EventArgs e)
        {
            Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
            txbNumExemplaire.Text = exemplaire.Numero.ToString();
            cbxEtat.SelectedItem = exemplaire.Etat;
            
        }

        private void btnMaJExemplaire_Click(object sender, EventArgs e)
        {
            controle.ModifierExemplaire(GetExemplaireSelectionne(), GetEtatSelectionne());
            bdgExemplairesListe.ResetBindings(false);
        }

        private void btnSupprExemplaire_Click(object sender, EventArgs e)
        {
            controle.SupprimerExemplaire(document, GetExemplaireSelectionne());
            bdgExemplairesListe.ResetBindings(false);
        }

        private Exemplaire GetExemplaireSelectionne()
        {
            return (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
        }

        private Etat GetEtatSelectionne()
        {
            return (Etat)bdgEtatsListe.List[bdgEtatsListe.Position];
        }
    }
}
