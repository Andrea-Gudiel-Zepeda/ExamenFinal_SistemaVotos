using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExamenFinal_SistemaVotos.Models;

public partial class SistemaVotosContext : DbContext
{
    public SistemaVotosContext()
    {
    }

    public SistemaVotosContext(DbContextOptions<SistemaVotosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voto> Votos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DBSVotos");
            optionsBuilder.UseMySQL(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("candidato");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadPiñones).HasColumnName("cantidad_piñones");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Partido)
                .HasMaxLength(255)
                .HasColumnName("partido");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(255)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Voto>(entity =>
        {
            entity.HasKey(e => e.VotoId).HasName("PRIMARY");

            entity.ToTable("votos");

            entity.HasIndex(e => e.CandidatoId, "candidato_id");

            entity.HasIndex(e => e.VotanteId, "votante_id");

            entity.Property(e => e.VotoId).HasColumnName("voto_id");
            entity.Property(e => e.CandidatoId).HasColumnName("candidato_id");
            entity.Property(e => e.FechaHora)
                .HasMaxLength(255)
                .HasColumnName("fecha_hora");
            entity.Property(e => e.Ip)
                .HasMaxLength(255)
                .HasColumnName("ip");
            entity.Property(e => e.VotanteId).HasColumnName("votante_id");
            entity.Property(e => e.Voto1).HasColumnName("voto");

            entity.HasOne(d => d.Candidato).WithMany(p => p.Votos)
                .HasForeignKey(d => d.CandidatoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("votos_ibfk_1");

            entity.HasOne(d => d.Votante).WithMany(p => p.Votos)
                .HasForeignKey(d => d.VotanteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("votos_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
