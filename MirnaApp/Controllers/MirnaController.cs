using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using model;
using service;

namespace MirnaApp.controllers
{
    [Route("/Mirna")]
    [ApiController]
    public class Mirna : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome!! Please crete your Id Key in MirnaWeb.com ";
        }
        [HttpGet("{token}/{str}")]
        public IActionResult Get(string token, string str)
        {
            Redirect("/[silvercon]/connect/"+token+"/"+str);
            return Content(token);
        }
    }
}