using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.DTOs;
using PokemonApi.Models;

namespace PokemonApi.Services;

public class PokemonService : IPokemonService
{
    private readonly ApplicationDbContext _context;

    public PokemonService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PokemonReadDto>> GetAllAsync(int page, int pageSize)
    {
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
        if (pokemon == null) return null;

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
        if (pokemon == null) return false;

        pokemon.Name = dto.Name;
        pokemon.Type = dto.Type;
        pokemon.Level = dto.Level;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pokemon = await _context.Pokemons.FindAsync(id);
        if (pokemon == null) return false;

        _context.Pokemons.Remove(pokemon);
        await _context.SaveChangesAsync();
        return true;
    }
}