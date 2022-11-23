using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

 


    private void Start()
    {
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp")) {
           
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null) {
         
                print("it: "+hitObject.objectName);

                bool shouldDisappear = false;

                switch (hitObject.itemType) {

                    case Item.ItemType.COIN:
                        shouldDisappear = true;
                        break;

                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;

                }

                if (shouldDisappear) {
                    print("shouldDisappear = "+ shouldDisappear);
                    collision.gameObject.SetActive(false);
                }

            }
                }
    }

    private bool AdjustHitPoints(int quantity)
    {
       
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value += quantity;
            print("Adjusted hitpoints by: " + quantity + " result = " + hitPoints.value);
            return true;
        }
        return false;
    }
}
