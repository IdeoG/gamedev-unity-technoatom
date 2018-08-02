using UnityEngine;

/**
 * EffectsComponent
 * Компонента, которая отвечает за возможность подбирать эффекты
 */
[RequireComponent(typeof(HealthComponent), typeof(MovementComponent))]
public class EffectsComponent : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private MovementComponent _movementComponent;

    private int _maxSpeedBottleEffectDurationInSeconds = 10;
    private int _speedBottleEffectDurationInSeconds;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _movementComponent = GetComponent<MovementComponent>();
    }

    private async void SpeedBottleEffect()
    {
        // TODO: Обновление UI о том, что SpeedBottle действует столько-то секунд
        
        // BUG: Что будет если за 10 секунд, условно, мы возьмём еще одну склянку? Правильно - БАГ!
        _speedBottleEffectDurationInSeconds += _maxSpeedBottleEffectDurationInSeconds;

        _movementComponent.MovementSpeed = _movementComponent.MaxMovementSpeed * 2f;

        await new WaitForSeconds(_speedBottleEffectDurationInSeconds);

        _movementComponent.MovementSpeed = _movementComponent.MaxMovementSpeed;
    }

    private void HealthBottleEffect()
    {
        var hp = Random.Range(20f, 30f);
        _healthComponent.EnhanceHealth(hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: ok");

        var isTagBottle = other.tag.Equals("Bottle");
        if (!isTagBottle) return;

        var isHealthBottle = other.name.Equals("HealthBottle");
        var isSpeedBottle = other.name.Equals("SpeedBottle");

        if (isHealthBottle) HealthBottleEffect();
        if (isSpeedBottle) SpeedBottleEffect();
    }
}