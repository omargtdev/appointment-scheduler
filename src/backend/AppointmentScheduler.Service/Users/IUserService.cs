using AppointmentScheduler.Service.DTO.Users;

namespace AppointmentScheduler.Service.Users;

public interface IUserService
{
    UserAuthenticationDto Authenticate(string? username, string? password);

    void Register(UserRegistrationDto userRegistrationDto);

}
