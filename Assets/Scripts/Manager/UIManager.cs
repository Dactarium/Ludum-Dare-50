using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] private GameObject OnPlaying;
    [SerializeField] private GameObject OnEnd;

    public string TargetName{
        set{
            _targetName.text = value;
        }
        get{
            return _targetName.text;
        }
    }
    [SerializeField] TextMeshProUGUI _targetName;
    
    public string SoulToDelay{
        set{
            _soulToDelay.text = "Need <color=#00AAFF>" + value + "</color> souls to delay";
        }
        get{
            return _soulToDelay.text;
        }
    }
    [SerializeField] TextMeshProUGUI _soulToDelay;

    public string Timer{
        set{
            _timer.text = value;
        }
        get{
            return _timer.text;
        }
    }

    [SerializeField] private TextMeshProUGUI _timer;

    public string SoulCounter{
        set{
            _soulCounter.text = value;
        }
        get{
            return _deathInfo.text;
        }
    }
    [SerializeField] private TextMeshProUGUI _soulCounter;

    public string DeathInfo{
        set{
            _deathInfo.text = value;
            _deathInfo.gameObject.SetActive(true);
            StartCoroutine(DisableDeathInfo());
        }
        get{
            return _deathInfo.text;
        }
    }
    [SerializeField] private TextMeshProUGUI _deathInfo;
    
    [SerializeField] float _infoTime;

    [SerializeField] private TextMeshProUGUI _win;
    [SerializeField] private TextMeshProUGUI _lose;

    void Awake(){
        Instance = this;
        ShowOnPlayingUI(false);
    }

    public void ShowOnPlayingUI(bool show){
         OnPlaying.SetActive(show);
    }

    public void ShowOnEndUI(bool show){
         OnEnd.SetActive(show);
    }

    public void ShowEndStatus(bool isWin){
        _win.gameObject.SetActive(isWin);
        _lose.gameObject.SetActive(!isWin);
    }

    public void ShowSoulToDelay(bool show){
        _soulToDelay.gameObject.SetActive(show);
    }

    IEnumerator DisableDeathInfo(){
        yield return new WaitForSeconds(_infoTime);
        _deathInfo.gameObject.SetActive(false);
    }
}
