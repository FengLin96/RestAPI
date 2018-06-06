using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Controllers.Models;

[Route("api/v1/planeten")]
public class PlanentenController : Controller
{
    
    private readonly LibraryContext context;

    public PlanentenController(LibraryContext context)
    {
                this.context = context;

    }

    [HttpGet]         // api/v1/palnet
    public List<Planet> GetPlaneten( string naam,string uitleg, int? page, string sort, int length = 2, string dir = "asc")
    {
        IQueryable<Planet> query = context.Planeten;

        if (!string.IsNullOrWhiteSpace(naam))
            query = query.Where(d => d.Naam == naam);
        if (!string.IsNullOrWhiteSpace(uitleg))
            query = query.Where(d => d.Uitleg == uitleg);

        if (!string.IsNullOrWhiteSpace(sort))
        {
            switch (sort)
            {
                case "naam":
                    if (dir == "asc")
                        query = query.OrderBy(d => d.Naam);
                    else if (dir == "desc")
                        query = query.OrderByDescending(d => d.Naam);
                    break;
                case "uitleg":
                    if (dir == "asc")
                        query = query.OrderBy(d => d.Uitleg);
                    else if (dir == "desc")
                        query = query.OrderByDescending(d => d.Uitleg);
                    break;
            }
        }

        if (page.HasValue)
            query = query.Skip(page.Value * length);
        query = query.Take(length);

        return query.ToList();
    }

    [Route("{id}")]   
    [HttpGet]
    public IActionResult Getplaneet(int id)
    {
        var planet = context.Planeten
                    .Include(d => d.Uitleg)
                    .SingleOrDefault(d => d.Id == id);

        if (planet == null)
            return NotFound();

        return Ok(planet);
    }


    [HttpPost]
    public IActionResult CreateBook([FromBody] Planet newPlaneten)
    {
        context.Planeten.Add(newPlaneten);
        context.SaveChanges();
        return Created("", newPlaneten);
    }

    [HttpPut]
    public IActionResult UpdateBook([FromBody] Planet updateBook)
    {
        var orgPlanet = context.Planeten.Find(updateBook.Id);
        if (orgPlanet == null)
            return NotFound();

        orgPlanet.Id = updateBook.Id;
        orgPlanet.Naam = updateBook.Naam;
        orgPlanet.Uitleg = updateBook.Uitleg;
       

        context.SaveChanges();
        return Ok(orgPlanet);
    }

    [Route("{id}")]
    [HttpDelete]
    public IActionResult DeletPlanet(int id)
    {
        var planet = context.Planeten.Find(id);
        if (planet == null)
            return NotFound();

        context.Planeten.Remove(planet);
        context.SaveChanges();
        return NoContent();
    }
}