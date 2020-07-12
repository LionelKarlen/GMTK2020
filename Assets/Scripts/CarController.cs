using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CarController : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed;
    public float deceleration;
    AudioSource CarSounds;
    public Tilemap roadTilemap;

    // Start is called before the first frame update
    void Start() {
        CarSounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        rb.AddForce(Input.GetAxisRaw("Vertical")*speed*transform.up);
        transform.Rotate(new Vector3(0f,0f,.5f), Input.GetAxisRaw("Horizontal")*speed*-1);
        if(Input.GetKey("up") == false && Input.GetKey(KeyCode.W) == false){
            rb.velocity = rb.velocity * deceleration;
        }
        if(Input.GetKey(KeyCode.W)||Input.GetKey("up")){
            CarSounds.Play(0);
        }
        else{
            CarSounds.Pause();
        }
        if(!roadTilemap.HasTile(roadTilemap.WorldToCell(transform.position))) {
            speed=1;
        } else {
            speed=2;
        }
    }
}
