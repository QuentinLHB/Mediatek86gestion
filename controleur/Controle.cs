using System.Collections.Generic;
using Mediatek86.modele;
using Mediatek86.metier;
using Mediatek86.vue;
using System;
using Serilog;
using System.Linq;

namespace Mediatek86.controleur
{
    public class Controle
    {
        private readonly List<Livre> lesLivres;
        private readonly List<Dvd> lesDvd;
        private readonly List<Revue> lesRevues;
        private readonly List<Categorie> lesRayons;
        private readonly List<Categorie> lesPublics;
        private readonly List<Categorie> lesGenres;
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        private Service service;

        /// <summary>
        /// Ouverture de la fenêtre
        /// </summary>
        public Controle()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            lesLivres = Dao.GetAllLivres();
            lesDvd = Dao.GetAllDvd();
            lesRevues = Dao.GetAllRevues();
            lesGenres = Dao.GetAllGenres();
            lesRayons = Dao.GetAllRayons();
            lesPublics = Dao.GetAllPublics();
            GetEtats();
            GetEtatsCommande();
            FrmConnexion frmConnexion = new FrmConnexion(this);
            frmConnexion.ShowDialog();
        }

        /// <summary>
        /// Tente de connecter l'utilisateur.
        /// </summary>
        /// <param name="login">Nom d'utilisateur</param>
        /// <param name="pwd">Mot de passe</param>
        /// <returns>True si les identifiants sont corrects, sinon false.</returns>
        public bool Connexion(string login, string pwd)
        {
            service = Dao.ControleAuthentification(login, pwd);
            return service != null;
        }
        
        public string GetUserLogin()
        {
            return service.Login;
        }

        /// <summary>
        /// Retourne true si l'utilisateur peut accéder à l'application en mode lecture.
        /// </summary>
        /// <returns></returns>
        public bool PeutLire()
        {
            if (service != null) return service.Lecture;
            else return false;
        }

        /// <summary>
        /// Retourne true si l'utilisateur peut modifier des éléments dans l'application.
        /// </summary>
        /// <returns></returns>
        public bool PeutModifier()
        {
            if (service != null) return service.Modification;
            else return false;
        }
        
        /// <summary>
        /// Ouvre le formulaire principal de l'application.
        /// </summary>
        public void OuvreFormulairePrincipal()
        {
            FrmMediatek frmMediatek = new FrmMediatek(this);
            frmMediatek.Size = new System.Drawing.Size(900, 800);
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
        /// Tri des livres selon un critère.
        /// </summary>
        /// <param name="critere">Critère de tri</param>
        public void SortLivres(string critere)
        {
            List<Livre> sortedList = new List<Livre>();
            switch (critere)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
                case "Exemplaires":
                    sortedList = lesLivres.OrderByDescending(o => o.NbExemplaires).ToList();
                    break;
            }

            lesLivres.Clear();
            lesLivres.AddRange(sortedList);
        }

        /// <summary>
        /// Tri des dvd selon un critère.
        /// </summary>
        /// <param name="critere">Critère de tri</param>
        public void SortDvd(string critere)
        {
            List<Dvd> sortedList = new List<Dvd>();
            switch (critere)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
                case "Exemplaires":
                    sortedList = lesDvd.OrderByDescending(o => o.NbExemplaires).ToList();
                    break;
            }

