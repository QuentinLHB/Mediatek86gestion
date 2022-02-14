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
            Init();

        }

        private void Init()
        {
            RemplirListeCommandes();
            RemplirListeDocuments();

            if (typeDocument == TypeDocument.REVUE)
            {
                cbxEtatCommande.Visible = false;
                lblEtat.Visible = false;
                btnMaJ.Visible = false;
                btnSupprCommande.Location = btnMaJ.Location;
                lblDateFin.Visible = true;
                dtpDateFin.Visible = true;
            }
            else
            {
                RemplirComboEtats();
            }
        }

        private void RemplirComboEtats()
        {


            bdgEtats.DataSource = controle.GetEtatsCommande();
            cbxEtatCommande.DataSource = bdgEtats;
            if (cbxEtatCommande.Items.Count > 0) cbxEtatCommande.SelectedIndex = 0;


        }

        private void RemplirListeCommandes()
        {

            if (typeDocument == TypeDocument.DVD)
            {
                InitDataGridViewDvdLivre(controle.GetCommandesDvd());
            }
            else if (typeDocument == TypeDocument.LIVRE)
            {
                InitDataGridViewDvdLivre(controle.GetCommandesLivres());
            }
            else
            {
                List<Abonnement> abonnements = controle.GetAbonnementsRevues();
                bdgCommandesListe.DataSource = abonnements;
                dgvListeCommandes.DataSource = bdgCommandesListe;
                dgvListeCommandes.Columns["idRevue"].HeaderText = "N° Revue";
                dgvListeCommandes.Columns["DateFin"].HeaderText = "Fin le";
                dgvListeCommandes.Columns["Date"].HeaderText = "Début le";

                dgvListeCommandes.Columns["Id"].DisplayIndex = 0;
                dgvListeCommandes.Columns["idRevue"].DisplayIndex = 1;
                dgvListeCommandes.Columns["Titre"].DisplayIndex = 2;
                dgvListeCommandes.Columns["Date"].DisplayIndex = 3;
                dgvListeCommandes.Columns["DateFin"].DisplayIndex = 4;
                dgvListeCommandes.Columns["Montant"].DisplayIndex = 5;
                dgvListeCommandes.Columns["Etat"].DisplayIndex = 6;

            }

            dgvListeCommandes.Columns["Id"].HeaderText = "N° Commande";
            dgvListeCommandes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void InitDataGridViewDvdLivre(List<CommandeDocument> commandes)
        {
            bdgCommandesListe.DataSource = commandes;
            dgvListeCommandes.DataSource = bdgCommandesListe;
            dgvListeCommandes.Columns["IdLivreDvd"].HeaderText = "N° Document";
        }

        private void RemplirListeDocuments()
        {
            List<Document> documents = new List<Document>();
            if (typeDocument == TypeDocument.DVD)
            {
                documents.AddRange(controle.GetAllDvd());
            }
            else if (typeDocument == TypeDocument.LIVRE)
            {
                documents.AddRange(controle.GetAllLivres());
            }
            else
            {
                documents.AddRange(controle.GetAllRevues());
            }
            bdgDocuments.DataSource = documents;
            dgvDocuments.DataSource = bdgDocuments;
            dgvListeCommandes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

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
            if (cbxEtatCommande.Items.Count > 0) cbxEtatCommande.SelectedIndex = 0;
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
                    if (typeDocument == TypeDocument.LIVRE || typeDocument == TypeDocument.DVD)
                    {
                        CommandeDocument commande = (CommandeDocument)bdgCommandesListe.List[bdgCommandesListe.Position];
                        txbCommande.Text = commande.ToString();
                        cbxEtatCommande.SelectedItem = commande.Etat;
                    }
                    else  // Revue
                    {
                        Abonnement abonnement = (Abonnement)bdgCommandesListe.List[bdgCommandesListe.Position];
                        txbCommande.Text = abonnement.ToString();
                    }

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

            bool succes;
            if (typeDocument == TypeDocument.LIVRE || typeDocument == TypeDocument.DVD)
            {
                succes = controle.AjouterCommandeDocument(txbIdCommande.Text, double.Parse(txbMontant.Text), (int)nudQuantite.Value, document.Id, document.Titre); ;

            }
            else // Revue
            {
                succes = controle.AjouterAbonnement(txbIdCommande.Text, document.Id, document.Titre, dtpDateFin.Value.Date, double.Parse(txbMontant.Text));
            }

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
            bool succes;
            if (typeDocument == TypeDocument.DVD || typeDocument == TypeDocument.LIVRE)
            {
                CommandeDocument commande = (CommandeDocument)bdgCommandesListe.List[bdgCommandesListe.Position];
              succes = controle.SupprCommandeDocument(commande);
            }
            else
            {
                Abonnement abonnement = (Abonnement)bdgCommandesListe.List[bdgCommandesListe.Position];
                succes = controle.SupprAbonnementRevue(abonnement);
            }
           
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


