using System;
using System.Collections;
using UnityEngine;

public class EnemyTankController : MonoBehaviour
{
    // TODO: Создать класс Attack, который будет пулять пули из танка
    [SerializeField] private Rigidbody _shellPrefab;
    [SerializeField] private Transform _barrel;
    [SerializeField] private Transform _turret;
    
    [SerializeField] private LayerMask _targetMask;

    [SerializeField] private float _viewRadius;
    [Range(0, 360)] [SerializeField] private float _viewAngle;

    [SerializeField] private EnemyHealthBarController _healthBarController;

    private bool _isFindEnemiesActive = true;

    private EnemyPathContol _agentPath;
    private HealthComponent _healthComponent;

    public void Hit(float hit)
    {
        _healthComponent.Health -= hit;
        _healthBarController.SetHealth(_healthComponent.Health, _healthComponent.MaxHealth);

        if (Math.Abs(_healthComponent.Health) < 1f)
        {
            Destroy(gameObject);
        }
    }

    private void FindVisibleEnemies()
    {
        var targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);

        foreach (var _collider in targetsInViewRadius)
        {
            var target = _collider.transform;
            var dirToTarget = (target.position - transform.position).normalized;
            if (_collider.name.Equals("PlayerTank"))
            {
                if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
                {
                    transform.rotation = Quaternion.LookRotation(dirToTarget);
                    _agentPath.PlayAgent = false;
                    
                    
                    var shell = Instantiate(_shellPrefab, _barrel.position, _barrel.rotation);
                    shell.AddForce(shell.transform.forward * 700);
                }
            }
            else
            {
                _agentPath.PlayAgent = true;
            }
        }

        if (targetsInViewRadius.Length == 0)
        {
            _agentPath.PlayAgent = true;
        }
    }

    private void Start()
    {
        const float delay = 0.5f;
        StartCoroutine(FindEnemiesWithDelay(delay));
    }

    private IEnumerator FindEnemiesWithDelay(float delay)
    {
        while (_isFindEnemiesActive)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleEnemies();
        }
    }

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _agentPath = GetComponent<EnemyPathContol>();
    }
}