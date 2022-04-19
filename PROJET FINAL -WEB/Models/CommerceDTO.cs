using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJET_FINAL__WEB.Models
{
    public class CommerceDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant la description du Commerce.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Propriété représentant l'adresse du Commerce.
        /// </summary>
        public string Adresse { get; set; }
        /// <summary>
        /// Propriété représentant le telephones du Commerce.
        /// </summary>
        public string Telephone { get; set; }

        #endregion Proprietes

        #region Constructeurs

        public CommerceDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="description">Description du Commerce.</param>
        /// <param name="adresse">Adresse du Commerce.</param>
        /// <param name="telephone">Telephone du Commerce.</param>
        public CommerceDTO(string description = "", string adresse = "", string telephone = "")
        {
            Description = description;
            Adresse = adresse;
            Telephone = telephone;
        }

        #endregion Constructeurs
    }
}