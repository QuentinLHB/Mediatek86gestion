
namespace Mediatek86.metier
{
    public class Document
    {

        private string id;

        public Document(string id, string titre, string image, string idGenre, string genre, 
            string idPublic, string lePublic, string idRayon, string rayon)
        {
            this.id = id;
            this.Titre = titre;
            this.Image = image;
            this.IdGenre = idGenre;
            this.Genre = genre;
            this.IdPublic = idPublic;
            this.Public = lePublic;
            this.IdRayon = idRayon;
            this.Rayon = rayon;
        }

        /// <summary>
        /// Identifiant du document.
        /// </summary>
        public string Id { get => id; }

        /// <summary>
        /// Titre du document.
        /// </summary>
        public string Titre { get; set; }

        /// <summary>
        /// URI de l'image.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Identifiant du genre du doculent.
        /// </summary>
        public string IdGenre { get; set; }

        /// <summary>
        /// Libellé du genre du document.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Identifiant du public visé par le document.
        /// </summary>
        public string IdPublic { get; set; }

        /// <summary>
        /// Libellé du public visé par le document.
        /// </summary>
        public string Public { get; set; }

        /// <summary>
        /// Identifiant du rayon dans lequel est présent le document.
        /// </summary>
        public string IdRayon { get; set; }

        /// <summary>
        /// Libellé du rayon dans lequel est présent le document.
        /// </summary>
        public string Rayon { get; set; }

    }


}
