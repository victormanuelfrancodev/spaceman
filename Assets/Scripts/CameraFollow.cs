using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    // Distancia a la que se ve el personaje
    public Vector3 offset = new Vector3(0.2f, 0, -10f);

    public float dampingTime = 0.3f;

    public Vector3 velocitiy = Vector3.zero;
    // Start is called before the first frame update

    void Awake(){
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition(){
        MoveCamera(false);
    }

    void MoveCamera(bool smooth){
        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);

        if(smooth){
            this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocitiy, dampingTime);
        }else{
            this.transform.position = destination;
        }
    }
}
