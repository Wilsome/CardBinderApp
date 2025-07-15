using CardInfrastructure.Interfaces;
using CardLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Services
{
    public class CardBinderService(CardDbContext context) : ICardBinder
    {
        //need db connection
        private readonly CardDbContext _context = context;

        public async Task CreateBinderAsync(string collectionId, string name)
        {
            CardBinder binder = new()
            {
                Name = name,
                CollectionId = collectionId,
            };

            //pull the collection
            Collection collection = await _context.Collections.SingleOrDefaultAsync(c => c.Id == collectionId);

            //validate
            if (collection == null) 
            {
                throw new KeyNotFoundException($"Collection {collectionId} not found.");
            }

            //add binder to collection
            collection.Binders.Add(binder);

            //save
            await _context.SaveChangesAsync();

        }

        public async Task DeleteBinderByNameAsync(string name)
        {
            CardBinder binder = await _context.Binders.SingleOrDefaultAsync(b => b.Name == name);

            //validate
            if (binder == null) 
            {
                throw new KeyNotFoundException($"{name} binder was not found.");
            }

            //remove
            _context.Binders.Remove(binder);

            //save 
            await _context.SaveChangesAsync();

        }

        public async Task<List<CardBinder>> GetAllBindersByCollectionIdAsync(string collectionId)
        {
            List<CardBinder> cardBinders = await _context.Binders
                .Where(b => b.CollectionId == collectionId)
                .ToListAsync();

            return cardBinders;
        }

        public async Task<CardBinder> GetBinderByIdAsync(string id)
        {
            CardBinder binder = await _context.Binders.SingleOrDefaultAsync(b => b.Id == id);

            //validate
            if (binder == null) 
            {
                throw new KeyNotFoundException($"Binder {id} not found.");
            }

            return binder;
        }

        public async Task<CardBinder> GetBinderByNameAsync(string name)
        {
            CardBinder binder = await _context.Binders.SingleOrDefaultAsync(b =>b.Name == name);

            //validate 
            if (binder == null) 
            {
                throw new KeyNotFoundException($"{name} binder was not found.");
            }

            return binder;
        }

        public async Task UpdateBinderNameByIdAsync(string id, string updatedName)
        {
            CardBinder binder = await _context.Binders.SingleOrDefaultAsync(b => b.Id == id);

            //validate
            if (binder == null) 
            {
                throw new KeyNotFoundException($"Binder {id} not found.");
            }

            //update binder to new name
            binder.Name = updatedName;

            //save
            await _context.SaveChangesAsync();
        }
    }
}
