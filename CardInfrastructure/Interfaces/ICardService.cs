using CardInfrastructure.DTO;
using CardLibrary.Enums;
using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Interfaces
{
    public interface ICardService
    {
        Task<CardBase> CreateCardAsync(CreateCardDto cardDto);
        Task<CardBase> GetCardByIdAsync(string id);
        Task<List<CardBase>> GetCardsByBinderIdAsync(string binderId);
        Task<bool> DeleteCardByIdAsync(string id);
        Task RemoveCardFromBinderAsync(string binderId, string cardId);
        Task TransferCardToAnotherBinder(string cardId, string destinationBinderId);
        Task<CardBase> UpdateCardAsync(CardBase card, UpdateCardDto cardDto);
        
    }
}
