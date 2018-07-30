using UnityEngine;

public class Shell : MonoBehaviour {

	private void Start () {
		Destroy(gameObject, 1.5f);
	}

	private void OnCollisionEnter(Collision other)
	{
		var name = other.collider.name;
		var tag = other.collider.tag;

		if (tag.Equals("Enemy"))
		{
			if (name.Equals("EnemyTank"))
			{
				Debug.Log("Shell hitted EnemyTank.");
				var enemy = other.collider.gameObject.GetComponent<EnemyTankController>();
				enemy.Hit(Random.Range(20f, 30f)); 
			}
		}
	}
}
