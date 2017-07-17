using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Movement : MonoBehaviour {

    // Use this for initialization
    public float angle = 0.0f;
    public float speed = 5.0f;
    public float current_speed;
   // public Rigidbody rb;
    public Vector3 center;
    private bool trigger;
    private int version = 0;
    private Player_State pl;
    //private GameObject camera;
    private Camera_Movement cameraS;
    public bool physics = true;
    void Start () {
        if (physics == false)   this.GetComponent<Rigidbody>().isKinematic=true;
        //rb = GetComponent<Rigidbody>();
        GameObject st=GameObject.FindGameObjectsWithTag("Stage")[0];
        center=st.transform.position ;
        version=GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<BuildVersion>().getVersion();
        //camera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        cameraS = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera_Movement>();
        print("Player_Movement versions found: " + version);
       // current_speed = 15.0f;
        pl= GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<Player_State>();
        print("////////////START-player- "+pl.getState()+" //////////////"+version);
    }

    // Update is called once per frame
    void Update () {
        
        if (version > 2) //MOOOOBILEEEE!!!!!!!!!!!!!!!!1
        {
            float translation = -(CrossPlatformInputManager.GetAxis("Horizontal") * current_speed);//Input.GetAxis("Horizontal") * speed;
            //  print("////////////translation//////////////" + translation);
            //transform.Translate(0, 0, translation);
            angle -= translation * Time.deltaTime;
            if ((int)translation == 0) pl.setState(1);
            if (translation > 0) pl.setState(2);
            if (translation < 0) pl.setState(3);
            transform.RotateAround(center, Vector3.up, translation * Time.deltaTime);
        }
        else {
            float translation = Input.GetAxis("Horizontal") * current_speed;//Input.GetAxis("Horizontal") * speed;
            angle -= translation * Time.deltaTime;
            if (pl.getState() > 0)
            {
                if ((int)translation == 0) pl.setState(1);
                if (translation > 0) pl.setState(2);
                if (translation < 0) pl.setState(3);
                transform.RotateAround(center, Vector3.up, translation * Time.deltaTime);
            }
           //
        }  //



    }
        void FixedUpdate()
    {
        
    }

    public float getCurrentSpeed() {  return current_speed;}
}
