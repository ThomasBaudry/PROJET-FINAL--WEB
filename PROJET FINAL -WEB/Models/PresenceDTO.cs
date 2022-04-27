using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJET_FINAL__WEB.Models
{
    public class PresenceDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant la date du moment de la présence de l'enfant.
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Propriété reliant avec la Garderie.
        /// </summary>
        public GarderieDTO Garderie { get; set; }
        /// <summary>
        /// Propriété reliant avec l'Enfant.
        /// </summary>
        public EnfantDTO Enfant { get; set; }

        #endregion Proprietes

        #region Constructeurs

        public PresenceDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="date">Date du moment de la présence de l'enfant.</param>
        public PresenceDTO(string date = "", GarderieDTO garderieDTO = null, EnfantDTO enfantDTO= null)
        {
            Date = date;
            Garderie = garderieDTO;
            Enfant = enfantDTO;
        }

        #endregion Constructeurs
    }
}