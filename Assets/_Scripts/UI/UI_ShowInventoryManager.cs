using UnityEngine;

public class UI_ShowInventoryManager : MonoBehaviour
{
   [SerializeField] private GameObject _inventoryWindow;
   public void ShowInventory()
   {
      _inventoryWindow.SetActive(!_inventoryWindow.activeInHierarchy);
   }
}

   
