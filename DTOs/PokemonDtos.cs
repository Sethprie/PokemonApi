namespace PokemonApi.DTOs;

public class PokemonReadDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public int Level { get; set; }
}

public class PokemonCreateUpdateDto
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public int Level { get; set; }
}