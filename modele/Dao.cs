using Mediatek86.metier;
using System.Collections.Generic;
using Mediatek86.bdd;
using System;
using System.Windows.Forms;

namespace Mediatek86.modele
{
    public static class Dao
    {

        private static readonly string server = "localhost";
        private static readonly string userid = "root";
        private static readonly string password = "";
        private static readonly string database = "mediatek86";
        private static readonly string connectionString = "server=" + server + ";user id=" + userid + ";password=" + password + ";database=" + database + ";SslMode=none";

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
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon ";
            req += "from livre l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
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
                Livre livre = new Livre(id, titre, image, isbn, auteur, collection, idgenre, genre,
                    idpublic, lepublic, idrayon, rayon);
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
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon ";
            req += "from dvd l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
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
                Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idgenre, genre,
                    idpublic, lepublic, idrayon, rayon);
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
            req += "d.idrayon, d.idpublic, d.idgenre, g.libelle as genre, p.libelle as public, r.libelle as rayon ";
            req += "from revue l join document d on l.id=d.id ";
            req += "join genre g on g.id=d.idGenre ";
            req += "join public p on p.id=d.idPublic ";
            req += "join rayon r on r.id=d.idRayon ";
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
                Revue revue = new Revue(id, titre, image, idgenre, genre,
                    idpublic, lepublic, idrayon, rayon, empruntable, periodicite, delaiMiseADispo);
                lesRevues.Add(revue);
            }
            curs.Close();

            return lesRevues;
        }

        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <returns>Liste d'objets Exemplaire</returns>
        public static List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            List<Exemplaire> lesExemplaires = new List<Exemplaire>();
            string req = "Select e.id, e.numero, e.dateAchat, e.photo, e.idEtat ";
            req += "from exemplaire e join document d on e.id=d.id ";
            req += "where e.id = @id ";
            req += "order by e.dateAchat DESC";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@id", idDocument}
                };

            BddMySql curs = BddMySql.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);

            while (curs.Read())
            {
                string idDocuement = (string)curs.Field("id");
                int numero = (int)curs.Field("numero");
                DateTime dateAchat = (DateTime)curs.Field("dateAchat");
                string photo = (string)curs.Field("photo");
                string idEtat = (string)curs.Field("idEtat");
                Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocuement);
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
                    { "@idEtat",exemplaire.IdEtat}
                };
                BddMySql curs = BddMySql.GetInstance(connectionString);
                curs.ReqUpdate(req, parameters);
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

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
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

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
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

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
                    curs.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

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
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SupprimerLivre(Livre livre)
        {
            return ReqDelete("livre", livre.Id) &&
            ReqDelete("livres_dvd", livre.Id) &&
            ReqDelete("document", livre.Id);
        }

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
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SupprimerDvd(Dvd dvd)
        {
            return ReqDelete("dvd", dvd.Id) &&
                 ReqDelete("livres_dvd", dvd.Id) &&
                 ReqDelete("document", dvd.Id);
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
                curs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

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
                    curs.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

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
                    curs.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Vérifie si un identifiant existe déjà dans la BDD ou s'il est unique.
        /// </summary>
        /// <param name="identifiant">Identifiant entré par l'utilisateur</param>
        /// <returns>True si l'identifiant est unique, false s'il existe déjà.</returns>
        public static bool verifieSiIdentifiantUnique(string identifiant)
        {
            bool existe;
            string req = "select id from document where id =  @id ";
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

    }

}
