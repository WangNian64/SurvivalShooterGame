using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int startHealth = 100;
    public int currentHealth;

    public Slider healthSlider;
    public Image damageImage;//受伤效果图

    public AudioClip deathClip;//死亡音效，是audioSource的属性
    PlayerMovement playerMovement;//死亡时disable该脚本

    AudioSource playerAudio;//AudioSource组件，默认音乐是受伤音乐，死亡时切换
    Animator anim;
    //控制屏幕变红的时间

    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    bool damaged;
    bool isDead;
	// Use this for initialization
	void Awake () {
        //绑定的都是Player自己的脚本
        playerMovement = GetComponent<PlayerMovement>();
        playerAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        currentHealth = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
        //检测是否受伤，若受伤，屏幕变红
		if (damaged)
        {
            damageImage.color = flashColor;//检测到受伤，修改颜色
        }else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);//颜色的恢复
        }
        damaged = false;
	}

    //若外部有人造成玩家受伤，调用该函数
    public void TakeDamage(int amount)
    {
        //血量减少，播放音效，触发死亡
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        //死亡时的动作：开启死亡动画，切换音效（变为死亡音效）,关闭走路脚本
        isDead = true;
        anim.SetTrigger("Die");//使用动画控制器触发死亡动画
        playerAudio.clip = deathClip;//播放死亡音乐
        playerAudio.Play();
        playerMovement.enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
