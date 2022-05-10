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
        /// Propriété représentant la date et l'heure de la Presence.
        /// </summary>
        public string DateTemps { get; set; }
        /// <summary>
        /// Propriété représentant l'enfant de la Presence.
        /// </summary>
        public EnfantDTO Enfant { get; set; }
        /// <summary>
        /// Propriété représentant l'enfant de la Presence.
        /// </summary>
        public EducateurDTO Educateur { get; set; }

        #endregion Proprietes

        #region Constructeurs

        public PresenceDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="dateTemps">Date et Heure de la Présence.</param>
        /// <param name="unEnfant">Enfant de la Présence.</param>
        /// <param name="unEducateur">Educateur de la Présence.</param>
        public PresenceDTO(string dateTemps = "", EnfantDTO unEnfant = null, EducateurDTO unEducateur = null)
        {
            DateTemps = dateTemps;
            Enfant = unEnfant;
            Educateur = unEducateur;
        }

        #endregion Constructeurs
    }
}