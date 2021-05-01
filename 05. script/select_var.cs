using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/*
 * 
 * 변수 옵션을 변경할 수 있는 기능입니다.
 * 
 */

public class select_var : MonoBehaviour
{
    public GameObject pub_text;
    public string[] arr_str;   // 스테이지별로 최대 4개의 상수. 4 * 26 = 104. 104개의 
    int temp;
    int stage_num;
    int max = 3;

    // Start is called before the first frame update
    void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
        temp = 0;
        pub_text.GetComponent<Text>().text = arr_str[temp];
        if(this.gameObject.name == "btn_var")
            switch (stage_num)
            {
            
                case 1:
                case 3:
                case 4:
                case 5:
                case 6:
                case 8:
                case 9:
                case 10:
                    max = 1;
                    break;
                case 7:
                    max = 2;
                    break;
            }
    }

    public void change_var()
    {

        temp++;
        this.gameObject.GetComponent<MoveBtn>().btn_numbering++;
        if (temp == max)
        {
            temp = 0;
            this.gameObject.GetComponent<MoveBtn>().btn_numbering -= max;
        }
        pub_text.GetComponent<Text>().text = arr_str[temp];

    }
}
