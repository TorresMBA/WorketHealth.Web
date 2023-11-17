﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorketHealth.DataAccess;

#nullable disable

namespace WorketHealth.DataAccess.Migrations
{
    [DbContext(typeof(WorketHealthContext))]
    [Migration("20231115230649_MigrationAddRuc")]
    partial class MigrationAddRuc
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.AppUsuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Fecha.Anho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Anho");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Personal.Personal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fec_Nacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Primer_Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Primer_Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Segundo_Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Segundo_Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Personal");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.Aptitud", b =>
                {
                    b.Property<int>("ID_APTITUD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_APTITUD"), 1L, 1);

                    b.Property<string>("COD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DESCRIPCION")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_APTITUD");

                    b.ToTable("Aptitudes");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.EnfermedadComun", b =>
                {
                    b.Property<int>("ID_ENFERMEDAD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_ENFERMEDAD"), 1L, 1);

                    b.Property<string>("COD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DESCRIPCION")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_ENFERMEDAD");

                    b.ToTable("EnfermedadesComunes");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.EnfermedadProfesional", b =>
                {
                    b.Property<int>("ID_ENFERMEDAD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_ENFERMEDAD"), 1L, 1);

                    b.Property<string>("COD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DESCRIPCION")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_ENFERMEDAD");

                    b.ToTable("EnfermedadesProfesionales");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.EnfermedadRelacionadaTrabajo", b =>
                {
                    b.Property<int>("ID_ENFERMEDAD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_ENFERMEDAD"), 1L, 1);

                    b.Property<string>("COD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DESCRIPCION")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_ENFERMEDAD");

                    b.ToTable("EnfermedadesRelacionadasTrabajo");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoEnfermedad", b =>
                {
                    b.Property<int>("SeguimientoMedicoId")
                        .HasColumnType("int");

                    b.Property<int>("EnfermedadComunId")
                        .HasColumnType("int");

                    b.HasKey("SeguimientoMedicoId", "EnfermedadComunId");

                    b.HasIndex("EnfermedadComunId");

                    b.ToTable("SeguimientoEnfermedad");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoEnfermedadProfesional", b =>
                {
                    b.Property<int>("SeguimientoMedicoId")
                        .HasColumnType("int");

                    b.Property<int>("EnfermedadProfesionalId")
                        .HasColumnType("int");

                    b.HasKey("SeguimientoMedicoId", "EnfermedadProfesionalId");

                    b.HasIndex("EnfermedadProfesionalId");

                    b.ToTable("SeguimientoEnfermedadProfesional");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoEnfermedadTrabajo", b =>
                {
                    b.Property<int>("SeguimientoMedicoId")
                        .HasColumnType("int");

                    b.Property<int>("EnfermedadRelacionadaTrabajoId")
                        .HasColumnType("int");

                    b.HasKey("SeguimientoMedicoId", "EnfermedadRelacionadaTrabajoId");

                    b.HasIndex("EnfermedadRelacionadaTrabajoId");

                    b.ToTable("SeguimientoEnfermedadTrabajo");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoMedico", b =>
                {
                    b.Property<int>("ID_SEG")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_SEG"), 1L, 1);

                    b.Property<string>("ANHO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AREA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FECHA_EXAM")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_SEG_APT")
                        .HasColumnType("int");

                    b.Property<int>("ID_TIPO_EXAMEN")
                        .HasColumnType("int");

                    b.Property<string>("MES")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PUESTO_DE_TRABAJO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RESTRICIONES")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RUC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_SEG");

                    b.HasIndex("ID_SEG_APT");

                    b.HasIndex("ID_TIPO_EXAMEN");

                    b.ToTable("SeguimientoMedicos");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.TipoExamen", b =>
                {
                    b.Property<int>("ID_TIPO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_TIPO"), 1L, 1);

                    b.Property<string>("COD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DESCRIPCION")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_TIPO");

                    b.ToTable("TipoExamenes");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Tablas.Ruc", b =>
                {
                    b.Property<int>("ID_RUC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_RUC"), 1L, 1);

                    b.Property<int>("COD_RUC")
                        .HasColumnType("int");

                    b.Property<string>("DESCRIPCION_RUC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NOM_RUC")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_RUC");

                    b.ToTable("Ruc");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.UserMenuAccess", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("UserMenuAccess");
                });

            modelBuilder.Entity("WorketHealth.Domain.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Accion")
                        .HasColumnType("int");

                    b.Property<int>("Controlador")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_Padre")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedByUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Menus");
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
                    b.HasOne("WorketHealth.DataAccess.Models.AppUsuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WorketHealth.DataAccess.Models.AppUsuario", null)
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

                    b.HasOne("WorketHealth.DataAccess.Models.AppUsuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WorketHealth.DataAccess.Models.AppUsuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoEnfermedad", b =>
                {
                    b.HasOne("WorketHealth.DataAccess.Models.Registros.EnfermedadComun", "EnfermedadComun")
                        .WithMany("SeguimientoMedicos")
                        .HasForeignKey("EnfermedadComunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorketHealth.DataAccess.Models.Registros.SeguimientoMedico", "SeguimientoMedico")
                        .WithMany("Enfermedades")
                        .HasForeignKey("SeguimientoMedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnfermedadComun");

                    b.Navigation("SeguimientoMedico");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoEnfermedadProfesional", b =>
                {
                    b.HasOne("WorketHealth.DataAccess.Models.Registros.EnfermedadProfesional", "EnfermedadProfesional")
                        .WithMany("SeguimientoMedicos")
                        .HasForeignKey("EnfermedadProfesionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorketHealth.DataAccess.Models.Registros.SeguimientoMedico", "SeguimientoMedico")
                        .WithMany("EnfermedadesProfesionales")
                        .HasForeignKey("SeguimientoMedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnfermedadProfesional");

                    b.Navigation("SeguimientoMedico");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoEnfermedadTrabajo", b =>
                {
                    b.HasOne("WorketHealth.DataAccess.Models.Registros.EnfermedadRelacionadaTrabajo", "EnfermedadRelacionadaTrabajo")
                        .WithMany("SeguimientoMedicos")
                        .HasForeignKey("EnfermedadRelacionadaTrabajoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorketHealth.DataAccess.Models.Registros.SeguimientoMedico", "SeguimientoMedico")
                        .WithMany("EnfermedadesTrabajo")
                        .HasForeignKey("SeguimientoMedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnfermedadRelacionadaTrabajo");

                    b.Navigation("SeguimientoMedico");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoMedico", b =>
                {
                    b.HasOne("WorketHealth.DataAccess.Models.Registros.Aptitud", "Aptitud")
                        .WithMany()
                        .HasForeignKey("ID_SEG_APT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorketHealth.DataAccess.Models.Registros.TipoExamen", "TipoExamen")
                        .WithMany()
                        .HasForeignKey("ID_TIPO_EXAMEN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aptitud");

                    b.Navigation("TipoExamen");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.UserMenuAccess", b =>
                {
                    b.HasOne("WorketHealth.Domain.Entities.Menu", "Menu")
                        .WithMany("UserMenuAccess")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorketHealth.DataAccess.Models.AppUsuario", "User")
                        .WithMany("UserMenuAccess")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.AppUsuario", b =>
                {
                    b.Navigation("UserMenuAccess");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.EnfermedadComun", b =>
                {
                    b.Navigation("SeguimientoMedicos");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.EnfermedadProfesional", b =>
                {
                    b.Navigation("SeguimientoMedicos");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.EnfermedadRelacionadaTrabajo", b =>
                {
                    b.Navigation("SeguimientoMedicos");
                });

            modelBuilder.Entity("WorketHealth.DataAccess.Models.Registros.SeguimientoMedico", b =>
                {
                    b.Navigation("Enfermedades");

                    b.Navigation("EnfermedadesProfesionales");

                    b.Navigation("EnfermedadesTrabajo");
                });

            modelBuilder.Entity("WorketHealth.Domain.Entities.Menu", b =>
                {
                    b.Navigation("UserMenuAccess");
                });
#pragma warning restore 612, 618
        }
    }
}