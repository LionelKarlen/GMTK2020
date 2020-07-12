using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PLayGame(){
        SceneManager.LoadScene("PlayableScene");
        Stats.Stages=0;
    }
}
