using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TulaHackWebAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [Authorize(Roles = "1")]
    public class AdminController: ControllerBase
    {
       

        
    }
}
