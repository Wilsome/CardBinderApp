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
        Task CreateCardAsync(string name, CardCondition condition, string binderId, CardGrading? grading = null, CardImage? image = null);
        Task<CardBase> GetCardByIdAsync(string id);
        Task<List<CardBase>> GetCardsByBinderIdAsync(string binderId);
        Task DeleteCardByIdAsync(string id);
        Task UpdateCardNameByIdAsync(string CardId,  string name);
        Task RemoveCardFromBinderAsync(string binderId, string cardId);
        Task UpdateCardGradingByIdAsync(string cardId,  CardGrading grading);
        Task UpdateCardImageByIdAsync(string cardId, CardImage image);
        Task TransferCardToAnotherBinder(string cardId, string destinationBinderId);
        
    }
}
