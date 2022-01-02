using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameCatalogue.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MVCGameCatalogue.Data
{
    public class GameCatalogueContext : IdentityDbContext
    {
        public GameCatalogueContext (DbContextOptions<GameCatalogueContext> options)
            : base(options)
        {
        }

        public DbSet<GameCatalogue.Models.Game> GamesModel { get; set; }
    }
}
