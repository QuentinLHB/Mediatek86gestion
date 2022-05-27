using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mediatek86.metier;

namespace Mediatek86.vue
{
    public partial class FrmMediatek : Form
    {
        /// <summary>
        /// Retourne le livre sélectionné dans le tableau.
        /// </summary>
        /// <returns></returns>
        private Livre GetSelectedLivre()
        {
            if (bdgLivresListe.Count != 0)
            {
                return (Livre)bdgLivresListe.List[bdgLivresListe.Position];
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            // Combos du bloc recherche.
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxLivresGenres, false);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxLivresPublics, false);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxLivresRayons, false);

            // Combos du bloc information, dans le scénario d'un ajout.
            RemplirComboCategorie(controle.GetAllGenres(), new BindingSource(), cbxInfoGenreLivres, true);
            RemplirComboCategorie(controle.GetAllPublics(), new BindingSource(), cbxInfoPublicLivre, true);
            RemplirComboCategorie(controle.GetAllRayons(), new BindingSource(), cbxInfoRayonLivre, true);

            RemplirLivresListeComplete();
            refreshAccessibiliteLivres();

            VideLivresInfos();
            DgvLivresListe_SelectionChanged(null, null);
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirLivresListe(List<Livre> livres)
        {
            lesLivres = livres;
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
            dgvLivresListe.Columns["nbExemplaires"].HeaderText = "Exemplaires";
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>
                    {
                        livre
                    };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre"></param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            txbNbExemplaires.Text = livre.NbExemplaires.ToString();
            btnExemplairesLivres.Enabled = livre.NbExemplaires > 0;

            Genre genre = (Genre)controle.TrouveCategorie(controle.GetAllGenres(), livre.IdGenre);
            if (genre != null) cbxInfoGenreLivres.SelectedItem = genre;
            Public lePublic = (Public)controle.TrouveCategorie(controle.GetAllPublics(), livre.IdPublic);
            if (lePublic != null) cbxInfoPublicLivre.SelectedItem = lePublic;
            Rayon rayon = (Rayon)controle.TrouveCategorie(controle.GetAllRayons(), livre.IdRayon);
            if (rayon != null) cbxInfoRayonLivre.SelectedItem = rayon;

            string image = livre.Image;
            afficheImage(image, pcbLivresImage, txbLivresImage);
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            txbNbExemplaires.Text = "0";
            resetCombobox(cbxInfoGenreLivres);
            resetCombobox(cbxInfoPublicLivre);
            resetCombobox(cbxInfoRayonLivre);
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    AfficheLivresInfos(GetSelectedLivre());
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(controle.GetAllLivres());
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            controle.SortLivres(titreColonne);
            bdgLivresListe.ResetBindings(false);
        }

        /// <summary>
        /// Active la zone d'ajout de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjoutLivre_Click(object sender, EventArgs e)
        {
            VideLivresInfos();
            modeActuel = Mode.Ajout;
            ChangeModeOngletLivre(Mode.Ajout);
        }

        /// <summary>
        /// Active la zone de modification du livre sélectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifLivre_Click(object sender, EventArgs e)
        {
            modeActuel = Mode.Modification;
            ChangeModeOngletLivre(Mode.Modification);
        }

        /// <summary>
        /// Supprime la revue sélectionnée après confirmation de l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprLivre_Click(object sender, EventArgs e)
        {

            Livre livre = GetSelectedLivre();
            if (livre == null) return;
            if (controle.GetExemplairesDocument(livre.Id).Count == 0)
            {
                DialogResult choix = MessageBox.Show("Confirmer la suppression ?",
                    "Confirmation", MessageBoxButtons.YesNo);
                if (choix == DialogResult.Yes &&
                    controle.SupprimerLivre(livre))
                {
                    lesLivres.Remove(livre);
                    bdgLivresListe.ResetBindings(false);
                    refreshAccessibiliteLivres();

                }
            }
            else
            {
                MessageBox.Show("Impossible de supprimer un livre ayant des exemplaires répertoriés.", "Suppression impossible");
            }
        }

        /// <summary>
        /// Modifie l'accessibilité des objets graphiques en fonction du mode actuel.
        /// </summary>
        /// <param name="mode">Mode actuel.</param>
        private void ChangeModeOngletLivre(Mode mode)
        {
            bool readOnlyChamps = mode == Mode.Info;
            bool readOnlyIdentifiant = mode == Mode.Info || mode == Mode.Modification;

            txbLivresAuteur.ReadOnly = readOnlyChamps;
            txbLivresCollection.ReadOnly = readOnlyChamps;
            txbLivresGenre.ReadOnly = readOnlyChamps;
            txbLivresIsbn.ReadOnly = readOnlyChamps;
            txbLivresTitre.ReadOnly = readOnlyChamps;
            dgvLivresListe.Enabled = readOnlyChamps;
            txbLivresNumero.ReadOnly = readOnlyIdentifiant;


            txbLivresGenre.Visible = readOnlyChamps;
            txbLivresPublic.Visible = readOnlyChamps;
            txbLivresRayon.Visible = readOnlyChamps;

            cbxInfoGenreLivres.Visible = !readOnlyChamps;
            cbxInfoPublicLivre.Visible = !readOnlyChamps;
            cbxInfoRayonLivre.Visible = !readOnlyChamps;

            btnValiderLivre.Visible = !readOnlyChamps;
            btnAnnulerLivre.Visible = !readOnlyChamps;
        }

