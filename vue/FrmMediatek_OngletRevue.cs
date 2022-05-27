using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mediatek86.metier;

namespace Mediatek86.vue
{
    public partial class FrmMediatek : Form
    {

        /// <summary>
        /// Retourne la revue sélectionnée dans le tableau.
        /// </summary>
        /// <returns></returns>
        private Revue GetSelectedRevue()
        {
            if (bdgRevuesListe.Count != 0)
            {
                return (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxRevuesGenres, false);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxRevuesPublics, false);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxRevuesRayons, false);

            // Combos du bloc information, dans le scénario d'un ajout.
            RemplirComboCategorie(controle.GetAllGenres(), new BindingSource(), cbxInfoGenreRevue, true);
            RemplirComboCategorie(controle.GetAllPublics(), new BindingSource(), cbxInfoPublicRevue, true);
            RemplirComboCategorie(controle.GetAllRayons(), new BindingSource(), cbxInfoRayonRevue, true);

            RemplirRevuesListeComplete();
            refreshAccessibiliteRevues();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            lesRevues = revues;
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["empruntable"].Visible = false;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
            dgvRevuesListe.Columns["nbExemplaires"].HeaderText = "Exemplaires";
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>
                    {
                        revue
                    };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue"></param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            chkRevuesEmpruntable.Checked = revue.Empruntable;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            txbNbExemplairesRevue.Text = revue.NbExemplaires.ToString();
            btnExemplairesRevue.Enabled = revue.NbExemplaires > 0;
            string image = revue.Image;
            afficheImage(image, pcbRevuesImage, txbRevuesImage);
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            chkRevuesEmpruntable.Checked = false;
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            txbNbExemplairesRevue.Text = "0";
            pcbRevuesImage.Image = null;
            resetCombobox(cbxInfoRayonRevue);
            resetCombobox(cbxInfoPublicRevue);
            resetCombobox(cbxInfoRayonRevue);
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = GetSelectedRevue();
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(controle.GetAllRevues());
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Bloque les caractères alphabétiques lors de la saisie d'une touche.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesDateMiseADispo_KeyPress(object sender, KeyPressEventArgs e)
        {
            BloqueChracteresAlpha(e);
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            controle.SortRevues(titreColonne);
            bdgRevuesListe.ResetBindings(false);
        }

        /// <summary>
        /// Active la zone d'ajout de revue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjoutRevue_Click(object sender, EventArgs e)
        {
            VideRevuesInfos();
            modeActuel = Mode.Ajout;
            ChangeModeOngletRevue(Mode.Ajout);
        }

        /// <summary>
        /// Active la zone de modification de la revue sélectionnée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifRevue_Click(object sender, EventArgs e)
        {
            modeActuel = Mode.Modification;
            ChangeModeOngletRevue(Mode.Modification);
        }

        /// <summary>
        /// Supprime la revue sélectionnée après confirmation de l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprRevue_Click(object sender, EventArgs e)
        {
            Revue revue = GetSelectedRevue();
            if (controle.GetExemplairesDocument(revue.Id).Count == 0)
            {
                DialogResult choix = MessageBox.Show("Confirmer la suppression ?",
              "Confirmation", MessageBoxButtons.YesNo);
                if (choix == DialogResult.Yes && controle.SupprimerRevue(revue))
                {
                    lesRevues.Remove(revue);
                    bdgRevuesListe.ResetBindings(false);
                    refreshAccessibiliteRevues();
                }
            }
            else
            {
                MessageBox.Show("Impossible de supprimer une revue ayant des exemplaires répertoriés.", "Suppression impossible");
            }


        }

        /// <summary>
        ///  Valide la modification ou l'aout de la revue saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValiderRevue_Click(object sender, EventArgs e)
        {
            bool ok = false;
            if (modeActuel == Mode.Ajout)
            {
                ok = ValiderAjoutRevue();
            }

            else if (modeActuel == Mode.Modification)
            {
                ok = ValiderModifRevue();
            }

            if (ok)
            {
                VideRevuesInfos();
                modeActuel = Mode.Info;
                ChangeModeOngletRevue(Mode.Info);
                bdgRevuesListe.ResetBindings(false);
                dgvRevuesListe_SelectionChanged(null, null);
                refreshAccessibiliteRevues();
            }
        }


        /// <summary>
        /// Ajoute une nouvelle revue avec les informations saisies.
        /// </summary>
        /// <returns>True si l'opération est un succès.</returns>
        private bool ValiderAjoutRevue()
        {
            if (txbRevuesNumero.Text == "" || !controle.VerifieIdentifiantDocumentUnique(txbRevuesNumero.Text))
            {
                MessageBox.Show("Veuillez renseigner un numéro de document unique.");
                return false;
            }
            if (!VerifieCompletionInfosRevue()) return false;

            Genre genre = (Genre)cbxInfoGenreRevue.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicRevue.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonRevue.SelectedItem;
            Revue revue = controle.AjouterRevue(txbRevuesNumero.Text, txbRevuesTitre.Text, txbRevuesImage.Text, genre.Id, genre.Libelle,
                lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle, chkRevuesEmpruntable.Checked, txbRevuesPeriodicite.Text, int.Parse(txbRevuesDateMiseADispo.Text));
            return revue != null;
        }

