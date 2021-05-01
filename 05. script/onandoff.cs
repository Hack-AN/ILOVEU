using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onandoff : MonoBehaviour
{
    public GameObject obj;

    public void onoff()
    {
        if (obj.activeSelf == true)
            obj.SetActive(false);
        else if (obj.activeSelf == false)
            obj.SetActive(true);
    }
}
