using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public GameObject player;
    public int attackDamage = 10;
    public float timeBetweenAttacks = 0.5f;

    Animator animatorController;
    PlayerHealth playerHealth;
    bool playerInRange;//判断是否发生碰撞
    float timer;//计时器，判断当前是否攻击
	// Use this for initialization
	void Awake () {
        animatorController = GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        timer = 0f;

	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Time.deltaTime的时长=一帧的时间
        timer += Time.deltaTime;
        //碰到玩家 且 到了攻击时间
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }

        //玩家死亡也会触发敌人动画
        if (playerHealth.currentHealth <= 0)
        {
            animatorController.SetTrigger("PlayerDead");
        }
    }

    private void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
