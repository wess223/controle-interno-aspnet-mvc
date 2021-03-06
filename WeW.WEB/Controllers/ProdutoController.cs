﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeW.WEB.Models;
using WeW.WEB.Repositorio;
using X.PagedList;

namespace WeW.WEB.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        ProdutoAplicacao appProduto = new ProdutoAplicacao();
        CategoriaAplicacao appCategoria = new CategoriaAplicacao();
        // GET: Produto       
        public ActionResult Index(int pagina = 1)
        {
            var listarProdutos = appProduto.ListarTodos().ToPagedList(pagina, 8);

            return View(listarProdutos);
        }

        public ActionResult Pesquisar(string Pesquisa, int pagina = 1)
        {
            var listarProdutos = appProduto.ListarTodos().ToPagedList(pagina, 8);
            ViewBag.Pesquisa = Pesquisa;
            if (!string.IsNullOrEmpty(Pesquisa))
            {
                var filtro = appProduto.ListarFiltro(Pesquisa).ToPagedList(pagina, 8);
                return PartialView("_Produto", filtro);
            }

            return PartialView("_Produto", listarProdutos);
        }

        public ActionResult Cadastrar()
        {
            ViewBag.ListarCategoria = new SelectList(appCategoria.ListarTodos(), "id", "nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (appProduto.ListarPorId(produto.Cod) != null)
                {
                    ViewBag.ListarCategoria = new SelectList(appCategoria.ListarTodos(), "id", "nome");                   
                    TempData["error"] = "Código já cadastrado.";
                    return View(produto);
                }
                else
                {
                    appProduto.Inserir(produto);
                    TempData["success"] = "Produto cadastrado com sucesso.";
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewBag.ListarCategoria = new SelectList(appCategoria.ListarTodos(), "id", "nome");

            return View(produto);
        }

        public ActionResult Alterar(long id)
        {
            var produto = appProduto.ListarPorId(id);
            ViewBag.ListarCategoria = new SelectList(appCategoria.ListarTodos(), "id", "nome");
            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alterar(Produto produto)
        {
            ViewBag.ListarCategoria = new SelectList(appCategoria.ListarTodos(), "id", "nome");

            if (ModelState.IsValid)
            {
                appProduto.Alterar(produto);
                TempData["success"] = " Alterado com sucesso";
                return RedirectToAction(nameof(Index));

            }
            return View(produto);
        }

        public ActionResult Detalhes(long id)
        {
            var produto = appProduto.ListarPorId(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }
    }
}