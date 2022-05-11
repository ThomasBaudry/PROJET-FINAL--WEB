namespace PROJET_FINAL__WEB.Models
{
    public class FinanceDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant le revenu total de la finance.
        /// </summary>
        public double Revenu { get; set; }
        /// <summary>
        /// Propriété représentant la depenses total de la finance.
        /// </summary>
        public double Depenses { get; set; }
        /// <summary>
        /// Propriété représentant le profit de la finance.
        /// </summary>
        public double Profit { get; set; }

        #endregion Proprietes

        #region Constructeurs

        public FinanceDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="unRevenu">Revenu de la Finance.</param>
        /// <param name="uneDepenses">Depense de la Finance.</param>
        public FinanceDTO(double unRevenu = 0, double uneDepenses = 0)
        {
            Revenu = unRevenu;
            Depenses = uneDepenses;
            Profit = unRevenu - uneDepenses;
        }

        #endregion Constructeurs
    }
}
