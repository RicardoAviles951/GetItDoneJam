using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountItems : MonoBehaviour
{
    public Transform origin;
    public float searchRadius = 10f;

    private void Update()
    {
        // Obtener todos los colliders en el radio de búsqueda
        Collider[] colliders = Physics.OverlapSphere(origin.position, searchRadius);

        int itemCount = 0; // Contador para llevar el número de objetos activos con el script Item

        // Recorrer todos los colliders y contar aquellos que tengan el script Item y estén activos
        foreach (Collider collider in colliders)
        {
            Item item = collider.GetComponent<Item>();
            if (item != null && collider.gameObject.activeSelf)
            {
                itemCount++;
            }
        }

        // Actualizar el contador en el GameManager
        GameManager.instance.winCount = itemCount;

    }
}
