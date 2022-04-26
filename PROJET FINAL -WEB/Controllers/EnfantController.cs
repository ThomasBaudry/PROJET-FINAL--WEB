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
    /// Classe représentant le controleur de vue des Enfants.
    /// </summary>
    public class EnfantController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Enfants.
        ///   -Afficher le formulaire pour l'ajout d'un Enfant.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Enfant")]
        [Route("Enfant/Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                //Préparation des données pour la vue...
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enfant");
                ViewBag.ListeEnfants = JsonConvert.DeserializeObject<List<EnfantDTO>>(jsonResponse.ToString()).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterEnfant.
        /// Rôles de l'action : 
        ///   -Ajouter un Enfant.
        /// </summary>
        /// <param name="enfantDTO">Le DTO de l'Enfant.</param>
        /// <returns>IActionResult</returns>
        [Route("/Enfant/AjouterEnfant")]
        [HttpPost]
        public async Task<IActionResult> AjouterEnfant([FromForm] EnfantDTO enfantDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enfant/AjouterEnfant", enfantDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Enfant");
        }

        /// <summary>
        /// Action FormulaireModifierEnfant.
        /// Permet d'afficher le formulaire pour la modification d'un Enfant.
        /// </summary>
        /// <param name="nomEnfant">Nom de l'Enfant.</param>
        /// <returns>IActionResult</returns>
        [Route("/Enfant/FormulaireModifierEnfant")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierEnfant([FromQuery] string nomEnfant)
        {
            try
            {
                if (TempData["MessageErreur"] != null)
                    ViewBag.MessageErreur = TempData["MessageErreur"];
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enfant/ObtenirEnfant?nomEnfant=" + nomEnfant);
                EnfantDTO enfant = JsonConvert.DeserializeObject<EnfantDTO>(jsonResponse.ToString());
                return View(enfant);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible d'ouvrir le formulaire de modification. " + e.Message;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ModifierEnfant.
        /// Permet de modifier un Enfant.
        /// </summary>
        /// <param name="enfantDTO">L'Enfant à modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Enfant/ModifierEnfant")]
        [HttpPost]
        public async Task<IActionResult> ModifierEnfant([FromForm] EnfantDTO enfantDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enfant/ModifierEnfant", enfantDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible de procéder à la modification." + e.Message;
                return RedirectToAction("FormulaireModifierEnfant", "Enfant", new { nomEnfant = enfantDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action SupprimerEnfant.
        /// Permet de supprimer un Enfant.
        /// </summary>
        /// <param name="nomEnfant">Le nom de l'Enfant.</param>
        /// <returns>ActionResult</returns>
        [Route("/Enfant/SupprimerEnfant")]
        [HttpPost]
        public async Task<IActionResult> SupprimerEnfant([FromForm] string nomEnfant)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enfant/SupprimerEnfant?nomEnfant=" + nomEnfant, null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ViderListeEnfant.
        /// Permet de vider la liste des Enfants.
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("/Enfant/ViderListeEnfant")]
        [HttpPost]
        public async Task<IActionResult> ViderListeEnfant()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enfant/ViderListeEnfant", null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }
    }
}