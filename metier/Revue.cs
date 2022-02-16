
namespace Mediatek86.metier
{
    /// <summary>
    /// Représente un document de type Revue.
    /// </summary>
    public class Revue : Document
    {
        /// <summary>
        /// Crée une revue.
        /// </summary>
        /// <param name="id">Identifiant de la revue (unique dans la BDD).</param>
        /// <param name="titre">Titre de la revue.</param>
        /// <param name="image">URI de l'image.</param>
        /// <param name="idGenre">Identfiant du genre de la revue.</param>
        /// <param name="genre">genre de la revue.</param>
        /// <param name="idPublic">Identifiant du public de la revue.</param>
        /// <param name="lePublic">public de la revue.</param>
        /// <param name="idRayon">Identifiant du rayon de la revue.</param>
        /// <param name="rayon">rayon de la revue.</param>
        /// <param name="empruntable">Booléen indiquant si la revue est empruntable. </param>
        /// <param name="periodicite">Périodicité de la revue.</param>
        /// <param name="delaiMiseADispo">Délai pendant lequel la revue est mise à dispo.</param>
        public Revue(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon, 
            bool empruntable, string periodicite, int delaiMiseADispo, long nbExemplaires)
             : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon, nbExemplaires)
        {
            Periodicite = periodicite;
            Empruntable = empruntable;
            DelaiMiseADispo = delaiMiseADispo;
        }

        /// <summary>
        /// Périodicité de la revue
        /// </summary>
        public string Periodicite { get; set; }

        /// <summary>
        /// Booléen indiquant si la revue est empruntable (true si elle l'est)
        /// </summary>
        public bool Empruntable { get; set; }

        /// <summary>
        /// Délai pendant lequel la revue est mise à dispo.
        /// </summary>
        public int DelaiMiseADispo { get; set; }
    }
}