        /// <summary>
        /// Vérifie les champs obligatoires.
        /// </summary>
        /// <returns>True si tous les champs sont correctement remplis.</returns>
        private bool VerifieCompletionInfosRevue()
        {
            if (txbRevuesTitre.Text == "")
            {
                MessageBox.Show("Veuillez renseigner les champs obligatoires.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Modifie la revue modifiée avec les informations saisies.
        /// </summary>
        /// <returns></returns>
        private bool ValiderModifRevue()
        {
            if (controle.VerifieIdentifiantDocumentUnique(txbLivresNumero.Text))
            {
                MessageBox.Show($"L'entrée {txbLivresNumero.Text} n'existe pas dans la base de données.");
                return false;
            }
            if (!VerifieCompletionInfosRevue()) return false;
            Genre genre = (Genre)cbxInfoGenreRevue.SelectedItem;
            Public lePublic = (Public)cbxInfoPublicRevue.SelectedItem;
            Rayon rayon = (Rayon)cbxInfoRayonRevue.SelectedItem;
            Revue revue = GetSelectedRevue();
            return controle.ModifierRevue(revue, txbRevuesTitre.Text, txbRevuesImage.Text, genre.Id, genre.Libelle,
                lePublic.Id, lePublic.Libelle, rayon.Id, rayon.Libelle, chkRevuesEmpruntable.Checked, txbRevuesPeriodicite.Text, int.Parse(txbRevuesDateMiseADispo.Text));
        }

        /// <summary>
        /// Annule la saisie en cours et vide les champs associés.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnulerRevue_Click(object sender, EventArgs e)
        {
            VideRevuesInfos();
            modeActuel = Mode.Info;
            ChangeModeOngletRevue(Mode.Info);
            dgvRevuesListe_SelectionChanged(null, null);
        }

        /// <summary>
        /// Modifie l'accessibilité des objets graphiques en fonction du mode actuel.
        /// </summary>
        /// <param name="mode"></param>
        private void ChangeModeOngletRevue(Mode mode)
        {
            bool readOnlyChamps = mode == Mode.Info;
            bool readOnlyIdentifiant = mode == Mode.Info || mode == Mode.Modification;

            txbRevuesGenre.ReadOnly = readOnlyChamps;
            txbRevuesDateMiseADispo.ReadOnly = readOnlyChamps;
            txbRevuesPeriodicite.ReadOnly = readOnlyChamps;
            txbRevuesTitre.ReadOnly = readOnlyChamps;
            dgvRevuesListe.Enabled = readOnlyChamps;
            chkRevuesEmpruntable.Enabled = !readOnlyChamps;
            txbRevuesNumero.ReadOnly = readOnlyIdentifiant;


            txbRevuesGenre.Visible = readOnlyChamps;
            txbRevuesPublic.Visible = readOnlyChamps;
            txbRevuesRayon.Visible = readOnlyChamps;

            cbxInfoGenreRevue.Visible = !readOnlyChamps;
            cbxInfoPublicRevue.Visible = !readOnlyChamps;
            cbxInfoRayonRevue.Visible = !readOnlyChamps;

            btnValiderRevue.Visible = !readOnlyChamps;
            btnAnnulerRevue.Visible = !readOnlyChamps;
        }

        /// <summary>
        /// Ouvre la fenêtre de gestion des commandes de revues.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeRevues_Click(object sender, EventArgs e)
        {
            controle.OuvreFormulaireCommandes(GetSelectedRevue());
            bdgRevuesListe.ResetBindings(false);
        }

        private void btnExemplairesRevue_Click(object sender, EventArgs e)
        {
            Revue revue = GetSelectedRevue();
            controle.OuvreFormulaireExemplaires(revue);
            bdgRevuesListe.ResetBindings(false);
        }

        private void btnRechercheImageRevue_Click(object sender, EventArgs e)
        {
            string img = selectionneImageLocale();
            afficheImage(img, pcbRevuesImage, txbRevuesImage);
        }

        /// <summary>
        /// Rend accessible ou non les boutons de l'onglet Revue en fonction du contenu de la liste des revues.
        /// </summary>
        private void refreshAccessibiliteRevues()
        {
            bool enable = dgvRevuesListe.Rows.Count != 0;
            if (!controle.PeutModifier()) enable = false;
            btnSupprRevue.Enabled = enable;
            btnModifRevue.Enabled = enable;
            btnCommandeRevues.Enabled = enable;
            btnExemplairesRevue.Enabled = enable;
        }
    }
}
