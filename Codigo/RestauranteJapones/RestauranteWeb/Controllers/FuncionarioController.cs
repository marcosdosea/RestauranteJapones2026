using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestauranteWeb.Mappers;
using RestauranteWeb.Models;
using Service;

namespace RestauranteWeb.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioService funcionarioService;
        private readonly IRestauranteService restauranteService;
        private readonly ITipoFuncionario tipoFuncionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService,
                                     IRestauranteService restauranteService,
                                     ITipoFuncionario tipoFuncionarioService)
        {
            this.funcionarioService = funcionarioService;
            this.restauranteService = restauranteService;
            this.tipoFuncionarioService = tipoFuncionarioService;
        }

        // GET: FuncionarioController
        public ActionResult Index()
        {
            var listaFuncionarios = funcionarioService.GetAll();   // IEnumerable<FuncionarioDTO>
            var listaFuncionariosViewModel = FuncionarioMapper.ToViewModel(listaFuncionarios);
            return View(listaFuncionariosViewModel);
        }

        // GET: FuncionarioController/Details/5
        public ActionResult Details(uint id)
        {
            Funcionario? funcionario = funcionarioService.Get(id);
            FuncionarioViewModel funcionarioViewModel = FuncionarioMapper.ToViewModel(funcionario);
            return View(funcionarioViewModel);
        }

        // GET: FuncionarioController/Create
        public ActionResult Create()
        {
            var funcionarioViewModel = new FuncionarioViewModel();
            CarregarListas(funcionarioViewModel);
            return View(funcionarioViewModel);
        }

        // POST: FuncionarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionarioViewModel funcionarioViewModel)
        {
            if (ModelState.IsValid)
            {
                Funcionario funcionario = FuncionarioMapper.ToEntity(funcionarioViewModel);
                funcionarioService.Create(funcionario);
                return RedirectToAction(nameof(Index));
            }
            CarregarListas(funcionarioViewModel);
            return View(funcionarioViewModel);
        }

        // GET: FuncionarioController/Edit/5
        public ActionResult Edit(uint id)
        {
            Funcionario? funcionario = funcionarioService.Get(id);
            FuncionarioViewModel funcionarioViewModel = FuncionarioMapper.ToViewModel(funcionario);
            CarregarListas(funcionarioViewModel);
            return View(funcionarioViewModel);
        }

        // POST: FuncionarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, FuncionarioViewModel funcionarioViewModel)
        {
            if (ModelState.IsValid)
            {
                Funcionario funcionario = FuncionarioMapper.ToEntity(funcionarioViewModel);
                funcionarioService.Edit(funcionario);
                return RedirectToAction(nameof(Index));
            }
            CarregarListas(funcionarioViewModel);
            return View(funcionarioViewModel);
        }

        // GET: FuncionarioController/Delete/5
        public ActionResult Delete(uint id)
        {
            Funcionario? funcionario = funcionarioService.Get(id);
            FuncionarioViewModel funcionarioViewModel = FuncionarioMapper.ToViewModel(funcionario);
            return View(funcionarioViewModel);
        }

        // POST: FuncionarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FuncionarioViewModel funcionarioViewModel)
        {
            funcionarioService.Delete(funcionarioViewModel.Id);
            return RedirectToAction(nameof(Index));
        }

        // Alimenta as listas suspensas do formulário
        private void CarregarListas(FuncionarioViewModel funcionarioViewModel)
        {
            ViewBag.Restaurantes = new SelectList(restauranteService.GetAll(), "Id", "Nome",
                                                  funcionarioViewModel.IdRestaurante);
            ViewBag.TiposFuncionario = new SelectList(tipoFuncionarioService.GetAll(), "Id", "Nome",
                                                      funcionarioViewModel.IdTipoFuncionario);
        }
    }
}
