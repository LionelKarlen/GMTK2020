using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed;
    public float deceleration;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        rb.AddForce(Input.GetAxisRaw("Vertical")*speed*transform.up);
        transform.Rotate(new Vector3(0f,0f,.5f), Input.GetAxisRaw("Horizontal")*speed*-1);
        if(Input.GetKey("up") == false && Input.GetKey(KeyCode.W) == false){
            rb.velocity = rb.velocity * deceleration;
        }
    }
}
