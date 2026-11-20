using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestauranteWeb.Mappers;
using RestauranteWeb.Models;

namespace RestauranteWeb.Controllers
{
    public class MesaController : Controller
    {
        private readonly IMesaService mesaService;
        private readonly IRestauranteService restauranteService;
        private readonly ILogger<MesaController> logger;

        public MesaController(
            IMesaService mesaService,
            IRestauranteService restauranteService,
            ILogger<MesaController> logger)
        {
            this.mesaService = mesaService;
            this.restauranteService = restauranteService;
            this.logger = logger;
        }

        public ActionResult Index()
        {
            try
            {
                var mesas = mesaService.GetAll()
                    .OrderBy(m => m.Identificacao);

                return View(MesaMapper.ToViewModel(mesas));
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Erro ao listar as mesas.");
                TempData["ErrorMessage"] = "Não foi possível consultar as mesas. Verifique a conexão com o banco de dados e tente novamente.";
                return View(new List<MesaViewModel>());
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var mesa = mesaService.Get(id);
                if (mesa is null)
                {
                    TempData["ErrorMessage"] = "A mesa solicitada não foi encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                return View(MesaMapper.ToViewModel(mesa));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Details));
            }
        }

        public ActionResult Create()
        {
            CarregarListas();
            return View(new MesaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MesaViewModel mesaViewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarListas(mesaViewModel.IdRestaurante);
                return View(mesaViewModel);
            }

            try
            {
                mesaService.Create(MesaMapper.ToEntity(mesaViewModel));
                TempData["SuccessMessage"] = "Mesa cadastrada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "cadastrar", mesaViewModel);
                CarregarListas(mesaViewModel.IdRestaurante);
                return View(mesaViewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var mesa = mesaService.Get(id);
                if (mesa is null)
                {
                    TempData["ErrorMessage"] = "A mesa que você deseja editar não foi encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                CarregarListas(mesa.IdRestaurante);
                return View(MesaMapper.ToViewModel(mesa));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Edit));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MesaViewModel mesaViewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarListas(mesaViewModel.IdRestaurante);
                return View(mesaViewModel);
            }

            try
            {
                var mesa = MesaMapper.ToEntity(mesaViewModel);
                mesa.Id = id;
                mesaService.Edit(mesa);
                TempData["SuccessMessage"] = "Mesa alterada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "alterar", mesaViewModel);
                CarregarListas(mesaViewModel.IdRestaurante);
                return View(mesaViewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var mesa = mesaService.Get(id);
                if (mesa is null)
                {
                    TempData["ErrorMessage"] = "A mesa que você deseja excluir não foi encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                return View(MesaMapper.ToViewModel(mesa));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Delete));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MesaViewModel mesaViewModel)
        {
            try
            {
                mesaService.Delete(mesaViewModel.Id);
                TempData["SuccessMessage"] = "Mesa excluída com sucesso.";
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "excluir", mesaViewModel);
                return View(mesaViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        private void CarregarListas(uint? restauranteSelecionado = null)
        {
            try
            {
                ViewBag.IdRestaurante = new SelectList(
                    restauranteService.GetAll(),
                    nameof(Restaurante.Id),
                    nameof(Restaurante.Nome),
                    restauranteSelecionado);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Erro ao carregar os restaurantes.");
                ViewBag.IdRestaurante = new SelectList(Array.Empty<Restaurante>());
                ModelState.AddModelError(string.Empty, "Não foi possível carregar os restaurantes. Tente novamente mais tarde.");
            }
        }

        private ActionResult TratarErroDeLeitura(Exception exception, string actionName)
        {
            logger.LogError(exception, "Erro ao executar a operação {ActionName} de mesa.", actionName);
            TempData["ErrorMessage"] = "Não foi possível consultar as mesas. Verifique a conexão com o banco de dados e tente novamente.";
            return RedirectToAction(nameof(Index));
        }

        private void AdicionarErroDePersistencia(Exception exception, string operacao, MesaViewModel mesaViewModel)
        {
            logger.LogError(exception, "Erro ao {Operacao} a mesa {MesaId}.", operacao, mesaViewModel.Id);
            ModelState.AddModelError(string.Empty, ObterMensagemDePersistencia(exception, operacao));
        }

        private static string ObterMensagemDePersistencia(Exception exception, string operacao)
        {
            var causa = exception.GetBaseException();
            if (causa is MySql.Data.MySqlClient.MySqlException mySqlException)
            {
                return mySqlException.Number switch
                {
                    1451 => "Não é possível excluir esta mesa porque existem atendimentos vinculados a ela.",
                    1452 => "O restaurante selecionado não existe mais.",
                    _ => "Não foi possível acessar o banco de dados. Verifique se o MySQL está ativo e tente novamente."
                };
            }

            if (exception is DbUpdateException)
            {
                return operacao == "excluir"
                    ? "Não foi possível excluir esta mesa porque existem atendimentos vinculados a ela."
                    : $"Não foi possível {operacao} a mesa. Verifique os dados informados e tente novamente.";
            }

            return $"Ocorreu um erro inesperado ao {operacao} a mesa. Tente novamente mais tarde.";
        }
    }
}