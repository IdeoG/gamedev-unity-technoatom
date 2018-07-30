using UnityEngine;

public class Shell : MonoBehaviour
{
    private string parentTag;
    
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        var tag = other.collider.tag;

        if (tag.Equals("Enemy") && !tag.Equals(parentTag))
        {
            var hit = Random.Range(20f, 30f);
            Debug.LogFormat("Shell hitted EnemyTank. hit = {0}", hit);
            var enemy = other.collider.gameObject.GetComponent<EnemyTankController>();
            enemy.Hit(hit);
            
            Destroy(gameObject);
        }

        if (tag.Equals("Player") && !tag.Equals(parentTag))
        {
            var hit = Random.Range(20f, 30f);
            Debug.LogFormat("Shell hitted PlayerTank. hit = {0}", hit);
            var player = other.collider.gameObject.GetComponent<TankController>();
            player.Hit(hit);
            
            Destroy(gameObject);
        }
    }

    public void SetParentTag(string tag)
    {
        parentTag = tag;
        Debug.Log(parentTag);
    }
}