using UnityEngine;

[CreateAssetMenu(fileName = "SkinDataSO", menuName = "SkinData/SkinDataSO")]
public class SkinDataSO : ScriptableObject
{
    public string Name;
    public string RuName;
    public Sprite SkinSprite;
    public AnimatorOverrideController AnimatorController;
}
