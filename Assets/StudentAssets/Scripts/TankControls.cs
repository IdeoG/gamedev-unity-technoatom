using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls : MonoBehaviour
{
    [SerializeField] private Rigidbody _shellPrefab;
    [SerializeField] private Transform _barrel;
    [SerializeField] private Transform _turret;

    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _turnTurretSpeed;

    [SerializeField] private float _viewRadius;
    [Range(0, 360)] [SerializeField] private float _viewAngle;

    [SerializeField] private LayerMask _targetMask;

    [SerializeField] private PlayerStatusBarControl _playerHealthBarController;
    [SerializeField] private EnemyHealthBarController _enemyHealthBarController;

    private float _forwardAxis;
    private float _sideAxis;
    private float _turretAxis;

    private bool _isFindEnemiesActive = true;
    private short speedBottleEffectInSeconds;
        
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(FindEnemiesWithDelay(0.2f));
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        TurnTurret();
    }

    private IEnumerator FindEnemiesWithDelay(float delay)
    {
        while (_isFindEnemiesActive)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleEnemies();
        }
    }

    private void FindVisibleEnemies()
    {
        var targetsInViewRadius = Physics.OverlapSphere(_turret.position, _viewRadius, _targetMask);

        foreach (var _collider in targetsInViewRadius)
        {
            var target = _collider.transform;
            var dirToTarget = (target.position - _turret.position).normalized;
            if (Vector3.Angle(_turret.forward, dirToTarget) < _viewAngle / 2)
            {
                if (_collider.name.Equals("EnemyTank"))
                {
                    Debug.DrawLine(_turret.position, target.position, Color.red);
                    _enemyHealthBarController.ShowHealthBar();
                }
            }
            else
            {
                _enemyHealthBarController.HideHealthBar();
            }
        }

        if (targetsInViewRadius.Length == 0)
        {
            _enemyHealthBarController.HideHealthBar();
        }
    }

    private void Move()
    {
        var shift = _speed * transform.forward * Time.deltaTime * _forwardAxis;
        _rigidbody.MovePosition(_rigidbody.position + shift);
    }

    private void Turn()
    {
        var turn = _turnSpeed * Time.deltaTime * _sideAxis;
        turn = _forwardAxis < 0 ? -turn : turn;
        var turnY = Quaternion.Euler(0, turn, 0);

        _rigidbody.MoveRotation(_rigidbody.rotation * turnY);
    }

    private void TurnTurret()
    {
        var turn = _turnTurretSpeed * Time.deltaTime * _turretAxis;
        var turnY = Quaternion.Euler(0, turn, 0);

        _turret.localRotation *= turnY;
    }

    private void Update()
    {
        _forwardAxis = Input.GetAxis("Vertical");
        _sideAxis = Input.GetAxis("Horizontal");
        _turretAxis = Input.GetAxis("Mouse X");

        if (Input.GetMouseButtonDown(0))
        {
            // TODO: Добавить скрипт на Пулю -> уменьшение здоровья противника

            var shell = Instantiate(_shellPrefab, _barrel.position, _barrel.rotation);
            shell.AddForce(shell.transform.forward * 700);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var name = other.name;
        var tag = other.tag;
        Debug.Log(string.Format("collider name: {0}, tag = {1}", name, tag));

        if (tag.Equals("Bottle"))
        {
            if (name.Equals("SpeedBottle"))
            {
                if (speedBottleEffectInSeconds == 0)
                {
                    _speed *= 2;
                    speedBottleEffectInSeconds = 10;
                    StartCoroutine(SpeedBottleEffect());
                
                    // TODO: Выполнить анимацию на разрушение танчика, после чего он уничтожится из своего скрипта
                    Destroy(other.gameObject);
 
                }
            }

            if (name.Equals("HealthBottle"))
            {
                
            }
        }
    }

    private IEnumerator SpeedBottleEffect()
    {
        while ( (speedBottleEffectInSeconds--) != 0)
        {
            yield return new WaitForSeconds(1f);
            SpeedBottleUpdateUI();
            
        }
        
    }

    private void SpeedBottleUpdateUI()
    {
        var text = string.Format ("Speed Effect x2  = {0} seconds", speedBottleEffectInSeconds);
        _playerHealthBarController.SetText (text);

        if (speedBottleEffectInSeconds == 0)
        {
            _playerHealthBarController.SetText("");
            _speed /= 2f;
        }
    }
}