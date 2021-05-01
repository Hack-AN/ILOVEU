using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBtnManager : MonoBehaviour
{
    int cleared_num;
    public GameObject[] stagebtn;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("stageNum") + 1 + 2 * (PlayerPrefs.GetInt("stageNum") / 14); i++)
            stagebtn[i].SetActive(true);

        Debug.Log("씬 불러옴");
    }
}
