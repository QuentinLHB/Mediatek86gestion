using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class Dvd : LivreDvd
    {

        private int duree;
        private string realisateur;
        private string synopsis;

        public Dvd(string id, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.duree = duree;
            this.realisateur = realisateur;
            this.synopsis = synopsis;
        }

        public int Duree { get => duree;
            set => duree = value;
        }
        public string Realisateur { get => realisateur;
            set => realisateur = value;
        }
        public string Synopsis { get => synopsis;
            set => synopsis = value;
        }

    }
}
