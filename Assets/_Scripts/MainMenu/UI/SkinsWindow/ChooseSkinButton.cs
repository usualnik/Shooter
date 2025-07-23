using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseSkinButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SkinDataSO skinDataSO;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if(skinDataSO != null)
            image.sprite = skinDataSO.SkinSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetPlayerSkinData();
    }

    private void SetPlayerSkinData()
    {
        PlayerData.Instance.SetSkinData(skinDataSO);
    }
}
