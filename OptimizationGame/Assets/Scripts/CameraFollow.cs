using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    private Vector3 camOffset;

    public float smoothSpeed = 0.125f;

    
    void Start()
    {
        camOffset = new Vector3(0, 0, -10);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void AssignCamera()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
  
    void LateUpdate()
    {
       Vector3 desiredPos = player.transform.position + camOffset;
       Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

       transform.position = smoothedPosition;
        
    }
}
