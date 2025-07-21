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
        Task DeleteBinderByNameAsync(string name);
        Task<List<CardBinder>> GetAllBindersByCollectionIdAsync(string collectionId);
        Task CreateBinderAsync(string colllectionId, string name);
        Task UpdateBinderNameByIdAsync(string id, string updatedName);

    }
}
