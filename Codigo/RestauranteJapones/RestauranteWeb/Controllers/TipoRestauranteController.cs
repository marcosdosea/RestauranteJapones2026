using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteWeb.Mappers;
using RestauranteWeb.Models;

namespace RestauranteWeb.Controllers
{
    public class TipoRestauranteController : Controller
    {
        private readonly ITipoRestauranteService tipoRestauranteService;

        public TipoRestauranteController(ITipoRestauranteService tipoRestauranteService)
        {
            this.tipoRestauranteService = tipoRestauranteService;
        }

        // GET: TipoRestauranteController
        public ActionResult Index()
        {
            var listaTiposRestaurante = tipoRestauranteService.GetAll();
            var listaTiposRestauranteViewModel = TipoRestauranteMapper.ToViewModel(listaTiposRestaurante);
            return View(listaTiposRestauranteViewModel);
        }

        // GET: TipoRestauranteController/Details/5
        public ActionResult Details(uint id)
        {
            Tiporestaurante tiporestaurante = tipoRestauranteService.Get(id);
            TipoRestauranteViewModel tiporestauranteViewModel = TipoRestauranteMapper.ToViewModel(tiporestaurante);
            return View(tiporestauranteViewModel);
        }

        // GET: TipoRestauranteController/Create
        public ActionResult Create()
        {
            var tiporestauranteViewModel = new TipoRestauranteViewModel();
            return View(tiporestauranteViewModel);
        }

        // POST: TipoRestauranteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoRestauranteViewModel tiporestauranteViewModel)
        {
            if(ModelState.IsValid)
            {
                Tiporestaurante tiporestaurante = TipoRestauranteMapper.ToEntity(tiporestauranteViewModel);
                tipoRestauranteService.Create(tiporestaurante);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TipoRestauranteController/Edit/5
        public ActionResult Edit(uint id)
        {
            Tiporestaurante tiporestaurante = tipoRestauranteService.Get(id);
            TipoRestauranteViewModel tiporestauranteViewModel = TipoRestauranteMapper.ToViewModel(tiporestaurante);
            return View(tiporestauranteViewModel);
        }

        // POST: TipoRestauranteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, TipoRestauranteViewModel tiporestauranteViewModel)
        {
            if(ModelState.IsValid)
            {
                Tiporestaurante tiporestaurante = TipoRestauranteMapper.ToEntity(tiporestauranteViewModel);
                tipoRestauranteService.Edit(tiporestaurante);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TipoRestauranteController/Delete/5
        public ActionResult Delete(uint id)
        {
            Tiporestaurante tiporestaurante = tipoRestauranteService.Get(id);
            TipoRestauranteViewModel tiporestauranteViewModel = TipoRestauranteMapper.ToViewModel(tiporestaurante);
            return View(tiporestauranteViewModel);
        }

        // POST: TipoRestauranteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TipoRestauranteViewModel tiporestauranteViewModel)
        {
            tipoRestauranteService.Delete(tiporestauranteViewModel.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
