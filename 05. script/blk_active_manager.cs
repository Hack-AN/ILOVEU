using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    스테이지마다 쓰는 코딩 블록이 다 다르다. 어떤 스테이지는 말하기, 문자 블록만 쓰기도 하고,
    어떤 스테이지는 모든 블록을 쓰기도 한다. 따라서 각 스테이지마다 쓰는 코딩 블록만 활성화하기로 한다.

*/


public class blk_active_manager : MonoBehaviour
{

    int stage_num;

    public GameObject blk_pritntf;
    public GameObject blk_scanf;
    public GameObject blk_oper;
    public GameObject blk_int_const;
    public GameObject blk_string_const;
    public GameObject blk_var;
    public GameObject blk_arr;
    public GameObject blk_if;
    public GameObject blk_loop;
    public GameObject blk_func;

    // Start is called before the first frame update
    void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;

        switch(stage_num)
        {
            case 0:
                blk_scanf.SetActive(false);
                blk_oper.SetActive(false);
                blk_int_const.SetActive(false);
                blk_var.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);

                break;

            case 1:
                blk_scanf.SetActive(false);
                blk_int_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);

                break;
            case 2:
                blk_scanf.SetActive(false);
                blk_oper.SetActive(false);
                blk_string_const.SetActive(false);
                blk_var.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);

                break;
            case 3:
                blk_scanf.SetActive(false);
                blk_string_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);

                break;
            case 4:
                blk_oper.SetActive(false);
                blk_string_const.SetActive(false);
                blk_int_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);

                break;
            case 5:
                blk_oper.SetActive(false);
                blk_string_const.SetActive(false);
                blk_int_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);

                break;
            case 6:
                blk_scanf.SetActive(false);
                blk_string_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);
                break;

            case 7:
                blk_scanf.SetActive(false);
                blk_string_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);
                break;
            case 8:
                blk_int_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);
                break;
            case 9:
                blk_int_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);
                break;
            case 10:
                blk_scanf.SetActive(false);
                blk_string_const.SetActive(false);
                blk_arr.SetActive(false);
                blk_if.SetActive(false);
                blk_func.SetActive(false);
                break;
            case 11:
                blk_arr.SetActive(false);
                blk_loop.SetActive(false);
                blk_func.SetActive(false);
                break;

        }

    }
}
