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
        private readonly string document;
        private readonly BindingSource bdgCommandesListe = new BindingSource();
        private readonly BindingSource bdgDocuments = new BindingSource();


        internal FrmCommandes(Controle controle, string document)
        {
            InitializeComponent();
            this.controle = controle;
            this.document = document;
            RemplirListeCommandes();
            RemplirListeDocuments();
        }

        private void RemplirListeCommandes()
        {
            List<CommandeDocument> commandes;
            if (document == "dvd")
            {
                commandes = controle.getCommandesDvd();
            }
            else /*if(document == "livre")*/
            {
                commandes = controle.getCommandesLivres();
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
            if (document == "dvd")
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
            txbNumero.Text = "";
            txbTitre.Text = "";
            nudQuantite.Value = 0;

;        }

        private void txbMontant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}


