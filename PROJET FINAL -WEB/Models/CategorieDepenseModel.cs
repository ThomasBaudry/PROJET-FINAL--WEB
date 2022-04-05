using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJET_FINAL__WEB.Models
{
    public class CategorieDepenseDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant la description de la Catégorie de Dépense.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Propriété représentant le pourcentage de la Catégorie de Dépense.
        /// </summary>
        public double Pourcentage { get; set; }

        #endregion Proprietes

        #region Constructeurs

        public CategorieDepenseDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="description">Nom de la Catégorie de Dépense.</param>
        /// <param name="pourcentage">Pourcentage de la Catégorie de Dépense.</param>
        public CategorieDepenseDTO(string description = "", double pourcentage = 0)
        {
            Description = description;
            Pourcentage = pourcentage;
        }

        #endregion Constructeurs
    }
}