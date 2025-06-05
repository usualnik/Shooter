using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<UI_InventoryCell> _inventoryCells;

    private PickableItemScanner _scanner;
    
    private void Start()
    {
        _scanner = gameObject.GetComponent<PickableItemScanner>();
        
        _scanner.OnItemFound += Scanner_OnItemFound;
        
        foreach (var inventoryCell in _inventoryCells)
        {
            inventoryCell.gameObject.SetActive(inventoryCell.IsEmpty);
        }
    }
    
    private void OnDestroy()
    {
        _scanner.OnItemFound -= Scanner_OnItemFound;
    }


    private void Scanner_OnItemFound(object sender, PickableItemScanner.OnItemFoundEventArgs e)
    {
        AddItemToInventory(e.BaseItemFound);
    }
   
    private void AddItemToInventory(BaseItemSO baseItem)
    {
        foreach (var inventoryCell in _inventoryCells)
        {
            if (inventoryCell.IsEmpty)
            {
                inventoryCell.AddItemToCell(baseItem);
                break;
            }
        }
    }
}
