// To make ShirtsController a WebAPI controller, we must do 2 things:-
//  1. Derive it from ControllerBase class
//  2. Use C# [ApiController] attribute to decorate our ShirtsController class

// There are two ways to create Web APIs:-
//  Minimal Approach and Controllers Approach (More defined and precise approach (Structured and well oriented))

// MODEL BINDING:- It is process of mapping data from HTTP request to the params of an action method

// MODEL BINDING -> MODEL VALIDATION -> ACTION METHOD TRIGGER

using DemoWebAPI.Filters;
using DemoWebAPI.Models;
using DemoWebAPI.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Here controller is the placeholder of shirts
    public class ShirtsController : ControllerBase
    {

        

        // These all shirt methods are action methods

        // Two Approaches of controller based approach:-
        // 1. To specify url in controller based approach we use [Route("/")] to define where to route and [HttpVerb] above it to define the action
        // 2. We specify the controller route below [ApiController] and can pass params in httpverb itself below

        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetAllShirts());
        }

        // 1.
        //[HttpGet]
        //[Route("api/shirts/{id}")]
        //public string GetShirtByID(int id)
        //{
        //    return $"Reading shirt with ID : {id}";
        //}

        // Data mapped from route to the params of action method
        //[HttpGet("{id}")]

        // We can also specify that where the data is coming from using [FromRoute], [FromQuery], etc to create distinction
        // [FromQuery] i,e Querystream : http://localhost:5000/api/shirts/343?color=black
        // [FromRoute] : http://localhost:5000/api/shirts/343/black
        // [FromHeader(Name = "KEY")] 
        // [FromBody] used in put, post, etc (Tried adding the body from postman)
        // [FromForm] is similar to FromBody only, just it's added in Key-Value pair

        //public string GetShirtByID(int id, [FromHeader(Name = "Color")] string color)
        //{
        //    return $"Reading shirt with ID : {id} and Color : {color}";
        //}


        // 1st validation way

        //// When our action method returns multiple type of values, we make the return type as IActionResult interface
        //[HttpGet("{id}")]
        //public IActionResult GetShirtByID(int id)
        //{
        //    if (id <= 0) return BadRequest(); // 400 status code
        //    var shirt = ShirtRepository.GetShirtByID(id);
        //    if(shirt == null)
        //    {
        //        return NotFound();  // returns status code 404
        //    }
        //    return Ok(shirt);   // returns shirt with status code 200
        //}


        // 2nd validation way by using action filter

        [HttpGet("{id}")]
        // action filter for validation before action method GetShirtID execution
        [Shirt_ValidateShirtIDFilter]
        public IActionResult GetShirtByID(int id)
        {
            return Ok(ShirtRepository.GetShirtByID(id));
        }


        [HttpPost]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            if (shirt == null) return BadRequest();
            if (ShirtRepository.ShirtExists(shirt.ShirtId)) { return BadRequest(); }

            ShirtRepository.AddShirt(shirt);

            // Convention to return CreatedAtAction(location headeractionname, object) for create method

            return CreatedAtAction(nameof(GetShirtByID), new { id = shirt.ShirtId }, shirt);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShirt(int id,Shirt shirt)
        {
            if(id != shirt.ShirtId)  return BadRequest();

            try
            {
                ShirtRepository.UpdateShirt(shirt);
            }
            catch
            {
                if(!ShirtRepository.ShirtExists(id)) return NotFound();
                throw;
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIDFilter]
        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.GetShirtByID(id);
            ShirtRepository.DeleteShirt(id);
            return Ok(shirt);
        }
    }
}
