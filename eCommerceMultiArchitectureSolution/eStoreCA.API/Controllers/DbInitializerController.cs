
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eStoreCA.Infrastructure.Data;

namespace eStoreCA.API.Controllers
{
     public class DbInitializerController : ControllerBase
{
    private readonly IDbInitializer _dbInitializer;

    public DbInitializerController(IDbInitializer dbInitializer)
    {
        _dbInitializer = dbInitializer;
    }


    [Route("api/db/setup")]
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> SetUp()
    {
        try
        {
            await _dbInitializer.Initialize();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);

        }
    }
            
#region Custom
#endregion Custom

}
}
