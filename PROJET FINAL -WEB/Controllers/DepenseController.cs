using Microsoft.AspNetCore.Mvc;
using System.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using GestionCegepWeb.Utils;
using PROJET_FINAL__WEB.Models;
using System;

/// <summary>
/// Namespace pour les controleurs de vue.
/// </summary>
namespace PROJET_FINAL__WEB.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de vue des Dépenses.
    /// </summary>
    public class DepenseController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Dépenses.
        ///   -Afficher le formulaire pour l'ajout d'une Dépense.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Depense")]
        [Route("Depense/Index")]
        [Route("Depense/ObtenirListeDepense")]
        [HttpGet]
        public async Task<IActionResult> Index(string nomGarderie, string descCommerce, string descCategorieDepense)
        {
            //Mettre le if et son contenu en commentaire avant de lancer les tests fonctionnels...
            //**Le contenu du ELSE doit toutefois rester actif...**//
            //Si erreur provenant d'une autre action...
            if (TempData["MessageErreur"] != null)
                ViewBag.MessageErreur = TempData["MessageErreur"];
            else
            {
                try
                {
                    if (nomGarderie == null)
                    {
                        JsonValue jsonResponseGarderieNom = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/ObtenirListeGarderie");
                        nomGarderie = JsonConvert.DeserializeObject<List<GarderieDTO>>(jsonResponseGarderieNom.ToString()).ToArray()[0].Nom;
                    }
                    if (descCommerce == null)
                    {
                        JsonValue jsonResponseCommerceDescription = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/ObtenirListeCommerce");
                        descCommerce = JsonConvert.DeserializeObject<List<CommerceDTO>>(jsonResponseCommerceDescription.ToString()).ToArray()[0].Description;
                    }
                    if (descCategorieDepense == null)
                    {
                        JsonValue jsonResponseCategorieDepenseDesc = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/CategorieDepense/ObtenirListeCategorieDepense");
                        descCategorieDepense = JsonConvert.DeserializeObject<List<CategorieDepenseDTO>>(jsonResponseCategorieDepenseDesc.ToString()).ToArray()[0].Description;
                    }

                    //Préparation des données pour la vue...
                    ViewBag.NomGarderie = nomGarderie;
                    ViewBag.DescCommerce = descCommerce;
                    ViewBag.DescCategorieDepense = descCategorieDepense;

                    JsonValue jsonResponseGarderie = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Garderie/ObtenirListeGarderie");
                    ViewBag.ListeGarderies = JsonConvert.DeserializeObject<List<GarderieDTO>>(jsonResponseGarderie.ToString()).ToArray();

                    JsonValue jsonResponseDepense = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Depense/ObtenirListeDepense?nomGarderie=" + nomGarderie);
                    ViewBag.ListeDepenses = JsonConvert.DeserializeObject<List<DepenseDTO>>(jsonResponseDepense.ToString()).ToArray();

                    JsonValue jsonResponseCommerce = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/ObtenirListeCommerce");
                    ViewBag.ListeCommerces = JsonConvert.DeserializeObject<List<CommerceDTO>>(jsonResponseCommerce.ToString()).ToArray();

                    JsonValue jsonResponseCategorieDepense = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/CategorieDepense/ObtenirListeCategorieDepense");
                    ViewBag.ListeCategorieDepense = JsonConvert.DeserializeObject<List<CategorieDepenseDTO>>(jsonResponseCategorieDepense.ToString()).ToArray();
                }
                catch (Exception e)
                {
                    ViewBag.MessageErreur = e.Message;
                }


            }
            //Retour de la vue...
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterDepense.
        /// Rôles de l'action : 
        ///   -Ajouter une Dépense.
        /// </summary>
        /// <param name="depenseDTO">Le DTO de la Dépense.</param>
        /// <returns>IActionResult</returns>
        [Route("/Depense/AjouterDepense")]
        [HttpPost]
        public async Task<IActionResult> AjouterDepense([FromForm] string nomGarderie, [FromForm] DepenseDTO depenseDTO)
        {
            try
            {
                 await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Depense/AjouterDepense?nomGarderie=" + nomGarderie, depenseDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Depense");
        }
    }
}