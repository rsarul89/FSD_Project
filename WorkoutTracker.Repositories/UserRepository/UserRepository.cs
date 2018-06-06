using System.Data.Entity;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories
{
    public class UserRepository : GenericRepository<user>, IUserRepository
    {
        public UserRepository(DbContext context)
           : base(context) { }

        public void AddUser(user user)
        {
            Add(user);
        }

        public void DeleteUser(user user)
        {
            Delete(user);
        }

        public void UpdateUser(user user)
        {
            Update(user);
        }
    }
}
