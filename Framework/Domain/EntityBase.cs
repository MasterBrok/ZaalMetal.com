namespace Framework.Domain;

public abstract class EntityBase
{
    protected EntityBase()
    {
        Id = Guid.NewGuid().ToString("N");
        CreationTimeAt = DateTime.UtcNow;
    }

    public string Id { get; private set; }
    public DateTime CreationTimeAt { get; private set; }
    public State State { get; private set; }


    public void ChangeState(State state) => State = state;
}

