using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

    public event Action<InputManager> OnPause;
    public event Action<InputManager> OnUnpause;

    public Vector2 Movement{
        get{
            return _movement;
        }
    } 
    public Vector2 Mouse {
        get{
            return _mouse;
        }
    }

    public float Scroll {
        get{
            return _scroll;
        }
    }
    
    public bool Attack {get; private set;} = false;
    public bool Pause {get; private set;} = false;
    private Vector2 _movement;
    private Vector2 _mouse;
    private float _scroll;

    private bool _axisFire = false;
    void Awake(){
        Instance = this;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause) || Input.GetKeyDown(KeyCode.JoystickButton7)){
            TriggerPause();
        }

        if(Pause) return;

        //Move Input
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        //Mouse Input
        _mouse.x = Input.GetAxis("Mouse X");
        _mouse.y = Input.GetAxis("Mouse Y");

        //Scroll Input
        _scroll = Input.mouseScrollDelta.y;

        if(Input.GetKeyDown(KeyCode.JoystickButton4)){
            _scroll += 1f;
        }
        
        if(Input.GetKeyDown(KeyCode.JoystickButton5)){
            _scroll -= 1f;
        }

        //Attack Input
        if(Input.GetAxisRaw("Fire1") != 0)
        {
            if(_axisFire == false) _axisFire = true;
         
        }
        if(Input.GetAxisRaw("Fire1") == 0)
        {
            _axisFire = false;
        }

        Attack = _axisFire;
    }
    
    public void TriggerPause(){
        print(!Pause);
        Pause = !Pause;
        if(Pause)OnPause?.Invoke(this);
        else OnUnpause?.Invoke(this);
    }

    void OnDisable(){
        _movement = Vector2.zero;
        _mouse = Vector2.zero;
    }
    
}
