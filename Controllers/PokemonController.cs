using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Models;
using PokemonApi.DTOs;

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
    public async Task<ActionResult<IEnumerable<PokemonReadDto>>> GetPokemons()
    {
        var pokemons = await _context.Pokemons.ToListAsync();
        
        var pokemonDtos = pokemons.Select(p => new PokemonReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Type = p.Type,
            Level = p.Level
        });

        return Ok(pokemonDtos);
    }

    [HttpPost]
    public async Task<ActionResult<PokemonReadDto>> PostPokemon([FromBody] PokemonCreateUpdateDto dto)
    {
        var pokemonEntity = new Pokemon
        {
            Name = dto.Name,
            Type = dto.Type,
            Level = dto.Level
        };

        _context.Pokemons.Add(pokemonEntity);
        await _context.SaveChangesAsync();

        var pokemonReadDto = new PokemonReadDto
        {
            Id = pokemonEntity.Id,
            Name = pokemonEntity.Name,
            Type = pokemonEntity.Type,
            Level = pokemonEntity.Level
        };

        return CreatedAtAction(nameof(GetPokemons), new { id = pokemonReadDto.Id }, pokemonReadDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPokemon(int id, [FromBody] PokemonCreateUpdateDto dto)
    {
        var pokemonEntity = await _context.Pokemons.FindAsync(id);
        if (pokemonEntity == null) return NotFound();

        pokemonEntity.Name = dto.Name;
        pokemonEntity.Type = dto.Type;
        pokemonEntity.Level = dto.Level;

        _context.Entry(pokemonEntity).State = EntityState.Modified;
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