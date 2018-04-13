using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;

    GameObject player;
    PlayerHeath playerHeath;
    bool playerInRange;
    float timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHeath = player.GetComponent<PlayerHeath>();
        anim = GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }

        if(playerHeath.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }

	}

    void Attack()
    {
        timer = 0;
        if(playerHeath.currentHealth > 0)
        {
            playerHeath.TakeDamage(attackDamage);
        }

    }

}
