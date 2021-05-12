using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * 연산자, 변수, 상수 옵션을 선택하는 기능입니다. 
 * 
 */

public class choose_const : MonoBehaviour
{
    public GameObject btn;
    public int btn_numbering;
    public int const_order;
    int stage_num;

    private void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
        if (btn_numbering == 21 || btn_numbering == 22)
        {
            if (btn.GetComponent<select_const>().arr_str[4*stage_num + const_order] != "")
                this.gameObject.transform.GetChild(0).GetComponent<Text>().text = btn.GetComponent<select_const>().arr_str[4 * stage_num + const_order];
            else
                this.gameObject.SetActive(false);
        }
    }

    public void _choose_const()
    {
        if(btn_numbering == 21 || btn_numbering == 22)
        {
            btn.transform.GetChild(0).GetComponent<Text>().text = btn.GetComponent<select_const>().arr_str[4 * stage_num + const_order];
        }
        else
        {
            btn.GetComponent<select_const_or_oper>().numbering = btn_numbering;
            btn.GetComponent<MoveBtn>().btn_numbering = btn_numbering;
            btn.GetComponent<Image>().sprite = btn.GetComponent<select_const_or_oper>().btn_array[btn_numbering - 23];
        }
        this.transform.parent.gameObject.SetActive(false);
    }
}
