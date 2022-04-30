using System;

public abstract class GameEvent
{
    public abstract event Action<GameEvent> OnComplete;
    
    public abstract void Initial();

    public abstract void Execute();

    public abstract void Shutdown();
}
