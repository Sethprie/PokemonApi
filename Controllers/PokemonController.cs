using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Models;

namespace PokemonApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PokemonController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pokemon>>> GetPokemons()
    {
        return await _context.Pokemons.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Pokemon>> PostPokemon(Pokemon pokemon)
    {
        _context.Pokemons.Add(pokemon);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPokemons), new { id = pokemon.Id }, pokemon);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPokemon(int id, Pokemon pokemon)
    {
        if (id != pokemon.Id) return BadRequest();

        _context.Entry(pokemon).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePokemon(int id)
    {
        var pokemon = await _context.Pokemons.FindAsync(id);
        if (pokemon == null) return NotFound();

        _context.Pokemons.Remove(pokemon);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}