namespace Hedgehog.Persistence.Tickets;

public class Ticket
{
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

public class Clerk
{
    public string Name { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}