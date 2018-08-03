using UnityEngine;

/**
 * BulletBehaviour
 * Поведение, которое реализует сущность пули
 */
public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float _lifeTimeInSeconds;
    
    private void Start()
    {
        Destroy(gameObject, _lifeTimeInSeconds);
        
        // TODO: Добавить проигрывание анимации после уничтожения пули
    }

    private void OnCollisionEnter(Collision other)
    {
        var damage = Random.Range(20f, 30f);
        Debug.Log($"Bullet hitted {other.collider.name}. hit = {damage}");

        var fight = other.gameObject.GetComponent<FightComponent>();

        if (fight != null) fight.EnemyHit(damage);
            
        Destroy(gameObject);
    }
}