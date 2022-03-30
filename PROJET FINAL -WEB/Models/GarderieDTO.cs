using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJET_FINAL__WEB.Models
{
    public class GarderieDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant le nom de la Garderie.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Propriété représentant l'adresse de la Garderie.
        /// </summary>
        public string Adresse { get; set; }
        /// <summary>
        /// Propriété représentant la ville de la Garderie.
        /// </summary>
        public string Ville { get; set; }
        /// <summary>
        /// Propriété représentant la province de la Garderie.
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// Propriété représenant le téléphone de la Garderie.
        /// </summary>
        public string Telephone { get; set; }

        #endregion Proprietes

        #region Constructeurs

        public GarderieDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="nom">Nom de la Garderie.</param>
        /// <param name="adresse">Adresse de la Garderie.</param>
        /// <param name="ville">Ville de la Garderie.</param>
        /// <param name="province">Province de la Garderie.</param>
        /// <param name="telephone">Téléphone de la Garderie.</param>
        public GarderieDTO(string nom = "", string adresse = "", string ville = "", string province = "", string telephone = "")
        {
            Nom = nom;
            Adresse = adresse;
            Ville = ville;
            Province = province;
            Telephone = telephone;
        }

        #endregion Constructeurs
    }
}