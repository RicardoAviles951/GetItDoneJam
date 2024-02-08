using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Sprite itemImage;
    public bool isCollected = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                isCollected = true;
                InventoryManager.instance.AddItem(this);
                gameObject.SetActive(false);
            }
        }
    }

    public void DropItem(Vector3 dropPosition)
    {
        transform.position = dropPosition;
        gameObject.SetActive(true);
        isCollected = false;
        InventoryManager.instance.RemoveItem(this);
    }

}
