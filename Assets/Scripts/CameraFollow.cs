using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;//相机跟随的目标
    public float smoothing = 5f;
    Vector3 offset;//相机距离玩家的偏移
	// Use this for initialization
	void Start () {
        offset = transform.position - target.transform.position;//offset从target指向camera
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
