using System;
using System.Collections;
using UnityEngine;

public class EnemyTankController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private LayerMask _targetMask;
    
    [SerializeField] private float _viewRadius;
    [Range(0, 360)] [SerializeField] private float _viewAngle;

    [SerializeField] private EnemyHealthBarController _healthBarController;
    
    private bool _isFindEnemiesActive = true;
    private float _health;

    public void Hit(float hit)
    {
        _health -= hit;
        _health = _health > 0f ? _health : 0f;
        
        _healthBarController.SetHealth(_health, _maxHealth);
        
        Debug.Log(string.Format("Hit: _health = {0}", _health));
        
        if (Math.Abs(_health) < 1f)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        StartCoroutine(FindEnemiesWithDelay(0.2f));
    }
    
    private void OnEnable()
    {
        _health = _maxHealth;
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
        var targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);

        foreach (var _collider in targetsInViewRadius)
        {    
            var target = _collider.transform;
            var dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
            {
//                if (_collider.name.Equals("EnemyTank"))
//                {
//                    Debug.DrawLine(transform.position, target.position, Color.red);
//                    _healthBarController.ShowHealthBar();
//                }
//                else
//                {
//                    _healthBarController.HideHealthBar();
//                }
                
            }
        }
        
        
    }
}
