using UnityEngine;
using UnityEngine.Rendering;
public class GameStateManager : MonoBehaviour
{   
    public static GameStateManager Instance {get; private set;}

    public GameObject Player;
    public GameObject TargetPointer;

    public Volume GlobalVolume;

    public int WinCondition = 5;
    public int TotalSoulCollected = 0;

    public AudioClip WinSound;
    public AudioClip LoseSound;

    private GameBaseState _currentState;
    public readonly GameStartState GameStartState = new GameStartState();
    public readonly GamePlayingState GamePlayingState = new GamePlayingState();
    public readonly GameEndState GameEndState = new GameEndState();

    void Awake(){
        Instance = this;
    }
    void Start(){
        _currentState = GameStartState;
        _currentState.BeginState(this);

        print("Current State : " + _currentState.GetType());
    }

    void Update(){
        _currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state){
        _currentState.EndState(this);
        _currentState = state;
        _currentState.BeginState(this);

        print("Current State : " + state.GetType());
    }
}
