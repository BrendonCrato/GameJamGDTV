using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public float health;
    public float pointsToGive;
    private GameObject player;
    public float waitTime;
    public float currentTime;
    public bool shot;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    private Transform bulletSpawned;
    private Transform pistolHolder;

    //Methods
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pistolHolder = this.transform.GetChild(0);
        bulletSpawnPoint = pistolHolder.GetChild(0);
    }

    public void Update()
    {
        //if (!bulletSpawnPoint)
        //{
        //    pistolHolder = this.transform.GetChild(0);
        //    bulletSpawnPoint = pistolHolder.GetChild(0);
        //}

        if(health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

        if(currentTime == 0)
        {
            Shoot();
        }

        if (shot && currentTime < waitTime)
            currentTime += 1 * Time.deltaTime;

        if (currentTime >= waitTime)
            currentTime = 0;
    }

    public void Die()
    {
        Destroy(this.gameObject);

        player.GetComponent<AnimationAndMovementController>().points += pointsToGive;
    }

    public void Shoot()
    {
        shot = true;
        bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawned.rotation = this.transform.rotation;
    }
}
