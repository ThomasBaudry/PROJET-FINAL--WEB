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
    /// Classe représentant le controleur de vue des Commerces.
    /// </summary>
    public class CommerceController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Commerce.
        ///   -Afficher le formulaire pour l'ajout d'un Commerce.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("")]
        [Route("Commerce")]
        [Route("Commerce/Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                //Préparation des données pour la vue...
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce");
                ViewBag.ListeCommerces = JsonConvert.DeserializeObject<List<CommerceDTO>>(jsonResponse.ToString()).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterCommerce.
        /// Rôles de l'action : 
        ///   -Ajouter un Commerce.
        /// </summary>
        /// <param name="commerceDTO">Le DTO du Commerce.</param>
        /// <returns>IActionResult</returns>
        [Route("/Commerce/AjouterCommerce")]
        [HttpPost]
        public async Task<IActionResult> AjouterCommerce([FromForm] CommerceDTO commerceDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/AjouterCommerce", commerceDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Commerce");
        }

        /// <summary>
        /// Action FormulaireModifierCommerce.
        /// Permet d'afficher le formulaire pour la modification d'un Commerce.
        /// </summary>
        /// <param name="descCommerce">Description du Commerce.</param>
        /// <returns>IActionResult</returns>
        [Route("/Commerce/FormulaireModifierCommerce")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierCommerce([FromQuery] string descCommerce)
        {
            try
            {
                if (TempData["MessageErreur"] != null)
                    ViewBag.MessageErreur = TempData["MessageErreur"];
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/ObtenirCommerce?descCommerce=" + descCommerce);
                CommerceDTO commerce = JsonConvert.DeserializeObject<CommerceDTO>(jsonResponse.ToString());
                return View(commerce);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible d'ouvrir le formulaire de modification. " + e.Message;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ModifierCommerce.
        /// Permet de modifier un Commerce.
        /// </summary>
        /// <param name="commerceDTO">Le Commerce a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Commerce/ModifierCommerce")]
        [HttpPost]
        public async Task<IActionResult> ModifierCommerce([FromForm] CommerceDTO commerceDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/ModifierCommerce", commerceDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = "Impossible de procéder à la modification." + e.Message;
                return RedirectToAction("FormulaireModifierCommerce", "Commerce", new { descCommerce = commerceDTO.Description });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action SupprimerCommerce.
        /// Permet de supprimer un Commerce.
        /// </summary>
        /// <param name="descCommerce">Le nom du Commerce.</param>
        /// <returns>ActionResult</returns>
        [Route("/Commerce/SupprimerCommerce")]
        [HttpPost]
        public async Task<IActionResult> SupprimerCommerce([FromForm] string descCommerce)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/SupprimerCommerce?nomCommerce=" + descCommerce, null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action ViderListeCommerce.
        /// Permet de vider la liste des Commerces.
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("/Commerce/ViderListeCommerce")]
        [HttpPost]
        public async Task<IActionResult> ViderListeCommerce()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Commerce/ViderListeCommerce", null);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }
    }
}