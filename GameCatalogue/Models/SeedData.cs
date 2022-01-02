using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCGameCatalogue.Data;
using MVCGameCatalogue;

namespace GameCatalogue.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GameCatalogueContext(serviceProvider.GetRequiredService<DbContextOptions<GameCatalogueContext>>()))
            {
                if (context.GamesModel.Any())
                {
                    return;
                }
                context.GamesModel.AddRange(
                    new Game
                    {
                        Title = "Minecraft",
                        Requirements = "Mojang",
                        Genre = "Sandbox",
                        Description = "E",
                        Price = (decimal)24.99
                    },
                    new Game
                    {
                        Title = "Fortnite",
                        Requirements = "EpicGames",
                        Genre = "Battle Royale",
                        Description = "E",
                        Price = (decimal)0
                    },
                    new Game
                    {
                        Title = "Life",
                        Requirements = "God",
                        Genre = "Suvivor",
                        Description = "E",
                        Price = (decimal)0
                    },
                    new Game
                    {
                        Title = "Super Mario",
                        Requirements = "Nintendo",
                        Genre = "Platformer",
                        Description = "E",
                        Price = (decimal)16.33
                    }
                    );
            }
        }
    }
}
