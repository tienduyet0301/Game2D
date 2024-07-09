using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletHit : MonoBehaviour 
{
public float weaponDamage;
    projectileController myPC;

    public GameObject bulletExplosion;

    void Awake(){
        myPC = GetComponentInParent<projectileController> ();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){    
        if(other.gameObject.tag == "Shootable"){
            myPC.removeForce ();
            Instantiate (bulletExplosion, transform.position, transform.rotation);
            Destroy (gameObject);
            if(other.gameObject.layer == LayerMask.NameToLayer("enemyy")){
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>();
                hurtEnemy.addDamge(weaponDamage);
                
            }
        }
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Shootable"){
            myPC.removeForce ();
            Instantiate(bulletExplosion, transform.position, transform.rotation);
            Destroy (gameObject);
            if(other.gameObject.layer == LayerMask.NameToLayer("enemyy")){
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>();
                hurtEnemy.addDamge(weaponDamage);
                
            }
        }
    }
}
