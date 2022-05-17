namespace Hedgehog.DTOs.Actors;

public class CreateRoleRequest : RoleRequest
{
    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}