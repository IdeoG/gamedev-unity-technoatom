using UniRx;
using UnityEngine;

/**
 * FightComponent
 * Компонента, которая отвечает за возможность атаковать и принимать урон
 */
[RequireComponent(typeof(HealthComponent))]
public class FightComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform _bulletInitPoint;
    [SerializeField] private float _bulletForce;

    private HealthComponent _healthComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        var inputs = Inputs.Instance;

        inputs.Fire
            .Subscribe(_ => Attack())
            .AddTo(this);
    }

    private void Attack()
    {
        var bullet = Instantiate(_bulletPrefab, _bulletInitPoint.position, _bulletInitPoint.rotation);
        bullet.AddForce(bullet.transform.forward * _bulletForce);

        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
    }

    public void EnemyHit(float damage)
    {
        _healthComponent.ReduceHealth(damage);
    }
}