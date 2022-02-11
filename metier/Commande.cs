using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class Commande
    {
        public Commande(string id, DateTime date, double montant)
        {
            this.Id = id;
            this.Date = date;
            this.Montant = montant;
        }

        public string Id { get; set; }

        public DateTime Date { get; set;}

        public double Montant { get; set; }
        

    }
}
