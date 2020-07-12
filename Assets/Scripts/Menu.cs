using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PLayGame(){
        SceneManager.LoadScene("PlayableScene");
        Stats.Stages=0;
        Stats.Lives=3;
    }
    public void QuitGame (){
        Application.Quit();
    }
    public void qUITtoMenu(){
        SceneManager.LoadScene("TitleScreen");
    }
}
