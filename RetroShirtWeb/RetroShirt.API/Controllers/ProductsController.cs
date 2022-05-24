using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RetroShirt.API.Filters;
using RetroShirt.Business.Abstract;
using RetroShirtDtos.Requests;
using RetroShirtEntities;
using System;
using System.Threading.Tasks;

namespace RetroShirt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //sağlıklı apiyi asenkron yaz. apiyi biz her yere yazıyoruz mobil vs. cookie, session gibi web öncelikli şeyler yok artık burda saçma olur.
    //istemci tarafındaki durumu apide tutmazsın.
    //metod adları önemli değil apide. burda view de yok zaten .
    //.net 6.0 'da startup ve program tek bir program.cs içinde.
    //cache işlemlemlerinde random yerlerden cache'i çağırabiliyoz. ( memory ve distributed caching yaptık biz . )
    //logging , eventlog microsofta özel. (şu uygulama logları windowstaki)
    public class ProductsController : ControllerBase
    {
        private IDistributedCache distributedCache;
        private IProductService productService;
        private ICategoryTeamService categoryTeamService;
        private ILogger logger;

        public ProductsController(IProductService productService,ICategoryTeamService categoryTeamService,IDistributedCache distributedCache,ILogger<Product> logger)
        {
            this.distributedCache = distributedCache;
            this.productService = productService;
            this.categoryTeamService = categoryTeamService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            logger.LogError($"Products served at: {DateTime.Now}");
            var products = await productService.GetProducts();

            return Ok(products);
        }

        

        [HttpGet("[action]/{id}")] // method id'yi burdan alıyor.                                  
                                   // bir tane daha get olsaydı 1 parametre alan multiple endpoints derdi ondan isim veriyoz.          action name ile veya id:int diye int'e zorlarız.
                                   // direkt Arama/{id} ' de yazabilirdik custom. 
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductById(id);
            if (product !=null)
            {
                return Ok(product);
            }
            return NotFound(new {message=$"{id} product can not found."});
        }
        [HttpPost]
        [Authorize(Roles ="admin,editor")]
        public async Task<IActionResult> AddProduct([FromBody]ProductAddingRequest productAddingRequest)
        {
            if (ModelState.IsValid)
            {
                var lastProductId=await productService.AddProduct(productAddingRequest);               
                await categoryTeamService.addMany2Many(productAddingRequest);
                
                return CreatedAtAction(nameof(GetProductById), routeValues: new {id=lastProductId},null);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        [IsExist]
        // idempotence; ne kadar çağırırsan çağır sunucunun durumunu aynı bırakan method idempotenttir.
        // post harici http 'ler idempotent. 
        //ben bu methodu , getbyid methodunu falan aynı id ile ne kadar çağırırsam çağırayım aynı şey gelcek.
        public async Task<IActionResult> Update(int id,ProductUpdateRequest productUpdateRequest)
        {                      
                       
                if (ModelState.IsValid)
                {
                    await productService.UpdateProduct(productUpdateRequest);
                    return Ok();
                }
                return BadRequest();
                        
        }

        [HttpDelete("{id}")]
        [IsExist]
        public async Task<IActionResult> Delete(int id)
        {           
                await productService.RemoveProductCompletely(id);
                return Ok();    
            
        }


    }
}   


//TODO: cache-log yapıcam.