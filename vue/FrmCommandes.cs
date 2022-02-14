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
    public partial class FrmCommandes : Form
    {
        private readonly Controle controle;
        private readonly TypeDocument typeDocument;
        private readonly BindingSource bdgCommandesListe = new BindingSource();
        private readonly BindingSource bdgDocuments = new BindingSource();
        private readonly BindingSource bdgEtats = new BindingSource();



        internal FrmCommandes(Controle controle, TypeDocument typeDocument)
        {
            InitializeComponent();
            this.controle = controle;
            this.typeDocument = typeDocument;
            RemplirComboEtats();
            RemplirListeCommandes();
            RemplirListeDocuments();
        }

        private void RemplirComboEtats()
        {
            bdgEtats.DataSource = controle.GetEtatsCommande();
            cbxEtatCommande.DataSource = bdgEtats;
            if (cbxEtatCommande.Items.Count > 0) cbxEtatCommande.SelectedIndex = 0;
        }

        private void RemplirListeCommandes()
        {
            List<CommandeDocument> commandes;
            if (typeDocument == TypeDocument.DVD)
            {
                commandes = controle.GetCommandesDvd();
            }
            else
            {
                commandes = controle.GetCommandesLivres();
            }
            bdgCommandesListe.DataSource = commandes;
            dgvListeCommandes.DataSource = bdgCommandesListe;
            dgvListeCommandes.Columns["Id"].HeaderText = "N° Commande";
            dgvListeCommandes.Columns["IdLivreDvd"].HeaderText = "N° Document";
            dgvListeCommandes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void RemplirListeDocuments()
        {
            List<Document> documents = new List<Document>();
            if (typeDocument == TypeDocument.DVD)
            {
                documents.AddRange(controle.GetAllDvd());
            }
            else
            {
                documents.AddRange(controle.GetAllLivres());
            }
            bdgDocuments.DataSource = documents;
            dgvDocuments.DataSource = bdgDocuments;
            dgvListeCommandes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void FrmCommandes_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Empêche la saisie de charactères alphabétiques pour ne garder que les chiffres.
        /// </summary>
        /// <param name="e">Evenement de type KeyPress</param>
        private void BloqueChracteresAlpha(KeyPressEventArgs e)
        {
            // Transforme la virgule (44 en ASCII) en point.
            if (e.KeyChar == 44)
            {
                e.Handled = true;
                txbMontant.Text += ".";
                txbMontant.SelectionStart = txbMontant.Text.Length;
                txbMontant.SelectionLength = 0;
            }

            // 48 à 57 : 0 à 9. 46 = point (pour la décimale)
            else if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 4 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dgvDocuments_SelectionChanged(object sender, EventArgs e)
        {
            VideInfosDocument();
            if (dgvDocuments.CurrentCell != null)
            {
                try
                {
                    Document document = (Document)bdgDocuments.List[bdgDocuments.Position];
                    AfficheInfosDocument(document);
                }
                catch
                {
                    VideInfosDocument();
                }
            }
            else
            {
                VideInfosDocument();
            }
        }

        private void AfficheInfosDocument(Document document)
        {
            txbNumero.Text = document.Id;
            txbTitre.Text = document.Titre;
        }

        private void VideInfosDocument()
        {
            txbIdCommande.Text = "";
            txbNumero.Text = "";
            txbTitre.Text = "";
            nudQuantite.Value = nudQuantite.Minimum;

        }

        private void VideInfosCommande()
        {
            txbCommande.Text = "";
            cbxEtatCommande.SelectedIndex = 0;
        }

        private void txbMontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            BloqueChracteresAlpha(e);
        }

        private void dgvListeCommandes_SelectionChanged(object sender, EventArgs e)
        {

            VideInfosCommande();
            if (dgvListeCommandes.CurrentCell != null)
            {
                try
                {
                    CommandeDocument commande = (CommandeDocument)bdgCommandesListe.List[bdgCommandesListe.Position];
                    txbCommande.Text = commande.ToString();
                    cbxEtatCommande.SelectedItem = commande.Etat;
                }
                catch
                {
                    VideInfosCommande();
                }
            }
            else
            {
                VideInfosCommande();
            }

        }

        private void btnMaJ_Click(object sender, EventArgs e)
        {
            CommandeDocument commande = (CommandeDocument)bdgCommandesListe.List[bdgCommandesListe.Position];
            EtatCommande etat = (EtatCommande)bdgEtats.List[bdgEtats.Position];
            if (!controle.MettreAJourCommandeDocument(commande, etat))
            {
                MessageBox.Show("La mise a jour a échoué.");
            }
            bdgCommandesListe.ResetBindings(false);
        }

        private void btnAjouterCommande_Click(object sender, EventArgs e)
        {
            VerifierCompletionChamps();
            Document document = (Document)bdgDocuments.List[bdgDocuments.Position];
            bool succes = controle.PasserCommandeDocument(txbIdCommande.Text, int.Parse(txbMontant.Text), (int)nudQuantite.Value, document.Id, document.Titre); ;
            if (succes)
            {
                MessageBox.Show("Commande effectuée.");
            }
            else
            {
                MessageBox.Show("La commande a échoué.");
            }
            bdgCommandesListe.ResetBindings(false);
        }

        private void VerifierCompletionChamps()
        {
            if (txbIdCommande.Text == "")
            {
                MessageBox.Show("Veuillez renseigner un numéro de commande unique.");
            }
        }

        private void btnSupprCommande_Click(object sender, EventArgs e)
        {
            CommandeDocument commande = (CommandeDocument)bdgCommandesListe.List[bdgCommandesListe.Position];
            bool succes = controle.SupprCommandeDocument(commande);
            if (succes)
            {
                MessageBox.Show("La commande a été annulée.");
            }
            else
            {
                MessageBox.Show("L'annulation a échoué.");
            }
            bdgCommandesListe.ResetBindings(false);
        }
    }

}


