using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    Transform player;//玩家
    UnityEngine.AI.NavMeshAgent nav;//获得enemy的nav组件
	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>(); //绑定nav组件
	}
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(player.position);
	}
}
