using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MilanCiCdExample;
using MilanCiCdExample.Controllers;

namespace MilanCiCdTests
{
    public class DemoTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(1 == 1);
        }

        [Fact]
        public async Task PokemonIntegrationTest()
        {
            // Create a db context
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PokemonContext>();
            optionsBuilder.UseSqlServer(config["ConnectionStrings:DefaultConnection"]);

            var context = new PokemonContext(optionsBuilder.Options);

            // Just to make sure: Delete all existing customer in DB
            //context.Pokemons.RemoveRange(await context.Pokemons.ToArrayAsync());
            //await context.SaveChangesAsync();

            // La manera correcta para levantar DB
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // Create a controller
            var controller = new PokemonsController(context);

            // Add a pokemon
            await controller.AddPokemon(new Pokemon() { Name = "Charmander" });

            // Check: Does getall return the added pokemon?
            IEnumerable<Pokemon> pokemons = await controller.GetAll();

            Assert.Single(pokemons);
            Assert.Equal("Charmander", pokemons.First().Name);
        }
    }
}