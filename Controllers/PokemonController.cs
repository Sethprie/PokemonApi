using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonApi.DTOs;
using PokemonApi.Services;

namespace PokemonApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PokemonReadDto>>> GetPokemons(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var pokemons = await _pokemonService.GetAllAsync(page, pageSize);
        return Ok(pokemons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PokemonReadDto>> GetPokemon(int id)
    {
        var pokemon = await _pokemonService.GetByIdAsync(id);
        if (pokemon == null) return NotFound();
        return Ok(pokemon);
    }

    [HttpPost]
    public async Task<ActionResult<PokemonReadDto>> PostPokemon([FromBody] PokemonCreateUpdateDto dto)
    {
        var created = await _pokemonService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetPokemon), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPokemon(int id, [FromBody] PokemonCreateUpdateDto dto)
    {
        var updated = await _pokemonService.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePokemon(int id)
    {
        var deleted = await _pokemonService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}