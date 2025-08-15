using CardInfrastructure.DTO;
using CardInfrastructure.Interfaces;
using CardLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardBinderApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BinderController(ICardBinder binderService) : ControllerBase
    {
        //need an instance of BinderService
        private readonly ICardBinder _binderService = binderService;

        [HttpGet("{binderId}")]
        public async Task<IActionResult> GetBinderByIdAsync(string binderId) 
        {
            //call method from service
            CardBinder binder = await _binderService.GetBinderByIdAsync(binderId);

            //validate response
            if (binder == null) 
            {
                return NotFound($"Binder {binderId} not found.");
            }

            //return 
            return Ok(binder);
        }

        //get all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBindersByCollectionAsync(string collectionId) 
        {
            //list of binder
            List<CardBinder> binders = await _binderService.GetAllBindersByCollectionIdAsync(collectionId);

            //validate
            if (binders == null) 
            {
                return NotFound($"No binders found for Collection {collectionId}");
            }

            //return 
            return Ok(binders);
        }

        //create
        [HttpPost]
        public async Task<IActionResult> CreateNewBinder([FromBody] CreateBinderDto binder) 
        {
            //pull colleciton
            Collection collection = await _binderService.ValidateCollection(binder.CollectionId);
            //validate 
            if (collection == null) 
            {
                return NotFound($"Collection {binder.CollectionId} not found.");
            }

            //we get here collection is valid. 
            //create a new binder from our dto
            CardBinder createdBinder = await _binderService.CreateBinderAsync(binder, collection);

            return CreatedAtAction(
                nameof(GetBinderByIdAsync),
                new { binderId = createdBinder.Id },
                createdBinder
            );

        }

        //delete
        [HttpDelete("{binderId}")]
        public async Task<IActionResult> DeleteBinderByIdAsync(string binderId) 
        {
            //pull binder
            CardBinder binder = await _binderService.GetBinderByIdAsync($"{binderId}");

            //validate
            if (binder == null) 
            {
                return NotFound($"Binder {binderId} not found.");
            }

            //call method to do work from service
            await _binderService.DeleteBinderAsync(binder);

            //return 
            return AcceptedAtAction($"Binder {binder.Name} has been deleted");
        }

        //patch, partial replacement
        [HttpPatch("{binderId}")]
        public async Task<IActionResult> UpdateBinderName(string binderId,[FromBody] UpdateBinderDto dto) 
        {
            // Validate input
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Name is required.");
            }


            //pull binder
            CardBinder binder = await _binderService.GetBinderByIdAsync(binderId);

            //validate
            if (binder == null) 
            {
                return NotFound($"Binder {binderId} not found.");
            }

            //update and save
            await _binderService.UpdateBinderNameByIdAsync(binder, dto.Name);

            return NoContent();
            
        }
    }
}
