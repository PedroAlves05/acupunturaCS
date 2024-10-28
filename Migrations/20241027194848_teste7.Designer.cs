﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewAcupuntura;

#nullable disable

namespace NewAcupuntura.Migrations
{
    [DbContext(typeof(AcupunturaDbContext))]
    [Migration("20241027194848_teste7")]
    partial class teste7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NewAcupuntura.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("NewAcupuntura.Entities.Atendimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Agressividade")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Alcool")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Alergia")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Altura")
                        .HasColumnType("REAL");

                    b.Property<bool>("AtividadeFisica")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Auditivo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ciatico")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Concordo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConsultaId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Coracao")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Darwin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Doenca")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Drogas")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Estomago")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Figado")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Filhos")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Fumante")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Garganta")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Genital")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Gonadas")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Joelho")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Mandibular")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Meditacao")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Medular")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MembrosInferiores")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MembrosSuperiores")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MetodoDePagamento")
                        .HasColumnType("TEXT");

                    b.Property<string>("MoraComQuem")
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacao")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Occipital")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Olfato")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Olho")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ombro")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Pancreas")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Pele")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Peso")
                        .HasColumnType("REAL");

                    b.Property<bool>("PressaoAlta")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PressaoBaixa")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Pulmoes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantidadeSessoes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Queixa")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Retal")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Rim")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Sintase")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Talamo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoDeTratamento")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Tragus")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Trigemios")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Vegetariano")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ConsultaId");

                    b.ToTable("Atendimentos");
                });

            modelBuilder.Entity("NewAcupuntura.Entities.Consulta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Cancelado")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExameId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HorarioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExameId");

                    b.HasIndex("HorarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Consultas");
                });

            modelBuilder.Entity("NewAcupuntura.Entities.Exame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("Duracao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Preco")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Exames");
                });

            modelBuilder.Entity("NewAcupuntura.Entities.Horario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Horarios");
                });

            modelBuilder.Entity("NewAcupuntura.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NewAcupuntura.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NewAcupuntura.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewAcupuntura.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NewAcupuntura.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewAcupuntura.Entities.Atendimento", b =>
                {
                    b.HasOne("NewAcupuntura.Entities.Consulta", "Consulta")
                        .WithMany()
                        .HasForeignKey("ConsultaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consulta");
                });

            modelBuilder.Entity("NewAcupuntura.Entities.Consulta", b =>
                {
                    b.HasOne("NewAcupuntura.Entities.Exame", "Exame")
                        .WithMany()
                        .HasForeignKey("ExameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewAcupuntura.Entities.Horario", "Horario")
                        .WithMany()
                        .HasForeignKey("HorarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewAcupuntura.Entities.ApplicationUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Exame");

                    b.Navigation("Horario");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
