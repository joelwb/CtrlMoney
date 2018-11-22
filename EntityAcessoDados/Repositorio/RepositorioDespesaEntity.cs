﻿using Dominio;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityAcessoDados.Repositorio
{
    public class RepositorioDespesaEntity : RepositorioGenericoEntity<Despesa, int> , IRepositorioHistorico<Despesa, int>


    {
        public RepositorioDespesaEntity(DbContext contexto) : base(contexto)
        {
        }

        public List<Despesa> ListarHistorico(string pessoaId, int ano, int mes)
        {
            DateTime inicioMes = new DateTime(ano, mes, 1);
            DateTime finalMes = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));

            List<SemParcelamento> semParcelamentos = _contexto.Set<SemParcelamento>()
                                                    .Where(p => p.Pessoa.Id == pessoaId && p.DataCompra > inicioMes && p.DataCompra < finalMes)
                                                    .ToList();

            List<Parcelamento> parcelamentos = _contexto.Set<Parcelamento>()
                                                .Where(p => p.Pessoa.Id == pessoaId && 
                                                DbFunctions.DiffMonths(p.DataCompra, finalMes) >= 0 && 
                                                DbFunctions.DiffMonths(p.DataCompra,finalMes) <= p.NumParcelas)
                                                .ToList();

            List<Despesa> despesas = new List<Despesa>(semParcelamentos);
            despesas.AddRange(parcelamentos);

            return despesas.OrderBy(p => p.DataCompra).OrderByDescending(p => p.Valor).ToList();
        }

        public List<Despesa> ListarHistoricoPorCartao(int cartaoId, int ano, int mes)
        {
            DateTime inicioMes = new DateTime(ano, mes, 1);
            DateTime finalMes = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));

            List<Parcelamento> parcelamentos = _contexto.Set<Parcelamento>().Where(p => p.Cartao.Id == cartaoId && p.DataCompra > inicioMes && p.DataCompra < finalMes).ToList();
            List<Despesa> despesas = new List<Despesa>();
            foreach(Despesa item in parcelamentos)
            {
                despesas.Add(item);
            }
            return despesas;
        }
    }
}
