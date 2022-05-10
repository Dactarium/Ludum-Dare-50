using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] private GameObject OnPlaying;
    [SerializeField] private GameObject OnPause;
    [SerializeField] private GameObject OnPauseSelected;
    [SerializeField] private GameObject OnConditionComplete;
    [SerializeField] private GameObject OnConditionCompleteSelected;
    [SerializeField] private GameObject OnEnd;

    public string MainEventDetail{
        set{
            _mainEventDetail.text = value;
        }
        get{
            return _mainEventDetail.text;
        }
    }
    [SerializeField] TextMeshProUGUI _mainEventDetail;

    public string Timer{
        set{
            _timer.text = value;
        }
        get{
            return _timer.text;
        }
    }
    
    [SerializeField] private TextMeshProUGUI _timer;

    public string TaskCounter{
        set{
            _taskCounter.text = value;
        }
        get{
            return _taskCounter.text;
        }
    }
    [SerializeField] private TextMeshProUGUI _taskCounter;

    [SerializeField] private TextMeshProUGUI _win;
    [SerializeField] private TextMeshProUGUI _lose;

    void Awake(){
        Instance = this;
        ShowOnPlayingUI(false);
    }

    public void ShowOnPlayingUI(bool show){
        OnPlaying.SetActive(show);
    }

    public void ShowOnPauseUI(bool show){
        OnPause.SetActive(show);

        if(show) SelectEventSystemCurrent(OnPauseSelected);
    }

    public void ShowOnConditionComplete(bool show){
        OnConditionComplete.SetActive(show);

        if(show) SelectEventSystemCurrent(OnConditionCompleteSelected);
    }

    public void ShowOnEndUI(bool show){
         OnEnd.SetActive(show);
    }

    public void ShowEndStatus(bool isWin){
        _win.gameObject.SetActive(isWin);
        _lose.gameObject.SetActive(!isWin);
    }

    public void ReturnMainMenu(){
        SceneManager.LoadScene(0);
    }

    private void SelectEventSystemCurrent(GameObject selected){

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selected);

    }
}
