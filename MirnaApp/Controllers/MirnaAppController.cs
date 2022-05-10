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
    }
}