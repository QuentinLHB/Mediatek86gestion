

namespace Mediatek86.metier
{
    /// <summary>
    /// Représente les catégories de rayons.
    /// </summary>
    public class Rayon : Categorie
    {
        /// <summary>
        /// Crée un rayon.
        /// </summary>
        /// <param name="id">Identifiant du rayon.</param>
        /// <param name="libelle">Libellé du rayon.</param>
        public Rayon(string id, string libelle):base(id, libelle)
        {
        }

    }
}
