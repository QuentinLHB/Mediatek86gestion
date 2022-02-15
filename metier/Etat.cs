

namespace Mediatek86.metier
{
    /// <summary>
    /// Représente l'état d'un document (neuf, inutilisable...)
    /// </summary>
    public class Etat
    {
        /// <summary>
        /// Crée un objet représentant l'état d'un document  (neuf, inutilisable...)
        /// </summary>
        /// <param name="id">Identifiant unique</param>
        /// <param name="libelle">Libellé de l'état</param>
        public Etat(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

        /// <summary>
        /// Identifiant unique de l'état du document.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Libellé de l'état.
        /// </summary>
        public string Libelle { get; set; }
    }
}
