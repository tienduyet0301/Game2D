using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;

    public GameObject bloodEffect;
    // Start is called before the first frame update

    //kb cac bien ui thanh mau
    public Slider playerHealthSlider;



    void Start()
    {
        currentHealth = maxHealth;

        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addDamage(float damage){
        if (damage <= 0)
        return;
    currentHealth -= damage;
    playerHealthSlider.value = currentHealth;

    if (currentHealth <= 0) 
    makeDead ();
    }

    //tao ra chuc nang hoi mau khi an tim
public void addHealth(float healthAmount){
    currentHealth += healthAmount;
    if(currentHealth > maxHealth ) currentHealth = maxHealth;
    playerHealthSlider.value = currentHealth;
}

    void makeDead(){
        Instantiate(bloodEffect, transform.position, transform.rotation);
        Destroy (gameObject);
    }
}
