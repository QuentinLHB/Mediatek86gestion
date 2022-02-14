using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class EtatCommande
    {
        public int Id { get; }
        public string Libelle { get; }

        public EtatCommande(int id, string libelle)
        {
            Id = id;
            Libelle = libelle;
        }


        public static List<EtatCommande> Etats
        {
            get; set;
        }

        public static EtatCommande FindEtat(int id)
        {
            foreach(EtatCommande etat in Etats)
            {
                if (etat.Id == id) return etat;
            }
            return new EtatCommande(-1, "erreur");
        }

        public override string ToString()
        {
            return Libelle;
        }
    }
}
