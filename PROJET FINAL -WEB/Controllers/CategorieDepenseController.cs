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
    /// Classe représentant le controleur de vue des Catégories de Dépenses.
    /// </summary>
    public class CategorieDepenseController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Catégories de Dépenses.
        ///   -Afficher le formulaire pour l'ajout d'une Catégorie de Dépense.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("CategorieDepense")]
        [Route("CategorieDepense/Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                //Préparation des données pour la vue...
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/CategorieDepense");
                ViewBag.ListeCategorieDepenses = JsonConvert.DeserializeObject<List<CategorieDepenseDTO>>(jsonResponse.ToString()).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterCategorieDepense.
        /// Rôles de l'action : 
        ///   -Ajouter une Catégorie de Dépense.
        /// </summary>
        /// <param name="categorieDepenseDTO">Le DTO de la Catégorie de Dépense.</param>
        /// <returns>IActionResult</returns>
        [Route("/CategorieDepense/AjouterCategorieDepense")]
        [HttpPost]
        public async Task<IActionResult> AjouterCategorieDepense([FromForm] CategorieDepenseDTO categorieDepenseDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/CategorieDepense/AjouterCategorieDepense", categorieDepenseDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "CategorieDepense");
        }

        /// <summary>
        /// Action FormulaireModifierCategorieDepense.
        /// Permet d'afficher le formulaire pour la modification d'une Catégorie de Dépense.
        /// </summary>
        /// <param name="descriptionCategorieDepense">Description de la Catégorie de Dépense.</param>
        /// <returns>IActionResult</returns>
        [Route("/CategorieDepense/FormulaireModifierCategorieDepense")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierCategorieDepense([FromQuery] string descriptionCategorieDepense)
        {
            try
            {
                if (TempData["MessageErreur"] != null)
                    ViewBag.MessageErreur = TempData["MessageErreur"];
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/CategorieDepense/ObtenirCategorieDepense?descriptionCategorieDepense=" + descriptionCategorieDepense);
                CategorieDepenseDTO categorieDepense = JsonConvert.DeserializeObject<CategorieDepenseDTO>(jsonResponse.ToString());
                return View(categorieDepense);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible d'ouvrir le formulaire de modification. " + e.Message;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ModifierCategorieDepense.
        /// Permet de modifier une Catégorie de Dépense.
        /// </summary>
        /// <param name="categorieDepenseDTO">La Catégorie de Dépense à modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/CategorieDepense/ModifierCategorieDepense")]
        [HttpPost]
        public async Task<IActionResult> ModifierCategorieDepense([FromForm] CategorieDepenseDTO categorieDepenseDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/CategorieDepense/ModifierCategorieDepense", categorieDepenseDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible de procéder à la modification." + e.Message;
                return RedirectToAction("FormulaireModifierCategorieDepense", "CategorieDepense", new { descriptionCategorieDepense = categorieDepenseDTO.Description });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action SupprimerCategorieDepense.
        /// Permet de supprimer une Catégorie de Dépense.
        /// </summary>
        /// <param name="descriptionCategorieDepense">La description de la Catégorie de Dépense.</param>
        /// <returns>ActionResult</returns>
        [Route("/CategorieDepense/SupprimerCategorieDepense")]
        [HttpPost]
        public async Task<IActionResult> SupprimerCategorieDepense([FromForm] string descriptionCategorieDepense)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/CategorieDepense/SupprimerCategorieDepense?descriptionCategorieDepense=" + descriptionCategorieDepense, null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ViderListeCategorieDepense.
        /// Permet de vider la liste des Catégories de Dépenses.
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("/CategorieDepense/ViderListeCategorieDepense")]
        [HttpPost]
        public async Task<IActionResult> ViderListeCategorieDepense()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/CategorieDepense/ViderCategorieDepense", null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }
    }
}