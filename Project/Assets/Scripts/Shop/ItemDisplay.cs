using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private ShopItem shopItem;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCost;
    [SerializeField] private Image itemImage;

    void Start()
    {
        itemName.text = shopItem.itemName;
        itemCost.text = shopItem.itemCost.ToString();
        itemImage.sprite = shopItem.itemImage;
    }
}
