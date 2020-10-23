using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTouchMove : MonoBehaviour
{
    public float speed;
    public Vector2 startPos;
    public Camera cam;
    private float startPositionPlayerZ;
    private float startPositionCamera;
    public float minZ;
    public float maxZ;

    public float targetPos;
    public float pos;

    public Transform playerTransform;
    void Start()
    {
        cam = GetComponent<Camera>();
        targetPos = transform.position.z;
        startPositionCamera = transform.position.z;
        startPositionPlayerZ = playerTransform.position.z;
      
    }
 

    private void Update()
    {  
        pos = startPositionPlayerZ - playerTransform.position.z; 
        targetPos = Mathf.Clamp(  pos, minZ, maxZ);  
        transform.position = new Vector3(transform.position.x, transform.position.y, startPositionCamera - targetPos);
 
    }

}