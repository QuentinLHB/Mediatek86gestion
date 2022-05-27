using Mediatek86.metier;
using System.Collections.Generic;
using Mediatek86.bdd;
using System;
using System.Windows.Forms;
using Serilog;

namespace Mediatek86.modele
{
    public static class Dao
    {

        private static readonly string server = "localhost";
        private static readonly string userid = "root";
        private static readonly string password = "";
        private static readonly string database = "mediatek86";
        private static readonly string connectionString = "server=" + server + ";user id=" + userid + ";password=" + password + ";database=" + database + ";SslMode=none";


        public static Service ControleAuthentification(string login, string pwd)
        {
            string req = "select * from utilisateur ";
            req += "where login=@login and pwd=SHA2(@pwd, 256)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@login", login },
                { "@pwd", pwd }
            };
            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);
            if (curs.Read())
            {
                Service service = new Service((int)curs.Field("IDSERVICE"), (string)curs.Field("login"));
                Log.Information("L'utilisateur {0} s'est connecté. Lecture: {1} ; Modif: {2}.", service.Login, service.Lecture, service.Modification);
                curs.Close();
                return service;
            }
            else
            {
                Log.Information("Echec de la connexion pour le nom d'utilisateur {0}", login);
                curs.Close();
                return null;
            }
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public static List<Categorie> GetAllGenres()
        {
            List<Categorie> lesGenres = new List<Categorie>();
            string req = "Select * from genre order by libelle";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                Genre genre = new Genre((string)curs.Field("id"), (string)curs.Field("libelle"));
                lesGenres.Add(genre);
            }
            curs.Close();
            return lesGenres;
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Collection d'objets Rayon</returns>
        public static List<Categorie> GetAllRayons()
        {
            List<Categorie> lesRayons = new List<Categorie>();
            string req = "Select * from rayon order by libelle";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                Rayon rayon = new Rayon((string)curs.Field("id"), (string)curs.Field("libelle"));
                lesRayons.Add(rayon);
            }
            curs.Close();
            return lesRayons;
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Collection d'objets Public</returns>
        public static List<Categorie> GetAllPublics()
        {
            List<Categorie> lesPublics = new List<Categorie>();
            string req = "Select * from public order by libelle";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                Public lePublic = new Public((string)curs.Field("id"), (string)curs.Field("libelle"));
                lesPublics.Add(lePublic);
            }
            curs.Close();
            return lesPublics;
        }

        /// <summary>
        /// Retourne toutes les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public static List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = new List<Livre>();
            string req = "Select l.id, l.ISBN, l.auteur, d.titre, d.image, l.collection, ";
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon, ";
            req += "COUNT(e.id) as nbExemplaires ";
            req += "from livre l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
            req += "LEFT JOIN exemplaire e ON (d.id = e.id) ";
            req += "GROUP by d.id ";
            req += "order by titre ";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                string id = (string)curs.Field("id");
                string isbn = (string)curs.Field("ISBN");
                string auteur = (string)curs.Field("auteur");
                string titre = (string)curs.Field("titre");
                string image = (string)curs.Field("image");
                string collection = (string)curs.Field("collection");
                string idgenre = (string)curs.Field("idgenre");
                string idrayon = (string)curs.Field("idrayon");
                string idpublic = (string)curs.Field("idpublic");
                string genre = (string)curs.Field("genre");
                string lepublic = (string)curs.Field("public");
                string rayon = (string)curs.Field("rayon");
                long nbExemplaires = (long)curs.Field("nbExemplaires");
                Livre livre = new Livre(id, titre, image, isbn, auteur, collection, idgenre, genre,
                    idpublic, lepublic, idrayon, rayon, nbExemplaires);
                lesLivres.Add(livre);
            }
            curs.Close();

            return lesLivres;
        }

        /// <summary>
        /// Retourne toutes les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public static List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = new List<Dvd>();
            string req = "Select l.id, l.duree, l.realisateur, d.titre, d.image, l.synopsis, ";
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon, ";
            req += "COUNT(e.id) as nbExemplaires ";
            req += "from dvd l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
            req += "LEFT JOIN exemplaire e ON (d.id = e.id) ";
            req += "GROUP by d.id ";
            req += "order by titre ";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                string id = (string)curs.Field("id");
                int duree = (int)curs.Field("duree");
                string realisateur = (string)curs.Field("realisateur");
                string titre = (string)curs.Field("titre");
                string image = (string)curs.Field("image");
                string synopsis = (string)curs.Field("synopsis");
                string idgenre = (string)curs.Field("idgenre");
                string idrayon = (string)curs.Field("idrayon");
                string idpublic = (string)curs.Field("idpublic");
                string genre = (string)curs.Field("genre");
                string lepublic = (string)curs.Field("public");
                string rayon = (string)curs.Field("rayon");
                long nbExemplaires = (long)curs.Field("nbExemplaires");
                Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idgenre, genre,
                    idpublic, lepublic, idrayon, rayon, nbExemplaires);
                lesDvd.Add(dvd);
            }
            curs.Close();

            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public static List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = new List<Revue>();
            string req = "Select l.id, l.empruntable, l.periodicite, d.titre, d.image, l.delaiMiseADispo, ";
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon, ";
            req += "COUNT(e.id) as nbExemplaires ";
            req += "from revue l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
            req += "LEFT JOIN exemplaire e ON (d.id = e.id) ";
            req += "GROUP by d.id ";
            req += "order by titre ";

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, null);

            while (curs.Read())
            {
                string id = (string)curs.Field("id");
                bool empruntable = (bool)curs.Field("empruntable");
                string periodicite = (string)curs.Field("periodicite");
                string titre = (string)curs.Field("titre");
                string image = (string)curs.Field("image");
                int delaiMiseADispo = (int)curs.Field("delaimiseadispo");
                string idgenre = (string)curs.Field("idgenre");
                string idrayon = (string)curs.Field("idrayon");
                string idpublic = (string)curs.Field("idpublic");
                string genre = (string)curs.Field("genre");
                string lepublic = (string)curs.Field("public");
                string rayon = (string)curs.Field("rayon");
                long nbExemplaires = (long)curs.Field("nbExemplaires");
                Revue revue = new Revue(id, titre, image, idgenre, genre,
                    idpublic, lepublic, idrayon, rayon, empruntable, periodicite, delaiMiseADispo, nbExemplaires);
                lesRevues.Add(revue);
            }
            curs.Close();

            return lesRevues;
        }

        /// <summary>
        /// Retourne les exemplaires d'un document
        /// </summary>
        /// <returns>Liste d'objets Exemplaire</returns>
        public static List<Exemplaire> GetExemplairesDocument(string idDocument)
        {
            List<Exemplaire> lesExemplaires = new List<Exemplaire>();
            string req = "SELECT e.id, e.numero, e.dateAchat, e.photo, e.idEtat ";
            req += "FROM exemplaire e JOIN document d ON e.id=d.id ";
            req += "WHERE e.id = @id ";
            req += "ORDER BY e.dateAchat DESC";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", idDocument}
                };

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);

            while (curs.Read())
            {
                Exemplaire exemplaire = new Exemplaire(
                    (int)curs.Field("numero"), 
                    (DateTime)curs.Field("dateAchat"),
                    (string)curs.Field("photo"), 
                    Etat.FindEtat((string)curs.Field("idEtat")), 
                    (string)curs.Field("id")
                    );
                lesExemplaires.Add(exemplaire);
            }
            curs.Close();

            return lesExemplaires;
        }

        /// <summary>
        /// ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire"></param>
        /// <returns>true si l'insertion a pu se faire</returns>
        public static bool CreerExemplaire(Exemplaire exemplaire)
        {
            try
            {
                string req = "insert into exemplaire values (@idDocument,@numero,@dateAchat,@photo,@idEtat)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idDocument", exemplaire.IdDocument},
                    { "@numero", exemplaire.Numero},
                    { "@dateAchat", exemplaire.DateAchat},
                    { "@photo", exemplaire.Photo},
                    { "@idEtat",exemplaire.Etat.Id}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Ajout d'un exemplaire à la BDD: N°{0} du document n°{1}.", exemplaire.Numero, exemplaire.IdDocument);
                curs.Close();
                return true;
            }
            catch(Exception e)
            {
                Log.Information(e, "Echec lors de l'ajout d'un exemplaire à la BDD");
                return false;
            }
        }

        /// <summary>
        /// Ajoute une nouvelle ligne dans la table document
        /// </summary>
        /// <param name="document">Document à ajouter.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        private static bool AjouterDocument(Document document)
        {
            try
            {
                string req = "insert into document values (@id, @titre, @image, @idrayon, @idpublic, @idgenre)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", document.Id},
                    {"@titre", document.Titre },
                    { "@image", document.Image},
                    { "@idrayon", document.IdRayon},
                    { "@idpublic", document.IdPublic},
                    { "@idgenre",document.IdGenre}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Ajout d'un document {0}", document.Id);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de l'ajout d'un document à la BDD");
                return false;
            }
        }

        /// <summary>
        /// Ajoute une nouvelle ligne dans la table LivreDvd
        /// </summary>
        ///  /// <param name="livreDvd">Livre ou DVD à ajouter.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        private static bool AjouterLivreDvd(LivreDvd livreDvd)
        {
            try
            {
                string req = "insert into livres_dvd values (@id)";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", livreDvd.Id}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Modification du LivreDvd {0}", livreDvd.Id);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de l'ajout d'un LivreDvd à la BDD");
                return false;
            }
        }

        /// <summary>
        /// Ajoute une nouvelle ligne dans la table livre
        /// </summary>
        /// <param name="livre">Livre à ajouter.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool AjouterLivre(Livre livre)
        {

            if (AjouterDocument(livre) && AjouterLivreDvd(livre))
            {
                try
                {
                    string req = "insert into livre values (@id, @isbn, @auteur, @collection)";
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", livre.Id},
                    { "@isbn", livre.Isbn},
                    { "@auteur", livre.Auteur},
                    { "@collection", livre.Collection}
                };
                    BddMySql curs = BddMySql.GetInstance(connectionString);
                    curs.ReqUpdate(req, parameters);
                    Log.Information("Ajout du livre {0}", livre.Id);
                    curs.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Log.Information(e, "Echec lors de l'ajout d'un livre à la BDD");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update une ligne de la table Livre.
        /// </summary>
        /// <param name="livre">Livre à modifier.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool ModifierLivre(Livre livre)
        {
            try
            {
                string req = "UPDATE livre JOIN livres_dvd USING (id) JOIN document USING (id) ";
                req += "SET livre.auteur = @auteur, ";
                req += "livre.collection = @collection, ";
                req += "livre.ISBN = @isbn, ";
                req += "document.titre = @titre, ";
                req += "document.image = @image, ";
                req += "document.idRayon = @idrayon, ";
                req += "document.idPublic = @idpublic, ";
                req += "document.idGenre = @idgenre ";
                req += "WHERE livre.id = @id";


                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", livre.Id},
                    { "@auteur", livre.Auteur},
                    { "@collection", livre.Collection},
                    { "@isbn", livre.Isbn},
                    { "@titre", livre.Titre},
                    { "@image", livre.Image},
                    { "@idrayon", livre.IdRayon},
                    { "@idpublic", livre.IdPublic},
                    { "@idgenre", livre.IdGenre},
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Modification du livre {0}", livre.Id);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la modification d'un livre à la BDD");
                return false;
            }
        }

        /// <summary>
        /// Supprime une ligne de la table livre.
        /// </summary>
        /// <param name="livre">Livre à supprimer.</param>
        /// <returns></returns>
        public static bool SupprimerLivre(Livre livre)
        {
            return ReqDelete("livre", livre.Id) &&
            ReqDelete("livres_dvd", livre.Id) &&
            ReqDelete("document", livre.Id);
        }


        /// <summary>
        /// Exécute la requête 'DELETE FROM table WHERE id = id' où table et id sont envoyés en paramètre.
        /// </summary>
        /// <param name="table">Table à partir de laquelle supprimer.</param>
        /// <param name="id">Identifiant de l'élément à supprimer.</param>
        /// <returns></returns>
        private static bool ReqDelete(string table, string id)
        {
            try
            {
                string req = $"DELETE FROM {table} WHERE id = @id; ";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", id},

                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Suppression d'un(e) {0}", table);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la suppression d'un(e) {0} à la BDD", table);
                return false;
            }
        }

        /// <summary>
        /// Ajoute une ligne dans la table DVD.
        /// </summary>
        /// <param name="dvd">DVD à ajouter</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool AjouterDvd(Dvd dvd)
        {
            if (AjouterDocument(dvd) && AjouterLivreDvd(dvd))
            {
                try
                {
                    string req = "insert into dvd values (@id, @synopsis, @realisateur, @duree)";
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", dvd.Id},
                    { "@synopsis", dvd.Synopsis},
                    { "@realisateur", dvd.Realisateur},
                    { "@duree", dvd.Duree}
                };
                    BddMySql curs = BddMySql.GetInstance(connectionString);
                    curs.ReqUpdate(req, parameters);
                    Log.Information("Ajout du dvd {0}", dvd.Id);
                    curs.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Log.Information(e, "Echec lors de l'ajout d'un livre à la BDD");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Modifie une ligne de la table DVD.
        /// </summary>
        /// <param name="dvd">DVD à modifier.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool ModifierDvd(Dvd dvd)
        {
            try
            {
                string req = "UPDATE dvd JOIN livres_dvd USING (id) JOIN document USING (id) ";
                req += "SET dvd.synopsis = @synopsis, ";
                req += "dvd.realisateur = @realisateur, ";
                req += "dvd.duree = @duree, ";
                req += "document.titre = @titre, ";
                req += "document.image = @image, ";
                req += "document.idRayon = @idrayon, ";
                req += "document.idPublic = @idpublic, ";
                req += "document.idGenre = @idgenre ";
                req += "WHERE dvd.id = @id";


                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", dvd.Id},
                    { "@synopsis", dvd.Synopsis},
                    { "@realisateur", dvd.Realisateur},
                    { "@duree", dvd.Duree},
                    { "@titre", dvd.Titre},
                    { "@image", dvd.Image},
                    { "@idrayon", dvd.IdRayon},
                    { "@idpublic", dvd.IdPublic},
                    { "@idgenre", dvd.IdGenre},
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Modification du dvd {0}", dvd.Id);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la modification d'un dvd à la BDD");
                return false;
            }
        }

        /// <summary>
        /// Supprime une ligne de la table DVD.
        /// </summary>
        /// <param name="dvd">DVD à supprimer.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool SupprimerDvd(Dvd dvd)
        {
            return ReqDelete("dvd", dvd.Id) &&
                 ReqDelete("livres_dvd", dvd.Id) &&
                 ReqDelete("document", dvd.Id);
        }

        /// <summary>
        /// Ajoute une ligne dans la table revue.
        /// </summary>
        /// <param name="revue">Revue à ajouter.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool AjouterRevue(Revue revue)
        {
            if (AjouterDocument(revue))
            {
                try
                {
                    string req = "insert into revue values (@id, @empruntable, @periodicite, @delaiMiseADispo)";
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", revue.Id},
                    { "@empruntable", revue.Empruntable},
                    { "@periodicite", revue.Periodicite},
                    { "@delaiMiseADispo", revue.DelaiMiseADispo}
                };
                    BddMySql curs = BddMySql.GetInstance(connectionString);
                    curs.ReqUpdate(req, parameters);
                    Log.Information("Ajout de la revue {0}", revue.Id);
                    curs.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Log.Information(e, "Echec lors de l'ajout d'une revue à la BDD");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Modifie une ligne dans la table revue.
        /// </summary>
        /// <param name="revue">Revue à modifier.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool ModifierRevue(Revue revue)
        {
            try
            {
                string req = "UPDATE revue JOIN document USING (id) ";
                req += "set revue.empruntable = @empruntable, ";
                req += "revue.periodicite = @periodicite    , ";
                req += "revue.delaiMiseADispo = @delaiMiseADispo, ";
                req += "document.titre = @titre, ";
                req += "document.image = @image, ";
                req += "document.idRayon = @idrayon, ";
                req += "document.idPublic = @idpublic, ";
                req += "document.idGenre = @idgenre ";
                req += "WHERE dvd.id = @id";


                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", revue.Id},
                    { "@empruntable", revue.Empruntable},
                    { "@periodicite", revue.Periodicite},
                    { "@delaiMiseADispo", revue.DelaiMiseADispo},
                    { "@titre", revue.Titre},
                    { "@image", revue.Image},
                    { "@idrayon", revue.IdRayon},
                    { "@idpublic", revue.IdPublic},
                    { "@idgenre", revue.IdGenre},
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Modification de la revue {0}", revue.Id);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la modification d'une revue dans la BDD");
                return false;
            }
        }

        /// <summary>
        /// Supprime une ligne dans la table revue.
        /// </summary>
        /// <param name="revue">Revue à supprimer.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool SupprimerRevue(Revue revue)
        {
            return ReqDelete("revue", revue.Id) &&
              ReqDelete("document", revue.Id);
        }

        /// <summary>
        /// Récupère les états de commande.
        /// </summary>
        /// <returns>Liste des états de commande.</returns>
        public static List<EtatCommande> GetEtatsCommande()
        {
            List<EtatCommande> etatsCommande = null;
            try
            {
                etatsCommande = new List<EtatCommande>();
                string req = "SELECT * FROM etat_commande";
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqSelect(req, null);

                while (curs.Read())
                {
                    int id = (int)curs.Field("id");
                    string libelle = (string)curs.Field("libelle");
                    etatsCommande.Add(new EtatCommande(id, libelle));
                }
                curs.Close();
                return etatsCommande;

            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la récupération des Etats de Commande");
                return etatsCommande;
            }
        }

        /// <summary>
        /// Récupère les commandes de DVD.
        /// </summary>
        /// <returns>Liste des commandes de DVD.</returns>
        public static List<CommandeDocument> GetCommandesDvd()
        {
            return GetCommandesLivreDvd(TypeDocument.DVD);
        }

        /// <summary>
        /// Récupère les commandes de livrs.
        /// </summary>
        /// <returns>Liste des commandes de DVD.</returns>
        public static List<CommandeDocument> GetCommandesLivres()
        {
            return GetCommandesLivreDvd(TypeDocument.LIVRE);
        }

        /// <summary>
        /// Récupère les commandes de Livres ou de DVD.
        /// </summary>
        /// <param name="typeDocument">Type de document sur lequel filtrer.</param>
        /// <returns></returns>
        private static List<CommandeDocument> GetCommandesLivreDvd(TypeDocument typeDocument)
        {
            string jointure = "";
            if (typeDocument == TypeDocument.LIVRE) jointure = "JOIN livre on (ld.id = livre.id)";
            else if (typeDocument == TypeDocument.DVD) jointure = "JOIN dvd on (ld.id = dvd.id) ";

            List<CommandeDocument> lesCommandes = null;
            try
            {
                lesCommandes = new List<CommandeDocument>();
                string req = "SELECT cde.id as 'id_commande', cde.dateCommande, cdedoc.nbExemplaire, cde.montant, d.id as 'id_document', d.titre, e.id as 'id_etat', e.libelle as 'etat' ";
                req += "FROM commandedocument cdedoc join commande cde USING (id) JOIN etat_commande e ON (e.id = cdedoc.idEtatCommande) JOIN document d ON (cdedoc.idLivreDvd = d.id) JOIN livres_dvd ld ON (ld.id = cdedoc.idLivreDvd) ";
                req += jointure;
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqSelect(req, null);

                while (curs.Read())
                {
                    CommandeDocument commandeDocument = new CommandeDocument(
                        (string)curs.Field("id_commande"),
                        (DateTime)curs.Field("dateCommande"),
                        (double)curs.Field("montant"),
                         (int)curs.Field("nbExemplaire"),
                        (string)curs.Field("id_document"),
                         (string)curs.Field("titre"),
                        EtatCommande.FindEtat((int)curs.Field("id_etat"))
                        );
                    lesCommandes.Add(commandeDocument);
                }
                curs.Close();
                return lesCommandes;

            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la récupération des commandes de livre ou dvd dans la BDD");
                return lesCommandes;
            }
        }

        /// <summary>
        /// Récupèrel es abonnements à des revues.
        /// </summary>
        /// <returns>Liste des abonnements.</returns>
        public static List<Abonnement> GetAbonnementsRevues()
        {
            List<Abonnement> lesAbonnements = new List<Abonnement>();
            try
            {
                string req = "SELECT a.id, r.id as 'idRevue', d.titre, c.dateCommande, a.dateFinAbonnement, c.montant ";
                req += "FROM abonnement a JOIN commande c using (id) JOIN revue r ON (r.id = a.idRevue) JOIN document d on (r.id = d.id)";
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqSelect(req, null);

                while (curs.Read())
                {

                    Abonnement abonnement = new Abonnement(
                        (string)curs.Field("id"),
                        (string)curs.Field("idRevue"),
                        (string)curs.Field("titre"),
                        (DateTime)curs.Field("dateCommande"),
                        (DateTime)curs.Field("dateFinAbonnement"),
                        (double)curs.Field("montant")
                        );
                    lesAbonnements.Add(abonnement);
                }
                curs.Close();
                return lesAbonnements;

            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la récupération des abonnements dans la BDD");
                return lesAbonnements;
            }
        }

        /// <summary>
        /// Ajoute une ligne à la table commande.
        /// </summary>
        /// <param name="commande">Commande à ajouter.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        private static bool AjouterCommande(Commande commande)
        {
            try
            {
                string req = "insert into commande (id, dateCommande, montant) values (@id, @date, @montant); ";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", commande.Id},
                    { "@date", commande.Date},
                    { "@montant", commande.Montant}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Suppression de la commande {0}", commande.Id);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de l'ajout d'une commande à la BDD");
                return false;
            }
        }

        /// <summary>
        /// Ajoute une ligne à la table commandedocument.
        /// </summary>
        /// <param name="commande">Commande à ajouter.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool AjouterCommandeDocument(CommandeDocument commande)
        {
            if (AjouterCommande(commande))
            {
                try
                {
                    string req = "insert into commandedocument(id, nbExemplaire, idLivreDvd, idEtatCommande) VALUES (@idCommandeDocument, @nbExemplaires, @idLivreDvd, @idEtatDocument);";
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idCommandeDocument", commande.Id},
                    { "@nbExemplaires", commande.NbExemplaire},
                    { "@idLivreDvd", commande.IdLivreDvd},
                    { "@idEtatDocument", commande.Etat.Id},


                };
                    BddMySql curs = BddMySql.GetInstance(connectionString);
                    curs.ReqUpdate(req, parameters);
                    Log.Information("Ajout de la commande {0} concernant le document {1}", commande.Id, commande.IdLivreDvd);
                    curs.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Log.Information(e, "Echec lors de l'ajout d'une commande document à la BDD");
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Modifie une ligne de la table commandedocument.
        /// </summary>
        /// <param name="commande">Commande à modifier.</param>
        /// <param name="etat">Nouvel état de la commande.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool UpdateCommandeDocument(CommandeDocument commande, EtatCommande etat)
        {
            try
            {
                string req = "update commandedocument set idEtatCommande = @idEtatCommande WHERE id = @idCommande ";


                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idEtatCommande", etat.Id},
                    { "@idCommande", commande.Id},
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Modification de la commande {0} concernant le document {1}", commande.Id, commande.IdLivreDvd);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la mise à jour d'une commande de document à la BDD");
                return false;
            }
        }

        /// <summary>
        /// Supprime une ligne de la table commandedocument.
        /// </summary>
        /// <param name="commande">Commande à supprimer.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool SupprCommandeDocument(CommandeDocument commande)
        {
            try
            {
                string req = "DELETE FROM commandedocument WHERE id = @id; ";


                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", commande.Id},
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Suppression de la commande {0} concernant le document {1}", commande.Id, commande.IdLivreDvd);
                curs.Close();
                return SupprCommande(commande);
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la suppression d'une commande de document de la BDD");
                return false;
            }
        }

        /// <summary>
        /// Supprime une ligne de la table commande.
        /// </summary>
        /// <param name="commande">Commande à supprimer.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        private static bool SupprCommande(Commande commande)
        {
            try
            {
                string req = "DELETE FROM commande WHERE id = @id ";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", commande.Id},
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Suppression de la commande {0}", commande.Id);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la suppression d'une commande de la BDD");
                return false;
            }
        }

        /// <summary>
        /// Ajoute une ligne à la table abonnement.
        /// </summary>
        /// <param name="abonnement">Abonnement à ajouter.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool AjouterAbonnementRevue(Abonnement abonnement)
        {
            if (AjouterCommande(abonnement))
            {
                try
                {
                    string req = "insert into abonnement VALUES (@id, @dateFin, @idRevue);";
                    Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", abonnement.Id},
                    { "@dateFin", abonnement.DateFin},
                    { "@idRevue", abonnement.IdRevue},
                };
                    BddMySql curs = BddMySql.GetInstance(connectionString);
                    curs.ReqUpdate(req, parameters);
                    Log.Information("Ajout de l'abonnement {0} à la revue {1}", abonnement.Id, abonnement.IdRevue);
                    curs.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Log.Information(e, "Echec lors de l'aout d'un abonnement à la BDD");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Supprime une ligne à la table abonnement.
        /// </summary>
        /// <param name="abonnement">Abonnement à supprimer.</param>
        /// <returns>True si l'opération est un succès, false sinon.</returns>
        public static bool SupprAbonnementRevue(Abonnement abonnement)
        {
            try
            {
                string req = "DELETE FROM abonnement WHERE id = @id; ";


                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", abonnement.Id},
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Suppression de l'abonnement {0} de la revue {1}", abonnement.Id, abonnement.IdRevue);
                curs.Close();
                return SupprCommande(abonnement);
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la suppression d'un abonnement de la BDD");
                return false;
            }
        }



        /// <summary>
        /// Vérifie si un identifiant de document existe déjà dans la BDD ou s'il est unique.
        /// </summary>
        /// <param name="identifiant">Identifiant entré par l'utilisateur</param>
        /// <returns>True si l'identifiant est unique, false s'il existe déjà.</returns>
        public static bool VerifieSiIdDocumentUnique(string identifiant)
        {
            return VerifieUniciteId("document", identifiant);
        }

        /// <summary>
        /// Vérifie si un identifiant de commande existe déjà dans la BDD ou s'il est unique. 
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns></returns>
        public static bool VerifieSiIdCommandeUnique(string identifiant)
        {
            return VerifieUniciteId("commande", identifiant);

        }
        /// <summary>
        /// Vérifie si un identifiant existe déjà dans une table de la BDD ou s'il est unique.
        /// </summary>
        /// <param name="table">Table dans laquelle chercher l'identifiant (doit être nommé id dans la bdd)</param>
        /// <param name="identifiant">Identifiant à chercher</param>
        /// <returns></returns>
        private static bool VerifieUniciteId(string table, string identifiant)
        {
            bool existe;
            string req = $"select id from {table} where id =  @id ";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", identifiant}
                };

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);
            // Si le curseur peut être lu, il y a une entrée, donc l'identifiant n'est pas unique.
            existe = curs.Read();
            curs.Close();

            return !existe;
        }

        /// <summary>
        /// Récupère les états des documents.
        /// </summary>
        /// <returns>Liste d'Etats</returns>
        public static List<Etat> GetEtats()
        {
            List<Etat> lesEtats = new List<Etat>();
            try
            { 
                string req = "Select * from etat order by id";

                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqSelect(req, null);

                while (curs.Read())
                {
                    Etat genre = new Etat((string)curs.Field("id"), (string)curs.Field("libelle"));
                    lesEtats.Add(genre);
                }
                curs.Close();
                return lesEtats;
            }
            catch(Exception e)
            {
                Log.Information(e, "Echec lors de la récupération des états de document.");
                return lesEtats;
            }
 
        }

        /// <summary>
        /// Modifie l'état d'un exemplaire dans la base de donnés.
        /// </summary>
        /// <param name="exemplaire">Exemplaire à modifier.</param>
        /// <param name="etat">Etat à attribuer.</param>
        /// <returns></returns>
        public static bool ModifierExemplaire(Exemplaire exemplaire, Etat etat)
        {
            try
            {
                string req = "update exemplaire set idEtat = @idEtat WHERE id = @id and numero = @numero ";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@idEtat", etat.Id},
                    { "@id", exemplaire.IdDocument},
                    { "@numero", exemplaire.Numero},

                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Modification de l'exemplaire {0} du document {1}", exemplaire.Numero, exemplaire.IdDocument);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la récupération des états de document.");
                return false;
            }
        }

        /// <summary>
        /// Supprime un exemplaire de la BDD.
        /// </summary>
        /// <param name="exemplaire">Exemplaire à supprimer.</param>
        /// <returns></returns>
        public static bool SupprimerExemplaire(Exemplaire exemplaire)
        {
            try
            {
                string req = "delete from exemplaire WHERE id = @id and numero = @numero ";


                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", exemplaire.IdDocument},
                    { "@numero", exemplaire.Numero},
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                Log.Information("Suppression de l'exemplaire {0} du document {1}", exemplaire.Numero, exemplaire.IdDocument);
                curs.Close();
                return true;
            }
            catch (Exception e)
            {
                Log.Information(e, "Echec lors de la récupération des états de document.");
                return false;
            }
        }



    }

}
