﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "BookStore";
        }
    }
}
