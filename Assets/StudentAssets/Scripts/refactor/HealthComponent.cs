using UniRx;
using UnityEngine;

/**
 * HealthComponent
 * Компонента, которая отвечает за возможность хранения жизни
 */
public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public float MaxHealth { get; private set; }
    public ReactiveProperty<float> Health { get; private set; }

    public void ReduceHealth(float value)
    {
        // TODO: Проверить будет ли вызываться onNext, если мы дергаем _health
        Health.Value = Mathf.Clamp(Health.Value - value, 0, _maxHealth);
    }
    
    public void EnhanceHealth(float value)
    {
        // TODO: Проверить будет ли вызываться onNext, если мы дергаем _health
        Health.Value = Mathf.Clamp(Health.Value + value, 0, _maxHealth);
    }
    
    private void Awake()
    {
        MaxHealth = _maxHealth;
        Health = new ReactiveProperty<float>(_maxHealth);
    }
    
}