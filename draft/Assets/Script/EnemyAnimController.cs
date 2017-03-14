using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{

    public float p_speed = 5.0f;
    public float e_speed = 1.0f;
    GameObject player, d_enemy;
    public float e_angle, p_angle;
    Vector3 newRotation = new Vector3(0, 0, 0);

    public float cooldownDef, cooldownCur;
    public int attperformance = 0;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        d_enemy = GameObject.Find("Dummy_anim");
        cooldownCur = 10;
        cooldownDef = 10;

    }

    // Update is called once per frame
    void Update()
    {

        //Player and enemy angles
        p_angle = player.transform.eulerAngles.y;
        e_angle = d_enemy.transform.eulerAngles.y;

        //To follow the player through the scenario
        // Use deltaAngle to now which enemy_angle_animation use and trigger attacks
        if (e_angle != p_angle)
        {

            if (Mathf.DeltaAngle(p_angle, e_angle) > 0)
            {
                d_enemy.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, d_enemy.transform.eulerAngles.y - 0.1f, player.transform.eulerAngles.z);
            }
            if (Mathf.DeltaAngle(p_angle, e_angle) < 0)
            {
                d_enemy.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, d_enemy.transform.eulerAngles.y + 0.1f, player.transform.eulerAngles.z);
            }

        }
        //Triggering attacks
        if (cooldownCur < 1 && Mathf.DeltaAngle(p_angle, e_angle) >= -10 && Mathf.DeltaAngle(p_angle, e_angle) <= 10)
        {
            if (Random.value > 0.2)
            {
                attperformance = 1;
                if (Random.value > 0.7)
                {
                    attperformance = 2;
                    if (Random.value > 0.8)
                    {
                        attperformance = 3;
                    }
                }
            }
            //Attack selection
            switch (attperformance)
            {
                case 1:
                    print("Basic front attack");
                    cooldownCur = 10;
                    break;
                case 2:
                    print("Dynamic diagonal attack");
                    cooldownCur = 10;
                    break;
                case 3:
                    print("STILL STANCE");
                    cooldownCur = 10;
                    break;
            }
        }
        else
        {
            cooldownCur = cooldownCur - Time.deltaTime;
        }
        // print("Diference between angles is:"+Mathf.DeltaAngle(p_angle, e_angle));
    }

    void FixedUpdate()
    {

    }
}
