using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using RestauranteWeb.Mappers;
using RestauranteWeb.Models;

namespace RestauranteWeb.Controllers
{
    public class TipoFuncionarioController : Controller
    {
        private readonly ITipoFuncionario tipoFuncionarioService;

        public TipoFuncionarioController(ITipoFuncionario tipoFuncionarioService)
        {
            this.tipoFuncionarioService = tipoFuncionarioService;
        }

        public ActionResult Index()
        {
            var tiposFuncionario = tipoFuncionarioService.GetAll();
            var tiposFuncionarioViewModel = TipoFuncionarioMapper.ToViewModel(tiposFuncionario);
            return View(tiposFuncionarioViewModel);
        }

        public ActionResult Details(uint id)
        {
            var tipoFuncionario = tipoFuncionarioService.Get(id);
            if (tipoFuncionario == null)
            {
                return NotFound();
            }

            return View(TipoFuncionarioMapper.ToViewModel(tipoFuncionario));
        }

        public ActionResult Create()
        {
            return View(new TipoFuncionarioViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoFuncionarioViewModel tipoFuncionarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoFuncionarioViewModel);
            }

            var tipoFuncionario = TipoFuncionarioMapper.ToEntity(tipoFuncionarioViewModel);
            tipoFuncionarioService.Create(tipoFuncionario);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(uint id)
        {
            var tipoFuncionario = tipoFuncionarioService.Get(id);
            if (tipoFuncionario == null)
            {
                return NotFound();
            }

            return View(TipoFuncionarioMapper.ToViewModel(tipoFuncionario));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, TipoFuncionarioViewModel tipoFuncionarioViewModel)
        {
            if (id != tipoFuncionarioViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(tipoFuncionarioViewModel);
            }

            var tipoFuncionario = TipoFuncionarioMapper.ToEntity(tipoFuncionarioViewModel);
            tipoFuncionarioService.Edit(tipoFuncionario);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(uint id)
        {
            var tipoFuncionario = tipoFuncionarioService.Get(id);
            if (tipoFuncionario == null)
            {
                return NotFound();
            }

            return View(TipoFuncionarioMapper.ToViewModel(tipoFuncionario));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TipoFuncionarioViewModel tipoFuncionarioViewModel)
        {
            tipoFuncionarioService.Delete(tipoFuncionarioViewModel.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
