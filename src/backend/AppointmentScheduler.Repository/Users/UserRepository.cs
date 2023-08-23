using AppointmentScheduler.Database;
using AppointmentScheduler.Database.Models;

namespace AppointmentScheduler.Repository.Users;

public class UserRepository : IUserRepository
{

    private readonly AppointmentSchedulerContext _appointmentSchedulerContext;

    public UserRepository(AppointmentSchedulerContext appointmentSchedulerContext)
    {
        _appointmentSchedulerContext = appointmentSchedulerContext;
    }

    public User? GetUserByEmailAndPassword(string email, string password) =>
        _appointmentSchedulerContext.Users.Where(user => user.Email == email && user.Password == password).FirstOrDefault();

}
