using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour {

    EnemyStats enemyStats;
    float attackCooldown;
    SectionStats sectionStats;
	public string attackSound;
	public bool hasAttackSound = true;

	// Use this for initialization
	void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void PassSection(SectionStats _sectionStats)
    {
        sectionStats = _sectionStats;
    }

    public void Attack()
    {
        if(attackCooldown <= 0)
        {
			if(hasAttackSound)
			{
				FindObjectOfType<AudioManager>().Play(attackSound);
			}
            sectionStats.TakeDamage(enemyStats.damage);
            attackCooldown = enemyStats.cooldown;
        }
    }
}
