using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using service;

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

    [Route("/[controller]/connect")]
    [ApiController]
    public class silverconController : ControllerBase
    {
        MongoSilverConnection consilver = new MongoSilverConnection();
        [HttpGet]
        public  string Get(){
            List<dynamic> results = new List<dynamic>();
            
            return consilver.connectionSilver("")[0][1]+" "; 
        }

    }
}
