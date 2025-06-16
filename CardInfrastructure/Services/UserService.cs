using CardLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardInfrastructure.Services
{
    public class UserService(CardDbContext context)
    {
        //field to store db connect
        private readonly CardDbContext _context = context;

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        public void CreateNewUser(string firstname, string lastName, string email) 
        {
            User user = new()
            {
                //id should be created by the Db
                //created at property populated when object is extentiated 
                //new collection as well. 
                FirstName = firstname,
                LastName = lastName,
                Email = email

            };

            //update db
            _context.Users.Add(user);

            //save db
            _context.SaveChanges();
        }

        /// <summary>
        /// Return a list of all Users
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsers() 
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Returns a single User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> GetUserById(string id) 
        {
            User user = await _context.Users.FindAsync(id);
            //validate
            if (user == null) 
            {
                throw new Exception($"Id of: {id} wasn't found");
            }

            return user;
        }

        /// <summary>
        /// Delete a User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public async void DeleteUserById(string id) 
        {
            User user = await GetUserById(id);

            //validate
            if (user == null) 
            {
                throw new Exception($"Id of: {id} wasn't found");
            }

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
        public async void UpdateUser(string id, string firstName, string lastName, string email) 
        {
            User user = await GetUserById(id);
            //validate 
            if (user == null)
            {
                throw new Exception($"Id of: {id} wasn't found");
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;

            //save updates
            await _context.SaveChangesAsync();

        }
    }
}
