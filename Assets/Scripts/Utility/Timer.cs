using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance{get; private set;}

    public event Action<Timer> OnZero;
    
    [SerializeField] private GameObject _timeGainPrefab;
    [SerializeField] private Vector2 _position;

    private Queue<GameObject> _spawnQueue = new Queue<GameObject>();
    private bool _isSpawned = true;
    private float _time = -1;

    private float CurrentTime{
        set{
            _time = value;
            UIManager.Instance.Timer = Mathf.CeilToInt(_time).ToString();
            if(_time <= 0) OnZero?.Invoke(this);
        }
        get{
            return _time;
        }
    }
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(InputManager.Instance.Pause)return;

        if(CurrentTime >= 0) CurrentTime -= Time.deltaTime;

        if(_isSpawned && _spawnQueue.Count > 0) StartCoroutine(ShowTimeGain());
    }

    public void Set(float seconds){
        CurrentTime = seconds;
    }

    public void Add(float seconds){
        CurrentTime += seconds;
        GameObject timegain = Instantiate(_timeGainPrefab, transform.parent as RectTransform);
        timegain.GetComponent<RectTransform>().anchoredPosition = _position;

        if(seconds < 0) {
            timegain.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }else{
            timegain.GetComponentInChildren<TextMeshProUGUI>().text = "+";
        }

        timegain.GetComponentInChildren<TextMeshProUGUI>().text += seconds.ToString();

        timegain.SetActive(false);

        _spawnQueue.Enqueue(timegain);
    }

    IEnumerator ShowTimeGain(){
        _isSpawned = false;
        yield return new WaitForSeconds(0.2f);
        _spawnQueue.Dequeue().SetActive(true);
        _isSpawned = true;
    }
}
