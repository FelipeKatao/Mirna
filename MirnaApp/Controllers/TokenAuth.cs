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
    public class TokenController : ControllerBase
    {
        [HttpGet]
        public  string Get(){
            return "Token: F455dhX45-001-009-009-0dF"; 
        }
        [HttpGet("{data}")]
        public string Get(string data){

            return "Redirect from "+data;
        }

    }

    [Route("/[controller]/connect")]
    [ApiController]
    public class silverconController : ControllerBase
    {
        List<dynamic> results = new List<dynamic>();
        MongoSilverConnection consilver = new MongoSilverConnection();
        ContextUsers usercon  = new ContextUsers();
        [HttpGet]
        public  string Get(){
            return consilver.connectionSilver()[0][1]+" "; 
        }
        [HttpGet("{token}")]
        public string Get(string token){
            results.Clear();
            string testToken = "Token not is valid to context!";
            if(usercon.SilverValidate(token))
            {
                testToken = "Token is valid to context";
                Response.Redirect("https://localhost:7044/Token/"+token,false);
            }
            return testToken;
        }
    }
}
