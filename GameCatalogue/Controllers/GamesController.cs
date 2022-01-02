using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameCatalogue.Models;
using MVCGameCatalogue.Data;
using GameCatalogue.Models.BindingModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GameCatalogue.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameCatalogueContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;


        public GamesController(GameCatalogueContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            this._hostEnviroment = hostEnviroment;
        }

        // GET: GamesModels
        /*public async Task<IActionResult> Index(string searchString)
        {
            var games = from g in _context.GamesModel select g;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(m => m.Title.Contains(searchString));
            }
            return View(await games.ToListAsync());
        }*/

        public async Task<IActionResult> Index(string gameGenre, string searchString)
        {
            IQueryable<string> genreQuery = from g in _context.GamesModel orderby g.Genre select g.Genre;

            IQueryable<Game> games = from g in _context.GamesModel select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(m => m.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(gameGenre))
            {
                games = games.Where(g => g.Genre.Equals(gameGenre));
            }

            GameGenreViewModel gameGenreViewModel = new GameGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Games = await games.ToListAsync()
            };

            return View(gameGenreViewModel);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return $"From [HttpPost]Index filter on " + searchString;
        }

        // GET: GamesModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamesModel = await _context.GamesModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamesModel == null)
            {
                return NotFound();
            }

            return View(gamesModel);
        }

        // GET: GamesModels/Create
        public IActionResult Create()
        {
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Genre,Category,ReleaseDate,Price,ImageUrl")] CreateGameBindingModel game)
        {
            if (ModelState.IsValid)
            {
                string wwwRoothPath = _hostEnviroment.WebRootPath;

                string filePath = Path.Combine(wwwRoothPath, "/images");

                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }*/

        // POST: GamesModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Requirements,Description,Genre,Price,ImageUrl")] CreateGameBindingModel createModel)
        {

            if (ModelState.IsValid)
            {
                string wwwRoothPath = _hostEnviroment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(createModel.ImageUrl.FileName);

                string fileExt = Path.GetExtension(createModel.ImageUrl.FileName);

                string generatedName = $"{fileName}_{Guid.NewGuid()}{fileExt}";

                string path = _hostEnviroment.WebRootPath;
                string imagePath = Path.Combine($"{path}/images/", generatedName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    createModel.ImageUrl.CopyTo(fileStream);
                }

                Game game = new Game
                {
                    Title = createModel.Title,
                    Requirements = createModel.Requirements,
                    Description = createModel.Description,
                    Genre = createModel.Genre,
                    Price = createModel.Price,
                    ImageUrl = generatedName
                };

                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createModel);
        }


        // GET: GamesModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamesModel = await _context.GamesModel.FindAsync(id);
            EditGameBindingModel editModel = new EditGameBindingModel
            {
                Title = gamesModel.Title,
                Requirements = gamesModel.Requirements,
                Description = gamesModel.Description,
                Genre = gamesModel.Genre,
                Price = gamesModel.Price
            };

            if (gamesModel == null)
            {
                return NotFound();
            }
            return View(editModel);
        }

        // POST: GamesModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Requirements,Description,Genre,Price,ImageUrl")] EditGameBindingModel editModel)
        {
            if (id != editModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRoothPath = _hostEnviroment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(editModel.ImageUrl.FileName);

                string fileExt = Path.GetExtension(editModel.ImageUrl.FileName);

                string generatedName = $"{fileName}_{Guid.NewGuid()}{fileExt}";

                string path = _hostEnviroment.WebRootPath;
                string imagePath = Path.Combine($"{path}/images/", generatedName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    editModel.ImageUrl.CopyTo(fileStream);
                }
                try
                {
                    Game game = _context.GamesModel.Where(m => m.Id == editModel.Id).FirstOrDefault();
                    game.Title = editModel.Title;
                    game.Requirements = editModel.Requirements;
                    game.Description = editModel.Description;
                    game.Genre = editModel.Genre;
                    game.Price = editModel.Price;
                    game.ImageUrl = generatedName;

                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesModelExists(editModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editModel);
        }

        // GET: GamesModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamesModel = await _context.GamesModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamesModel == null)
            {
                return NotFound();
            }

            return View(gamesModel);
        }

        // POST: GamesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, bool notUsed)
        {
            var gamesModel = await _context.GamesModel.FindAsync(id);
            _context.GamesModel.Remove(gamesModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamesModelExists(int id)
        {
            return _context.GamesModel.Any(e => e.Id == id);
        }
    }
}
