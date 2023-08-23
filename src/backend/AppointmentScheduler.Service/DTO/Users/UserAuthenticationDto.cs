namespace AppointmentScheduler.Service.DTO.Users;

public class UserAuthenticationDto
{
    public int UserId { get; set; }

    public string Email { get; init; } = null!;
    
}
