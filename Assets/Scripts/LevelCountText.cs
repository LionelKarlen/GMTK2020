using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCountText : MonoBehaviour
{
    public Text LevelCounter;
    
    void Update() {
        LevelCounter.text = "Level: "+Stats.Stages.ToString();
    }
}
