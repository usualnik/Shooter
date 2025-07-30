using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StoreWindow : MonoBehaviour
{
    [SerializeField] private BuySkin _buySkinButton;

    [SerializeField] private GameObject _chestImageObject;

    [Header("New skin refs")]
    [SerializeField] private GameObject _newSkinImageObject;
    [SerializeField] private TextMeshProUGUI _newSkinText;
    [SerializeField] private Image _newSkinImage;

    [Header("System")] 
    [SerializeField] private SkinHandler _skinHandler;


    private void Start()
    {
        _buySkinButton.OnBuySkin += BuySkinButton_OnBuySkin;
    }
    private void OnDestroy()
    {
        _buySkinButton.OnBuySkin -= BuySkinButton_OnBuySkin;
    }


    private void OnEnable()
    {
        _chestImageObject.SetActive(true);
        _newSkinImageObject.SetActive(false);
    }

    private void BuySkinButton_OnBuySkin()
    {
        _newSkinImageObject.SetActive(true);
        ShowNewSkinInfo();
    }


    private void ShowNewSkinInfo()
    {
        if (YG2.envir.language == "ru")
        {
            _newSkinText.text = _skinHandler.UnlockedSkinData.RuName;
        }
        else
        {
            _newSkinText.text = _skinHandler.UnlockedSkinData.Name;
        }

        _newSkinImage.sprite = _skinHandler.UnlockedSkinData.SkinSprite;
    }
}
