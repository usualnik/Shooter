using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventoryCell : MonoBehaviour, IPointerClickHandler
{
    public bool IsEmpty { get; private set; } = true;
    
    [SerializeField] private BaseItemSO baseItemSo;
    [SerializeField] private Image _cellSprite;
    [SerializeField] private TextMeshProUGUI _stackText;
    [SerializeField] private GameObject _deleteButton;
    
    
    private void Start()
    {
        if (baseItemSo)
        {
            _cellSprite.sprite = baseItemSo.ItemPreview;
            _stackText.text = baseItemSo.Amount > 1 ? baseItemSo.Amount.ToString() : string.Empty ;
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
        baseItemSo = null;
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

    public void AddItemToCell(BaseItemSO baseItem)
    {
        _cellSprite.sprite = baseItem.ItemPreview;
        _stackText.text = baseItem.Amount > 1 ? baseItem.Amount.ToString() : string.Empty ;
        IsEmpty = false;
    }

  
}
