using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/*
 * 
 * 함수 옵션을 변경하는 기능입니다.
 * 
 */


public class select_func : MonoBehaviour
{
    public GameObject pub_text;
    public string[] arr_str;   // 스테이지별로 최대 4개의 상수. 4 * 26 = 104. 104개의 
    int temp;

    // Start is called before the first frame update
    void Start()
    {
        temp = 0;
        pub_text.GetComponent<Text>().text = arr_str[temp];
    }

    public void change_func()
    {

        temp++;
        this.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().DownBlk = this.gameObject.GetComponent<BlkOnNoteComp>().DownBlk += 2;

        if (temp == 8)
        {
            temp = 0;
            this.gameObject.GetComponent<MoveBtn>().btn_numbering -= 16;
        }
        pub_text.GetComponent<Text>().text = arr_str[temp];

    }
}