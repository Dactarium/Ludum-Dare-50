using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public event Action<Npc> OnDeath;

    [HideInInspector] public Waypoint CurrentWaypoint;
    [HideInInspector] public NpcSpawner Spawner;
    public NamePicker.Type nameType;
    public float Speed = 1f;

    [SerializeField] private GameObject OnDestroyPrefab;

    private Waypoint _previousWaypoint;
    private float _previousBias = .05f;
    private float _reachDistance = .2f;
    private Vector3 _targetPosition;
    private Rigidbody _rigidbody;
    private Animator _animator;
    void Start(){
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _targetPosition = CurrentWaypoint.RandomPosition;
        transform.position = _targetPosition;

        _animator.enabled = false;
        StartCoroutine(EnableMove(UnityEngine.Random.Range(0f, 1f)));
    }

    void Update(){
        CheckReached();
        Move();
    }

    void CheckReached(){
        if((_targetPosition - transform.position).magnitude < _reachDistance){
            Waypoint nextWaypoint = null;

            List<Waypoint> connections = GetConnections();

            if(connections.Count == 0) return;
            
            Waypoint connection = connections[UnityEngine.Random.Range(0, connections.Count)];
               
            if(connections.Count>0)nextWaypoint = connection;
            else nextWaypoint = _previousWaypoint;

            _previousWaypoint = CurrentWaypoint;
            CurrentWaypoint = nextWaypoint;

            _targetPosition = CurrentWaypoint.RandomPosition;
        }

        
        transform.LookAt(_targetPosition);
        transform.eulerAngles = transform.eulerAngles.y * Vector3.up;
    }

    private List<Waypoint> GetConnections(){
        List<Waypoint> connections = CurrentWaypoint.Connections;
        if(UnityEngine.Random.Range(0f, 1f) >= _previousBias) connections.Remove(_previousWaypoint);
        return connections;
    }

    void Move(){
        Vector3 direction = (_targetPosition - transform.position).normalized;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.position += direction * Time.deltaTime * Speed;
    }

    IEnumerator EnableMove(float time){
        yield return new WaitForSeconds(time);
        _animator.enabled = true;
        if(GetComponent<AudioSource>())GetComponent<AudioSource>().Play();
    }

    public void Kill(){
        Spawner.Remove(gameObject);

        Destroy(gameObject);

        GameObject ghost = Instantiate(OnDestroyPrefab);

        ghost.transform.position = transform.position;
        ghost.transform.eulerAngles = -90f * Vector3.right + transform.eulerAngles.y * Vector3.up;

        Spawner.Spawn(ConfigManager.Instance.WalkableWaypointRoot.GetFarestWaypoint(transform.position));

        OnDeath?.Invoke(this);
    }
}
