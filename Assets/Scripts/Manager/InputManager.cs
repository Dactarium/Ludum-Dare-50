using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

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
    public bool Attack {get; private set;} = false;

    private Vector2 _movement;
    private Vector2 _mouse;

    private bool _AxisFire = false;
    void Awake(){
        Instance = this;
    }

    void Update(){
        //Move Input

        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        //Mouse Input
        _mouse.x = Input.GetAxis("Mouse X");
        _mouse.y = Input.GetAxis("Mouse Y");

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
    
    void OnDisable(){
        _movement = Vector2.zero;
        _mouse = Vector2.zero;
    }

    
}
