using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{

    public string enemyName;
    public double worth;
    public float damage;
    public float health;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Damage(float _damage)
    {
        health -= _damage;
        if(isDead())
        {
            Die();
        }
    }

    public bool isDead()
    {
        if (health <= 0)
        {
            return true;
        }
        return false;
    }

    void Die()
    {
        CurrencyManager.instance.Add(worth);
        anim.SetBool("isDead", true);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
