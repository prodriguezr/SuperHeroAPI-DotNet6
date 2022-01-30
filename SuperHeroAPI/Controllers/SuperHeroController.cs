using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _Context;

        public SuperHeroController(DataContext Context)
        {
            _Context = Context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _Context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _Context.SuperHeroes.FindAsync(id);

            if (hero == null)
                return NotFound("Hero not found");

            return Ok(hero);
        } 

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _Context.SuperHeroes.Add(hero);

            await _Context.SaveChangesAsync();

            return Ok(await _Context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var hero = await _Context.SuperHeroes.FindAsync(updatedHero.Id);

            if (hero == null)
                return NotFound("Hero not found. Nothing to update");

            hero.Name = updatedHero.Name;
            hero.FirstName = updatedHero.FirstName;
            hero.LastName = updatedHero.LastName;
            hero.Place = updatedHero.Place;

            await _Context.SaveChangesAsync();

            return Ok(await _Context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id) 
        {
            if (id == 0)
                return NotFound("Hero not found");

            var hero = await _Context.SuperHeroes.FindAsync(id); //heroes.Find(h => h.Id == id);

            if (hero == null)
                return NotFound("Hero not found");

            _Context.SuperHeroes.Remove(hero);

            await _Context.SaveChangesAsync();

            return Ok(await _Context.SuperHeroes.ToListAsync());
        }
    }
}
