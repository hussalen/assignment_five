using System.Collections;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace assignment_five.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly DatabaseManager _dbManager;

    private readonly ILogger<AnimalsController> _logger;

    public AnimalsController(ILogger<AnimalsController> logger, DatabaseManager dbManager)
    {
        _logger = logger;
        _dbManager = dbManager;
    }

    [HttpGet]
    public IActionResult Get(string? orderBy)
    {
        string query = "SELECT * FROM Animals ORDER BY ";

        try
        {
            switch (orderBy)
            {
                case AnimalDBField.Description:
                    query += AnimalDBField.Description;
                    break;
                case AnimalDBField.Category:
                    query += AnimalDBField.Category;
                    break;
                case AnimalDBField.Area:
                    query += AnimalDBField.Area;
                    break;
                case AnimalDBField.Name
                or null
                or "":
                    query += AnimalDBField.Name;
                    break;
            }
            query += " ASC";
            DataTable results = _dbManager.ExecuteQuery(query);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Add(Animal animal)
    {
        string addDesc = animal.Description is not null ? ", " + AnimalDBField.Description : "";
        string query =
            $"INSERT INTO Animal({AnimalDBField.IdAnimal}, {AnimalDBField.Name} {addDesc}, {AnimalDBField.Category}, {AnimalDBField.Area}) VALUES ({animal.IdAnimal},{animal.Name}, {animal.Category}, {animal.Area})";
        try
        {
            DataTable results = _dbManager.ExecuteQuery(query);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("{idAnimal}")]
    public IActionResult Update(Animal animal)
    {
        string addDesc = animal.Description is not null
            ? ", " + $"{AnimalDBField.Description}={animal.Description}"
            : "";
        string query =
            $"UPDATE Animal SET {AnimalDBField.Name}={animal.Name} {addDesc}, {AnimalDBField.Category}={animal.Category}, {AnimalDBField.Area}={animal.Area} WHERE {AnimalDBField.IdAnimal}={animal.IdAnimal}";
        try
        {
            DataTable results = _dbManager.ExecuteQuery(query);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{idAnimal}")]
    public IActionResult Delete(int id)
    {
        string query = $"DELETE FROM Animal WHERE {AnimalDBField.IdAnimal}={id}";
        try
        {
            DataTable results = _dbManager.ExecuteQuery(query);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
