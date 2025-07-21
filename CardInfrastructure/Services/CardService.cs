using CardInfrastructure.Interfaces;
using CardInfrastructure.Models;
using CardLibrary.Enums;
using CardLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Services
{
    public class CardService(CardDbContext context) : ICardService
    {
        //connection to our database
        private readonly CardDbContext _context = context;

        public async Task CreateCardAsync(string name, CardCondition condition, string binderId, CardGrading? grading = null, CardImage? image = null)
        {
            //create a cardbase object
            Card card = new()
            {
                Name = name,
                Condition = condition,
                BinderId = binderId,
                Grading = grading,
                Image = image
            };

            await _context.Cards.AddAsync(card);    
            
            //save
            await _context.SaveChangesAsync();

        }

        public async Task DeleteCardByIdAsync(string id)
        {
            CardBase card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == id);

            //validate
            if (card == null) 
            {
                throw new KeyNotFoundException($"Card {id} not found.");
            }

            //remove
            _context.Cards.Remove(card);
            //save
            await _context.SaveChangesAsync();
        }

        public async Task<CardBase> GetCardByIdAsync(string id)
        {
            CardBase card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == id);

            //validate
            if (card == null) 
            {
                throw new KeyNotFoundException($"Card {id} not found.");
            }

            return card;
        }

        public async Task<List<CardBase>> GetCardsByBinderIdAsync(string binderId)
        {
            List<CardBase> cards = await _context.Cards
                .Where(c => c.BinderId == binderId)
                .ToListAsync();

            return cards;
        }

        public async Task RemoveCardFromBinderAsync(string binderId, string cardId)
        {
          //pull the card
            CardBase card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);

            //validate
            if (card == null) 
            {
                throw new KeyNotFoundException($"Card {cardId} not found.");
            }

            //unlink card from binder
            card.BinderId = null;

            //save
            await _context.SaveChangesAsync();
            
        }

        public async Task TransferCardToAnotherBinder(string cardId, string destinationBinderId)
        {
            //pull the card
            CardBase card = await _context.Cards.SingleOrDefaultAsync(c =>c.Id == cardId);
            //validate
            if (card == null) 
            {
                throw new KeyNotFoundException($"Card {cardId} not found.");
            }

            //update card property
            card.BinderId = destinationBinderId;
            
            //save changed
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCardGradingByIdAsync(string cardId, CardGrading grading)
        {
            //pull the card
            CardBase card = await _context.Cards.SingleOrDefaultAsync(c =>c.Id==cardId);

            //validate
            if (card == null) 
            {
                throw new KeyNotFoundException($"Card {cardId} not found.");
            }

            //update CardGrading property
            card.Grading = grading;

            //save
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCardImageByIdAsync(string cardId, CardImage image)
        {
            //pull the card
            CardBase card = await _context.Cards.SingleOrDefaultAsync(c =>c.Id==cardId);

            //validate
            if (card == null) 
            {
                throw new KeyNotFoundException($"Card {cardId} not found.");
            }

            //update Image property
            card.Image = image;

            //save
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCardNameByIdAsync(string cardId, string name)
        {
            //pull card
            CardBase card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == cardId);

            //validate
            if (card == null) 
            {
                throw new KeyNotFoundException($"Card {cardId} not found.");
            }

            //update name property
            card.Name = name;

            //save
            await _context.SaveChangesAsync();
        }
    }
}
