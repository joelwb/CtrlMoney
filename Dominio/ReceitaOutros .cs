﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ReceitaOutros : AbstractClassCategoriaReceita
    {
        protected CategoriaReceita categoria = CategoriaReceita.Outros;

        protected override decimal Somar(decimal valor, DateTime data)
        {
            var ReceitaMes = 0M;//vai pegar todas as receitas do mes passado
            var valorSomado = ReceitaMes + valor;
            return valorSomado;
        }
    }
}