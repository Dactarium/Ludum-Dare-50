using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class GameEndState : GameBaseState
{   

    private ColorAdjustments _colorAdjustments;
    public override void BeginState(GameStateManager gameStateManager)
    {   
        Time.timeScale = 0.25f;

        if(!gameStateManager.GlobalVolume.profile.TryGet(out _colorAdjustments)) throw new System.Exception(nameof(_colorAdjustments));
        _colorAdjustments.active = true;

        UIManager.Instance.ShowOnEndUI(true);

        bool isWin = gameStateManager.TotalSoulCollected >= gameStateManager.WinCondition;
        UIManager.Instance.ShowEndStatus(isWin);

        AudioSource playerSource = gameStateManager.Player.GetComponent<AudioSource>();
        playerSource.clip = (isWin)? gameStateManager.WinSound: gameStateManager.LoseSound;
        playerSource.Play();

        gameStateManager.StartCoroutine(Restart());
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public override void EndState(GameStateManager gameStateManager)
    {
        _colorAdjustments.active = false;
        UIManager.Instance.ShowOnEndUI(false);
        Time.timeScale = 1f;
    }

    IEnumerator Restart(){
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
