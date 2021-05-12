using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * 상수 블록, 변수 블록의 옵션을 바꿀 수 있는 기능입니다.
 * 
 */ 


public class select_const_or_oper : MonoBehaviour
{
    public int numbering;
    public Sprite[] btn_array;
    int stage_num;
    bool only_assign = false;
    public GameObject choice_window;


    // Start is called before the first frame update
    void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
        switch(stage_num)
        {
            case 1:
            case 3:
                only_assign = true;
                break;
        }
        if (this.CompareTag("block"))
        {
            numbering = this.gameObject.GetComponent<MoveBtn>().btn_numbering;
            this.gameObject.GetComponent<Image>().sprite = btn_array[numbering - 23];
        }
        else
        {
            numbering = this.gameObject.GetComponent<BlkOnNoteComp>().thisBlk;
            this.gameObject.GetComponent<Image>().sprite = btn_array[numbering - 23];
        }

    }

    public void change_const_or_oper()
    {
        if (!only_assign)
            choice_window.SetActive(true);
            /*
            if (this.CompareTag("block"))
            {
                if (numbering == 30)
                    numbering = this.gameObject.GetComponent<MoveBtn>().btn_numbering = 22;

                numbering = ++this.gameObject.GetComponent<MoveBtn>().btn_numbering;
                this.gameObject.GetComponent<Image>().sprite = btn_array[numbering - 23];
            }
            else
            {
                if (numbering == 30)
                    numbering = this.gameObject.GetComponent<BlkOnNoteComp>().thisBlk = 22;

                numbering = ++this.gameObject.GetComponent<BlkOnNoteComp>().thisBlk;
                this.gameObject.GetComponent<Image>().sprite = btn_array[numbering - 23];
            }
            */


    }

}
