using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonShoot : MonoBehaviour
{

    public GameObject theBom;
    public Transform shootForm;
    public float shootTime;
    float nextShoot = 0f;
    

  void Awake(){
      }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player" && Time.time > nextShoot){
            nextShoot = Time.time +shootTime;
            Instantiate(theBom, shootForm.position, Quaternion.identity);
                   }
    }
}
