using System.Collections.Generic;
using Mediatek86.modele;
using Mediatek86.metier;
using Mediatek86.vue;
using System;

namespace Mediatek86.controleur
{
    internal class Controle
    {
        private readonly List<Livre> lesLivres;
        private readonly List<Dvd> lesDvd;
        private readonly List<Revue> lesRevues;
        private readonly List<Categorie> lesRayons;
        private readonly List<Categorie> lesPublics;
        private readonly List<Categorie> lesGenres;

        /// <summary>
        /// Ouverture de la fenêtre
        /// </summary>
        public Controle()
        {
            lesLivres = Dao.GetAllLivres();
            lesDvd = Dao.GetAllDvd();
            lesRevues = Dao.GetAllRevues();
            lesGenres = Dao.GetAllGenres();
            lesRayons = Dao.GetAllRayons();
            lesPublics = Dao.GetAllPublics();
            FrmMediatek frmMediatek = new FrmMediatek(this);
            frmMediatek.ShowDialog();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Collection d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return lesGenres;
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Collection d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return lesLivres;
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Collection d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return lesDvd;
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Collection d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return lesRevues;
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Collection d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return lesRayons;
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Collection d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return lesPublics;
        }

        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <returns>Collection d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return Dao.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return Dao.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// Vérifie si un identifiant est unique.
        /// </summary>
        /// <param name="identifiant">Identifiant à vérifier</param>
        /// <returns>true si l'identifiant est unique (nouveau), false s'il existe déjà dans la BDD.</returns>
        public bool verifieIdentifiantUnique(string identifiant)
        {
            return Dao.verifieSiIdentifiantUnique(identifiant);
        }

        /// <summary>
        /// Ajoute un livre à la base de données.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="image"></param>
        /// <param name="isbn"></param>
        /// <param name="auteur"></param>
        /// <param name="collection"></param>
        /// <param name="idGenre"></param>
        /// <param name="genre"></param>
        /// <param name="idPublic"></param>
        /// <param name="lePublic"></param>
        /// <param name="idRayon"></param>
        /// <param name="rayon"></param>
        public Livre AjouterLivre(string id, string titre, string image, string isbn, string auteur, string collection,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
        {
            Livre livre = new Livre(id, titre, image, isbn, auteur, collection, idGenre, genre, idPublic, lePublic, idRayon, rayon);
            if (Dao.AjouterLivre(livre)) return livre;
            else return null;

        }

        public bool ModifierLivre(Livre livre, string titre, string image, string isbn, string auteur, string collection,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
        {
            livre.Titre = titre;
            livre.Image = image;
            livre.Isbn = isbn;
            livre.Auteur = auteur;
            livre.Collection = collection;
            livre.IdGenre = idGenre;
            livre.Genre = genre;
            livre.IdPublic = idPublic;
            livre.Public = lePublic;
            livre.IdRayon = idRayon;
            livre.Rayon = rayon;
            return Dao.ModifierLivre(livre);
        }

        public bool SupprimerLive(Livre livre)
        {
            return Dao.SupprimerLivre(livre);
        }

        public Categorie trouveCategorie(List<Categorie> categories, string idCategorie)
        {
            foreach(Categorie categorie in categories)
            {
                if (categorie.Id == idCategorie) return categorie;
            }
            return null;
        }


        /// <summary>
        /// Ajoute un DVD à la base de données.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="image"></param>
        /// <param name="duree"></param>
        /// <param name="realisateur"></param>
        /// <param name="synopsis"></param>
        /// <param name="idGenre"></param>
        /// <param name="genre"></param>
        /// <param name="idPublic"></param>
        /// <param name="lePublic"></param>
        /// <param name="idRayon"></param>
        /// <param name="rayon"></param>
        public Dvd AjouterDvd(string id, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
        {
            Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idGenre, genre, idPublic, lePublic, idRayon, rayon);
            if (Dao.AjouterDvd(dvd)) return dvd;
            else return null;
        }

        public bool ModifierDvd(Dvd dvd, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
        {
            dvd.Titre = titre;
            dvd.Image = image;
            dvd.Duree = duree;
            dvd.Realisateur = realisateur;
            dvd.Synopsis = synopsis;
            dvd.Genre = genre;
            dvd.IdGenre = idGenre;
            dvd.IdPublic = idPublic;
            dvd.Public = lePublic;
            dvd.Rayon = rayon;
            dvd.IdRayon = idRayon;
            return Dao.ModifierDvd(dvd);
        }

        public bool SupprimerDVD(Dvd dvd)
        {
            return Dao.SupprimerDvd(dvd);
        }

        /// <summary>
        /// Ajoute une revue à la base de données.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="image"></param>
        /// <param name="idGenre"></param>
        /// <param name="genre"></param>
        /// <param name="idPublic"></param>
        /// <param name="lePublic"></param>
        /// <param name="idRayon"></param>
        /// <param name="rayon"></param>
        /// <param name="empruntable"></param>
        /// <param name="periodicite"></param>
        /// <param name="delaiMiseADispo"></param>
        public void AjouterRevue(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon,
            bool empruntable, string periodicite, int delaiMiseADispo)
        {
            Revue revue = new Revue(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon, empruntable, periodicite, delaiMiseADispo);
            Dao.AjouterRevue(revue);
        }


    }

}

