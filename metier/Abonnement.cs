using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Abonnement à une revue.
    /// </summary>
    public class Abonnement : Commande
    {
        /// <summary>
        /// Identifiant de la revue concernée.
        /// </summary>
        public string IdRevue { get; }

        /// <summary>
        /// Titre de la revue concernée.
        /// </summary>
        public string Titre { get; }

        /// <summary>
        /// Etat de l'abonnement, "En cours" ou "Expiré" si sa date de fin est antérieure au jour actuel.
        /// </summary>
        public string Etat { get
            {
                if (DateTime.Today > DateFin) return "Expiré";
                else return "En cours";
            } }

        /// <summary>
        /// Date de fin de l'abonnement.
        /// </summary>
        public DateTime DateFin { get; set; }

        /// <summary>
        /// Abonnement à une revue.
        /// </summary>
        /// <param name="id">Identifiant de l'abonnement.</param>
        /// <param name="idRevue">Identifiant de la revue.</param>
        /// <param name="titre">Titre de la revue.</param>
        /// <param name="dateDebut">Début de l'abonnement.</param>
        /// <param name="dateFin">Fin de l'abonnement.</param>
        /// <param name="montant">Montant de l'abonnement.</param>
        public Abonnement(string id, string idRevue, string titre, DateTime dateDebut, DateTime dateFin, double montant) : base(id, dateDebut, montant)
        {
            IdRevue = idRevue;
            Titre = titre;
            DateFin = dateFin;
        }

        /// <summary>
        /// Retourne un abonnement formaté. 
        /// Exemple :
        /// "00001" - Alternative Economiques (expire le 01/01/2022) : 5€"
        /// </summary>
        /// <returns>Un string présentant l'abonnement.</returns>
        public override string ToString()
        {
            return $"{Id} - {Titre} (expire le {String.Format("{0:dd/MM/yyyy}", DateFin)}) : {Montant}€";
        }
    }
}
