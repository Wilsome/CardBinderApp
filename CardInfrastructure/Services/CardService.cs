using CardInfrastructure.DTO;
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

        public async Task<bool> DeleteCardByIdAsync(string id)
        {
            CardBase card = await _context.Cards
                    .Include(c => c.Binder)
                    .SingleOrDefaultAsync(c => c.Id == id);

            if (card == null) 
            {
                return false;
            }

            //remove from binder
            if (card.Binder != null)
            {
                card.Binder.Cards.Remove(card);
            }
            
            //remove
            _context.Cards.Remove(card);
            //save
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CardBase> GetCardByIdAsync(string id)
        {
            CardBase card = await _context.Cards.SingleOrDefaultAsync(c => c.Id == id);

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

        public async Task<CardBase> UpdateCardAsync(CardBase card, UpdateCardDto cardDto )
        {   
            //helper method calls
            PatchScalarFields(card, cardDto);
            PatchCardImage(card, cardDto.Image);
            PatchCardGrading(card, cardDto.Grading);

            //save
            await _context.SaveChangesAsync();

            return card;
        }

        /// <summary>
        /// Will update CardBase basic fields
        /// </summary>
        /// <param name="card"></param>
        /// <param name="dto"></param>
        private void PatchScalarFields(CardBase card, UpdateCardDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name))
                card.Name = dto.Name;

            if (dto.Value.HasValue)
                card.Value = dto.Value.Value;

            if (dto.Condition.HasValue)
                card.Condition = dto.Condition.Value;

            if (!string.IsNullOrWhiteSpace(dto.BinderId))
                card.BinderId = dto.BinderId;

            if (!string.IsNullOrWhiteSpace(dto.GradingId))
                card.GradingId = dto.GradingId;
        }

        /// <summary>
        /// Update a CardBase Image object
        /// </summary>
        /// <param name="card"></param>
        /// <param name="imageDto"></param>
        private void PatchCardImage(CardBase card, UpdateCardImageDto imageDto)
        {
            if (imageDto == null) return;

            if (card.Image != null)
            {
                if (!string.IsNullOrWhiteSpace(imageDto.ImageUrl))
                    card.Image.ImageUrl = imageDto.ImageUrl;

                if (imageDto.IsUserUploaded.HasValue)
                    card.Image.IsUserUploaded = imageDto.IsUserUploaded.Value;

                if (imageDto.IsPlaceHolder.HasValue)
                    card.Image.IsPlaceholder = imageDto.IsPlaceHolder.Value;

                card.Image.UploadedAt = DateTime.UtcNow;
            }
            else
            {
                card.Image = new CardImage
                {
                    CardId = card.Id,
                    Card = card,
                    ImageUrl = imageDto.ImageUrl ?? string.Empty,
                    IsUserUploaded = imageDto.IsUserUploaded ?? false,
                    IsPlaceholder = imageDto.IsPlaceHolder ?? false,
                    UploadedAt = DateTime.UtcNow
                };
            }
        }

        /// <summary>
        /// Update a CardBase Grading object
        /// </summary>
        /// <param name="card"></param>
        /// <param name="gradingDto"></param>
        private void PatchCardGrading(CardBase card, UpdateCardGradingDto gradingDto)
        {
            if (gradingDto == null) return;

            if (card.Grading != null)
            {
                if (!string.IsNullOrWhiteSpace(gradingDto.CompanyName))
                    card.Grading.CompanyName = gradingDto.CompanyName;

                if (gradingDto.Grade.HasValue)
                    card.Grading.Grade = gradingDto.Grade.Value;

                if (!string.IsNullOrWhiteSpace(gradingDto.CertificationNumber))
                    card.Grading.CertificationNumber = gradingDto.CertificationNumber;

                if (gradingDto.GradedDate.HasValue)
                    card.Grading.GradedDate = gradingDto.GradedDate.Value;
            }
            else
            {
                card.Grading = new CardGrading
                {
                    GradingId = Guid.NewGuid().ToString(),
                    CompanyName = gradingDto.CompanyName ?? string.Empty,
                    Grade = gradingDto.Grade ?? 0,
                    CertificationNumber = gradingDto.CertificationNumber ?? string.Empty,
                    GradedDate = gradingDto.GradedDate ?? DateOnly.FromDateTime(DateTime.UtcNow),
                    Card = card
                };
            }
        }

    }
}
