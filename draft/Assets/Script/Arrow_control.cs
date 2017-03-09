using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_control : MonoBehaviour {
    public float angle = 0.0f;
    public GameObject bow, arrowSpawner;//, arrow, arrowClone;
    ArrowSpawner arrowSpawnerScript;
    // Vector3 v3 = Vector3.zero;
    public int power = 0;
    public float arrowSpeed = 50.0f;
    public bool resetPose = false;
    
    private Vector2 middle, firstPosition;
    private bool mouseDown = false;
    //CursorLockMode wantedMode;

    void Start () {
        middle = new Vector2(Screen.width / 2, Screen.height / 2);
        if (!bow) bow = GameObject.FindGameObjectsWithTag("Bow")[0];
        if (!arrowSpawner) arrowSpawner = GameObject.FindGameObjectsWithTag("ArrowSpawner")[0];
        arrowSpawnerScript = arrowSpawner.GetComponent<ArrowSpawner>();
       // if (!arrowSpawner) arrow=

        Screen.fullScreen = false;
       // Cursor.visible = false;
        //Cursor.lockState = wantedMode;
        // arrow.transform.eulerAngles = v3;
    }
	
	// Update is called once per frame
	void Update () {
      /*  if (Input.GetButtonDown("Jump"))
        {
            print("Fire!");

            arrowClone = Instantiate(arrow, transform.position, transform.rotation);
            arrow.GetComponent<Rigidbody>().velocity= transform.right * arrowSpeed;

        }*/
        if (Input.GetButtonDown("Cancel")) { 
            print("escape");
            Screen.fullScreen = !Screen.fullScreen;
        }
        if (Input.GetButton("Fire1")) {//being dragged
            if (!mouseDown) {
                mouseDown = true;
                firstPosition = Input.mousePosition;
                print("position"+firstPosition);
            }
            
            
            float d=Input.mousePosition.x - firstPosition.x;//middle.x
            if (d == 0) d = 0.1f;
            float h=Input.mousePosition.y- firstPosition.y;//middle.y
            float distance_percent = Vector2.Distance(Input.mousePosition, firstPosition)/middle.y;//distance to center would be middle in the second parameter
            if (distance_percent > 0.3)
            {
                power = 1;
                if (distance_percent > 0.7)
                {
                    power=2;
                    if (distance_percent > 0.9) { power=3; }
                }
            }
            print(power);
            angle = Mathf.Rad2Deg * Mathf.Atan(h/d);
            if (d < 0) angle -= 180;
            bow.transform.localEulerAngles = new Vector3(bow.transform.rotation.x, bow.transform.rotation.y, angle+180);// Quaternion.Euler(bow.transform.rotation.x, bow.transform.rotation.y, angle);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //fire arrow projectile
            mouseDown = false;
            firstPosition = new Vector2();
            print("up");
            
            print(power);
            if(resetPose) bow.transform.localEulerAngles = new Vector3(bow.transform.rotation.x, bow.transform.rotation.y, 45f);
            arrowSpawnerScript.fireArrow(power*150);
            power = 0;
        }

    }
   
    
}
