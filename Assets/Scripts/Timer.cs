using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        string Minutes = ((int) t/60).ToString();
        string Seconds = (t % 60).ToString("f2");
        if((t % 60) <= 9.99f){
            timerText.text = Minutes + ":0" + Seconds; 
        }
        else{
        timerText.text = Minutes + ":" + Seconds;
        } 
    }
}
