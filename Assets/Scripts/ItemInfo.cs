using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public string ItemName {  get; set; }
    public string ItemDescription { get; set; }

    public ItemInfo(string itemName, string itemDescription)
    {
        ItemName = itemName;
        ItemDescription = itemDescription;
    }
}
