using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fix_location : MonoBehaviour
{
    Vector3 location;

    // Start is called before the first frame update
    void Start()
    {
        //location = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(945.0104f, -442.4544f, 0f);
    }
}
