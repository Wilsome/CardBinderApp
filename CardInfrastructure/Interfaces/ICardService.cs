using CardInfrastructure.DTO;
using CardInfrastructure.Models;
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
        Task<Card> GetCardByIdAsync(string id);
        Task<List<Card>> GetCardsByBinderIdAsync(string binderId);
        Task<bool> DeleteCardByIdAsync(string id);
        Task RemoveCardFromBinderAsync(string binderId, string cardId);
        Task TransferCardToAnotherBinder(string cardId, string destinationBinderId);
        Task<CardBase> UpdateCardAsync(CardBase card, UpdateCardDto cardDto);
        
    }
}
