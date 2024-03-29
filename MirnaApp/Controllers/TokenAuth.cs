using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using model;
using service;
using Newtonsoft.Json;
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
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMemoryCache _memory;
        private const string SILVER_KEY = "silverKey";
        MongoSilverConnection consilver = new MongoSilverConnection();
        ReturnDataMongoDb monDb_server = new ReturnDataMongoDb();
        DriverService _driver_con = new DriverService();
        public TokenController(IMemoryCache memorycache)
        {
            _memory = memorycache;
        }
       
        [HttpGet("{data}/{str}/{query}")]
        public dynamic Get(string data,string str,string query)
        {
           
            if (_memory.TryGetValue(SILVER_KEY, out List<UserContext> silverKey))
            {
                _memory.Remove(SILVER_KEY);

                foreach(var itemIterator in consilver.readAlldata())
                {
                    if(itemIterator[2] == data )
                    {
                        var strCon = "mongodb+srv://"+str+itemIterator[5];
                        var database = ""+itemIterator[4];
                        string table = ""+itemIterator[6];
                        string type = ""+itemIterator[7];
                        return _driver_con.DriverCall(strCon,database,table,type,query);                       
                    }
                }
                return "Error in load Database, please consult your variables name";
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

        [HttpGet("{token}/{str}")]
        public string Get(string token,string str)
        {
            if (usercon.SilverValidate(token) && _memory.TryGetValue(SILVER_KEY, out List<UserContext> silverKey))
            {
                return "Silver Mirna data is ok!";
            }
            else
            {
                if (usercon.SilverValidate(token))
                {
                    List<UserContext> usCtx;
                    List<dynamic> resultsOftokens= consilver.connectionSilver();
                    usCtx = new();
                    usCtx.Add(new() { token = resultsOftokens[0][2]+"", silverString = resultsOftokens[0][2]+"", database = resultsOftokens[0][4]+"" });
                    _memory.Set(SILVER_KEY, usCtx, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3900),
                        SlidingExpiration = TimeSpan.FromSeconds(1200)
                    });
                    string dataResponse = usCtx[0].database+""+usCtx[0].silverString+""+usCtx[0].token;
                    return "Silver data is acess data type";
                }
            }
            return "You put one Invalid Token or one expired Mirna Token, consult the our documentation";
        }
    }
}
