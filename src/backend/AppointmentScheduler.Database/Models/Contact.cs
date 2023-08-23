namespace AppointmentScheduler.Database.Models;

public partial class Contact
{
    public int ContactId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Nickname { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int ContactUserId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual User ContactUser { get; set; } = null!;
}
