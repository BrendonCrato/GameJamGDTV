using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smooth = 0.3f;
    public float offSet = 7f;
    public float height;

    private Vector3 velocity = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - offSet; 
        pos.y = player.position.y + height;

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);

        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            height += Input.GetAxis("Mouse ScrollWheel");
            offSet += Input.GetAxis("Mouse ScrollWheel");
        }
    }
}
