using FinalProjectJob.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectJob.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<category> Category { get; set; }
        public List<elan> Elan { get; set; }
    }
}