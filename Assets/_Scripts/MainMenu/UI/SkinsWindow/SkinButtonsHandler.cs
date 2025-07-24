
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkinButtonsHandler : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private List<ChooseSkinButton> unlockedButtons;


    private Color _selectedColor = Color.red;
    private Color _color = Color.white;


    public void UpdateColor(Image image)
    {
        foreach (var item in images)
        {
            if(item.GetComponent<ChooseSkinButton>().IsUnlocked())
                item.color = _color;
        }

        image.color = _selectedColor;
    }

    public void UnlockNewSkinButton()
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
        unlockedButtons.Add(button);

        UpdateButtonsUi();

    }

    private void UpdateButtonsUi()
    {
        foreach (var item in images)
        {
            if (item.GetComponent<ChooseSkinButton>().IsUnlocked())
                item.color = _color;
        }
    }


}

