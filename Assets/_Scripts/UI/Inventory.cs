using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryCell> _inventoryCells;

    private void Start()
    {
        foreach (var inventoryCell in _inventoryCells)
        {
            inventoryCell.gameObject.SetActive(inventoryCell.IsEmpty);
        }
    }
}
