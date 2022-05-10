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
    /// Classe représentant le controleur de vue des Educateurs.
    /// </summary>
    public class EducateurController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Educateurs.
        ///   -Afficher le formulaire pour l'ajout d'un Educateur.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Educateur")]
        [Route("Educateur/Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                //Préparation des données pour la vue...
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Educateur");
                ViewBag.ListeEducateurs = JsonConvert.DeserializeObject<List<EducateurDTO>>(jsonResponse.ToString()).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterEducateur.
        /// Rôles de l'action : 
        ///   -Ajouter un Educateur.
        /// </summary>
        /// <param name="EducateurDTO">Le DTO de l'Educateur.</param>
        /// <returns>IActionResult</returns>
        [Route("/Educateur/AjouterEducateur")]
        [HttpPost]
        public async Task<IActionResult> AjouterEducateur([FromForm] EducateurDTO EducateurDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Educateur/AjouterEducateur", EducateurDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Educateur");
        }

        /// <summary>
        /// Action FormulaireModifierEducateur.
        /// Permet d'afficher le formulaire pour la modification d'un Educateur.
        /// </summary>
        /// <param name="nomEducateur">Nom de l'Educateur.</param>
        /// <returns>IActionResult</returns>
        [Route("/Educateur/FormulaireModifierEducateur")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierEducateur([FromQuery] string nomEducateur)
        {
            try
            {
                if (TempData["MessageErreur"] != null)
                    ViewBag.MessageErreur = TempData["MessageErreur"];
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Educateur/ObtenirEducateur?nomEducateur=" + nomEducateur);
                EducateurDTO Educateur = JsonConvert.DeserializeObject<EducateurDTO>(jsonResponse.ToString());
                return View(Educateur);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible d'ouvrir le formulaire de modification. " + e.Message;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ModifierEducateur.
        /// Permet de modifier un Educateur.
        /// </summary>
        /// <param name="EducateurDTO">L'Educateur à modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Educateur/ModifierEducateur")]
        [HttpPost]
        public async Task<IActionResult> ModifierEducateur([FromForm] EducateurDTO EducateurDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Educateur/ModifierEducateur", EducateurDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible de procéder à la modification." + e.Message;
                return RedirectToAction("FormulaireModifierEducateur", "Educateur", new { nomEducateur = EducateurDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action SupprimerEducateur.
        /// Permet de supprimer un Educateur.
        /// </summary>
        /// <param name="nomEducateur">Le nom de l'Educateur.</param>
        /// <returns>ActionResult</returns>
        [Route("/Educateur/SupprimerEducateur")]
        [HttpPost]
        public async Task<IActionResult> SupprimerEducateur([FromForm] string nomEducateur)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Educateur/SupprimerEducateur?nomEducateur=" + nomEducateur, null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ViderListeEducateur.
        /// Permet de vider la liste des Educateurs.
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("/Educateur/ViderListeEducateur")]
        [HttpPost]
        public async Task<IActionResult> ViderListeEducateur()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Educateur/ViderListeEducateur", null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }
    }
}