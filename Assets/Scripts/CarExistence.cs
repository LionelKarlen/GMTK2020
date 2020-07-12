using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarExistence : MonoBehaviour
{
    public GameObject Square;
    private AudioSource Bump;
    public Text counter;
    public bool vulnerable = true;
    public float timerStart;
    public bool HasTimerStarted = false;
    public GameObject mapGenerator;
    public GameObject timeHandler;
    // Start is called before the first frame update
    void Start()
    {
        Bump = Square.GetComponent<AudioSource>();
        counter.text = "Lives: "+Stats.Lives.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(vulnerable){
        if (other.gameObject.tag == "Wall")
        {
            vulnerable = false;
            Stats.Lives--;
            Bump.Play(0);
            counter.text = "Lives: " + Stats.Lives.ToString();
        } else if(other.gameObject.tag == "Goal") {
            timeHandler.GetComponent<Timer>().end=true;
            Stats.Lives++;
            // mapGenerator.GetComponent<MapGeneration>().resetMap();
        }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(vulnerable==false){
            if(HasTimerStarted == false){
                timerStart = Time.time;
                HasTimerStarted = true;
            }
            else{
                if((Time.time - timerStart)>= 2){
                    vulnerable= true;
                    HasTimerStarted= false;
                }
            }
        }
        if(Stats.Lives <= 0){
            SceneManager.LoadScene("GameOver");
        }
    }
}
