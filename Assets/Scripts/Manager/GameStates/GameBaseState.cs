public abstract class GameBaseState
{
    public abstract void BeginState(GameStateManager gameStateManager);

    public abstract void UpdateState(GameStateManager gameStateManager);
    
    public abstract void EndState(GameStateManager gameStateManager);
}
