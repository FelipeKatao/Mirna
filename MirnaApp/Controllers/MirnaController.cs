using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using model;
using service;
using Microsoft.AspNetCore.Cors;
using QueryEditor;

namespace MirnaApp.controllers
{
    [Route("/Mirna")]
    [ApiController]
    
    public class MirnaController : ControllerBase
    {
        private readonly IMemoryCache _memory;
        private const string SILVER_KEY = "silverKey";
        ContextUsers usercon = new ContextUsers();
        public MirnaController(IMemoryCache memorycache)
        {
            _memory = memorycache;
        }

        [HttpGet]
        public string Get()
        {
            return "Welcome!! Please crete your Id Key in MirnaWeb.com ";
        }
        [HttpGet("{token}/{str}/{query}")]

        public void Get(string token, string str,string query)
        {
            if (usercon.SilverValidate(token) && _memory.TryGetValue(SILVER_KEY, out List<UserContext> silverKey) && query.Length >=4)
            {
                Response.Redirect("/token/"+token+"/"+str+"/"+query);
            }    
        }
    }
}