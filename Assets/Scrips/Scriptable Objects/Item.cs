using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]

public class Item : ScriptableObject
{
    public string objectName;//道具名称
    public Sprite sprite;//精灵🧚🏻‍♀️引用
    public int quantity;//道具数量
    public bool stackable;//道具是否可堆叠

    public enum ItemType {
        COIN,HEALTH
    }

    public ItemType itemType;

}
