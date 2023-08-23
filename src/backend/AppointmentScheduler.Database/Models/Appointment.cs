namespace AppointmentScheduler.Database.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public string Subject { get; set; } = null!;

    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public string? Notes { get; set; }

    public short AppointmentStateId { get; set; }

    public int? AppointmentContactId { get; set; }

    public int AppointmentUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Contact? AppointmentContact { get; set; }

    public virtual AppointmentState AppointmentState { get; set; } = null!;

    public virtual User AppointmentUser { get; set; } = null!;
}
