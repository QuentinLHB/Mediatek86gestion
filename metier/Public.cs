using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Représente la catégorie de public.
    /// </summary>
    public class Public : Categorie
    {
        /// <summary>
        /// Crée une catégorie de public.
        /// </summary>
        /// <param name="id">Id du public</param>
        /// <param name="libelle">Libellé de la catégorie de public.</param>
        public Public(string id, string libelle):base(id, libelle)
        {
        }

    }
}
