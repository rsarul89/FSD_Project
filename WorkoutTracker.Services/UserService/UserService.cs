using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories;

namespace WorkoutTracker.Services
{
    public class UserService : EntityService<user>, IUserService
    {
        IUnitOfWork unitOfWork;
        IUserRepository userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository) : base(unitOfWork, userRepository)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateUser(user user)
        {
            var usr = userRepository.FindBy(u => u.user_name.Equals(user.user_name, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            if (usr == null)
            {
                Create(user);
            }
        }

        public void DeleteUser(user user)
        {
            var usr = userRepository.FindBy(u => u.user_name.Equals(user.user_name, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            if (usr != null)
            {
                Delete(user);
            }
        }

        public user GetUser(string userName, string password)
        {
            return userRepository.FindBy(u => u.user_name.Equals(userName, StringComparison.InvariantCultureIgnoreCase) && u.password == password)
                .FirstOrDefault();
        }

        public user GetUserByUserName(string userName)
        {
            return userRepository.FindBy(u => u.user_name.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }

        public IEnumerable<user> GetUsers()
        {
            return userRepository.GetAll();
        }
        public void UpdateUser(user user)
        {
            var usr = userRepository.FindBy(u => u.user_name.Equals(user.user_name, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            if (usr != null)
            {
                Update(user);
            }
        }
    }
}
