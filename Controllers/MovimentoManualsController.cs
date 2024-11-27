using SinqiaParibas45.Models;
using SinqiaParibas45.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SinqiaParibas45.Controllers
{
  

    public class MovimentoManualsController : Controller
    {
        private readonly AppDbContext _context;

        public MovimentoManualsController() : this(new AppDbContext())
        {
        }

        public MovimentoManualsController(AppDbContext context)
        {
            _context = context;
        }

       // private readonly AppDbContext _context = new AppDbContext();

        // GET: MovimentoManuals
        public ActionResult Index()
        {
            var appDbContext = _context.MovimentosManuais.Include(m => m.ProdutoCosif);

            var viewModel = new MovimentoManualViewModel
            {
                Movimentos = appDbContext.ToList(),
                MovimentoAtual = new MovimentoManual { COD_USUARIO = "TESTE" }
            };

            ViewBag.Produtos = new SelectList(_context.Produtos, "COD_PRODUTO", "DES_PRODUTO");
            ViewBag.ProdutoCosifs = new SelectList(_context.ProdutoCosifs, "COD_COSIF", "COD_CLASSIFICACAO");

            return View(viewModel);
        }

        public ActionResult Create()
        {
            ViewBag.Produtos = new SelectList(_context.Produtos, "COD_PRODUTO", "DES_PRODUTO");
            ViewBag.ProdutoCosifs = new SelectList(_context.ProdutoCosifs, "COD_COSIF", "COD_CLASSIFICACAO");
            return View();
        }

        // POST: MovimentoManuals/Create
        [HttpPost]
        public ActionResult Create(MovimentoManualViewModel movimentoManual)
        {
            try
            {
                movimentoManual.MovimentoAtual.DAT_MOVIMENTO = DateTime.Now;
                movimentoManual.MovimentoAtual.COD_USUARIO = "TESTE";

                var ultimoLancamento = _context.MovimentosManuais
                    .Where(m => m.DAT_MES == movimentoManual.MovimentoAtual.DAT_MES && m.DAT_ANO == movimentoManual.MovimentoAtual.DAT_ANO)
                    .OrderByDescending(m => m.NUM_LANCAMENTO)
                    .FirstOrDefault();

                movimentoManual.MovimentoAtual.NUM_LANCAMENTO = (ultimoLancamento?.NUM_LANCAMENTO ?? 0) + 1;

                _context.MovimentosManuais.Add(movimentoManual.MovimentoAtual);
                _context.SaveChanges();

                var movimentos = _context.MovimentosManuais
                    .Include(m => m.ProdutoCosif.Produto)
                    .ToList();

                return PartialView("_MovimentosGrid", movimentos);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Erro ao salvar no banco: {ex.Message}");
            }
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
