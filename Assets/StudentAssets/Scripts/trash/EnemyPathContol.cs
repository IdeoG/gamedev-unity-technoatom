using UnityEngine;
using UnityEngine.AI;

public class EnemyPathContol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    private NavMeshAgent _agent;
    private int _currentWaypoint;

    public bool PlayAgent
    {
        set { _agent.isStopped = !value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        var distance = Vector3.Distance(other.transform.position, _waypoints[_currentWaypoint].position);
        if (distance < 0.1f)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
            _agent.destination = _waypoints[_currentWaypoint].position;
        }
    }
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.destination = _waypoints[_currentWaypoint].position;
    }

}