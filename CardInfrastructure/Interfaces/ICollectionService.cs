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
        Task CreateCollectionAsync(Collection collection);
        Task<bool> DeleteCollectionByIdAsync(string id);
        Task UpdateCollectionAsync(Collection collection);
        Task<List<Collection>> GetAllCollectionsAsyn();
        
    }
}
