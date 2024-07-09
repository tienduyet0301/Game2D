using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{

    public float maxHealth;
    float currentHealth;

    //bien de tao hieu ung khi cay chet

    public GameObject enemyHealthEF;

    //khai bao cac bien de tao thanh mau cho cay

    public Slider enemyHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        enemyHealthSlider.maxValue = maxHealth;
        enemyHealthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //khai bao bien khi quat chet se ra mau

    public bool drop;
    public GameObject theDrop;
    public void addDamge(float damage){
        enemyHealthSlider.gameObject.SetActive(true);
        currentHealth -= damage;
        enemyHealthSlider.value = currentHealth;
        if (currentHealth <=0)
        makeDead ();
    }
    void makeDead(){
        Destroy (gameObject);
        Instantiate(enemyHealthEF, transform.position, transform.rotation);
        //chuc nang roi ra vien mau
        if(drop){
            Instantiate(theDrop, transform.position, transform.rotation);
        }
    }
}
