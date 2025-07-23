using UnityEngine;

[CreateAssetMenu(fileName = "SkinDataSO", menuName = "SkinData/SkinDataSO")]
public class SkinDataSO : ScriptableObject
{
    public string Name;
    public Sprite SkinSprite;
    public AnimatorOverrideController AnimatorController;
}
