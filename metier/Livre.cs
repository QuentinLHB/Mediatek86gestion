

namespace Mediatek86.metier
{
    /// <summary>
    /// Document de type Livre.
    /// </summary>
    public class Livre : LivreDvd
    {
        /// <summary>
        /// Crée un nouveau livre.
        /// </summary>
        /// <param name="id">Identifiant du livre. Doit être unique dans la base de données.</param>
        /// <param name="titre">Titre du livre</param>
        /// <param name="image">URL de l'image du livre.</param>
        /// <param name="isbn">International Standard Book Number : Identifiant unique attribué à un ouvrage.</param>
        /// <param name="auteur">Auteur du livre.</param>
        /// <param name="collection">Collection du livre.</param>
        /// <param name="idGenre">Identifiant du genre du livre.</param>
        /// <param name="genre">Libellé du genre du livre.</param>
        /// <param name="idPublic">Identifiant du public visé par le livre.</param>
        /// <param name="lePublic">Public visé par le livre.</param>
        /// <param name="idRayon">Identifiant du rayon dans lequel est présent le livre.</param>
        /// <param name="rayon">Rayon dans lequel est présent le livre.</param>
        public Livre(string id, string titre, string image, string isbn, string auteur, string collection, 
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            :base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.Isbn = isbn;
            this.Auteur = auteur;
            this.Collection = collection;
        }

        /// <summary>
        /// International Standard Book Number : Identifiant unique attribué à un ouvrage.
        /// </summary>
        public string Isbn { get; set; }

        /// <summary>
        /// Auteur du livre.
        /// </summary>
        public string Auteur { get; set; }

        /// <summary>
        /// Collection du livre.
        /// </summary>
        public string Collection { get; set; }

    }
}
