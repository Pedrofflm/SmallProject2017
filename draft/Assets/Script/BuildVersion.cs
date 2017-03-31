using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildVersion : MonoBehaviour {
    public static int version=1;
    /*Versions:
        1.PC Casual
        2.PC Hardcore
        3.Mobile Casual (Android)
        4.Mobile Hardcore (Android)
	*/
    public int getVersion() { return version; }
}
