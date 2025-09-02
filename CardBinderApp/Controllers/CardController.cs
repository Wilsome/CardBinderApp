using CardInfrastructure.DTO;
using CardInfrastructure.Interfaces;
using CardInfrastructure.Models;
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
        [HttpPost]
        public async Task<IActionResult> CreateCardAsync([FromBody] CreateCardDto cardDto) 
        {
            //validate state
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            //pass our Dto to our method
            CardBase card = await _cardService.CreateCardAsync(cardDto);

            return Ok(card);
        }

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
        [HttpPatch("{cardId}")]
        public async Task<IActionResult> UpdateCardAsync(string cardId, [FromBody] UpdateCardDto cardDto) 
        {
            //pull the card
            CardBase card = await _cardService.GetCardByIdAsync(cardId);

            //validate
            if (card == null) 
            {
                return NotFound($"Card with id {cardId} not found.");
            }
            
            //update
            await _cardService.UpdateCardAsync(card, cardDto);

            //return 
            return Ok(card);
        }

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
        [HttpGet("binder/{binderId}")]
        public async Task<IActionResult> GetAllCardsByBinderIdAsync(string binderId) 
        {
            List<Card> cards = await _cardService.GetCardsByBinderIdAsync(binderId);

            if (cards.Count == 0) 
            {
                return NotFound($"No cards found under Binder Id {binderId}");
            }

            //change list of CardBase to CartBaseDto
            List<CardBaseDto> cardDtoList = new();

            foreach (CardBase card in cards)
            {
                //add to new list
                cardDtoList.Add(new CardBaseDto
                {
                    Name = card.Name,
                    Value = card.Value,
                    BinderId = binderId,
                    Condition = card.Condition,
                    GradingId = card.GradingId,
                });
            } 

            return Ok(cardDtoList);
        }

    }
}
