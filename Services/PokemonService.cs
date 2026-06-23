using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokemonApi.Data;
using PokemonApi.DTOs;
using PokemonApi.Models;

namespace PokemonApi.Services;

public class PokemonService : IPokemonService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PokemonService> _logger;

    public PokemonService(ApplicationDbContext context, ILogger<PokemonService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PokemonReadDto>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogInformation("Fetching pokemons - page {Page}, pageSize {PageSize}", page, pageSize);

        return await _context.Pokemons
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PokemonReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                Level = p.Level
            })
            .ToListAsync();
    }

    public async Task<PokemonReadDto?> GetByIdAsync(int id)
    {
        var pokemon = await _context.Pokemons.FindAsync(id);

        if (pokemon == null)
        {
            _logger.LogWarning("Pokemon with id {Id} not found", id);
            return null;
        }

        return new PokemonReadDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level
        };
    }

    public async Task<PokemonReadDto> CreateAsync(PokemonCreateUpdateDto dto)
    {
        var pokemon = new Pokemon
        {
            Name = dto.Name,
            Type = dto.Type,
            Level = dto.Level
        };

        _context.Pokemons.Add(pokemon);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Pokemon created with id {Id}", pokemon.Id);

        return new PokemonReadDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Level = pokemon.Level
        };
    }

    public async Task<bool> UpdateAsync(int id, PokemonCreateUpdateDto dto)
    {
        var pokemon = await _context.Pokemons.FindAsync(id);
        if (pokemon == null)
        {
            _logger.LogWarning("Update failed - Pokemon with id {Id} not found", id);
            return false;
        }

        pokemon.Name = dto.Name;
        pokemon.Type = dto.Type;
        pokemon.Level = dto.Level;

        await _context.SaveChangesAsync();
        _logger.LogInformation("Pokemon with id {Id} updated", id);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pokemon = await _context.Pokemons.FindAsync(id);
        if (pokemon == null)
        {
            _logger.LogWarning("Delete failed - Pokemon with id {Id} not found", id);
            return false;
        }

        _context.Pokemons.Remove(pokemon);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Pokemon with id {Id} deleted", id);
        return true;
    }
}