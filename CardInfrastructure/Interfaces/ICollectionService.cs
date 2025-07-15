using CardLibrary.Enums;
using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Interfaces
{
    public interface ICollectionService
    {
        Task<Collection> GetCollectionById(string id);
        Task<List<Collection>> GetAllCollectionsByUserId(string userId);
        Task CreateCollectionAsync(string userId, string name, CollectionTheme theme);
        Task DeleteCollectionById(string id);
        Task UpdateCollectionById(string id, string name, CollectionTheme theme);
        
    }
}
