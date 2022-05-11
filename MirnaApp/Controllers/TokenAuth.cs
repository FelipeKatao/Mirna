using Microsoft.AspNetCore.Mvc;

namespace MirnaApp.controllers
{
    [Route("/")]
    [ApiController]
    public class startBase :ControllerBase
    {
        [HttpGet]
        public string Get(){
            return "Welcome!! Please crete your Id Key in MirnaWeb.com ";
        }
    }
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
