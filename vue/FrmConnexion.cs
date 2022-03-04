using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mediatek86.controleur;

namespace Mediatek86.vue
{
    public partial class FrmConnexion : Form
    {
        private Controle controle;

        public FrmConnexion(Controle controle)
        {
            InitializeComponent();
            txbMdp.UseSystemPasswordChar = true;
            this.controle = controle;
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            if(controle.Connection(txbLogin.Text, txbMdp.Text))
            {
                this.Hide();
                if (controle.peutLire())
                {
                    controle.OuvreFormulairePrincipal();
                }
                else
                {
                    MessageBox.Show("Vous ne possédez pas les droits nécessaires à l'utilisation de cette application.", "Echec");
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Identifiant ou mot de passe incorrect", "Echec de la connexion");
            }
        }
    }
}
