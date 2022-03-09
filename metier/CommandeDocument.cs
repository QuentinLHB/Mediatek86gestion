using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Repréente la commande d'un document. 
    /// </summary>
    public class CommandeDocument : Commande
    {

        public CommandeDocument(string id, DateTime date, double montant, int nbExemplaire, string idLivreDvd, string titre, EtatCommande etat) : base(id, date, montant)
        {
            NbExemplaire = nbExemplaire;
            IdLivreDvd = idLivreDvd;
            Titre = titre;
            Etat = etat;
        }



        /// <summary>
        /// Identifiant du document.
        /// </summary>
        public string IdLivreDvd { get; }

        /// <summary>
        /// Titre du document.
        /// </summary>
        public string Titre { get; }

        /// <summary>
        /// Quantité commandée.
        /// </summary>
        public int NbExemplaire { get; }

        /// <summary>
        /// Etat de la commande (en cours, livrée...)
        /// </summary>
        public EtatCommande Etat { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Titre} (x{NbExemplaire}) : {Montant}€";
        }
    }
}