            lesDvd.Clear();
            lesDvd.AddRange(sortedList);
        }

        /// <summary>
        /// Tri des revues selon un critère.
        /// </summary>
        /// <param name="critere">Critère de tri</param>
        public void SortRevues(string critere)
        {
            List<Revue> sortedList = new List<Revue>();
            switch (critere)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
                case "Exemplaires":
                    sortedList = lesRevues.OrderByDescending(o => o.NbExemplaires).ToList();
                    break;
            }
            lesRevues.Clear();
            lesRevues.AddRange(sortedList);
        }



        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <returns>Collection d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesDocument(string idDocuement)
        {
            return Dao.GetExemplairesDocument(idDocuement);
        }

        /// <summary>
        /// Tri des exemplaires sur un critère.
        /// </summary>
        /// <param name="critere">Critère de tri.</param>
        public void SortExemplaires(string critere)
        {
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (critere)
            {
                case "Date d'achat":
                    sortedList = lesExemplaires.OrderByDescending(o => o.DateAchat).ToList();
                    break;
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).ToList();
                    break;
                case "Etat":
                    sortedList = lesExemplaires.OrderBy(o => o.Etat.Libelle).ToList();
                    break;
            }

            lesExemplaires.Clear();
            lesExemplaires.AddRange(sortedList);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            if (Dao.CreerExemplaire(exemplaire))
            {
                lesRevues.Clear();
                lesRevues.AddRange(Dao.GetAllRevues());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Vérifie si un identifiant est unique.
        /// </summary>
        /// <param name="identifiant">Identifiant à vérifier</param>
        /// <returns>true si l'identifiant est unique (nouveau), false s'il existe déjà dans la BDD.</returns>
        public bool VerifieIdentifiantDocumentUnique(string identifiant)
        {
            return Dao.VerifieSiIdDocumentUnique(identifiant);
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
            Livre livre = new Livre(id, titre, image, isbn, auteur, collection, idGenre, genre, idPublic, lePublic, idRayon, rayon, 0);
            if (Dao.AjouterLivre(livre))
            {
                lesLivres.Add(livre);
                return livre;
            }
            else return null;

        }

        /// <summary>
        /// Modifie un livre.
        /// </summary>
        /// <param name="livre">Livre à modifier</param>
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
        /// <returns>True si l'opération est un succès.</returns>
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

        /// <summary>
        /// Supprime un livre.
        /// </summary>
        /// <param name="livre">Livre à supprimer.</param>
        /// <returns>True si l'opération est un succès.</returns>
        public bool SupprimerLivre(Livre livre)
        {
            if (Dao.SupprimerLivre(livre))
            {
                lesLivres.Remove(livre);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Trouve une catégorie parmi une liste de catégories par rapport à son identifiant.
        /// </summary>
        /// <param name="categories">Liste de catégories.</param>
        /// <param name="idCategorie">Identifiant à chercher.</param>
        /// <returns></returns>
        public Categorie TrouveCategorie(List<Categorie> categories, string idCategorie)
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
            Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idGenre, genre, idPublic, lePublic, idRayon, rayon, 0);
            if (Dao.AjouterDvd(dvd))
            {
                lesDvd.Add(dvd);
                return dvd;
            }
            else return null;
        }

        /// <summary>
        /// Modifie un dvd.
        /// </summary>
        /// <param name="dvd">DVD à modifier.</param>
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
        /// <returns>True si l'opération est un succès.</returns>
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

        /// <summary>
        /// Supprime un DVD.
        /// </summary>
        /// <param name="dvd">DVD à supprimer.</param>
        /// <returns>True si l'opération est un succès.</returns>
        public bool SupprimerDVD(Dvd dvd)
        {
            if (Dao.SupprimerDvd(dvd)){
                lesDvd.Remove(dvd);
                return true;
            }
            return false;
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
        public Revue AjouterRevue(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon,
            bool empruntable, string periodicite, int delaiMiseADispo)
        {
            Revue revue = new Revue(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon, empruntable, periodicite, delaiMiseADispo, 0);
            if (Dao.AjouterRevue(revue))
            {
                lesRevues.Add(revue);
                return revue;
            }
            else return null;
        }

        /// <summary>
        /// Modifie une revue.
        /// </summary>
        /// <param name="revue">Revue à modifier.</param>
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
        /// <returns>True si l'opération est un succès.</returns>
        public bool ModifierRevue(Revue revue, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon,
            bool empruntable, string periodicite, int delaiMiseADispo)
        {
            revue.Titre = titre;
            revue.Image = image;
            revue.IdGenre = idGenre;
            revue.Genre = genre;
            revue.IdPublic = idPublic;
            revue.Public = lePublic;
            revue.IdRayon = idRayon;
            revue.Rayon = rayon;
            revue.Empruntable = empruntable;
            revue.Periodicite = periodicite;
            revue.DelaiMiseADispo = delaiMiseADispo;
            return Dao.ModifierRevue(revue);
        }

        /// <summary>
        /// Supprime une revue.
        /// </summary>
        /// <param name="revue">Revue à supprimer.</param>
        /// <returns>True si l'opération est un succès.</returns>
        public bool SupprimerRevue(Revue revue)
        {
            if( Dao.SupprimerRevue(revue)){
                lesRevues.Remove(revue);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Ouvre le formulaire de commandes.
        /// </summary>
        /// <param name="document"></param>
        public void OuvreFormulaireCommandes(Document document)
        {
            FrmCommandes frmCommandes = new FrmCommandes(this, document);
            frmCommandes.ShowDialog();
        }

        /// <summary>
        /// Récupère les états de commande (suivi)
        /// </summary>
        /// <returns>Liste des états de commande.</returns>
        public List<EtatCommande> GetEtatsCommande()
        {
            if (EtatCommande.Etats == null)
            {
                EtatCommande.Etats = Dao.GetEtatsCommande();
            }
            return EtatCommande.Etats;
        }

        /// <summary>
        /// Récupère les états physiques des livres.
        /// </summary>
        /// <returns>Liste des etats.</returns>
        public List<Etat> GetEtats()
        {
            if (Etat.Etats == null)
            {
                Etat.Etats = Dao.GetEtats();
            }
            return Etat.Etats;
        }

        private List<CommandeDocument> lesCommandes;
        private List<Abonnement> lesAbonnements;

        /// <summary>
        /// Récupère les commandes de DVD.
        /// </summary>
        /// <returns></returns>
        public List<CommandeDocument> GetCommandesDvd()
        {
            lesCommandes = Dao.GetCommandesDvd();
            return lesCommandes;
        }

        /// <summary>
        /// Récupère les commades de livres.
        /// </summary>
        /// <returns></returns>
        public List<CommandeDocument> GetCommandesLivres()
        {
            lesCommandes=Dao.GetCommandesLivres();
            return lesCommandes; 
        }

        /// <summary>
        /// Récupère les abonnements aux revues.
        /// </summary>
        /// <returns></returns>
        public List<Abonnement> GetAbonnementsRevues()
        {
            lesAbonnements = Dao.GetAbonnementsRevues();
            return lesAbonnements;
        }

        /// <summary>
        /// Vérifie si un identifiant de commande est unique dans la base de données.
        /// </summary>
        /// <param name="idCommande">Identifiant à vérifier</param>
        /// <returns>True s'il est unique (il n'en existe pas encore dans la bdd), false si une ligne possède l'identifiant.</returns>
        public bool VerifieSiIdentifiantCommandeUnique(string idCommande)
        {
            return Dao.VerifieSiIdCommandeUnique(idCommande);
        }

        /// <summary>
        /// Ajoute une commande de document (livre ou dvd).
        /// </summary>
        /// <param name="idCommande"></param>
        /// <param name="montant"></param>
        /// <param name="nbExemplaire"></param>
        /// <param name="idLivreDvd"></param>
        /// <param name="titre"></param>
        /// <returns>True si l'opération est un succès.</returns>
        public bool AjouterCommandeDocument(string idCommande, double montant, int nbExemplaire, string idLivreDvd, string titre)
        {
            CommandeDocument commande = new CommandeDocument(idCommande, DateTime.Today, montant, nbExemplaire, idLivreDvd, titre, EtatCommande.FindEtat(1));
            bool succes = Dao.AjouterCommandeDocument(commande);
            if (succes) lesCommandes.Add(commande);
            return succes;
        }

        /// <summary>
        /// Ajoute un abonnement de revue.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idRevue"></param>
        /// <param name="titre"></param>
        /// <param name="dateFin"></param>
        /// <param name="montant"></param>
        /// <returns>True si l'opération est un succès.</returns>
        public bool AjouterAbonnement(string id, string idRevue, string titre, DateTime dateFin, double montant)
        {
            Abonnement abonnement = new Abonnement(id, idRevue, titre, DateTime.Today, dateFin, montant);
            bool succes = Dao.AjouterAbonnementRevue(abonnement);
            if (succes) lesAbonnements.Add(abonnement);
            return succes;
        }

        /// <summary>
        /// Met à jour l'état d'une commande de document (livre ou dvd).
        /// </summary>
        /// <param name="commande">Commande concernée.</param>
        /// <param name="etat">Nouvel état.</param>
        /// <returns>True si l'opération est un succès.</returns>
        public bool MettreAJourCommandeDocument(CommandeDocument commande, EtatCommande etat)
        {
            if (commande.Etat == etat) return true;
            bool succes = Dao.UpdateCommandeDocument(commande, etat);
            if (succes)
            {
                commande.Etat = etat;
            }
            if (etat == EtatCommande.FindEtat(2)) //Livrée: Refresh car de nouvelles lignes ont été insérées via un trigger
            {
                lesLivres.Clear();
                lesLivres.AddRange(Dao.GetAllLivres());
                lesDvd.Clear();
                lesDvd.AddRange(Dao.GetAllDvd());
            }
            return succes;
        }

        /// <summary>
        /// Supprime une commande de livre ou de dvd.
        /// </summary>
        /// <param name="commande">Commande à supprimer.</param>
        /// <returns>True si l'opération est un succès.</returns>
        public bool SupprCommandeDocument(CommandeDocument commande)
        {
            bool succes = Dao.SupprCommandeDocument(commande);
            if (succes) lesCommandes.Remove(commande);
            return succes;
        }

        /// <summary>
        /// Supprime un abonnement à une revue.
        /// </summary>
        /// <param name="abonnement">Abonnement à supprimer.</param>
        /// <returns>True si l'opération est un succès.</returns>
        public bool SupprAbonnementRevue(Abonnement abonnement)
        {
            bool succes = Dao.SupprAbonnementRevue(abonnement);
            if (succes) lesAbonnements.Remove(abonnement);
            return succes;
        }

        /// <summary>
        /// Ouvre le formulaire des exemplaires pour un document spécifié.
        /// </summary>
        /// <param name="document">Document dont on consulte les exemplaires.</param>
        public void OuvreFormulaireExemplaires(Document document)
        {
            GetEtats();
            lesExemplaires = GetExemplairesDocument(document.Id);
            FrmExemplaires frmExemplaires = new FrmExemplaires(document, this);
            frmExemplaires.ShowDialog();

        }

        /// <summary>
        /// Retourne les exemplaires du document ouvert avec OuvreFormulaireExemplaires()
        /// </summary>
        /// <returns>Liste des exemplaires</returns>
        public List<Exemplaire> GetExemplaires()
        {
            return lesExemplaires;
        }

        /// <summary>
        /// Modification de l'état physique d'un exemplaire.
        /// </summary>
        /// <param name="exemplaire">Exemplaire à modifier.</param>
        /// <param name="etat">Nouvel état de l'exemplaire.</param>
        /// <returns></returns>
        public bool ModifierExemplaire(Exemplaire exemplaire, Etat etat)
        {
            bool succes = Dao.ModifierExemplaire(exemplaire, etat);
            if (succes)exemplaire.Etat = etat;
            return succes;
        }

        /// <summary>
        /// Supprime un exemplaire
        /// </summary>
        /// <param name="document">Document dont on supprime un exemplaire.</param>
        /// <param name="exemplaire">Exemplaire à supprimer.</param>
        /// <returns></returns>
        public bool SupprimerExemplaire(Document document, Exemplaire exemplaire)
        {
            bool succes = Dao.SupprimerExemplaire(exemplaire);
            if (succes)
            {
                lesExemplaires.Remove(exemplaire);
                document.NbExemplaires -= 1;
            }
            return succes;
        }
    }

}

