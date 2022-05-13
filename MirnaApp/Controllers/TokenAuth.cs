using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using model;
using service;

namespace MirnaApp.controllers
{
    [Route("/")]
    [ApiController]
    public class startBase : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome!! Please crete your Id Key in MirnaWeb.com ";
        }
    }
    [Route("/[controller]")]
    [Route("/[controller]/data")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMemoryCache _memory;
        private const string SILVER_KEY = "silverKey";
        public TokenController(IMemoryCache memorycache)
        {
            _memory = memorycache;
        }
        [HttpGet("{data}")]
        public string Get(string data)
        {
            if (_memory.TryGetValue(SILVER_KEY, out List<UserContext> silverKey))
            {
                _memory.Dispose();
                return "Redirect from " + data;
            }
            else
            {
                return "You put one Invalid Token or one expired Mirna Token, consult the our documentation";
            }
        }

    }

    [Route("/[controller]/connect")]
    [ApiController]
    public class silverconController : ControllerBase
    {
        //Variaveis padrão da classe
        private IMemoryCache _memory;
        private const string SILVER_KEY = "silverKey";
        public silverconController(IMemoryCache memorycache)
        {
            _memory = memorycache;
        }
        MongoSilverConnection consilver = new MongoSilverConnection();
        ContextUsers usercon = new ContextUsers();

        [HttpGet("{token}")]
        public string Get(string token)
        {
            if (usercon.SilverValidate(token) && _memory.TryGetValue(SILVER_KEY, out List<UserContext> silverKey))
            {
                Response.Redirect("/Token/" + token, false);
                return "Silver Mirna data is ok!";
            }
            else
            {
                if (usercon.SilverValidate(token))
                {
                    List<UserContext> usCtx;
                    List<dynamic> resultsOftokens= consilver.connectionSilver();
                    usCtx = new();
                    usCtx.Add(new() { token = resultsOftokens[1], silverString = resultsOftokens[2], database = "Dash" });
                    _memory.Set(SILVER_KEY, usCtx, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3900),
                        SlidingExpiration = TimeSpan.FromSeconds(1200)
                    });
                    Response.Redirect("https://localhost:7044/Token/" + token, false);
                    return "Silver data is acess data type";
                }
            }
            return "You put one Invalid Token or one expired Mirna Token, consult the our documentation";
        }
    }
}
