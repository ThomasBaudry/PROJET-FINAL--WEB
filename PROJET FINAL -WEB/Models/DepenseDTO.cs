using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJET_FINAL__WEB.Models
{
    public class DepenseDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant la date du moment de la Dépense.
        /// </summary>
        public string DateTemps { get; set; }
        /// <summary>
        /// Propriété représentant le montant de la Dépense.
        /// </summary>
        public double Montant { get; set; }
        /// <summary>
        /// Propriété représentant le montant admissible de la Dépense (= montant * categorie depense).
        /// </summary>
        public double MontantAdmissible { get; set; }
        /// <summary>
        /// Propriété reliant avec le Commerce.
        /// </summary>
        public CommerceDTO commerceDTO { get; set; }
        /// <summary>
        /// Propriété reliant avec la Catégorie de Dépenses).
        /// </summary>
        public CategorieDepenseDTO categorieDepenseDTO { get; set; }

        #endregion Proprietes

        #region Constructeurs

        public DepenseDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="dateTemps">Date du moment de la Dépense.</param>
        /// <param name="montant">Montant de la Dépense.</param>
        /// <param name="montantAdmissible">Montant admissible de la Dépense.</param>
        public DepenseDTO(string dateTemps = "", double montant = 0, double montantAdmissible = 0)
        {
            DateTemps = dateTemps;
            Montant = montant;
            MontantAdmissible = montantAdmissible;
        }

        #endregion Constructeurs
    }
}