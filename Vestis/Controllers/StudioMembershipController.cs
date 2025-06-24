using Microsoft.AspNetCore.Mvc;

namespace Vestis._01_Presentation.Controllers;

public class StudioMembershipController : VestisController
{
    // GET: api/<StudioMembershipController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<StudioMembershipController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<StudioMembershipController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<StudioMembershipController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<StudioMembershipController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
