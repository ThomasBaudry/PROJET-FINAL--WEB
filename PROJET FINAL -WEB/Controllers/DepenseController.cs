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

        /// <summary>
        /// Action FormulaireModifierDepense.
        /// Permet d'afficher le formulaire pour la modification d'une Dépense.
        /// </summary>
        /// <param name="nomGarderie">Nom de la Garderie.</param>
        /// <param name="dateTemps">Date de la Depense.</param>
        /// <returns>IActionResult</returns>
        [Route("/Depense/FormulaireModifierDepense")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierDepense([FromQuery] string nomGarderie, [FromQuery] string dateTemps)
        {
            try
            {
                if (TempData["MessageErreur"] != null)
                    ViewBag.MessageErreur = TempData["MessageErreur"];
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Depense/ObtenirDepense?dateTemps=" + dateTemps + "&nomGarderie=" + nomGarderie);
                DepenseDTO depense = JsonConvert.DeserializeObject<DepenseDTO>(jsonResponse.ToString());
                ViewBag.nomGarderie = nomGarderie;
                ViewBag.DescCommerce = depense.Commerce.Description;
                ViewBag.DescCategorieDepense = depense.Categorie.Description;

                JsonValue jsonResponseCommerce = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/ObtenirListeCommerce");
                List<CommerceDTO> listeCommerce = JsonConvert.DeserializeObject<List<CommerceDTO>>(jsonResponse.ToString());
                ViewBag.ListeCommerces = listeCommerce;

                JsonValue jsonResponseCategorie = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/ObtenirListeCategorieDepense");
                List<CommerceDTO> listeCategorie = JsonConvert.DeserializeObject<List<CommerceDTO>>(jsonResponse.ToString());
                ViewBag.ListeCategorieDepense = listeCategorie;

                return View(depense);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible d'ouvrir le formulaire de modification. " + e.Message;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ModifierDepense.
        /// Permet de modifier une Dépense.
        /// </summary>
        /// <param name="depenseDTO">La Depense à modifier.</param>
        /// <param name="nomGarderie">Nom de la Garderie.</param>
        /// <returns>ActionResult</returns>
        [Route("/Depense/ModifierDepense")]
        [HttpPost]
        public async Task<IActionResult> ModifierDepense([FromForm] DepenseDTO depenseDTO, [FromForm] string nomGarderie)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Depense/ModifierDepense?nomGarderie=" + nomGarderie, depenseDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible de procéder à la modification." + e.Message;
                return RedirectToAction("FormulaireModifierDepense", "Depense", new { nomDepense = depenseDTO.DateTemps, nomGarderie = nomGarderie });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action SupprimerDepense.
        /// Permet de supprimer une Depense.
        /// </summary>
        /// <param name="nomGarderie">Le nom de la Garderie.</param>
        /// <param name="dateTemps">date de la Depense.</param>
        /// <returns>ActionResult</returns>
        [Route("/Depense/SupprimerDepense")]
        [HttpPost]
        public async Task<IActionResult> SupprimerDepense([FromForm] string dateTemps, [FromForm] string nomGarderie)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Depense/SupprimerDepense?datetemps=" + dateTemps + "&nomGarderie=" + nomGarderie, null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ViderListeDepense.
        /// Permet de vider la liste des Dépenses.
        /// <param name="nomGarderie">Le nom de la Garderie.</param>
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("/Depense/ViderListeDepense")]
        [HttpPost]
        public async Task<IActionResult> ViderListeDepense([FromForm] string nomGarderie)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Depense/ViderListeDepense?nomGarderie=" + nomGarderie, null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }
    }
}