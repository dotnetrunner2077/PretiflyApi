using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using PretiflyAPI.Domain.Entities;

namespace PretiflyAPI.Infrastructure.Data;

public partial class PretiflyDbContext : DbContext
{
    public PretiflyDbContext()
    {
    }

    public PretiflyDbContext(DbContextOptions<PretiflyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriesXContent> CategoriesXContents { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<ContentLanguage> ContentLanguages { get; set; }

    public virtual DbSet<ContentLocation> ContentLocations { get; set; }

    public virtual DbSet<ContentsType> ContentsTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<MembershipsClient> MembershipsClients { get; set; }

    public virtual DbSet<MembershipsType> MembershipsTypes { get; set; }

    public virtual DbSet<Subtitle> Subtitles { get; set; }

    public virtual DbSet<View> Views { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CategoriesXContent>(entity =>
        {
            entity.HasKey(e => e.IdCategoriesXContents).HasName("PRIMARY");

            entity.ToTable("categories_x_contents");

            entity.HasIndex(e => e.IdCategories, "idCategories");

            entity.HasIndex(e => e.IdContents, "idContents");

            entity.Property(e => e.IdCategoriesXContents).HasColumnName("idCategories_X_Contents");
            entity.Property(e => e.IdCategories).HasColumnName("idCategories");
            entity.Property(e => e.IdContents).HasColumnName("idContents");

            entity.HasOne(d => d.IdCategoriesNavigation).WithMany(p => p.CategoriesXContents)
                .HasForeignKey(d => d.IdCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categories_x_contents_ibfk_1");

            entity.HasOne(d => d.IdContentsNavigation).WithMany(p => p.CategoriesXContents)
                .HasForeignKey(d => d.IdContents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categories_x_contents_ibfk_2");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategories).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.IdCategories).HasColumnName("idCategories");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClients).HasName("PRIMARY");

            entity.ToTable("clients");

            entity.HasIndex(e => e.Email, "clients_unique").IsUnique();

            entity.HasIndex(e => e.IdCountries, "idCountries");

            entity.Property(e => e.IdClients).HasColumnName("idClients");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IdCountries).HasColumnName("idCountries");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.IdCountriesNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdCountries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clients_ibfk_1");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.IdContents).HasName("PRIMARY");

            entity.ToTable("contents");

            entity.HasIndex(e => e.IdContentsTypes, "idContents_Types");

            entity.Property(e => e.IdContents).HasColumnName("idContents");
            entity.Property(e => e.IdContentsTypes).HasColumnName("idContents_Types");
            entity.Property(e => e.ReleaseYear).HasColumnName("Release_Year");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.IdContentsTypesNavigation).WithMany(p => p.Contents)
                .HasForeignKey(d => d.IdContentsTypes)
                .HasConstraintName("contents_ibfk_2");
        });

        modelBuilder.Entity<ContentLanguage>(entity =>
        {
            entity.HasKey(e => e.IdContentLanguage).HasName("PRIMARY");

            entity.ToTable("content_languages");

            entity.HasIndex(e => e.IdContents, "idContents");

            entity.HasIndex(e => e.IdLanguage, "idLanguage");

            entity.Property(e => e.IdContentLanguage).HasColumnName("idContent_Language");
            entity.Property(e => e.IdContents).HasColumnName("idContents");
            entity.Property(e => e.IdLanguage).HasColumnName("idLanguage");
            entity.Property(e => e.Url).HasMaxLength(500);

            entity.HasOne(d => d.IdContentsNavigation).WithMany(p => p.ContentLanguages)
                .HasForeignKey(d => d.IdContents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("content_languages_ibfk_1");

            entity.HasOne(d => d.IdLanguageNavigation).WithMany(p => p.ContentLanguages)
                .HasForeignKey(d => d.IdLanguage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("content_languages_ibfk_2");
        });

        modelBuilder.Entity<ContentLocation>(entity =>
        {
            entity.HasKey(e => e.IdContentLocation).HasName("PRIMARY");

            entity.ToTable("content_locations");

            entity.HasIndex(e => e.IdContents, "idContents");

            entity.Property(e => e.IdContentLocation).HasColumnName("idContent_Location");
            entity.Property(e => e.Format).HasMaxLength(50);
            entity.Property(e => e.IdContents).HasColumnName("idContents");
            entity.Property(e => e.Quality).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(500);

            entity.HasOne(d => d.IdContentsNavigation).WithMany(p => p.ContentLocations)
                .HasForeignKey(d => d.IdContents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("content_locations_ibfk_1");
        });

        modelBuilder.Entity<ContentsType>(entity =>
        {
            entity.HasKey(e => e.IdContentsTypes).HasName("PRIMARY");

            entity.ToTable("contents_types");

            entity.Property(e => e.IdContentsTypes).HasColumnName("idContents_Types");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountries).HasName("PRIMARY");

            entity.ToTable("countries");

            entity.Property(e => e.IdCountries).HasColumnName("idCountries");
            entity.Property(e => e.CountryCode).HasMaxLength(5);
            entity.Property(e => e.Countryname).HasMaxLength(100);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.IdLanguage).HasName("PRIMARY");

            entity.ToTable("languages");

            entity.Property(e => e.IdLanguage).HasColumnName("idLanguage");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MembershipsClient>(entity =>
        {
            entity.HasKey(e => e.IdMembershipsClients).HasName("PRIMARY");

            entity.ToTable("memberships_clients");

            entity.HasIndex(e => e.IdClients, "Memberships_Clients_clients_FK");

            entity.HasIndex(e => e.IdMemebershipsTypes, "Memberships_Clients_membership_type_FK");

            entity.Property(e => e.IdMembershipsClients).HasColumnName("idMemberships_Clients");
            entity.Property(e => e.DateCancel)
                .HasColumnType("datetime")
                .HasColumnName("Date_Cancel");
            entity.Property(e => e.DateFrom)
                .HasColumnType("datetime")
                .HasColumnName("Date_From");
            entity.Property(e => e.DateTo)
                .HasColumnType("datetime")
                .HasColumnName("Date_To");
            entity.Property(e => e.IdClients).HasColumnName("idClients");
            entity.Property(e => e.IdMemebershipsTypes).HasColumnName("idMemeberships_Types");
            entity.Property(e => e.ValuePay)
                .HasPrecision(8, 2)
                .HasColumnName("Value_Pay");

            entity.HasOne(d => d.IdClientsNavigation).WithMany(p => p.MembershipsClients)
                .HasForeignKey(d => d.IdClients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Memberships_Clients_clients_FK");

            entity.HasOne(d => d.IdMemebershipsTypesNavigation).WithMany(p => p.MembershipsClients)
                .HasForeignKey(d => d.IdMemebershipsTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Memberships_Clients_membership_type_FK");
        });

        modelBuilder.Entity<MembershipsType>(entity =>
        {
            entity.HasKey(e => e.IdMemebershipsTypes).HasName("PRIMARY");

            entity.ToTable("memberships_types");

            entity.Property(e => e.IdMemebershipsTypes).HasColumnName("idMemeberships_Types");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Value).HasPrecision(8, 2);
        });

        modelBuilder.Entity<Subtitle>(entity =>
        {
            entity.HasKey(e => e.IdSubtitle).HasName("PRIMARY");

            entity.ToTable("subtitles");

            entity.HasIndex(e => e.IdContents, "idContents");

            entity.HasIndex(e => e.IdLanguage, "idLanguage");

            entity.Property(e => e.IdSubtitle).HasColumnName("idSubtitle");
            entity.Property(e => e.Format).HasMaxLength(50);
            entity.Property(e => e.IdContents).HasColumnName("idContents");
            entity.Property(e => e.IdLanguage).HasColumnName("idLanguage");
            entity.Property(e => e.Url).HasMaxLength(500);

            entity.HasOne(d => d.IdContentsNavigation).WithMany(p => p.Subtitles)
                .HasForeignKey(d => d.IdContents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subtitles_ibfk_1");

            entity.HasOne(d => d.IdLanguageNavigation).WithMany(p => p.Subtitles)
                .HasForeignKey(d => d.IdLanguage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subtitles_ibfk_2");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.IdViews).HasName("PRIMARY");

            entity.ToTable("views");

            entity.HasIndex(e => e.IdClients, "idClients");

            entity.HasIndex(e => e.IdContents, "idContents");

            entity.Property(e => e.IdViews).HasColumnName("idViews");
            entity.Property(e => e.IdClients).HasColumnName("idClients");
            entity.Property(e => e.IdContents).HasColumnName("idContents");
            entity.Property(e => e.ViewDate)
                .HasColumnType("datetime")
                .HasColumnName("View_Date");

            entity.HasOne(d => d.IdClientsNavigation).WithMany(p => p.Views)
                .HasForeignKey(d => d.IdClients)
                .HasConstraintName("views_clients_FK");

            entity.HasOne(d => d.IdContentsNavigation).WithMany(p => p.Views)
                .HasForeignKey(d => d.IdContents)
                .HasConstraintName("views_contents_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
