using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using RestauranteWeb.Mappers;
using RestauranteWeb.Models;

namespace RestauranteWeb.Controllers
{
    public class FormaPagamentoController : Controller
    {
        private readonly IFormaPagamentoService formaPagamentoService;

        public FormaPagamentoController(IFormaPagamentoService formaPagamentoService)
        {
            this.formaPagamentoService = formaPagamentoService;
        }

        // GET: FormaPagamentoController
        public ActionResult Index()
        {
            var listaFormasPagamento = formaPagamentoService.GetAll();
            var listaFormasPagamentoViewModel = FormaPagamentoMapper.ToViewModel(listaFormasPagamento);
            return View(listaFormasPagamentoViewModel);
        }

        // GET: FormaPagamentoController/Details/5
        public ActionResult Details(uint id)
        {
            Formapagamento formaPagamento = formaPagamentoService.Get(id);
            FormaPagamentoViewModel formaPagamentoViewModel = FormaPagamentoMapper.ToViewModel(formaPagamento);
            return View(formaPagamentoViewModel);
        }

        // GET: FormaPagamentoController/Create
        public ActionResult Create()
        {
            var formaPagamentoViewModel = new FormaPagamentoViewModel();
            return View(formaPagamentoViewModel);
        }

        // POST: FormaPagamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormaPagamentoViewModel formaPagamentoViewModel)
        {
            if (ModelState.IsValid)
            {
                Formapagamento formaPagamento = FormaPagamentoMapper.ToEntity(formaPagamentoViewModel);
                formaPagamentoService.Create(formaPagamento);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: FormaPagamentoController/Edit/5
        public ActionResult Edit(uint id)
        {
            Formapagamento formaPagamento = formaPagamentoService.Get(id);
            FormaPagamentoViewModel formaPagamentoViewModel = FormaPagamentoMapper.ToViewModel(formaPagamento);
            return View(formaPagamentoViewModel);
        }

        // POST: FormaPagamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, FormaPagamentoViewModel formaPagamentoViewModel)
        {
            if (ModelState.IsValid)
            {
                Formapagamento formaPagamento = FormaPagamentoMapper.ToEntity(formaPagamentoViewModel);
                formaPagamentoService.Edit(formaPagamento);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: FormaPagamentoController/Delete/5
        public ActionResult Delete(uint id)
        {
            Formapagamento formaPagamento = formaPagamentoService.Get(id);
            FormaPagamentoViewModel formaPagamentoViewModel = FormaPagamentoMapper.ToViewModel(formaPagamento);
            return View(formaPagamentoViewModel);
        }

        // POST: FormaPagamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormaPagamentoViewModel formaPagamentoViewModel)
        {
            formaPagamentoService.Delete(formaPagamentoViewModel.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
