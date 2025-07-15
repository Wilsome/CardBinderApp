using CardInfrastructure.Interfaces;
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
    public class CollectionService(CardDbContext context) : ICollectionService
    {
        //our db connection
        private readonly CardDbContext _context = context;

        public async Task CreateCollectionAsync(string userId, string name, CollectionTheme theme)
        {
            //pull the user
            User user = await _context.Users.SingleOrDefaultAsync(u =>u.Id == userId);

            //validate
            if (user == null) 
            {
                throw new KeyNotFoundException($"User {userId} not found.");
            }

            //create the collection
            Collection collection = new() 
            {
                Name = name,
                Theme = theme,
                UserId = userId
            };

            //save
            await _context.SaveChangesAsync();

        }

        public async Task DeleteCollectionById(string id)
        {
            Collection collection = await _context.Collections.SingleOrDefaultAsync(c => c.Id == id);

            //validate
            if (collection == null) 
            {
                throw new KeyNotFoundException($"Collection id {id} not found.");
            }

            //remove from db
            _context.Collections.Remove(collection);

            //save db 
            await _context.SaveChangesAsync();
        }

        public async Task<List<Collection>> GetAllCollectionsByUserId(string userId)
        {
            //pull user
            User user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            //validate 
            if (user == null) 
            {
                throw new KeyNotFoundException($"User {userId} not found.");
            }

            //pull the collections
            List<Collection> collections = user.Collections;


            return collections;
            
        }

        public async Task<Collection> GetCollectionById(string id)
        {
            Collection collection = await _context.Collections.SingleOrDefaultAsync(c => c.Id == id);

            //validate
            if (collection == null) throw new KeyNotFoundException($"Collection id {id} not found.");
           
            return collection;
        }

        public async Task UpdateCollectionById(string id, string name, CollectionTheme theme)
        {
            //pull collection
            Collection collection = await GetCollectionById(id);

            if (collection == null) 
            {
                throw new KeyNotFoundException($"Collect {id} not found.");
            }

           //collection found
            collection.Name = name;
            collection.Theme = theme;

            
            //save changes
            await _context.SaveChangesAsync();

        }
    }
}
