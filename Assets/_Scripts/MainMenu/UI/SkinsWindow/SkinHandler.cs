using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkinHandler : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private List<ChooseSkinButton> unlockedButtons;
    [SerializeField] private Image _currentlySelectedImage; 

    private Color _selectedColor = Color.red;
    private Color _color = Color.white;

    private void Start()
    {
        UpdateButtonColor(_currentlySelectedImage);
    }

    public void UpdateButtonColor(Image image)
    {
        
        if (!image.GetComponent<ChooseSkinButton>().IsUnlocked())
            return;

        
        _currentlySelectedImage = image;

        foreach (var item in images)
        {
            if (item.GetComponent<ChooseSkinButton>().IsUnlocked())
                item.color = _color;
        }

        image.color = _selectedColor;
    }

    public void UnlockNewSkin()
    {
        var lockedSkins = images.Where(img =>
            !unlockedButtons.Contains(img.GetComponent<ChooseSkinButton>())).ToArray();

        if (lockedSkins.Length == 0)
        {
            Debug.Log("All skins are already unlocked!");
            return;
        }

        var rand = Random.Range(0, lockedSkins.Length);
        var button = lockedSkins[rand].GetComponent<ChooseSkinButton>();

        button.SetUnlocked();
        button.RemoveLockIcon();
        unlockedButtons.Add(button);
         
        UpdateButtonsUi();             
        
    }

    private void UpdateButtonsUi()
    {
        foreach (var item in images)
        {
            var button = item.GetComponent<ChooseSkinButton>();
            if (button.IsUnlocked())
            {                
                item.color = (item == _currentlySelectedImage) ? _selectedColor : _color;
            }
        }
    }
}