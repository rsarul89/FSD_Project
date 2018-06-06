using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories
{
    public interface IUserRepository : IGenericRepository<user>
    {
        void AddUser(user user);
        void DeleteUser(user user);
        void UpdateUser(user user);
    }
}
