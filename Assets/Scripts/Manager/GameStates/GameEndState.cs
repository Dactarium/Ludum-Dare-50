using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class GameEndState : GameBaseState
{   

    private ColorAdjustments _colorAdjustments;
   
    public override void BeginState(GameStateManager gameStateManager)
    {   
        #region Destroy player and spawn ghost
        GameObject.Destroy(gameStateManager.Player);

        GameObject ghost = GameObject.Instantiate(gameStateManager.Ghost);
        ghost.transform.position = gameStateManager.Player.transform.position;
        ghost.transform.eulerAngles = -90f * Vector3.right + gameStateManager.Player.transform.eulerAngles.y * Vector3.up;
        #endregion

        gameStateManager.Mixer.SetFloat("SFX", -80);
        
        Time.timeScale = 0.25f;

        if(!gameStateManager.GlobalVolume.profile.TryGet(out _colorAdjustments)) throw new System.Exception(nameof(_colorAdjustments));
        _colorAdjustments.saturation.Override(-80);

        UIManager.Instance.ShowOnEndUI(true);

        bool isWin = gameStateManager.TotalSoulCollected >= gameStateManager.WinCondition;
        UIManager.Instance.ShowEndStatus(isWin);

        AudioSource musicSource = MusicManager.Instance.GetComponent<AudioSource>();
        musicSource.Stop();
        musicSource.loop = false;
        musicSource.clip = (isWin)? gameStateManager.WinSound: gameStateManager.LoseSound;
        musicSource.Play();

        gameStateManager.StartCoroutine(Restart(gameStateManager));
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        
    }

    public override void EndState(GameStateManager gameStateManager)
    {
        gameStateManager.Mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX", 0));
        _colorAdjustments.saturation.Override(-20);
        UIManager.Instance.ShowOnEndUI(false);
        Time.timeScale = 1f;
    }

    IEnumerator Restart(GameStateManager gameStateManager){
        yield return new WaitForSecondsRealtime(5f);
        EndState(gameStateManager);
        SceneManager.LoadScene(0);
    }
}
