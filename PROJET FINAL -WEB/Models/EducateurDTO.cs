using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJET_FINAL__WEB.Models
{
    public class EducateurDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant le nom de l'Educateur.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Propriété représentant le prénom de l'Educateur.
        /// </summary>
        public string Prenom { get; set; }
        /// <summary>
        /// Propriété représentant la date de naissance de l'Educateur.
        /// </summary>
        public string DateDeNaissance { get; set; }
        /// <summary>
        /// Propriété représentant l'adresse de l'Educateur.
        /// </summary>
        public string Adresse { get; set; }
        /// <summary>
        /// Propriété représentant la ville de l'Educateur.
        /// </summary>
        public string Ville { get; set; }
        /// <summary>
        /// Propriété représentant la province de l'Educateur.
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// Propriété représentant le téléphone de l'Educateur.
        /// </summary>
        public string Telephone { get; set; }

        #endregion Proprietes

        #region Constructeurs

        public EducateurDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="nom">Nom de l'Educateur.</param>
        /// <param name="prenom">Prénom de l'Educateur.</param>
        /// <param name="dateNaissance">Date de Naissance de l'Educateur.</param>
        /// <param name="adresse">Adresse de l'Educateur.</param>
        /// <param name="ville">Ville de l'Educateur.</param>
        /// <param name="province">Province de l'Educateur.</param>
        /// <param name="telephone">Téléphone de l'Educateur.</param>
        public EducateurDTO(string nom = "", string prenom = "", string dateNaissance = "", string adresse = "", string ville = "", string province = "", string telephone = "")
        {
            Nom = nom;
            Prenom = prenom;
            DateDeNaissance = dateNaissance;
            Adresse = adresse;
            Ville = ville;
            Province = province;
            Telephone = telephone;
        }

        #endregion Constructeurs
    }
}