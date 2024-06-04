﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyTE_Migration.Context;

#nullable disable

namespace MyTE_Migration.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240604181916_MigrationTres")]
    partial class MigrationTres
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyTE_Migration.Areas.Admin.Models.Departamento", b =>
                {
                    b.Property<int>("Departamento_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Departamento_ID"));

                    b.Property<string>("Departamento_Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Departamento_ID");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("MyTE_Migration.Areas.Admin.Models.Funcionario", b =>
                {
                    b.Property<int>("Funcionario_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Funcionario_ID"));

                    b.Property<int>("Departamento_ID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Funcionario_DataContratacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Funcionario_Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Funcionario_NomeCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Funcionario_ID");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("MyTE_Migration.Areas.Admin.Models.HorasTrabalhadas", b =>
                {
                    b.Property<int>("HorasTrabalhadas_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HorasTrabalhadas_ID"));

                    b.Property<int>("Funcionario_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("HorasTabalhadas_Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("HorasTrabalhadas_QtdeHoras")
                        .HasColumnType("int");

                    b.Property<int>("WBS_ID")
                        .HasColumnType("int");

                    b.HasKey("HorasTrabalhadas_ID");

                    b.HasIndex("Funcionario_ID");

                    b.ToTable("HorasTrabalhadas");
                });

            modelBuilder.Entity("MyTE_Migration.Areas.Admin.Models.WBS", b =>
                {
                    b.Property<int>("WBS_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WBS_ID"));

                    b.Property<string>("WBS_Codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WBS_Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WBS_Tipo")
                        .HasColumnType("bit");

                    b.HasKey("WBS_ID");

                    b.ToTable("WBS");
                });

            modelBuilder.Entity("MyTE_Migration.Areas.Admin.Models.HorasTrabalhadas", b =>
                {
                    b.HasOne("MyTE_Migration.Areas.Admin.Models.Funcionario", "funcionario")
                        .WithMany("horasTrabalhadas")
                        .HasForeignKey("Funcionario_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("funcionario");
                });

            modelBuilder.Entity("MyTE_Migration.Areas.Admin.Models.Funcionario", b =>
                {
                    b.Navigation("horasTrabalhadas");
                });
#pragma warning restore 612, 618
        }
    }
}
