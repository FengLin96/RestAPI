using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/steden")]
public class StedenContoller : Controller
{
    
    private static List<Stad> list = new List<Stad>();

    public StedenContoller()
    {

    }

    static StedenContoller()
    {
        list.Add(new Stad()
        {
            Id = 1,
            Naam = "Aalst",
            Provincie = "Oost-Vlaanderen",
            Inwoners = 82587
            
        });
        list.Add(new Stad(){
            Id = 2,
            Naam = "Aarlen",
            Provincie = "Luxemburg",
            Inwoners = 28520
        });
        list.Add(new Stad(){
            Id = 3,
            Naam = "Aarschot",
            Provincie = "Vlaanderen",
            Inwoners = 28969
        });
        list.Add(new Stad(){
            Id = 4,
            Naam = "Beringen",
            Provincie = "Luxemburg",
            Inwoners = 43975
        });
        list.Add(new Stad(){
            Id = 5,
            Naam = "Diest",
            Provincie = "Vlaams-Brabant",
            Inwoners = 23271
        });
    }
    [Route("{id}")]
    [HttpGet]

    public IActionResult GetStad(int id)
    {
        if (list.Exists(d => d.Id == id))
            return NotFound();

        return Ok(list.FirstOrDefault(d => d.Id == id));
    }
    [HttpGet]         // api/v1/books
    public List<Stad> GetAlleSteden()
    {
        return list;
    }

    [Route("{id}")]
    [HttpDelete]
    public IActionResult DeletStad(int id)
    {
        if (list.Exists(d => d.Id == id))
            return NotFound();
        else list.Remove(list.FirstOrDefault(d => d.Id == id));
        
        return NoContent();
    }

    [HttpPost]
    public IActionResult CreateStad([FromBody] Stad newStad)
    {
        newStad.Id = 6;
        return Created("", newStad);
      
    }

    [HttpPut]
    public IActionResult updateStad([FromBody] Stad UpdateStad)
    {
        var orgStad = list.FirstOrDefault(d => d.Id == UpdateStad.Id);
        if(orgStad == null)
            return NotFound();

        orgStad.Provincie = UpdateStad.Provincie;
        orgStad.Naam = UpdateStad.Naam;
        orgStad.Inwoners = UpdateStad.Inwoners;
        
        return Ok(orgStad);
    }
}