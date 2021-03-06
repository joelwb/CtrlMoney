﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityAcessoDados.TypeConfig
{
    class UsuarioConfig : GenericConfig<Usuario>
    {
        protected override void ConfigurarCamposTabela()
        {
            Property(p => p.Id)
                .HasMaxLength(128)
                .HasColumnName("id");

            Property(p => p.Login)
                .IsRequired()
                .HasColumnName("login")
                .HasMaxLength(150);

            Property(p => p.Senha)
                .IsRequired()
                .HasColumnName("senha")
                .HasMaxLength(128);

            Property(p => p.DataCadastro)
                .HasColumnName("data_cadastro")
                .IsRequired();

            HasIndex(p => p.Login).IsUnique();
        }

        protected override void ConfigurarChavePrimaria()
        {
            HasKey(p => p.Id);
        }

        protected override void ConfigurarChavesEstrangeiras()
        {
            HasRequired(p => p.Pessoa)
                .WithRequiredDependent(p => p.Usuario);
        }

        protected override void ConfigurarNomeTabela()
        {
            ToTable("pessoa_usuario");
        }
    }
}
