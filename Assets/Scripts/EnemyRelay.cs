using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRelay : MonoBehaviour {

    EnemyCombat enemyCombat;
    EnemyController enemyController;
    EnemyStats enemyStats;
	Animator anim;

    void Start()
    {
        enemyCombat = GetComponentInParent<EnemyCombat>();
        enemyController = GetComponentInParent<EnemyController>();
        enemyStats = GetComponentInParent<EnemyStats>();
		anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        enemyCombat.Attack();
    }

    public void ResumePath()
    {
        enemyController.ResumePath();
    }

    public void Remove()
    {
        enemyStats.Remove(gameObject.transform.parent.gameObject);
    }

    public void StopMove()
    {
        enemyController.StopMove();
    }
}
