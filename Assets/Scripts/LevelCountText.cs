using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCountText : MonoBehaviour
{
    static public int LevelCount = 0;
    public Text LevelCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.T)){
            LevelCount++;
        }
        LevelCounter.text = LevelCount.ToString();
    }
}
