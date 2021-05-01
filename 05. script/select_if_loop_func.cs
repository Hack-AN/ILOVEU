using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * if, loop 블록의 옵션을 변경할 수 있는 기능입니다.
 * 
 */

public class select_if_loop_func : MonoBehaviour
{
    int numbering;
    public Sprite[] btn_array;
    public int start_num;

    private void Start()
    {
        if (this.CompareTag("block"))
        {
            numbering = this.gameObject.GetComponent<MoveBtn>().btn_numbering;
            this.gameObject.GetComponent<Image>().sprite = btn_array[numbering % 100 + numbering / 100 - start_num];
        }
        else
        {
            numbering = this.gameObject.GetComponent<BlkOnNoteComp>().thisBlk;
            this.gameObject.GetComponent<Image>().sprite = btn_array[numbering % 100 + numbering / 100 - start_num];
        }
    }

    public void change_picture()
    {
        Debug.Log(numbering);
        if (this.CompareTag("block"))
        {
            if (numbering == start_num + 100)
                numbering = this.gameObject.GetComponent<MoveBtn>().btn_numbering = start_num;
            else
                numbering = this.gameObject.GetComponent<MoveBtn>().btn_numbering += 100;
            this.gameObject.GetComponent<Image>().sprite = btn_array[numbering % 100 + numbering / 100 - start_num];
        }
        else
        {
            if (numbering == start_num + 100)
                numbering = this.gameObject.GetComponent<BlkOnNoteComp>().thisBlk = start_num;
            else
                numbering = this.gameObject.GetComponent<BlkOnNoteComp>().thisBlk += 100;
            this.gameObject.GetComponent<Image>().sprite = btn_array[numbering % 100 + numbering / 100 - start_num];
        }

    }

}