using CardInfrastructure.Interfaces;
using CardLibrary.Enums;
using CardLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Services
{
    public class CollectionService(CardDbContext context) : ICollectionService
    {
        //our db connection
        private readonly CardDbContext _context = context;

        public async Task CreateCollectionAsync(Collection collection)
        {
            //validate 
            if (collection == null) throw new ArgumentNullException(nameof(collection));
          
            //add
            await _context.Collections.AddAsync(collection);

            //save
            await _context.SaveChangesAsync();

        }

        public async Task<bool> DeleteCollectionByIdAsync(string id)
        {
            Collection collection = await _context.Collections.SingleOrDefaultAsync(c => c.Id == id);

            if (collection == null) 
            {
                return false;
            }

            //remove from db
            _context.Collections.Remove(collection);

            //save db 
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Collection>> GetAllCollectionsAsyn()
        {
            return await _context.Collections.ToListAsync();
        }

        public async Task<List<Collection>> GetAllCollectionsByUserId(string userId)
        {
            //pull user
            //User user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            User user = await _context.Users
                .Include(u => u.Collections)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null) 
            {
                return null;
            }

            //pull the collections
            List<Collection> collections = user.Collections;


            return collections;
            
        }

        public async Task<Collection> GetCollectionById(string id)
        {
            Collection collection = await _context.Collections.SingleOrDefaultAsync(c => c.Id == id);

            return collection;
        }

        public async Task UpdateCollectionAsync(Collection collection)
        {
            //validate 
            if (collection == null) throw new ArgumentNullException(nameof(collection));

             _context.Collections.Update(collection);

            //save
            await _context.SaveChangesAsync();

        }
    }
}
