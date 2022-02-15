using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe abstraite représentant une commande générique.
    /// </summary>
    public abstract class Commande
    {
        protected Commande(string id, DateTime date, double montant)
        {
            this.Id = id;
            this.Date = date;
            this.Montant = montant;
        }

        /// <summary>
        /// Identifiant de la commande.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Date de la commande.
        /// </summary>
        public DateTime Date { get; set;}

        /// <summary>
        /// Montant de la commande.
        /// </summary>
        public double Montant { get; set; }

    }
}
