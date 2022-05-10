using Microsoft.AspNetCore.Mvc;

namespace MirnaApp.controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MirnaAppController : ControllerBase
    {
        [HttpGet]
        public  string Get(){
            return "Seja bem vindo a mirna"; 
        }
        [HttpGet("{token}")]
        public ActionResult<string> Get(string token)
        {
            //Transferir isso para o modulo Silver (Service)
            string data= "error Data";
            if(token == "F455dhX45-001-009-009-0dF")
            {
                data = "Validation Ok";
            }
            return data;
        }
    }
}
