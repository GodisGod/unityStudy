using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp")) {
           
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null) {
                collision.gameObject.SetActive(false);
                print("it: "+hitObject.objectName);


                switch (hitObject.itemType) {

                    case Item.ItemType.COIN:
                        break;

                    case Item.ItemType.HEALTH:
                        AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;




                }

            }
                }
    }

    private void AdjustHitPoints(int quantity)
    {
        hitPoints += quantity;
        print("Adjusted hitpoints by: " + quantity+" result = "+hitPoints);
        if (hitPoints >= maxHitPoints) {
            hitPoints = maxHitPoints;
        }
    }
}