        /// <summary>
        /// Valide la modification ou l'aout du livre saisi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValiderLivre_Click(object sender, EventArgs e)
        {
            bool ok = false;
            if (modeActuel == Mode.Ajout)
            {
                ok = ValiderAjoutLivre();
            }

            else if (modeActuel == Mode.Modification)
            {
                ok = ValiderModifLivre();
            }

            if (ok)
            {
                VideLivresInfos();
                modeActuel = Mode.Info;
                ChangeModeOngletLivre(Mode.Info);
                bdgLivresListe.ResetBindings(false);
                DgvLivresListe_SelectionChanged(null, null);
                refreshAccessibiliteLivres();
            }



        }

        /// <summary>
        /// Ajoute un nouveau livre avec les informations saisies.
        /// </summary>
        /// <returns></returns>
        private bool ValiderAjoutLivre()
        {
            if (txbLivresNumero.Text == "" || !controle.VerifieIdentifiantDocumentUnique(txbLivresNumero.Text))
            {
                MessageBox.Show("Veuillez renseigner un numéro de document unique.");
                return false;
            }
            if (!VerifieCompletionInfosLivre()) return false;

            Genre genre = (Genre)cbxInfoGenreLivres.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicLivre.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonLivre.SelectedItem;
            Livre livre = controle.AjouterLivre(txbLivresNumero.Text, txbLivresTitre.Text, txbLivresImage.Text, txbLivresIsbn.Text,
                txbLivresAuteur.Text, txbLivresCollection.Text, genre.Id, genre.Libelle, lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle);
            return livre != null;
        }

        /// <summary>
        /// Active la zone de modification de la revue sélectionnée.
        /// </summary>
        /// <returns>True si l'opération est un succès.</returns>
        private bool ValiderModifLivre()
        {
            if (controle.VerifieIdentifiantDocumentUnique(txbLivresNumero.Text))
            {
                MessageBox.Show($"L'entrée {txbLivresNumero.Text} n'existe pas dans la base de données.");
                return false;
            }
            if (!VerifieCompletionInfosLivre()) return false;
            Genre genre = (Genre)cbxInfoGenreLivres.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicLivre.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonLivre.SelectedItem;
            Livre livre = GetSelectedLivre();
            return controle.ModifierLivre(livre, txbLivresTitre.Text, txbLivresImage.Text, txbLivresIsbn.Text,
                txbLivresAuteur.Text, txbLivresCollection.Text, genre.Id, genre.Libelle, lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle);
        }

        /// <summary>
        /// Vérifie les champs obligatoires.
        /// </summary>
        /// <returns>True si tous les champs sont correctement remplis.</returns>
        private bool VerifieCompletionInfosLivre()
        {
            if (txbLivresTitre.Text == "" || txbLivresAuteur.Text == "")
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Annule la saisie en cours et réinitialise les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnulerLivre_Click(object sender, EventArgs e)
        {
            VideLivresInfos();
            modeActuel = Mode.Info;
            ChangeModeOngletLivre(Mode.Info);
            DgvLivresListe_SelectionChanged(null, null);
        }

        /// <summary>
        /// Ouvre la fenêtre des commandes de livres.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesLivre_Click(object sender, EventArgs e)
        {
            controle.OuvreFormulaireCommandes(GetSelectedLivre());
            bdgLivresListe.ResetBindings(false);
        }

        /// <summary>
        /// Ouvre le formulaire des exemplaires de livres.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExemplairesLivres_Click(object sender, EventArgs e)
        {
            Livre livre = GetSelectedLivre();
            controle.OuvreFormulaireExemplaires(livre);
            bdgLivresListe.ResetBindings(false);
        }

        private void btnRechercheImageLivre_Click(object sender, EventArgs e)
        {
            string img = selectionneImageLocale();
            afficheImage(img, pcbLivresImage, txbLivresImage);
        }

        /// <summary>
        /// Rend accessible ou non les boutons de l'onglet Livre en fonction du contenu de la liste des livres.
        /// </summary>
        private void refreshAccessibiliteLivres()
        {
            bool enable = dgvLivresListe.Rows.Count != 0;
            if (!controle.PeutModifier()) enable = false;
            btnSupprLivre.Enabled = enable;
            btnModifLivre.Enabled = enable;
            btnCommandesLivre.Enabled = enable;
            btnExemplairesLivres.Enabled = enable;
        }
    }
}
