using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SierraMelladoBack.Models
{
    public partial class SierraMelladoDBContext : DbContext
    {
        public SierraMelladoDBContext()
        {
        }

        public SierraMelladoDBContext(DbContextOptions<SierraMelladoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Articulo> Articulos { get; set; } = null!;
        public virtual DbSet<Citum> Cita { get; set; } = null!;
        public virtual DbSet<Especialidad> Especialidads { get; set; } = null!;
        public virtual DbSet<Etiquetum> Etiqueta { get; set; } = null!;
        public virtual DbSet<Horario> Horarios { get; set; } = null!;
        public virtual DbSet<Informe> Informes { get; set; } = null!;
        public virtual DbSet<InformePaciente> InformePacientes { get; set; } = null!;
        public virtual DbSet<Medico> Medicos { get; set; } = null!;
        public virtual DbSet<Mensaje> Mensajes { get; set; } = null!;
        public virtual DbSet<MetodoPago> MetodoPagos { get; set; } = null!;
        public virtual DbSet<Orden> Ordens { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DBConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .HasName("XPKAdmin");

                entity.ToTable("Admin");

                entity.Property(e => e.IdAdmin).HasColumnName("id_admin");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Rol)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("rol")
                    .IsFixedLength();

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("R_8");
            });

            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.CodArticulo)
                    .HasName("XPKArticulo");

                entity.ToTable("Articulo");

                entity.Property(e => e.CodArticulo).HasColumnName("cod_articulo");

                entity.Property(e => e.Autor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("autor");

                entity.Property(e => e.Contenido)
                    .HasColumnType("text")
                    .HasColumnName("contenido");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_crea");

                entity.Property(e => e.FechaMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_mod");

                entity.Property(e => e.IdAdmin).HasColumnName("id_admin");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("imagen");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdAdmin)
                    .HasConstraintName("R_25");

                entity.HasMany(d => d.CodEtiqueta)
                    .WithMany(p => p.CodArticulos)
                    .UsingEntity<Dictionary<string, object>>(
                        "ArticuloEtiquetum",
                        l => l.HasOne<Etiquetum>().WithMany().HasForeignKey("CodEtiqueta").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("R_23"),
                        r => r.HasOne<Articulo>().WithMany().HasForeignKey("CodArticulo").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("R_21"),
                        j =>
                        {
                            j.HasKey("CodArticulo", "CodEtiqueta").HasName("XPKArticulo_Etiqueta");

                            j.ToTable("Articulo_Etiqueta");

                            j.IndexerProperty<int>("CodArticulo").HasColumnName("cod_articulo");

                            j.IndexerProperty<int>("CodEtiqueta").HasColumnName("cod_etiqueta");
                        });
            });

            modelBuilder.Entity<Citum>(entity =>
            {
                entity.HasKey(e => e.IdCita)
                    .HasName("XPKCita");

                entity.Property(e => e.IdCita).HasColumnName("id_cita");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora).HasColumnName("hora");

                entity.Property(e => e.Medico).HasColumnName("medico");

                entity.Property(e => e.NumOrden).HasColumnName("num_orden");

                entity.Property(e => e.Paciente).HasColumnName("paciente");

                entity.Property(e => e.Servicio).HasColumnName("servicio");

                entity.HasOne(d => d.MedicoNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.Medico)
                    .HasConstraintName("R_16");

                entity.HasOne(d => d.NumOrdenNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.NumOrden)
                    .HasConstraintName("R_19");

                entity.HasOne(d => d.PacienteNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.Paciente)
                    .HasConstraintName("R_17");

                entity.HasOne(d => d.ServicioNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.Servicio)
                    .HasConstraintName("R_18");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.CodEspecialidad)
                    .HasName("XPKEspecialidad");

                entity.ToTable("Especialidad");

                entity.Property(e => e.CodEspecialidad).HasColumnName("cod_especialidad");

                entity.Property(e => e.Abreviatura)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("abreviatura");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.HasMany(d => d.IdMedicos)
                    .WithMany(p => p.CodEspecialidads)
                    .UsingEntity<Dictionary<string, object>>(
                        "EspecialidadMedico",
                        l => l.HasOne<Medico>().WithMany().HasForeignKey("IdMedico").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("R_32"),
                        r => r.HasOne<Especialidad>().WithMany().HasForeignKey("CodEspecialidad").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("R_30"),
                        j =>
                        {
                            j.HasKey("CodEspecialidad", "IdMedico").HasName("XPKEspecialidad_Medico");

                            j.ToTable("Especialidad_Medico");

                            j.IndexerProperty<int>("CodEspecialidad").HasColumnName("cod_especialidad");

                            j.IndexerProperty<int>("IdMedico").HasColumnName("id_medico");
                        });
            });

            modelBuilder.Entity<Etiquetum>(entity =>
            {
                entity.HasKey(e => e.CodEtiqueta)
                    .HasName("XPKEtiqueta");

                entity.Property(e => e.CodEtiqueta).HasColumnName("cod_etiqueta");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descripcion")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.CodHorario)
                    .HasName("XPKHorario");

                entity.ToTable("Horario");

                entity.Property(e => e.CodHorario).HasColumnName("cod_horario");

                entity.Property(e => e.Dia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dia");

                entity.Property(e => e.HoraFin).HasColumnName("hora_fin");

                entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");

                entity.Property(e => e.IdMedico).HasColumnName("id_medico");

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.IdMedico)
                    .HasConstraintName("R_33");
            });

            modelBuilder.Entity<Informe>(entity =>
            {
                entity.HasKey(e => e.NumInforme)
                    .HasName("XPKInforme");

                entity.ToTable("Informe");

                entity.Property(e => e.NumInforme).HasColumnName("num_informe");

                entity.Property(e => e.Archivo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("archivo");

                entity.Property(e => e.Asunto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("asunto");

                entity.Property(e => e.FechaEmi)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_emi");

                entity.Property(e => e.IdMedico).HasColumnName("id_medico");

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.Informes)
                    .HasForeignKey(d => d.IdMedico)
                    .HasConstraintName("R_26");
            });

            modelBuilder.Entity<InformePaciente>(entity =>
            {
                entity.HasKey(e => e.NumInforme)
                    .HasName("XPKInforme_paciente");

                entity.ToTable("Informe_Paciente");

                entity.Property(e => e.NumInforme).HasColumnName("num_informe");

                entity.Property(e => e.Archivo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("archivo");

                entity.Property(e => e.Cita).HasColumnName("cita");

                entity.Property(e => e.FechaEmi)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_emi");

                entity.Property(e => e.Medico).HasColumnName("medico");

                entity.Property(e => e.Paciente).HasColumnName("paciente");

                entity.Property(e => e.Resumen)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("resumen");

                entity.HasOne(d => d.CitaNavigation)
                    .WithMany(p => p.InformePacientes)
                    .HasForeignKey(d => d.Cita)
                    .HasConstraintName("R_20");

                entity.HasOne(d => d.MedicoNavigation)
                    .WithMany(p => p.InformePacientes)
                    .HasForeignKey(d => d.Medico)
                    .HasConstraintName("R_28");

                entity.HasOne(d => d.PacienteNavigation)
                    .WithMany(p => p.InformePacientes)
                    .HasForeignKey(d => d.Paciente)
                    .HasConstraintName("R_29");
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.IdMedico)
                    .HasName("XPKMedico");

                entity.ToTable("Medico");

                entity.Property(e => e.IdMedico).HasColumnName("id_medico");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.Celular)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.CodColegiado)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("cod_colegiado");

                entity.Property(e => e.Dni)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("dni");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaNac)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_nac");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("R_7");
            });

            modelBuilder.Entity<Mensaje>(entity =>
            {
                entity.HasKey(e => e.IdMensaje)
                    .HasName("XPKMensaje");

                entity.ToTable("Mensaje");

                entity.Property(e => e.IdMensaje).HasColumnName("id_mensaje");

                entity.Property(e => e.CelularRem)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("celular_rem");

                entity.Property(e => e.Contenido)
                    .HasColumnType("text")
                    .HasColumnName("contenido");

                entity.Property(e => e.CorreoRem)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo_rem");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaEmi)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_emi");

                entity.Property(e => e.IdAdmin).HasColumnName("id_admin");

                entity.Property(e => e.NombreApellidoRem)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre_apellido_rem");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tipo");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.IdAdmin)
                    .HasConstraintName("R_27");
            });

            modelBuilder.Entity<MetodoPago>(entity =>
            {
                entity.HasKey(e => e.CodMetodo)
                    .HasName("XPKMetodo_pago");

                entity.ToTable("Metodo_Pago");

                entity.Property(e => e.CodMetodo).HasColumnName("cod_metodo");

                entity.Property(e => e.Abreviatura)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("abreviatura");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(e => e.NumOrden)
                    .HasName("XPKOrden");

                entity.ToTable("Orden");

                entity.Property(e => e.NumOrden).HasColumnName("num_orden");

                entity.Property(e => e.CodMetodo).HasColumnName("cod_metodo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.ImporteTotal).HasColumnName("importe_total");

                entity.Property(e => e.MontoTotal).HasColumnName("monto_total");

                entity.Property(e => e.Vuelto).HasColumnName("vuelto");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente)
                    .HasName("XPKPaciente");

                entity.ToTable("Paciente");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.Celular)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.Dni)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("dni");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaNac)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_nac");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("R_9");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio)
                    .HasName("XPKServicio");

                entity.ToTable("Servicio");

                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

                entity.Property(e => e.Abreviatura)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("abreviatura");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("XPKUsuario");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido_materno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido_paterno");

                entity.Property(e => e.Clave)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.FechaCrea)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_crea");

                entity.Property(e => e.FechaMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_mod");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
