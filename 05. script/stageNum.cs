using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 스테이지의 넘버를 저장하는 스크립트입니다.
 * 
 */


public class stageNum : MonoBehaviour
{

    public int stage_number;
    public int ClearedStageNumber;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("stageNum"))
            PlayerPrefs.SetInt("stageNum", 0);       
    }

}
