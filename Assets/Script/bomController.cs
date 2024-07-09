using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomController : MonoBehaviour
{

    public float bomSpeedHight;
    public float bomSpeedLow;
    public float bomAngle;

    Rigidbody2D cannonRB;
void Awake(){
    cannonRB = GetComponent<Rigidbody2D>();

    
}
    // Start is called before the first frame update
    void Start()
    {
cannonRB.AddForce(new Vector2 (Random.Range( -bomAngle, bomAngle), Random.Range(bomSpeedLow, bomSpeedHight)), ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
