using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject car;
    public GameObject mainCamera;
    public float speed;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        mainCamera.transform.position=new Vector3(car.transform.position.x,car.transform.position.y,-10f);
        // transform.position=Vector3.Lerp(transform.position,mainCamera.transform.position,speed*Time.deltaTime);
    }
}
