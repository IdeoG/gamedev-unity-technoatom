using System;
using UniRx;
using UnityEngine;

public class _HealthComponent : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    
    public ReactiveProperty<float> Health;

    public float MaxHealth => _maxHealth;
    
    private void OnEnable()
    {
        Health = new ReactiveProperty<float>(_maxHealth);
    }
    
}