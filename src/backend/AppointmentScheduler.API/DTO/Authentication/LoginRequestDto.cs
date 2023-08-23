namespace AppointmentScheduler.API.DTO.Authentication;

public record LoginRequestDto(string? Email, string? Password);