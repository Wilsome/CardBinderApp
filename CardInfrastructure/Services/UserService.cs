using CardInfrastructure.DTO;
using CardInfrastructure.Interfaces;
using CardLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Services
{
    public class UserService(CardDbContext context) : IUserService
    {
        //field to store db connect
        private readonly CardDbContext _context = context;

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        public async Task<User> CreateNewUserAsync(User user) 
        {
            //update db
            _context.Users.Add(user);

            //save db
            await _context.SaveChangesAsync();
            
            return user;
        }

        /// <summary>
        /// Return a list of all Users
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync() 
        {
            return await _context.Users.ToListAsync();
        }


        /// <summary>
        /// Returns a single User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> GetUserByIdAsync(string id) 
        {
            User user = await _context.Users.FindAsync(id);
        
            return user;
        }

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public async Task DeleteUserAsync(User user) 
        {
            //remove
            _context.Users.Remove(user);
            //save
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update a Users name and/or email. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <exception cref="Exception"></exception>
        public async Task UpdateUserAsync(User user) 
        {
            //save updates
            await _context.SaveChangesAsync();

        }
    }
}
