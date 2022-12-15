using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Project> Projects { get; set; }

        public IEnumerable<Cv> Cvs { get; set; }
    }
}
