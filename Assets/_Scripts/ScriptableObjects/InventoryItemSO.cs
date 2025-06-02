using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItem")]
public class InventoryItemSO : ScriptableObject
{
    public string ItemName;
    public Sprite ItemPreview;
    public int Amount;
    public GameObject ItemPrefab;
}
