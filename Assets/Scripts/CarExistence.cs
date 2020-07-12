using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarExistence : MonoBehaviour
{
    public int lives;
    public GameObject Square;
    private AudioSource Bump;

    // Start is called before the first frame update
    void Start()
    {
        Bump = Square.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (other.gameObject.tag == "Wall")
        {
            lives--;
            Bump.Play(0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(lives <= 0){
            SceneManager.LoadScene("GameOver");
        }
    }
}
