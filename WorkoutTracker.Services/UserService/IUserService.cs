using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Services
{
    public interface IUserService : IEntityService<user>
    {
        IEnumerable<user> GetUsers();
        user GetUserByUserName(string userName);
        user GetUser(string userName, string password);
        void CreateUser(user user);
        void UpdateUser(user user);
        void DeleteUser(user user);
    }
}
