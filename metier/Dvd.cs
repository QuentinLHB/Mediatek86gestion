using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Représente un document de type DVD.
    /// </summary>
    public class Dvd : LivreDvd
    {
        public Dvd(string id, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon, long nbExemplaires)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon, nbExemplaires)
        {
            this.Duree = duree;
            this.Realisateur = realisateur;
            this.Synopsis = synopsis;
        }

        /// <summary>
        /// Durée du DVD.
        /// </summary>
        public int Duree { get; set; }

        /// <summary>
        /// Réalisateur du DVD.
        /// </summary>
        public string Realisateur { get; set; }

        /// <summary>
        /// Synopsis du DVD.
        /// </summary>
        public string Synopsis { get; set; }

    }
}
