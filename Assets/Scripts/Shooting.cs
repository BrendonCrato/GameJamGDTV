using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float movementSpeed;
    public GameObject camera;

    public GameObject bulletSpawnPoint;
    public float waitTime;
    public GameObject bullet;
    public float currentTime;
    public bool shot;

    private Transform bulletSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ////Player facing mouse
        //Plane playerPlane = new Plane(Vector3.up, transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float hitDist = 0.0f;

        //if (playerPlane.Raycast(ray, out hitDist))
        //{
        //    Vector3 targetPoint = ray.GetPoint(hitDist);
        //    Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        //    targetRotation.x = 0;
        //    targetRotation.z = 0;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.W))
        //    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);



        if (Input.GetMouseButton(0))
        {
            if (currentTime == 0)
            {
                Shoot();
                Debug.Log(currentTime + " " + waitTime);
            }
        }

        if (shot && currentTime < waitTime)
            currentTime += 1 * Time.deltaTime;

        if (currentTime >= waitTime)
            currentTime = 0;
    }

    void Shoot()
    {
        shot = true;
        bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawned.rotation = bulletSpawnPoint.transform.rotation;
    }
}
