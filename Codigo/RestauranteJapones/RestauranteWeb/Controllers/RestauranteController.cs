using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestauranteWeb.Mappers;
using RestauranteWeb.Models;

namespace RestauranteWeb.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly IRestauranteService restauranteService;
        private readonly ITipoRestauranteService tipoRestauranteService;
        private readonly ILogger<RestauranteController> logger;

        public RestauranteController(
            IRestauranteService restauranteService,
            ITipoRestauranteService tipoRestauranteService,
            ILogger<RestauranteController> logger)
        {
            this.restauranteService = restauranteService;
            this.tipoRestauranteService = tipoRestauranteService;
            this.logger = logger;
        }

        public ActionResult Index()
        {
            try
            {
                return View(RestauranteMapper.ToViewModel(restauranteService.GetAll()));
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Erro ao listar os restaurantes.");
                TempData["ErrorMessage"] = "Não foi possível consultar os restaurantes. Verifique a conexão com o banco de dados e tente novamente.";
                return View(new List<RestauranteViewModel>());
            }
        }

        public ActionResult Details(uint id)
        {
            try
            {
                var restaurante = restauranteService.Get(id);
                if (restaurante is null)
                {
                    TempData["ErrorMessage"] = "O restaurante solicitado não foi encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                return View(RestauranteMapper.ToViewModel(restaurante));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Details));
            }
        }

        public ActionResult Create()
        {
            CarregarTiposRestaurante();
            return View(new RestauranteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RestauranteViewModel restauranteViewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarTiposRestaurante(restauranteViewModel.IdTipoRestaurante);
                return View(restauranteViewModel);
            }

            try
            {
                restauranteService.Create(RestauranteMapper.ToEntity(restauranteViewModel));
                TempData["SuccessMessage"] = "Restaurante cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "cadastrar", restauranteViewModel);
                CarregarTiposRestaurante(restauranteViewModel.IdTipoRestaurante);
                return View(restauranteViewModel);
            }
        }

        public ActionResult Edit(uint id)
        {
            try
            {
                var restaurante = restauranteService.Get(id);
                if (restaurante is null)
                {
                    TempData["ErrorMessage"] = "O restaurante que você deseja editar não foi encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                CarregarTiposRestaurante(restaurante.IdTipoRestaurante);
                return View(RestauranteMapper.ToViewModel(restaurante));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Edit));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, RestauranteViewModel restauranteViewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarTiposRestaurante(restauranteViewModel.IdTipoRestaurante);
                return View(restauranteViewModel);
            }

            try
            {
                var restaurante = RestauranteMapper.ToEntity(restauranteViewModel);
                restaurante.Id = id;
                restauranteService.Edit(restaurante);
                TempData["SuccessMessage"] = "Restaurante alterado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "alterar", restauranteViewModel);
                CarregarTiposRestaurante(restauranteViewModel.IdTipoRestaurante);
                return View(restauranteViewModel);
            }
        }

        public ActionResult Delete(uint id)
        {
            try
            {
                var restaurante = restauranteService.Get(id);
                if (restaurante is null)
                {
                    TempData["ErrorMessage"] = "O restaurante que você deseja excluir não foi encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                return View(RestauranteMapper.ToViewModel(restaurante));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Delete));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RestauranteViewModel restauranteViewModel)
        {
            try
            {
                restauranteService.Delete(restauranteViewModel.Id);
                TempData["SuccessMessage"] = "Restaurante excluído com sucesso.";
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "excluir", restauranteViewModel);
                return View(restauranteViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        private void CarregarTiposRestaurante(uint? tipoSelecionado = null)
        {
            try
            {
                ViewBag.IdTipoRestaurante = new SelectList(
                    tipoRestauranteService.GetAll(),
                    nameof(Tiporestaurante.Id),
                    nameof(Tiporestaurante.Nome),
                    tipoSelecionado);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Erro ao carregar os tipos de restaurante.");
                ViewBag.IdTipoRestaurante = new SelectList(Array.Empty<Tiporestaurante>());
                ModelState.AddModelError(string.Empty, "Não foi possível carregar os tipos de restaurante. Tente novamente mais tarde.");
            }
        }

        private ActionResult TratarErroDeLeitura(Exception exception, string actionName)
        {
            logger.LogError(exception, "Erro ao executar a operação {ActionName} de restaurante.", actionName);
            TempData["ErrorMessage"] = "Não foi possível consultar os restaurantes. Verifique a conexão com o banco de dados e tente novamente.";
            return RedirectToAction(nameof(Index));
        }

        private void AdicionarErroDePersistencia(Exception exception, string operacao, RestauranteViewModel restauranteViewModel)
        {
            logger.LogError(exception, "Erro ao {Operacao} o restaurante {RestauranteId}.", operacao, restauranteViewModel.Id);
            ModelState.AddModelError(string.Empty, ObterMensagemDePersistencia(exception, operacao));
        }

        private static string ObterMensagemDePersistencia(Exception exception, string operacao)
        {
            var causa = exception.GetBaseException();
            if (causa is MySql.Data.MySqlClient.MySqlException mySqlException)
            {
                return mySqlException.Number switch
                {
                    1062 => "Já existe um restaurante cadastrado com este CNPJ.",
                    1451 => "Não é possível excluir este restaurante porque existem dados vinculados a ele.",
                    1452 => "O tipo de restaurante selecionado não existe mais.",
                    _ => "Não foi possível acessar o banco de dados. Verifique se o MySQL está ativo e tente novamente."
                };
            }

            if (exception is DbUpdateException)
            {
                return operacao == "excluir"
                    ? "Não foi possível excluir o restaurante porque existem dados vinculados a ele."
                    : $"Não foi possível {operacao} o restaurante. Verifique os dados informados e tente novamente.";
            }

            return $"Ocorreu um erro inesperado ao {operacao} o restaurante. Tente novamente mais tarde.";
        }
    }
}
