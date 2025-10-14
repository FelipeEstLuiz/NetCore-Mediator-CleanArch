namespace NetCore_Mediator_CleanArch.Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }
    public string? Name { get; protected set; }
}
