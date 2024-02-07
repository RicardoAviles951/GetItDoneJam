using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryImageManager : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventoryManager;

    public List<Image> inventorySlots;

        private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        // Iniciar la verificación cada 0.25 segundos
        InvokeRepeating("UpdateInventoryImages", 0f, 0.3f);
    }

    private void UpdateInventoryImages()
    {
        // Verificar si el InventoryManager no está vacío
        if (inventoryManager != null)
        {
            // Obtener la lista de ítems del inventario
            List<Item> inventoryItems = inventoryManager.GetInventoryItems();

            // Actualizar las imágenes del inventario
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (i < inventoryItems.Count)
                {
                    Image slotImage = inventorySlots[i];
                    Item item = inventoryItems[i];

                    if (item != null && item.itemImage != null)
                    {
                        // Asignar la imagen del ítem al panel de inventario
                        slotImage.sprite = item.itemImage;
                        slotImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        // Si no hay ítem en este espacio del inventario, desactivar la imagen del panel
                        slotImage.gameObject.SetActive(false);
                    }
                }
                else
                {
                    // Si no hay ítem en este espacio del inventario, desactivar la imagen del panel
                    inventorySlots[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
