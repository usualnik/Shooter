using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    public bool IsEmpty { get; private set; } = true;
    
    [SerializeField] private InventoryItemSO _inventoryItemSo;
    [SerializeField] private Image _cellSprite;
    [SerializeField] private TextMeshProUGUI _stackText;
    [SerializeField] private GameObject _deleteButton;
    private void Start()
    {
        if (_inventoryItemSo)
        {
            _cellSprite.sprite = _inventoryItemSo.ItemPreview;
            _stackText.text = _inventoryItemSo.Amount > 1 ? _inventoryItemSo.Amount.ToString() : string.Empty ;
            IsEmpty = false;
        }
    }

    private void ShowDeleteButton()
    {
        _deleteButton.SetActive(!_deleteButton.activeInHierarchy);
    }
    public void ClearCell()
    {
        _deleteButton.SetActive(!_deleteButton.activeInHierarchy);
        _inventoryItemSo = null;
        _cellSprite.sprite = null;
        _stackText.text = string.Empty;
        IsEmpty = true;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            ShowDeleteButton();
        }
        
    }

    public void AddItemToCell(InventoryItemSO item)
    {
        _cellSprite.sprite = item.ItemPreview;
        _stackText.text = item.Amount > 1 ? item.Amount.ToString() : string.Empty ;
        IsEmpty = false;
    }

  
}
