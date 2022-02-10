
namespace Mediatek86.metier
{
    public class Document
    {

        private string id;
        private string titre;
        private string image;
        private string idGenre;
        private string genre;
        private string idPublic;
        private string lePublic;
        private string idRayon;
        private string rayon;

        public Document(string id, string titre, string image, string idGenre, string genre, 
            string idPublic, string lePublic, string idRayon, string rayon)
        {
            this.id = id;
            this.titre = titre;
            this.image = image;
            this.idGenre = idGenre;
            this.genre = genre;
            this.idPublic = idPublic;
            this.lePublic = lePublic;
            this.idRayon = idRayon;
            this.rayon = rayon;
        }

        public string Id { get => id; }
        public string Titre { get => titre;
            set => titre = value;
        }
        public string Image { get => image;
            set => image = value;
        }
        public string IdGenre { get => idGenre;
            set => idGenre = value;
        }
        public string Genre { get => genre;
            set => genre = value;
        }
        public string IdPublic { get => idPublic;
            set => idPublic = value;
        }
        public string Public { get => lePublic;
            set => lePublic = value;
        }
        public string IdRayon { get => idRayon;
            set => idRayon = value;
        }
        public string Rayon { get => rayon;
            set => rayon = value;
        }

    }


}
