using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mediatek86.metier;
using Mediatek86.controleur;
using System.Drawing;
using System.Linq;
using Mediatek86.modele;
using Serilog;

namespace Mediatek86.vue
{
    public partial class FrmMediatek : Form
    {
        /// <summary>
        /// Retourne le dvd sélectionné dans le tableau.
        /// </summary>
        /// <returns></returns>
        private Dvd GetSelectedDvd()
        {
            if (bdgDvdListe.Count != 0)
            {
                return (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxDvdGenres, false);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxDvdPublics, false);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxDvdRayons, false);

            // Combos du bloc information, dans le scénario d'un ajout.
            RemplirComboCategorie(controle.GetAllGenres(), new BindingSource(), cbxInfoGenreDVD, true);
            RemplirComboCategorie(controle.GetAllPublics(), new BindingSource(), cbxInfoPublicDVD, true);
            RemplirComboCategorie(controle.GetAllRayons(), new BindingSource(), cbxInfoRayonDVD, true);

            RemplirDvdListeComplete();
            refreshAccessibiliteDvd();
            VideDvdInfos();
            dgvDvdListe_SelectionChanged(null, null);
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirDvdListe(List<Dvd> dvds)
        {
            lesDvd = dvds;
            bdgDvdListe.DataSource = dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
            dgvDvdListe.Columns["nbExemplaires"].HeaderText = "Exemplaires";
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
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            txbNbExemplaires.Text = dvd.NbExemplaires.ToString();
            btnExemplairesDvd.Enabled = dvd.NbExemplaires > 0;
            string image = dvd.Image;
            afficheImage(image, pcbDvdImage, txbDvdImage);
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
            txbNbExemplairesDvd.Text = "0";
            pcbDvdImage.Image = null;
            resetCombobox(cbxInfoPublicDVD);
            resetCombobox(cbxInfoGenreDVD);
            resetCombobox(cbxInfoRayonDVD);

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

                    AfficheDvdInfos(GetSelectedDvd());
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

        /// <summary>
        /// Bloque la saisie de caractères alphabétiques sur la textbox de la durée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdDuree_KeyPress(object sender, KeyPressEventArgs e)
        {
            BloqueChracteresAlpha(e);
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
            RemplirDvdListe(controle.GetAllDvd());
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
            controle.SortDvd(titreColonne);
            bdgDvdListe.ResetBindings(false);
        }

        /// <summary>
        /// Active la zone d'ajout de DVD.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjoutDVD_Click(object sender, EventArgs e)
        {
            VideDvdInfos();
            modeActuel = Mode.Ajout;
            ChangemodeOngletDVD(Mode.Ajout);
        }

        /// <summary>
        /// Active la zone de modification du DVD sélectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifDVD_Click(object sender, EventArgs e)
        {
            modeActuel = Mode.Modification;
            ChangemodeOngletDVD(Mode.Modification);
        }

        /// <summary>
        /// Supprime le DVD sélectionné après confirmation de l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprDVD_Click(object sender, EventArgs e)
        {
            Dvd dvd = GetSelectedDvd();
            if (controle.GetExemplairesDocument(dvd.Id).Count == 0)
            {
                DialogResult choix = MessageBox.Show("Confirmer la suppression ?",
                "Confirmation", MessageBoxButtons.YesNo);
                if (choix == DialogResult.Yes &&
                    controle.SupprimerDVD(dvd))
                {
                    lesDvd.Remove(dvd);
                    bdgDvdListe.ResetBindings(false);
                    refreshAccessibiliteDvd();

                }
            }
            else
            {
                MessageBox.Show("Impossible de supprimer une revue ayant des exemplaires répertoriés.", "Suppression impossible");
            }
            refreshAccessibiliteDvd();
        }

        /// <summary>
        /// Valide la modification ou l'aout du DVD saisi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                refreshAccessibiliteDvd();
            }
        }

        /// <summary>
        ///  Annule la saisie en cours et vide les champs associés.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnulerDVD_Click(object sender, EventArgs e)
        {
            VideDvdInfos();
            modeActuel = Mode.Info;
            ChangemodeOngletDVD(Mode.Info);
            dgvDvdListe_SelectionChanged(null, null);
        }

