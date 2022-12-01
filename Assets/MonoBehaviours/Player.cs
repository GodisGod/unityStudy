using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HitPoints hitPoints;//只有玩家需要血量，所以放在玩家类里
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    Inventory inventory;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp")) {
           
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null) {
         
                print("it: "+hitObject.objectName);

                bool shouldDisappear = false;

                switch (hitObject.itemType) {

                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.addItem(hitObject);
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

    public override void ResetCharacter()
    {
        inventory = Instantiate(inventoryPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;

        hitPoints.value = startingHitPoints;
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true) {
            hitPoints.value = hitPoints.value - damage;
            if (hitPoints.value<=float.Epsilon) {
                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else {
                break;
            }

        }
    }

    public override void KillCharacter() {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);

    }
    private void OnEnable()
    {
        ResetCharacter();
    }


}
