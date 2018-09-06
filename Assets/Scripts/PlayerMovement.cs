using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 6f;//玩家移动速度
    Vector3 movement;//玩家位移
    int floorMask;//floor图层
    Animator anim;//绑定动画组件
    float camRayLength;//相机射线长度
    Rigidbody playerRigidbody;//玩家刚体属性
    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        PlayerMove(h, v);//运动
        PlayerTurning();//转向

	}

    private void PlayerMove(float h, float v)
    {
        movement.Set(h, 0f, v);//设置玩家运动向量
        movement = movement.normalized * speed * Time.deltaTime;//设置每帧的位移
        playerRigidbody.MovePosition(transform.position + movement);
    }
    private void PlayerTurning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);//从鼠标位置发出与摄像机射线平行的射线。
        RaycastHit floorHit;//射线碰撞点，这里是鼠标射线与floor的交点
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) //如果射线击中了Floor层上的物体(点击到其他层不监测射线碰撞)
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;
            //根据位移信息 得到 旋转信息,然后旋转角色
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }
}
