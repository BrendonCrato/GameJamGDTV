using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variable
    public float speed;
    public float maxDistance;
    public float damage;

    private GameObject triggeringEnemy;
    private GameObject player;

    // Method 

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        maxDistance += 1 * Time.deltaTime;
        if(maxDistance >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
        }

        if(other.tag == "Player")
        {
            player.GetComponent<AnimationAndMovementController>().health -= 20;
        }
    }
}
