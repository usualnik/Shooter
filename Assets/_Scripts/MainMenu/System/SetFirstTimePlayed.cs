using UnityEngine;
using YG;

public class SetFirstTimePlayed : MonoBehaviour
{    
    void Start()
    {        
        YG2.saves.IsFirstTimePlayed = false;
        YG2.SaveProgress();
    }

}
