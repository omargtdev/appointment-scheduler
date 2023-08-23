namespace AppointmentScheduler.Database.Models;

public partial class AppointmentState
{
    public short AppointmentStateId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
