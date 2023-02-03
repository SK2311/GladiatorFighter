using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemCost;
    public GameObject itemGameObject;
}
