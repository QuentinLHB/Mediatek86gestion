using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class Abonnement : Commande
    {

        public string IdRevue { get; }
        public string Titre { get; }

        public string Etat { get
            {
                if (DateTime.Today > DateFin) return "Expiré";
                else return "En cours";
            } }

        public DateTime DateFin { get; set; }

        public Abonnement(string id, string idRevue, string titre, DateTime dateDebut, DateTime dateFin, double montant) : base(id, dateDebut, montant)
        {
            IdRevue = idRevue;
            Titre = titre;
            DateFin = dateFin;
        }

        public override string ToString()
        {
            return $"{Id} - {Titre} (expire le {String.Format("{0:dd/MM/yyyy}", DateFin)}) : {Montant}€";
        }
    }
}
