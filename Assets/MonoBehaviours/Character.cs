using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float maxHitPoints;
    public float startingHitPoints;
    public enum CharacterCategory
    {
        PLAYER,
        ENEMY
    }
    public CharacterCategory characterCategory;
    public abstract void ResetCharacter();

    public abstract IEnumerator DamageCharacter(int damage,float interval);

    public virtual void KillCharacter() {
        Destroy(gameObject);
    }
}
