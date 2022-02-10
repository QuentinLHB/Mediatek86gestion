

namespace Mediatek86.metier
{
    public class Livre : LivreDvd
    {

        private string isbn;
        private string auteur;
        private string collection;

        public Livre(string id, string titre, string image, string isbn, string auteur, string collection, 
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            :base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.isbn = isbn;
            this.auteur = auteur;
            this.collection = collection;
        }

        public string Isbn { get => isbn;
            set => isbn = value;
        }

        public string Auteur { get => auteur;
            set => auteur = value;
        }
        public string Collection { get => collection;
            set => collection = value;
        }

    }
}
