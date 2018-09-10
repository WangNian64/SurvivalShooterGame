using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int startHealth = 100;
    public int currentHealth;

    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;//分数
    public AudioClip deathClip;//死亡音效

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;//粒子效果
    CapsuleCollider capsuleCollider;//胶囊碰撞器
    bool isDead;
    bool isSinking;

	// Use this for initialization
	void Awake () {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        hitParticles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        //sink
		if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
	}
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }
        enemyAudio.Play();

        currentHealth -= amount;
        //播放粒子特效
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        anim.SetTrigger("Dead");

        //开启enemy的胶囊碰撞器
        capsuleCollider.isTrigger = true;
    }

    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;//消除重力
        //enemy死亡后让其下沉
        isSinking = true;
    }
}
