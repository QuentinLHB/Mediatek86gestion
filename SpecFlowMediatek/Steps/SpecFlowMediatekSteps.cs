using Mediatek86.controleur;
using Mediatek86.vue;
using NUnit.Framework;
using System;
using System.Windows.Forms;
using TechTalk.SpecFlow;

namespace SpecFlowMediatek.Steps
{
    [Binding]
    public class SpecFlowMediatekSteps
    {
        private readonly Controle controleur = new Controle();
        private readonly FrmConnexion frmConnexion;

        SpecFlowMediatekSteps()
        {
            frmConnexion = new FrmConnexion(controleur);
        }


        [Given(@"the pseudo is '(.*)'")]
        public void GivenThePseudoIs(string pseudo)
        {
            TextBox txbLogin = (TextBox)frmConnexion.Controls["txbLogin"];
            txbLogin.Text = pseudo;
        }
        
        [Given(@"the password is '(.*)'")]
        public void GivenThePasswordIs(string mdp)
        {
            TextBox txbMdp = (TextBox)frmConnexion.Controls["txbMdp"];
            txbMdp.Text = mdp;
        }
        
        [When(@"we press the submit button")]
        public void WhenWePressTheSubmitButton()
        {
            Button btnConnexion = (Button)frmConnexion.Controls["btnConnexion"];
            btnConnexion.PerformClick();
        }
        
        [Then(@"the user should be logged in")]
        public void ThenTheUserShouldBeLoggedIn()
        {
            bool isVisible = frmConnexion.Visible;
            Assert.AreEqual(false, isVisible);
        }
    }
}
