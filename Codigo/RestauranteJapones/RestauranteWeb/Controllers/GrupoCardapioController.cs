using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteWeb.Mappers;
using RestauranteWeb.Models;
using Service;
using System.Collections.Generic;

namespace RestauranteWeb.Controllers
{
    public class GrupoCardapioController : Controller
    {
        private readonly IGrupoCardapioService grupoCardapioService;

        public GrupoCardapioController(IGrupoCardapioService grupoCardapioService)
        {
            this.grupoCardapioService = grupoCardapioService;
        }

        // GET: GrupoCardapioController
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var listaGrupoCardapio = grupoCardapioService.GetAll();
                var listaGrupoCardapioViewModel = GrupoCardapioMapper.ToViewModel(listaGrupoCardapio);
                return View(listaGrupoCardapioViewModel);
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                TempData["ErrorMessage"] = "Não foi possível ligar ao servidor de base de dados. Certifique-se de que o MySQL está ativo.";

                return View(new List<GrupocardapioViewModel>());
            }
        }

        // GET: GrupoCardapioController/Details/5
        public ActionResult Details(uint id)
        {
            Grupocardapio? grupocardapio = grupoCardapioService.Get(id);
            if (grupocardapio == null)
            {
                return NotFound();
            }

            GrupocardapioViewModel? grupocardapioViewModel = GrupoCardapioMapper.ToViewModel(grupocardapio);
            return View(grupocardapioViewModel);
        }

        // GET: GrupoCardapioController/Create
        public ActionResult Create()
        {
            var grupocardapioViewModel = new GrupocardapioViewModel();
            return View(grupocardapioViewModel);
        }

        // POST: GrupoCardapioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GrupocardapioViewModel? grupocardapioViewModel)
        {
            if (ModelState.IsValid && grupocardapioViewModel != null)
            {
                Grupocardapio? grupocardapio = GrupoCardapioMapper.ToEntity(grupocardapioViewModel);
                if (grupocardapio != null)
                {
                    grupoCardapioService.Create(grupocardapio);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: GrupoCardapioController/Edit/5
        public ActionResult Edit(uint id)
        {
            Grupocardapio? grupocardapio = grupoCardapioService.Get(id);
            if (grupocardapio == null)
            {
                return NotFound();
            }

            GrupocardapioViewModel? grupocardapioViewModel = GrupoCardapioMapper.ToViewModel(grupocardapio);
            return View(grupocardapioViewModel);
        }

        // POST: GrupoCardapioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, GrupocardapioViewModel? grupocardapioViewModel)
        {
            if (ModelState.IsValid && grupocardapioViewModel != null)
            {
                Grupocardapio? grupocardapio = GrupoCardapioMapper.ToEntity(grupocardapioViewModel);
                if (grupocardapio != null)
                {
                    grupoCardapioService.Edit(grupocardapio);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: GrupoCardapioController/Delete/5
        public ActionResult Delete(uint id)
        {
            Grupocardapio? grupocardapio = grupoCardapioService.Get(id);
            if (grupocardapio == null)
            {
                return NotFound();
            }

            GrupocardapioViewModel? grupocardapioViewModel = GrupoCardapioMapper.ToViewModel(grupocardapio);
            return View(grupocardapioViewModel);
        }

        // POST: GrupoCardapioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(GrupocardapioViewModel grupocardapioViewModel)
        {
            grupoCardapioService.Delete(grupocardapioViewModel.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}