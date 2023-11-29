using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : MonoBehaviour
{

	/* AI */
	GameObject section;
	NavMeshAgent agent;
	Animator anim;
	SectionStats sectionStats;
	EnemyStats enemyStats;
	EnemyCombat enemyCombat;
	Transform target;
	float stoppingDistance;

	public int attacks;

	public float cooldown = 1f;

	bool isAttacking = false;

	void Start()
	{
		section = ClosestSection().transform.parent.gameObject;
		target = ClosestSection().transform;
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
		sectionStats = section.GetComponent<SectionStats>();
		enemyStats = GetComponent<EnemyStats>();
		enemyCombat = GetComponent<EnemyCombat>();
		stoppingDistance = sectionStats.stoppingDistance;

		agent.Warp(transform.position);
	}

	void Update()
	{
		anim.SetBool("isAttacking", isAttacking);
		if (enemyStats.isDead || GameManager.instance.isLost)
		{
			StopMove();
			return;
		}
		if (target != null)
		{
			float distance = Vector3.Distance(target.position, transform.position);
			if (distance <= stoppingDistance)
			{
				AttackAnim();
			}
			else
			{
				agent.SetDestination(target.position);
			}
		}
		else
		{
			if (ClosestSection() == null)
			{
				GameManager.instance.Lose();
				return;
			}
			section = ClosestSection().transform.parent.gameObject;
			target = ClosestSection().transform;
			sectionStats = section.GetComponent<SectionStats>();
			enemyCombat.PassSection(sectionStats);
			agent.SetDestination(target.position);
		}
	}

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void AttackAnim()
	{
		isAttacking = true;
		if (!agent.isStopped)
		{
			StopMove();
		}
		if (target != null)
		{
			anim.SetTrigger("attack");
			anim.SetInteger("attackType", UnityEngine.Random.Range(0, attacks));
			enemyCombat.PassSection(sectionStats);
		}
	}

	GameObject ClosestSection()
	{
		var distances = new List<float>();
		var sections = SectionManager.instance.GetSections();
		var attackingPoints = new List<GameObject>();
		distances.Clear();
		attackingPoints.Clear();
		float closest = 0;
		int index = 0;
		if (sections.Count != 0)
		{
			for (int i = 0; i < sections.Count; i++)
			{
				if (sections[i] != null)
				{
					attackingPoints.AddRange(GetChildObject(sections[i].transform, "AP"));
				}
			}
			for (int i = 0; i < attackingPoints.Count; i++)
			{
				if (attackingPoints[i] != null)
				{
					distances.Add(Vector3.Distance(attackingPoints[i].transform.position, transform.position));
				}
			}
			if (distances.Count == 0)
			{
				return null;
			}
			closest = distances[0];
			foreach (float distance in distances)
			{
				if (distance <= closest)
				{
					closest = distance;
				}
			}
			index = distances.IndexOf(closest);
			return attackingPoints[index];
		}
		return null;
	}

	public void ResumePath()
	{
		isAttacking = false;
		if (enemyStats.isDead)
		{
			StopMove();
		}
		else
		{
			agent.isStopped = false;
		}
	}

	public List<GameObject> GetChildObject(Transform parent, string _tag)
	{
		List<GameObject> actors = new List<GameObject>();
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.tag == _tag)
			{
				actors.Add(child.gameObject);
			}
			if (child.childCount > 0)
			{
				GetChildObject(child, _tag);
			}
		}
		return actors;
	}

	public void StopMove()
	{
		if (!agent.isOnNavMesh)
		{
		}
		else
		{
			agent.isStopped = true;
		}
	}
}
