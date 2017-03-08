using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

    // Use this for initialization
    public float speed = 5.0f;
    private float current_speed;
    public Rigidbody rb;
    public Vector3 center;
    private bool trigger;
    void Start () {
        //rb = GetComponent<Rigidbody>();
        GameObject st=GameObject.FindGameObjectsWithTag("Stage")[0];
        center=st.transform.position ;


        current_speed = 0.0f;

    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetKey("left")){
           // print("left arrow key is held down");
            trigger = true;
            if (current_speed < speed)
                current_speed++;
        }
        if (Input.GetKey("right")) { 
          //  print("right arrow key is held down");
            trigger = true;
            if (current_speed > -speed)
                current_speed--;
        }
        if (!trigger)
        {
            if (current_speed < 0) current_speed++;
            if (current_speed > 0) current_speed--;

        }
        trigger = false;
        transform.RotateAround(center, Vector3.up, current_speed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        
    }
}
