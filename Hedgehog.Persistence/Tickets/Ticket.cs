using Hedgehog.Persistence.Actors;

namespace Hedgehog.Persistence.Tickets;

public class Ticket
{
    public string Id { get; set; } = "Tickets|";

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public string Responsible { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime LastModified { get; set; }
    
    public DateTime SolutionExpectedBy { get; set; }

    public List<TicketHistoryEntry> History { get; } = new();

    public List<Clerk> Clerks { get; } = new();
}