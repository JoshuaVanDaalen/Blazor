using System;
using System.Collections.Generic;
using System.Text;

namespace greenSacrifice.Blazor.Models.Web.ViewModels.HelloWorld
{
    public class DemoMessageViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedUTC { get; set; }
    }
}
