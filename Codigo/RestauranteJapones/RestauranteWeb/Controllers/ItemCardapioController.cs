using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestauranteWeb.Mappers;
using RestauranteWeb.Models;

namespace RestauranteWeb.Controllers
{
    public class ItemCardapioController : Controller
    {
        private readonly IItemCardapioService itemCardapioService;
        private readonly IRestauranteService restauranteService;
        private readonly IGrupoCardapioService grupoCardapioService;
        private readonly ILogger<ItemCardapioController> logger;

        public ItemCardapioController(
            IItemCardapioService itemCardapioService,
            IRestauranteService restauranteService,
            IGrupoCardapioService grupoCardapioService,
            ILogger<ItemCardapioController> logger)
        {
            this.itemCardapioService = itemCardapioService;
            this.restauranteService = restauranteService;
            this.grupoCardapioService = grupoCardapioService;
            this.logger = logger;
        }

        public ActionResult Index()
        {
            try
            {
                return View(ItemCardapioMapper.ToViewModel(itemCardapioService.GetAll()));
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Erro ao listar os itens do cardápio.");
                TempData["ErrorMessage"] = "Não foi possível consultar os itens do cardápio. Verifique a conexão com o banco de dados e tente novamente.";
                return View(new List<ItemCardapioViewModel>());
            }
        }

        public ActionResult Details(uint id)
        {
            try
            {
                var itemCardapio = itemCardapioService.Get((int)id);
                if (itemCardapio is null)
                {
                    TempData["ErrorMessage"] = "O item do cardápio solicitado não foi encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                return View(ItemCardapioMapper.ToViewModel(itemCardapio));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Details));
            }
        }

        public ActionResult Create()
        {
            CarregarListas();
            return View(new ItemCardapioViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCardapioViewModel itemCardapioViewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarListas(itemCardapioViewModel.IdRestaurante, itemCardapioViewModel.IdGrupoCardapio);
                return View(itemCardapioViewModel);
            }

            try
            {
                itemCardapioService.Create(ItemCardapioMapper.ToEntity(itemCardapioViewModel));
                TempData["SuccessMessage"] = "Item do cardápio cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "cadastrar", itemCardapioViewModel);
                CarregarListas(itemCardapioViewModel.IdRestaurante, itemCardapioViewModel.IdGrupoCardapio);
                return View(itemCardapioViewModel);
            }
        }

        public ActionResult Edit(uint id)
        {
            try
            {
                var itemCardapio = itemCardapioService.Get((int)id);
                if (itemCardapio is null)
                {
                    TempData["ErrorMessage"] = "O item do cardápio que você deseja editar não foi encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                CarregarListas(itemCardapio.IdRestaurante, itemCardapio.IdGrupoCardapio);
                return View(ItemCardapioMapper.ToViewModel(itemCardapio));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Edit));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, ItemCardapioViewModel itemCardapioViewModel)
        {
            if (!ModelState.IsValid)
            {
                CarregarListas(itemCardapioViewModel.IdRestaurante, itemCardapioViewModel.IdGrupoCardapio);
                return View(itemCardapioViewModel);
            }

            try
            {
                var itemCardapio = ItemCardapioMapper.ToEntity(itemCardapioViewModel);
                itemCardapio.Id = id;
                itemCardapioService.Edit(itemCardapio);
                TempData["SuccessMessage"] = "Item do cardápio alterado com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "alterar", itemCardapioViewModel);
                CarregarListas(itemCardapioViewModel.IdRestaurante, itemCardapioViewModel.IdGrupoCardapio);
                return View(itemCardapioViewModel);
            }
        }

        public ActionResult Delete(uint id)
        {
            try
            {
                var itemCardapio = itemCardapioService.Get((int)id);
                if (itemCardapio is null)
                {
                    TempData["ErrorMessage"] = "O item do cardápio que você deseja excluir não foi encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                return View(ItemCardapioMapper.ToViewModel(itemCardapio));
            }
            catch (Exception exception)
            {
                return TratarErroDeLeitura(exception, nameof(Delete));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ItemCardapioViewModel itemCardapioViewModel)
        {
            try
            {
                itemCardapioService.Delete((int)itemCardapioViewModel.Id);
                TempData["SuccessMessage"] = "Item do cardápio excluído com sucesso.";
            }
            catch (Exception exception)
            {
                AdicionarErroDePersistencia(exception, "excluir", itemCardapioViewModel);
                return View(itemCardapioViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        private void CarregarListas(uint? restauranteSelecionado = null, uint? grupoSelecionado = null)
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

            try
            {
                ViewBag.IdGrupoCardapio = new SelectList(
                    grupoCardapioService.GetAll(),
                    nameof(Grupocardapio.Id),
                    nameof(Grupocardapio.Nome),
                    grupoSelecionado);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Erro ao carregar os grupos do cardápio.");
                ViewBag.IdGrupoCardapio = new SelectList(Array.Empty<Grupocardapio>());
                ModelState.AddModelError(string.Empty, "Não foi possível carregar os grupos do cardápio. Tente novamente mais tarde.");
            }
        }

        private ActionResult TratarErroDeLeitura(Exception exception, string actionName)
        {
            logger.LogError(exception, "Erro ao executar a operação {ActionName} de item do cardápio.", actionName);
            TempData["ErrorMessage"] = "Não foi possível consultar os itens do cardápio. Verifique a conexão com o banco de dados e tente novamente.";
            return RedirectToAction(nameof(Index));
        }

        private void AdicionarErroDePersistencia(Exception exception, string operacao, ItemCardapioViewModel itemCardapioViewModel)
        {
            logger.LogError(exception, "Erro ao {Operacao} o item do cardápio {ItemCardapioId}.", operacao, itemCardapioViewModel.Id);
            ModelState.AddModelError(string.Empty, ObterMensagemDePersistencia(exception, operacao));
        }

        private static string ObterMensagemDePersistencia(Exception exception, string operacao)
        {
            var causa = exception.GetBaseException();
            if (causa is MySql.Data.MySqlClient.MySqlException mySqlException)
            {
                return mySqlException.Number switch
                {
                    1451 => "Não é possível excluir este item do cardápio porque existem pedidos vinculados a ele.",
                    1452 => "O restaurante ou o grupo do cardápio selecionado não existe mais.",
                    _ => "Não foi possível acessar o banco de dados. Verifique se o MySQL está ativo e tente novamente."
                };
            }

            if (exception is DbUpdateException)
            {
                return operacao == "excluir"
                    ? "Não foi possível excluir o item do cardápio porque existem pedidos vinculados a ele."
                    : $"Não foi possível {operacao} o item do cardápio. Verifique os dados informados e tente novamente.";
            }

            return $"Ocorreu um erro inesperado ao {operacao} o item do cardápio. Tente novamente mais tarde.";
        }
    }
}
