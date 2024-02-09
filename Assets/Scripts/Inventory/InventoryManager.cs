using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private void Update()
    {
        //This logic will be handled in the player state machine
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Vector3 cameraPosition = Camera.main.transform.position;

        //    Vector3 cameraForward = Camera.main.transform.forward;

        //    Vector3 dropPosition = cameraPosition + cameraForward * 2.0f; 


        //    DropCurrentItem(dropPosition);
        //}
    }
    public static InventoryManager instance;

    [SerializeField]
    private List<IExaminable> inventory;

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
        inventory = new List<IExaminable>();
    }

    //public void AddItem(Item item)
    //{
    //    inventory.Add(item);
    //    Debug.Log("Objeto agregado al inventario");
    //}
    public void AddItem(IExaminable item)
    {
        inventory.Add(item);
        Debug.Log("Added item: " + item.ToString());
        Debug.Log("Inventory: " + inventory);
    }
    public void RemoveItem(IExaminable item)
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
    public bool HasItem(IExaminable item)
    {
        return inventory.Contains(item);
    }

    //public bool HasItem(Item item)
    //{
    //    return inventory.Contains(item);
    //}

    //public void RemoveItem(Item item)
    //{
    //    if (inventory.Contains(item))
    //    {
    //        inventory.Remove(item);
    //        Debug.Log("Objeto removido del inventario");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("El objeto no está en el inventario");
    //    }
    //}

    //public bool HasItem(Item item)
    //{
    //    return inventory.Contains(item);
    //}

    public void ClearInventory()
    {
        inventory.Clear();
        Debug.Log("Inventario vaciado");
    }
    public List<IExaminable> GetInventoryItems()
    {
        return inventory;
    }
    public void DropCurrentItem(Vector3 dropPosition)
    {
        if (inventory.Count > 0)
        {
            GetLastItem().ToggleExaminable(true, dropPosition);
            inventory.Remove(GetLastItem());

            //IExaminable currentItem = inventory[inventory.Count - 1]; // El último ítem agregado es el que se vota
            //Logic for placing item down on surface
            
            //currentItem.DropItem(dropPosition);
        }
        else
        {
            Debug.LogWarning("No hay ningún objeto en el inventario para votar.");
        }
    }

    public IExaminable GetLastItem()
    {
        IExaminable nextItem = inventory[inventory.Count -1];
        return nextItem;
    }

    public IExaminable DropNextItem()
    {
        if (inventory.Count > 0)
        {
            IExaminable nextItem = inventory[inventory.Count - 1];
            inventory.Remove(nextItem);
            return nextItem;
        }
        else
        {
            Debug.LogWarning("No item in inventory to drop!");
            return null;
        }
        

        
    }

    //public List<Item> GetInventoryItems()
    //{
    //    return inventory;
    //}

    //public void DropCurrentItem(Vector3 dropPosition)
    //{
    //    if (inventory.Count > 0)
    //    {
    //        Item currentItem = inventory[inventory.Count - 1]; // El último ítem agregado es el que se vota
    //        currentItem.DropItem(dropPosition);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No hay ningún objeto en el inventario para votar.");
    //    }
    //}
}
