using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildVersion : MonoBehaviour {
    public int version=1;
    //public bool gameover = false;

    void Start()
    {
        GameObject mobileInterface = GameObject.FindGameObjectsWithTag("MobileInterface")[0];
        switch (version)
        {
            case 1: mobileInterface.SetActive(false);
                break;
            case 2: mobileInterface.SetActive(false);
                break;
            case 3: mobileInterface.SetActive(true);
                break;
            case 4: mobileInterface.SetActive(true);
                break;
        }
    }
    /*Versions:
        1.PC Casual
        2.PC Hardcore
        3.Mobile Casual (Android)
        4.Mobile Hardcore (Android)
	*/
    public int getVersion() { return version; }
}
