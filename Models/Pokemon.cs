using System.ComponentModel.DataAnnotations;

namespace PokemonApi.Models;

public class Pokemon
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public int Level { get; set; }
}