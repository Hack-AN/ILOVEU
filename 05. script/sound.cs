using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{

    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(obj);
    }
}
