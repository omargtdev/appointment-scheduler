namespace AppointmentScheduler.API;

public record RegisterRequestDto(
    string? Email, string? Name, string? LastName,
    string? Password, string? ConfirmPassword, int? Age
);