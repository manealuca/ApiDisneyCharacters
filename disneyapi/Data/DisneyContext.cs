using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using disneyapi;
using disneyapi.Controllers;
using Microsoft.EntityFrameworkCore;
namespace Data
{
    public class DisneyContext : DbContext
    {
        public DbSet<CharacterEntity> Characters { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<CharacterMovieEntity> CharacterEntityMovieEntity { get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {

                options.UseSqlServer("Server=LAPTOP-TU4DOCKA;Database=DysneyAPI;User=root;Password=8ak87xwa;MultipleActiveResultSets=true");
                options.EnableSensitiveDataLogging();

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterMovieEntity>().HasKey(x => new { x.CharactersCharacterId, x.MoviesMovieId });
             modelBuilder.Entity<CharacterEntity>().HasMany(c => c.Movies).WithMany(m =>m.Characters)
                 .UsingEntity<CharacterMovieEntity>(cm => cm.HasOne(prop => prop.Movie).WithMany().HasForeignKey(prop => prop.MoviesMovieId),
                 cm => cm.HasOne(prop => prop.Character).WithMany().HasForeignKey(prop => prop.MoviesMovieId),
                 cm => {
                     cm.HasKey(prop => new { prop.CharactersCharacterId, prop.MoviesMovieId });
                 });
            modelBuilder.Entity<CategoryEntity>().HasMany(c => c.Movies).WithOne(m => m.Category).HasForeignKey(c => c.CategoryId);

            /*modelBuilder.Entity<MovieEntity>()
                .HasOne<CategoryEntity>(m => m.Category)
               .WithMany(c => c.Movies).HasForeignKey(m => m.CategoryId);*/
        
        }
    }
}