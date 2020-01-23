﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WeW.WEB.Models;

namespace WeW.WEB.Repositorio
{
    public class CategoriaAplicacao
    {
        private Base Base;

        public void Inserir()
        {
           
        }
        public void Alterar()
        {

        }

        public List<Categoria> ListarTodos()
        {
            using (Base = new Base())
            {
                string strQuery = "SELECT id, nome FROM Categoria";
                var retorno = Base.ExecutaComandoComRetorno(strQuery);
                return ReaderEmList(retorno);
            }
        }

        public List<Categoria>ListarPorId(int id)
        {
            using (Base = new Base())
            {
                string strQuery = $"SELECT id, nome FROM Categoria WHERE id = '{id}'";
                var retorno = Base.ExecutaComandoComRetorno(strQuery);
                return ReaderEmList(retorno);
            }
        }

        private List<Categoria> ReaderEmList(SqlDataReader reader)
        {
            var categoria = new List<Categoria>();

            while (reader.Read())
            {
                var TempoObjeto = new Categoria()
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Nome = reader["nome"].ToString()
                };
                categoria.Add(TempoObjeto);
            }
            reader.Close();
            return categoria;
        }
    }
}