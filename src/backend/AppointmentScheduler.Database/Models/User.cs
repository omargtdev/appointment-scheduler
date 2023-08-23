namespace AppointmentScheduler.Database.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Age { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
