using System;

public class NullEvent : MainEvent
{
    public override event Action<GameEvent> OnComplete;

    public override void Initial(){}

    public override void Execute(){}

    public override void Shutdown(){}
}
