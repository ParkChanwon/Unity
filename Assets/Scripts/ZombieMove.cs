using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    public Transform FreeZombie;
    public AudioClip zomDead;
    Transform Player;
    int speed;
    float HP;
    float delayTime = 1f;
    private bool isDead = false;

    void Start()
    {
        Player = GameObject.Find("Player").transform;
        speed = 4;
        HP = 1;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        float dist = Vector3.Distance(transform.position, Player.position);
        if(dist > 1){
            float deltaMove = speed * Time.smoothDeltaTime;
            transform.Translate(Vector3.forward * deltaMove);
        }
    }
    void OnCollisionEnter(Collision coll){
        switch(coll.transform.tag){
            case "Bullet":
            DecreseHP();
            break;
        }
    }


    void OnCollisionStay(Collision coll){
        switch(coll.transform.tag){
            case "Bullet":
                DecreseHP();
                break;
        }
    }
    public void DecreseHP(){
        if(isDead)return;
        HP--;
        Debug.Log($"Zombie HP: {HP} ");
        if(HP <= 0){
            Die();
        }
    }
    void Die(){
        if(isDead)return;
        isDead = true;
        Debug.Log("Zombie Die");
        ZombieManager.Instance.RemoveZombie(); // UI 좀비 카운트--
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) //총알과 부딪힘
        {
            Debug.Log("Bullet hit Zombie!");
            DecreseHP();
            Destroy(other.gameObject);
        }
    }

}
