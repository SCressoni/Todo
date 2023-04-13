namespace Todo.Domain.Entities;

public abstract class Entity : IEquatable<Entity>
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; private set; }

    // Metodo do IEquatable - permite verificar a igualdade entre dois objetos do mesmo tipo
    public bool Equals(Entity other)
    {
        return Id == other.Id;
    }
}