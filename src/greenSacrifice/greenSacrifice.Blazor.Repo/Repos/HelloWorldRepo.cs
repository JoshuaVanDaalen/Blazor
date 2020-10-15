using greenSacrifice.Blazor.Models.DbDataModels;
using greenSacrifice.Blazor.Models.Web.ViewModels.HelloWorld;
using greenSacrifice.Blazor.Repo.Context;
using System;
using System.Linq;

namespace greenSacrifice.Blazor.Repo.Repos
{
    public class HelloWorldRepo : AppRepo<HelloWorldDbContext>
    {
        public HelloWorldRepo(HelloWorldDbContext dbContext) : base(dbContext)
        {
        }
        public DemoMessageViewModel GetDemoMessageById(int id)
        {
            var t = DbContext.TbDemo.FirstOrDefault(i => i.Id == id);
            if (t == null)
            {
                var viewModel = new DemoMessageViewModel()
                {
                    Id = 0,
                    Message = "InMemory Message",
                    CreatedUTC = DateTime.UtcNow,
                };
                return viewModel;
            }
            return t.ToViewModel();
        }
    }
}
