using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 cameraPosition = Camera.main.transform.position;

            Vector3 cameraForward = Camera.main.transform.forward;

            Vector3 dropPosition = cameraPosition + cameraForward * 2.0f; 


            DropCurrentItem(dropPosition);
        }
    }
    public static InventoryManager instance;

    [SerializeField]
    private List<Item> inventory;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        inventory = new List<Item>();
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log("Objeto agregado al inventario");
    }

    public void RemoveItem(Item item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            Debug.Log("Objeto removido del inventario");
        }
        else
        {
            Debug.LogWarning("El objeto no está en el inventario");
        }
    }

    public bool HasItem(Item item)
    {
        return inventory.Contains(item);
    }

    public void ClearInventory()
    {
        inventory.Clear();
        Debug.Log("Inventario vaciado");
    }

    public List<Item> GetInventoryItems()
    {
        return inventory;
    }
    public void DropCurrentItem(Vector3 dropPosition)
    {
        if (inventory.Count > 0)
        {
            Item currentItem = inventory[inventory.Count - 1]; // El último ítem agregado es el que se vota
            currentItem.DropItem(dropPosition);
        }
        else
        {
            Debug.LogWarning("No hay ningún objeto en el inventario para votar.");
        }
    }
}
