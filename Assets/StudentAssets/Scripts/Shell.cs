using UnityEngine;

public class Shell : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        var tag = other.collider.tag;

        if (tag.Equals("Enemy"))
        {
            var hit = Random.Range(20f, 30f);
            Debug.LogFormat("Shell hitted EnemyTank. hit = {0}", hit);
            var enemy = other.collider.gameObject.GetComponent<EnemyTankController>();
            enemy.Hit(hit);
        }

        if (tag.Equals("Player"))
        {
            var hit = Random.Range(20f, 30f);
            Debug.LogFormat("Shell hitted PlayerTank. hit = {0}", hit);
            var player = other.collider.gameObject.GetComponent<TankController>();
            player.Hit(hit);
        }
    }
}