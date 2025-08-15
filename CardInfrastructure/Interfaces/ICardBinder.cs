using CardInfrastructure.DTO;
using CardLibrary.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Interfaces
{
    public interface ICardBinder
    {
        Task<CardBinder> GetBinderByIdAsync(string id);
        Task<CardBinder> GetBinderByNameAsync(string name);
        Task<CardBinder> DeleteBinderAsync(CardBinder binder);
        Task<List<CardBinder>> GetAllBindersByCollectionIdAsync(string collectionId);
        Task<CardBinder> CreateBinderAsync(CreateBinderDto binderDto, Collection collection);
        Task UpdateBinderNameByIdAsync(CardBinder binder, string updatedName);
        Task<Collection> ValidateCollection(string collectionId);

    }
}
