namespace Hedgehog.Persistence.Actors;

public class User
{
    public string Id { get; set; } = "Users|";

    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string EMail { get; set; } = string.Empty;
}