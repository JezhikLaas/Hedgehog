namespace Hedgehog.Persistence.Tickets;

public class TicketHistoryEntry
{
    public string Title { get; set; } = string.Empty;
    
    public DateTime StartedWork { get; set; }
    
    public DateTime FinishedWork { get; set; }

    public string Description { get; set; } = string.Empty;
}