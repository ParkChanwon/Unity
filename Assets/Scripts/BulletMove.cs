using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 8f;
    Rigidbody bulletRigidbody;
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;

    }

    // Update is called once per frame
    void Update()
    {
    
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZOMBIE"))
        {
            other.GetComponent<ZombieMove>().DecreseHP();
            Destroy(gameObject);
        }
    }

}
