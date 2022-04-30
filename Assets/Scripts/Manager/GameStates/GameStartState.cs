using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class GameStartState : GameBaseState
{
    private ColorAdjustments _colorAdjustments;
    private FilmGrain _filmGrain;
    public override void BeginState(GameStateManager gameStateManager)
    {
        InputManager.Instance.enabled = false;

        // if(!gameStateManager.GlobalVolume.profile.TryGet(out _colorAdjustments)) throw new System.Exception(nameof(_colorAdjustments));
        // _colorAdjustments.saturation.Override(-20);

        if(!gameStateManager.GlobalVolume.profile.TryGet(out _filmGrain)) throw new System.Exception(nameof(_filmGrain));
        _filmGrain.active = PlayerPrefs.GetInt("FilmGrain", 1) == 1;

    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public override void EndState(GameStateManager gameStateManager)
    {
        
    }
}
