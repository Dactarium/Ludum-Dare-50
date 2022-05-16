using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{   
    public static GameEventManager Instance {get; private set;}

    #region Main Event
        public MainEvent CurrentMainEvent{
            set{
                _currentMainEvent.Shutdown();
                _currentMainEvent = value;
                Debug.Log("Current Main Event: " + _currentMainEvent.GetType());
                _currentMainEvent.Initial();
            }

            get{
                return _currentMainEvent;
            }
        }

        private MainEvent _currentMainEvent = NullEvent;

        //Main Events
        public static NullEvent NullEvent = new NullEvent();
        public static GetSoulFromTarget GetSoulFromTarget; 
        public static DelayYourDeath DelayYourDeath;
    #endregion
    
    #region Side Events
        private CollectLosts _collectLosts;
        private CollectingSoulTooSoon _collectingSoulTooSoon;
    #endregion

    void Awake(){
        Instance = this;
    }

    void Start(){
        GetSoulFromTarget = new GetSoulFromTarget();
        DelayYourDeath = new DelayYourDeath();
        _collectLosts = new CollectLosts();
        _collectingSoulTooSoon = new CollectingSoulTooSoon();

        _collectLosts.Initial();
        _collectingSoulTooSoon.Initial();
    }

    void Update(){
        _currentMainEvent.Execute();
    }

    public void SelectNullEvent() => CurrentMainEvent = NullEvent;
    
    public void RandomMainEvent() => NextMainEvent(DelayYourDeath, GetSoulFromTarget);
    public void NextMainEvent(params MainEvent[] mainEvents){
        CurrentMainEvent = MainEvent.GetMainEventByLuck(Random.Range(0f,1f), mainEvents);
    }
}
