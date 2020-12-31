using greenSacrifice.Blazor.Models.Web.ViewModels.HelloWorld;
using System;

namespace greenSacrifice.Blazor.Models.DbDataModels
{
    public class TbDemoMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string HiddenMessage { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DemoMessageViewModel ToViewModel()
        {
            return new DemoMessageViewModel
            {
                Id = Id,
                Message = Message,
                HiddenMessage = HiddenMessage,
                CreatedUTC = DateTime.UtcNow,
            };
        }
    }
}