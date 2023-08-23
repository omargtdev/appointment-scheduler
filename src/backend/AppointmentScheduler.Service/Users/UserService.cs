using System.Security.Authentication;
using AppointmentScheduler.Repository.Users;
using AppointmentScheduler.Service.DTO.Users;
using AppointmentScheduler.Util;

namespace AppointmentScheduler.Service.Users;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserAuthenticationDto Authenticate(string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email), "Not allowed to be null or empty.");

        if (password is null)
            throw new ArgumentNullException(nameof(password), "Not allowed to be null.");

        var user = _userRepository.GetUserByEmailAndPassword(email, Encrypter.ToSha256(password)) ?? throw new AuthenticationException($"Invalid credentails.");

        // TODO: Implement automapper
        return new UserAuthenticationDto { UserId = user.UserId , Email = user.Email };
    }

    public void Register(UserRegistrationDto userRegistrationDto)
    {
        //1. Valid dto

        //2. If not valid, return reason exceptions

        //3. Check if user is already registered with that email

        //4. If user is already registered, throw exception

        //5. If user is not registered, create new user
        throw new NotImplementedException();
    }
}
