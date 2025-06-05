using UnityEngine;

[CreateAssetMenu(fileName = "GameItems", menuName = "GameItem/AmmoSO")]
public class AmmoSO : BaseItemSO
{
    public float Speed; 
    public float Lifetime;
    public int Damage;
}
