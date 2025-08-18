using CardInfrastructure.DTO;
using CardInfrastructure.Interfaces;
using CardLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardBinderApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController(ICardService cardService) : ControllerBase
    {
        //need an instance of CardService
        private readonly ICardService _cardService = cardService;

        //create

        //delete
        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCardByIdAsync(string cardId) 
        {  
            bool deleted = await _cardService.DeleteCardByIdAsync(cardId);

            if (!deleted) 
            {
                return NotFound($"Card {cardId} not found.");
            }

            return NoContent();
            
        }


        //patch

        //get
        [HttpGet("{cardId}")]
        public async Task<IActionResult> GetCardByIdAsync(string cardId) 
        {
            CardBase card = await _cardService.GetCardByIdAsync(cardId);

            //validate
            if (card == null) 
            {
                return NotFound($"Card {cardId} not found.");
            }

            return Ok(new CardBaseDto { Name = card.Name, BinderId = card.BinderId, Condition = card.Condition, GradingId = card.GradingId, Value = card.Value });
        }

        //get all
    }
}
