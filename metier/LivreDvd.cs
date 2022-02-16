using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe abstraite regroupant les attributs communs des livres et DVD.
    /// </summary>
    public abstract class LivreDvd : Document
    {

        protected LivreDvd(string id, string titre, string image, string idGenre, string genre, 
            string idPublic, string lePublic, string idRayon, string rayon, long nbExemplaires)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon, nbExemplaires)
        {
        }

    }
}

