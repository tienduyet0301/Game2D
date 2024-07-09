using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{

    public float damage;
    float dameRate = 0.5f;
    public float pushBackForce;
    float nextDamage;


    // Start is called before the first frame update
    void Start()
    {
    nextDamage = 0f;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" && nextDamage < Time.time) {
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth> ();
            thePlayerHealth.addDamage (damage);
            nextDamage = dameRate + Time.time;

            pushBack (other.transform);
        }
  
    }
    void pushBack (Transform pushedObject){
        Vector2 pushDrirection = new Vector2(0, (pushedObject.position.y - transform.position.y)).normalized;
        pushDrirection *= pushBackForce;
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D> ();
        pushRB.velocity = Vector2.zero;
        pushRB.AddForce(pushDrirection, ForceMode2D.Impulse);
    }
}
