using System;

namespace Mediatek86.metier
{
    /// <summary>
    /// Représente une unité d'un document.
    /// </summary>
    public class Exemplaire
    {
        /// <summary>
        /// Crée un exemplaire d'un document.
        /// </summary>
        /// <param name="numero">Identifiant du document </param>
        /// <param name="dateAchat">Date d'achat</param>
        /// <param name="photo">URI de la photo.</param>
        /// <param name="idEtat">Identifiant de l'état du document.</param>
        /// <param name="idDocument">Identifiant du document.</param>
        public Exemplaire(int numero, DateTime dateAchat, string photo, Etat etat, string idDocument)
        {
            this.Numero = numero;
            this.DateAchat = dateAchat;
            this.Photo = photo;
            this.Etat = etat;
            this.IdDocument = idDocument;
        }

        /// <summary>
        /// Identifiant de la commande.
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// URI de la photo.
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Date d'achat du document.
        /// </summary>
        public DateTime DateAchat { get; set; }

        /// <summary>
        /// Identifiant de l'état de la commande
        /// </summary>
        public Etat Etat { get; set; }

        /// <summary>
        /// Identifiant du document.
        /// </summary>
        public string IdDocument { get; set; }
    }
}
