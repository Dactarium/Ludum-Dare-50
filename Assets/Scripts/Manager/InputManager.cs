using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    [SerializeField] private bl_Joystick _joystick;
    [SerializeField] private GameObject _attackButton;

    public event Action<InputManager> OnPause;
    public event Action<InputManager> OnUnpause;

    public Vector2 Movement
    {
        get
        {
            return _movement;
        }
    }
    public Vector2 Mouse
    {
        get
        {
            return _mouse;
        }
    }

    public float Scroll
    {
        get
        {
            return _scroll;
        }
    }

    public bool Attack { get; private set; } = false;
    public bool Pause { get; private set; } = false;
    private Vector2 _movement;
    private Vector2 _mouse;
    private float _scroll;

    private bool _axisFire = false;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Pause) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            TriggerPause();
        }

        if (Pause) return;

        //Move Input
        _movement = Vector2.zero;

        _movement.x += Input.GetAxis("Horizontal");
        _movement.y += Input.GetAxis("Vertical");

        Vector2 joystickInput = Vector2.right * _joystick.Horizontal + Vector2.up * _joystick.Vertical;
        joystickInput = joystickInput.normalized;
        print(joystickInput);
        _movement.x += joystickInput.x;
        _movement.y += joystickInput.y;
#if UNITY_ANDROID
#endif

        _movement = _movement.normalized;

        //Mouse Input
        _mouse.x = Input.GetAxis("Mouse X");
        _mouse.y = Input.GetAxis("Mouse Y");

        //Scroll Input
        _scroll = Input.mouseScrollDelta.y;

        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            _scroll += 1f;
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            _scroll -= 1f;
        }

        //Attack Input
        float fireInput = Input.GetAxis("Fire1");
#if UNITY_ANDROID
#else
        fireInput += Input.GetAxis("Fire1Mouse");
#endif

        if (fireInput != 0)
        {
            if (_axisFire == false) _axisFire = true;

        }
        if (fireInput == 0)
        {
            _axisFire = false;
        }

        Attack = _axisFire;
    }

    public void TriggerPause()
    {
        print(!Pause);
        Pause = !Pause;
        if (Pause) OnPause?.Invoke(this);
        else OnUnpause?.Invoke(this);
    }

    public void CheckJoystick()
    {
#if UNITY_ANDROID
        _joystick.gameObject.SetActive(true);
        _attackButton.SetActive(true);
#else
        _attackButton.SetActive(false);
        _joystick.gameObject.SetActive(false);
#endif
    }

    public void AttackButton()
    {
        Attack = _axisFire = true;
    }

    void OnDisable()
    {
        _movement = Vector2.zero;
        _mouse = Vector2.zero;
    }

}
