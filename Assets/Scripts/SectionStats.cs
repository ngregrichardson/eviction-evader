using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionStats : MonoBehaviour {

    public int maxDurability = 5;
    public int currentDurability { get; private set; }
    public int stoppingDistance = 15;
	public GameObject replacement;
	public SimpleHealthBar durabilityBar;

	private void Awake()
    {
        currentDurability = maxDurability;
    }

    public void TakeDamage(int damage)
    {
        currentDurability -= damage;

		durabilityBar.UpdateBar(currentDurability, maxDurability);

		if (currentDurability <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
		FindObjectOfType<AudioManager>().Play("SectionDie");
		SectionManager.instance.sections.Remove(gameObject);
		Instantiate(replacement);
        Destroy(gameObject);
		Destroy(durabilityBar);
    }
}
