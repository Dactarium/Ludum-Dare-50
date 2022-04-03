using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : GameBaseState
{
    public override void BeginState(GameStateManager gameStateManager)
    {
        InputManager.Instance.enabled = false;
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public override void EndState(GameStateManager gameStateManager)
    {
        
    }
}
