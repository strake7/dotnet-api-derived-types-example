using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("musicians")]
public class MusiciansController : ControllerBase
{
    [HttpPost]
    public ActionResult Post(IEnumerable<Musician> musiciansToCreate) => Accepted(musiciansToCreate);
}