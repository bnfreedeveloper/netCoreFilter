using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using filterApp.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace filterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DebugRessourceFilter]
    [DebugActionFilter]
    public class ProductsController : ControllerBase
    {
        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name ="amazon product"},
            new Product { Id = 2, Name ="alibaba product"}
        };

        [HttpGet]
        [DebugRessourceFilterMethodOnly]
        //ACTION FILTER FOR THIS METHOD SPECIFICALLY TOO IS POSSIBLE,if so both actionfilte will be executed
        //[DebugActionFilter]
        [TokenAuthenticationFilter]
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.FromResult(Products));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Task.FromResult($"one product {Products.FirstOrDefault(x => x.Id == id)?.Name} for the id {id}"));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
             await Task.Run(() =>Products.Add(product));
             return NoContent();
            }
            return BadRequest(new
            {
                error = "the product coudn't be added"
            });
        }
        [HttpGet("test")]
        [MultipleValidation]
        public async Task<IActionResult> Test(int id, string name)
        {
            return Ok($"{id} : {name}");
        }

    }

    //Custom validation
    public class checkReleaseDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext vcontext)
        {
            var productDto = (Product)vcontext.ObjectInstance;
            if (productDto.ReleaseDate >= DateTime.Today) return new ValidationResult("the date must be in the past");
            return ValidationResult.Success;
        }
    }

    public class MultipleValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            int id;
            try
            {
                int.TryParse(context.ActionArguments["id"]?.ToString(), out id);
                var name = (string)context.ActionArguments["name"];
                if (id == 0 || string.IsNullOrEmpty(name))
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        error = "arguments not valable"
                    });
                }

            }
            catch (Exception ex)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    error = "arguments not valable"
                });
            }
           
        }
    }
    public class Product
    {
        [Required]
        public int Id { get; set; } 
        [Required]
        [MinLength(5)]
        public string Name { get; set; }  
        [checkReleaseDate]
        public DateTime ReleaseDate { get; set; }
    }
}
