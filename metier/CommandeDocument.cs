using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class CommandeDocument : Commande
    {

        public CommandeDocument(string id, DateTime date, double montant, int nbExemplaire, string idLivreDvd, string titre, string etat) : base(id, date, montant)
        {
            Id = id;
            Date = date;
            Montant = montant;
            NbExemplaire = nbExemplaire;
            IdLivreDvd = idLivreDvd;
            Titre = titre;
            Etat = etat;
        }

        public string Id { get; }
        public DateTime Date { get; }
        public double Montant { get; }
        public int NbExemplaire { get; }
        public string IdLivreDvd { get; }
        public string Titre { get; }
        public string Etat { get; }
    }
}
