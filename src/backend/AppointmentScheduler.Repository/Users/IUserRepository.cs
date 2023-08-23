using AppointmentScheduler.Database.Models;

namespace AppointmentScheduler.Repository.Users;

public interface IUserRepository
{
    User? GetUserByEmailAndPassword(string email, string password);

}
