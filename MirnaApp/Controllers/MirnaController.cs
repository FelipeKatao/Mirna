using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using model;
using service;

namespace MirnaApp.controllers
{
    [Route("/Mirna")]
    [ApiController]
    public class Mirna: ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Welcome!! Please crete your Id Key in MirnaWeb.com ";
        }
    }

}