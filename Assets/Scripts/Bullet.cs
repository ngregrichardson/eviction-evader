using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage = 10;
	public float range = 100f;
	public bool isPoop = false;
    EnemyStats enemyStats;

    void OnTriggerEnter(Collider other)
    {
		if(isPoop)
		{
			FindObjectOfType<AudioManager>().Play("PoopHit");
		}
		if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Turret"))
        {
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(damage);
        }
    }
}
