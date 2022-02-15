

namespace Mediatek86.metier
{
    /// <summary>
    /// Représente le genre d'un document.
    /// </summary>
    public class Genre : Categorie
    {
        /// <summary>
        /// Crée un genre de document.
        /// </summary>
        /// <param name="id">Identifiant du genre.</param>
        /// <param name="libelle">Libellé du genre.</param>
        public Genre(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
