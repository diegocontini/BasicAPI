﻿// <auto-generated />
using System;
using BasicAPI.Features.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BasicAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BasicAPI.Models.Entities.Fornecedor", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("FOR_CODIGO");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Codigo"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FOR_DESCRICAO");

                    b.HasKey("Codigo");

                    b.ToTable("TB_FORNECEDORES");
                });

            modelBuilder.Entity("BasicAPI.Models.Entities.Funcionario", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("FUN_CODIGO");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Codigo"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FUN_CPF");

                    b.Property<string>("Funcao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FUN_FUNCAO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FUN_NOME");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FUN_SENHA");

                    b.HasKey("Codigo");

                    b.ToTable("TB_FUNCIONARIOS");
                });

            modelBuilder.Entity("BasicAPI.Models.Entities.Produto", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("PRO_CODIGO");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Codigo"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PRO_DESCRICAO");

                    b.Property<long>("FornecedorCodigo")
                        .HasColumnType("bigint")
                        .HasColumnName("TB_FORNECEDORES_FOR_CODIGO");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("numeric")
                        .HasColumnName("PRO_QUANTIDADE");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric")
                        .HasColumnName("PRO_VALOR");

                    b.HasKey("Codigo");

                    b.ToTable("TB_PRODUTOS");
                });

            modelBuilder.Entity("BasicAPI.Models.Entities.Venda", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("VEN_CODIGO");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Codigo"));

                    b.Property<long>("FuncionarioCodigo")
                        .HasColumnType("bigint")
                        .HasColumnName("TB_FUNCIONARIOS_FUN_CODIGO");

                    b.Property<DateTime>("Horario")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("VEN_HORARIO");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(7,2)")
                        .HasColumnName("VEN_VALOR_TOTAL");

                    b.HasKey("Codigo");

                    b.ToTable("TB_VENDAS");
                });

            modelBuilder.Entity("BasicAPI.Models.Entities.VendaItem", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ITE_CODIGO");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Codigo"));

                    b.Property<long>("ProdutoCodigo")
                        .HasColumnType("bigint")
                        .HasColumnName("TB_PRODUTOS_PRO_CODIGO");

                    b.Property<decimal>("Quantidade")
                        .HasColumnType("numeric")
                        .HasColumnName("ITE_QUANTIDADE");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric")
                        .HasColumnName("ITE_VALOR_PRODUTOS");

                    b.Property<long>("VendaCodigo")
                        .HasColumnType("bigint")
                        .HasColumnName("TB_VENDAS_VEN_CODIGO");

                    b.HasKey("Codigo");

                    b.ToTable("TB_ITENS");
                });
#pragma warning restore 612, 618
        }
    }
}
