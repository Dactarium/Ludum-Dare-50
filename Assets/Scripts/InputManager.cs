using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

    public Vector2 Movement {get; private set;}
    public Vector2 Mouse {get; private set;}
    public bool Attack {get; private set;} = false;

    private Vector2 _movement;
    private Vector2 _mouse;

    private bool _AxisFire = false;
    void Awake(){
        Instance = this;
    }

    void Update(){
        _movement = Vector2.zero;
        _mouse = Vector2.zero;

        //Move Input

        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        Movement = _movement;

        //Mouse Input
        _mouse.x = Input.GetAxis("Mouse X");
        _mouse.y = Input.GetAxis("Mouse Y");

        Mouse = _mouse;

        if(Input.GetAxisRaw("Fire1") != 0)
        {
            if(_AxisFire == false) _AxisFire = true;
         
        }
        if(Input.GetAxisRaw("Fire1") == 0)
        {
            _AxisFire = false;
        } 

        //Use Input
        Attack = _AxisFire;
    }

    
}
