using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    AudioSource Topout;
    // Start is called before the first frame update
    void Start()
    {
        Topout = GetComponent<AudioSource>();
        Topout.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
