using CardInfrastructure.DTO;
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

        public async Task<CardBinder> CreateBinderAsync(CreateBinderDto binderDto, Collection collection)
        {
            CardBinder binder = new()
            {
                Name = binderDto.Name,
                CollectionId = binderDto.CollectionId,
            };

            //add binder to collection
            collection.Binders.Add(binder);

            //save
            await _context.SaveChangesAsync();

            return binder;

        }

        public async Task<CardBinder> DeleteBinderAsync(CardBinder binder)
        {
            //remove
            _context.Binders.Remove(binder);

            //save 
            await _context.SaveChangesAsync();

            return binder;

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

        public async Task UpdateBinderNameByIdAsync(CardBinder binder, string updatedName)
        {
            
            //update binder to new name
            binder.Name = updatedName;

            //save
            await _context.SaveChangesAsync();
        }

        public async Task<Collection> ValidateCollection(string collectionId) 
        {
            //pull the collection
            Collection collection = await _context.Collections.SingleOrDefaultAsync(c => c.Id == collectionId);

            //return
            return collection;
        }
    }
}
