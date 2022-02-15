using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Représente l'état (/le statut) de la commande : En cours, livrée...
    /// </summary>
    public class EtatCommande
    {
        /// <summary>
        /// Identifiant de l'état de la commande.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Libellé de l'état de la commande.
        /// </summary>
        public string Libelle { get; }

        /// <summary>
        /// Crée un objet représentant l'état de la commande : En cours, livrée...
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public EtatCommande(int id, string libelle)
        {
            Id = id;
            Libelle = libelle;
        }

        /// <summary>
        /// Stocke les états de la commande. 
        /// </summary>
        public static List<EtatCommande> Etats
        {
            get; set;
        }

        /// <summary>
        /// Permet de rechercher un état parmi les états existants à partir d'un identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'état voulu.</param>
        /// <returns>L'objet EtatCommande correspondant, ou un objet EtatCommande affichant une erreur.</returns>
        public static EtatCommande FindEtat(int id)
        {
            foreach(EtatCommande etat in Etats)
            {
                if (etat.Id == id) return etat;
            }
            return new EtatCommande(-1, "erreur");
        }

        /// <summary>
        /// Afficge le libellé de l'état de la commande.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Libelle;
        }
    }
}
