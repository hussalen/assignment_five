namespace assignment_five;

using System.ComponentModel.DataAnnotations;

public record Animal
{
    [Required]
    public int IdAnimal { get; set; }

    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string Area { get; set; }
}
