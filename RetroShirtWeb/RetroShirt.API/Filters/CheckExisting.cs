using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RetroShirt.Business.Abstract;
using System.Threading.Tasks;

namespace RetroShirt.API.Filters
{
    //attribute yazarak mesela isexist'e baktık. mesela if içinde isexist'i update deletede tekrar yaptık.
    // dont repeat olmasın diye filtre.
    // bu alttaki yaklaşım yemedi. çünkü serviceleri implemente edemezsin filtere.


    //public class IsExistAttribute: ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext context) // bir sürü var böyle executed, executing.
    //    {
    //        var idExist=context.ActionArguments.ContainsKey("id"); // action method neler içeriyor parametre.
    //        if (!idExist)
    //        {
    //            context.Result = new BadRequestObjectResult(new { message = $"id parameter can not found." });
    //        }

    //        var id = (int)context.ActionArguments["id"]; // id'yi aldık.
    //    }
    //}


    public class CheckExisting : IAsyncActionFilter
    {
        private readonly IProductService productService;

        public CheckExisting(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idExist = context.ActionArguments.ContainsKey("id");

            if (!idExist)
            {
                context.Result = new BadRequestObjectResult(new { message = $"id parameter can not found." });
                
            }
            else
            {
                var id = (int)context.ActionArguments["id"];
                if (await productService.ProductIsExistWithId(id))
                {
                    await next.Invoke(); // id'yi servis bulursa işleme devam aga demek bu.
                }
                else
                {
                    context.Result = new BadRequestObjectResult(new { message = $"id parameter can not found." });
                }
            }

        }
    }
}
