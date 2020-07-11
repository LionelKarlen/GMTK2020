using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    public bool start;
    public bool end;
    public float TextFlashf;
    public Text TextFlash;
    public float startTime2;
    public bool Stop;
    public GameObject ScreenDim;
    void Start()
    {
        ScreenDim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(start){
            startTime = Time.time;
            start=false;
            Stop = true;
        }
        float t = Time.time - startTime;
        string Minutes = ((int) t/60).ToString();
        string Seconds = (t % 60).ToString("f2");
        if((t % 60) <= 9.99f){
            timerText.text = Minutes + ":0" + Seconds; 
        }
        else{
        timerText.text = Minutes + ":" + Seconds;
        }

        if(end){
            if(Stop){
            startTime2 = Time.time;
            Stop = false;
            }
            else{
            if(Time.time - startTime2 <= 3){
                ScreenDim.SetActive(true);
            TextFlashf = startTime2 - startTime;
            string Minutes2 = ((int) TextFlashf/60).ToString();
            string Seconds2 = (TextFlashf % 60).ToString("f2");
            if((TextFlashf % 60) <= 9.99f){
            TextFlash.text = Minutes2 + ":0" + Seconds2; 
            }
            else{
            TextFlash.text = Minutes2 + ":" + Seconds2;
            }
            }
            else{
                ScreenDim.SetActive(false);
            TextFlash.text = "";
            end = false;
            }
        }
        }
         
    }
}