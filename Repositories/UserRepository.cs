﻿using Microsoft.EntityFrameworkCore;
using nettbutikk_api.Data;
using nettbutikk_api.Models.Entities;
using nettbutikk_api.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nettbutikk_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly nettButikkDbContext _context;

        public UserRepository(nettButikkDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> AddUserAsync(User user)
        {
            user.Created = DateTime.Now;
            var newUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return newUser.Entity;
        }

        public async Task<User?> UpdateUserAsync(int userId, User user)
        {
            var userToUpdate = await _context.Users.FindAsync(userId);

            if (userToUpdate != null && user != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Username = user.Username;
                userToUpdate.Email = user.Email;
                userToUpdate.Updated = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            return userToUpdate;
        }

        public async Task<User?> DeleteUserByIdAsync(int userId)
        {
            var userToDelete = await GetUserByIdAsync(userId);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
            return userToDelete;
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.Equals(name));

            return user;

        }
    }
}
