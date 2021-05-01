using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/*
 * 
 * 숫자, 문자 상수 버튼을 누르면 다음 옵션으로 바꾸는 기능입니다.
 * 
 */


public class select_const : MonoBehaviour
{
    public GameObject pub_text;
    public string[] arr_str;   // 스테이지별로 최대 4개의 상수. 4 * 26 = 104. 104개의 
    int stage_num;
    public int[] string_num_per_stage;  // 최솟값이 0, 최댓값이 3. ( 0이면 string의 개수가 1개라는 뜻, 1이면 2개, 2면 3개, 3이면 4개)
    int temp;
    BoxCollider2D this_col;
    // Start is called before the first frame update
    void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
        temp = stage_num * 4;
        pub_text.GetComponent<Text>().text = arr_str[temp];
    }

    // 버튼을 누르면 다음 옵션으로 넘어갑니다.
    public void change_const()
    {
        temp++;
        if (temp == stage_num * 4 + string_num_per_stage[stage_num] + 1)
            temp = stage_num * 4;
        pub_text.GetComponent<Text>().text = arr_str[temp];


    }
}