        /// <summary>
        /// Ajoute un nouveau DVD avec les informations saisies.
        /// </summary>
        /// <returns></returns>
        private bool ValiderAjoutDVD()
        {
            if (txbDvdNumero.Text == "" || !controle.VerifieIdentifiantDocumentUnique(txbDvdNumero.Text))
            {
                MessageBox.Show("Veuillez renseigner un numéro de document unique.");
                return false;
            }
            if (!VerifieCompletionInfosDVD()) return false;

            Genre genre = (Genre)cbxInfoGenreDVD.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicDVD.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonDVD.SelectedItem;
            Dvd dvd = controle.AjouterDvd(txbDvdNumero.Text, txbDvdTitre.Text, txbDvdImage.Text, int.Parse(txbDvdDuree.Text), txbDvdRealisateur.Text, txbDvdSynopsis.Text, genre.Id, genre.Libelle, lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle);
            return dvd != null;
        }

        /// <summary>
        /// Modifie le DVD sélectionné avec les informations saisies.
        /// </summary>
        /// <returns></returns>
        private bool ValiderModifDVD()
        {
            if (controle.VerifieIdentifiantDocumentUnique(txbDvdNumero.Text))
            {
                MessageBox.Show($"L'entrée {txbDvdNumero.Text} n'existe pas dans la base de données.");
                return false;
            }
            if (!VerifieCompletionInfosDVD()) return false;
            Genre genre = (Genre)cbxInfoGenreDVD.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicDVD.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonDVD.SelectedItem;
            Dvd dvd = GetSelectedDvd();
            return controle.ModifierDvd(dvd, txbDvdTitre.Text, txbDvdImage.Text, int.Parse(txbDvdDuree.Text), txbDvdRealisateur.Text, txbDvdSynopsis.Text, genre.Id, genre.Libelle, lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle);
        }

        /// <summary>
        /// Vérifie les champs obligatoires.
        /// </summary>
        /// <returns></returns>
        private bool VerifieCompletionInfosDVD()
        {
            if (txbDvdDuree.Text == "" || txbDvdRealisateur.Text == "" || txbDvdTitre.Text == "")
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Modifie l'accessibilité des objets graphiques en fonction du mode actuel.
        /// </summary>
        /// <param name="mode"></param>
        private void ChangemodeOngletDVD(Mode mode)
        {
            bool readOnlyChamps = mode == Mode.Info;
            bool readOnlyIdentifiant = mode == Mode.Info || mode == Mode.Modification;

            txbDvdDuree.ReadOnly = readOnlyChamps;
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

        /// <summary>
        /// Ouvre la fenêtre de gestion des commandes de DVD.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeDVD_Click(object sender, EventArgs e)
        {
            controle.OuvreFormulaireCommandes(GetSelectedDvd());
            bdgDvdListe.ResetBindings(false);
        }

        /// <summary>
        /// Ouvre le formulaire des exemplaires de dvd.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExemplairesDvd_Click(object sender, EventArgs e)
        {
            Dvd dvd = GetSelectedDvd();
            controle.OuvreFormulaireExemplaires(dvd);
            bdgDvdListe.ResetBindings(false);
        }

        private void btnRechercheImageDvd_Click(object sender, EventArgs e)
        {
            string img = selectionneImageLocale();
            afficheImage(img, pcbDvdImage, txbDvdImage);
        }

        /// <summary>
        /// Rend accessible ou non les boutons de l'onglet Dvd en fonction du contenu de la liste des dvds.
        /// </summary>
        private void refreshAccessibiliteDvd()
        {
            bool enable = dgvDvdListe.Rows.Count != 0;
            if (!controle.PeutModifier()) enable = false;
            btnSupprDVD.Enabled = enable;
            btnModifDVD.Enabled = enable;
            btnCommandesDvd.Enabled = enable;
            btnExemplairesDvd.Enabled = enable;
        }
    }
}
