using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using model;
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
        private readonly IMemoryCache? _memory;
        private const string SILVER_KEY = "silverKey";
        public TokenController(IMemoryCache memorycache)
        {
            _memory = memorycache;
        }
        [HttpGet]
        public  string Get(){
            return "Token: F455dhX45-001-009-009-0dF"; 
        }
        [HttpGet("{data}")]
        public string Get(string data)
        {
             if(_memory.TryGetValue(SILVER_KEY,out List<UserContext> silverKey))
                {
                    return "Redirect from "+data;
                }
                else{
                    return "eita";
                }
        }

    }

    [Route("/[controller]/connect")]
    [ApiController]
    public class silverconController : ControllerBase
    {
        private readonly IMemoryCache? _memory;
        private const string SILVER_KEY = "silverKey";
        public silverconController(IMemoryCache memorycache)
        {
            _memory = memorycache;
        }
        
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
                if(_memory.TryGetValue(SILVER_KEY,out List<UserContext> silverKey))
                {
                    return "Ok";
                }
                UserContext usCtx = new UserContext();
                usCtx.token = "GHTSDFC87S";
                usCtx.silverString = "SUA CHAVE";
                usCtx.database = "DATABASE";

                var memorycahceEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3900),
                    SlidingExpiration =TimeSpan.FromSeconds(1200)
                };

                _memory.Set(SILVER_KEY,usCtx,memorycahceEntryOptions);
                testToken = "Token is valid to context";
                Response.Redirect("https://localhost:7044/Token/"+token,false);
            }
            return testToken;
        }
    }
}
