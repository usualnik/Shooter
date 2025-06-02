using UnityEngine;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject _inventoryWindow;
   
   public void ShowInventory()
   {
      _inventoryWindow.SetActive(!_inventoryWindow.activeInHierarchy);
   }
}
