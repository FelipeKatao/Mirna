using Microsoft.AspNetCore.Mvc;

namespace MirnaApp.controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class Token : ControllerBase
    {
        [HttpGet]
        public  string Get(){
            return "Token: F455dhX45-001-009-009-0dF"; 
        }
    }
}
