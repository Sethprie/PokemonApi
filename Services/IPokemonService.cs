using PokemonApi.DTOs;

namespace PokemonApi.Services;

public interface IPokemonService
{
    Task<IEnumerable<PokemonReadDto>> GetAllAsync(int page, int pageSize);
    Task<PokemonReadDto?> GetByIdAsync(int id);
    Task<PokemonReadDto> CreateAsync(PokemonCreateUpdateDto dto);
    Task<bool> UpdateAsync(int id, PokemonCreateUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}