using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MilanCiCdExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private readonly PokemonContext context;

        public PokemonsController(PokemonContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> GetAll()
            => await context.Pokemons.ToArrayAsync();

        [HttpPost]
        public async Task<Pokemon> AddPokemon([FromBody] Pokemon pokemon)
        {
            context.Pokemons.Add(pokemon);
            await context.SaveChangesAsync();
            return pokemon;
        }
    }
}
