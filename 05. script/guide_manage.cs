using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

    튜토리얼을 위해 화살표를 정해진 순서에 따라 움직이는 코드입니다.

*/
[System.Serializable]
public class tuto_objects
{
    public GameObject[] objs;
}

public class guide_manage : MonoBehaviour
{
    bool isbig = false;
    int stage_num;

    

    public GameObject Qtext;
    public GameObject Play;
    public GameObject Replay;

    public GameObject oper_btn;
    public GameObject const_int_btn;
    public GameObject const_string_btn;
    public GameObject var_btn;
    public GameObject if_btn;
    public GameObject oper_window;
    public GameObject int_window;
    public GameObject string_window;

    Vector2 loc_Qtext_before = new Vector2(351, 380);
    Vector2 loc_Qtext = new Vector2(538, 393);
    Vector2 loc_printf = new Vector2(250, 334);
    Vector2 loc_scanf = new Vector2(250, 248);
    Vector2 loc_oper = new Vector2(321, 135);
    Vector2 loc_const_int = new Vector2(311, 63);
    Vector2 loc_const_string = new Vector2(291, 7);
    Vector2 loc_var = new Vector2(298, -88);
    Vector2 loc_arr = new Vector2(258, -160);
    Vector2 loc_if = new Vector2(254, -242);
    Vector2 loc_loop = new Vector2(254, -327);
    Vector2 loc_func = new Vector2(254, -397);
    Vector2 loc_play = new Vector2(-316, -400);
    Vector2 loc_exit = new Vector2(-718, 426);
    Vector2 loc_next = new Vector2(-47, -80);

    Vector2 loc_assign_ = new Vector2(0, 134);
    Vector2 loc_plus_ = new Vector2(89, 134);
    Vector2 loc_minus_ = new Vector2(185, 134);
    Vector2 loc_multiply_ = new Vector2(281, 134);
    Vector2 loc_divide_ = new Vector2(370, 134);
    Vector2 loc_compare_ = new Vector2(453, 134);
    Vector2 loc_left_ = new Vector2(549, 134);
    Vector2 loc_right_ = new Vector2(645, 134);

    bool ison = false;

    float time = 0;
    public tuto_objects[] Objs;
    bool Qtext_tuto_done = false;
    bool click_swch = true;

    private void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
    }

    // Start is called before the first frame update
    void Update()
    {
        // 스테이지에 따라 드래그, 클릭하는 순서 다르게 하기
        
        Qtext_tuto();

        if(Qtext_tuto_done && Qtext.activeSelf == false)
            switch (stage_num)
            {
                case 0:

                    if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 1)
                    {
                        Vector2 loc = new Vector2(364, 259);
                        drag(loc_printf, loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 22)
                    {
                        Vector2 loc = new Vector2(555, 259);
                        drag(loc_const_string, loc);
                    }
                    else if (!ison && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk == 1 && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk == 22)
                    {
                        click(loc_play);

                    }
                    else if (Play.GetComponent<Language_script>().iscleared == true)
                    {
                        click(loc_next);
                    }
                    /*
                    else
                    {
                        click(loc_exit);
                    }*/
                    break;

                case 1:

                    GameObject first_blk = Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn;

                    if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 31)
                    {
                        Vector2 loc = new Vector2(364, 259);
                        drag(loc_var, loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        Vector2 loc = new Vector2(490, 259);
                        drag(loc_oper, loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 22)
                    {
                        Vector2 loc = new Vector2(607, 259);
                        drag(loc_const_string, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk != 1)
                    {
                        Vector2 loc = new Vector2(364, 201);
                        drag(loc_printf, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        Vector2 loc = new Vector2(490, 201);
                        drag(loc_var, loc);
                    }
                    else if (Play.GetComponent<Language_script>().iscleared == true)
                    {
                        click(loc_next);
                    }
                    else
                    {
                        click(loc_play);
                    }

                    break;
                case 2:

                    if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 1)
                    {
                        Vector2 loc = new Vector2(364, 259);
                        drag(loc_printf, loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(555, 259);
                        drag(loc_const_int, loc);
                    }
                    else if (Play.GetComponent<Language_script>().iscleared == true)
                    {
                        click(loc_next);
                    }
                    else
                    {
                        click(loc_play);
                    }
                    break;
                case 3:

                    first_blk = Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn;

                    if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 31)
                    {
                        Vector2 loc = new Vector2(364, 259);
                        drag(loc_var, loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        Vector2 loc = new Vector2(490, 259);
                        drag(loc_oper, loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(607, 259);
                        drag(loc_const_int, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk != 1)
                    {
                        Vector2 loc = new Vector2(364, 201);
                        drag(loc_printf, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        Vector2 loc = new Vector2(490, 201);
                        drag(loc_var, loc);
                    }
                    else if (Play.GetComponent<Language_script>().iscleared == true)
                    {
                        click(loc_next);
                    }
                    else
                    {
                        click(loc_play);
                    }
                    break;
                case 4:
                case 5:
                    first_blk = Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn;
                    if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 2)
                    {
                        Vector2 loc = new Vector2(364, 259);
                        drag(loc_scanf, loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        Vector2 loc = new Vector2(490, 259);
                        drag(loc_var, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk != 1)
                    {
                        Vector2 loc = new Vector2(364, 201);
                        drag(loc_printf, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        Vector2 loc = new Vector2(490, 201);
                        drag(loc_var, loc);
                    }
                    else if (Play.GetComponent<Language_script>().iscleared == true)
                    {
                        click(loc_next);
                    }
                    else
                    {
                        click(loc_play);
                    }
                    break;
                case 6:
                    first_blk = Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn;
                    if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 31)
                    {
                        Vector2 loc = new Vector2(364, 259);
                        drag(loc_var, loc);
                    }
                    else if (oper_btn.GetComponent<MoveBtn>().btn_numbering != 23 && oper_window.activeSelf == false && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        click(loc_oper);
                    }
                    else if (oper_window.activeSelf == true && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        click(loc_assign_);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        Vector2 loc = new Vector2(490, 259);
                        drag(loc_oper, loc);
                    }
                    else if (const_int_btn.transform.GetChild(0).GetComponent<Text>().text != "0328" && int_window.activeSelf == false && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        click(loc_const_int);
                    }
                    else if(int_window.activeSelf == true && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(267, 55);
                        click(loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(607, 259);
                        drag(loc_const_int, loc);
                    }
                    else if (oper_btn.GetComponent<MoveBtn>().btn_numbering != 25 && oper_window.activeSelf == false && first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 25)
                    {
                        click(loc_oper);
                    }
                    else if (oper_window.activeSelf == true && first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 25)
                    {
                        click(loc_minus_);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 25)
                    {
                        Vector2 loc = new Vector2(620, 259);
                        drag(loc_oper, loc);
                    }
                    else if (const_int_btn.transform.GetChild(0).GetComponent<Text>().text != "0307" && int_window.activeSelf == false && first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        click(loc_const_int);
                    }
                    else if(int_window.activeSelf == true && first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(364, 55);
                        click(loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(700, 259);
                        drag(loc_const_int, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk != 1)
                    {
                        Vector2 loc = new Vector2(364, 201);
                        drag(loc_printf, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        Vector2 loc = new Vector2(490, 201);
                        drag(loc_var, loc);
                    }
                    else if (Play.GetComponent<Language_script>().iscleared == true)
                    {
                        click(loc_next);
                    }
                    else
                    {
                        click(loc_play);
                    }
                    break;
                case 7:
                    first_blk = Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn;
                    if (var_btn.transform.GetChild(0).GetComponent<Text>().text != "1" && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 31)
                    {
                        click(loc_var);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 31)
                    {
                        Vector2 loc = new Vector2(364, 259);
                        drag(loc_var, loc);
                    }
                    else if (oper_btn.GetComponent<MoveBtn>().btn_numbering != 23 && oper_window.activeSelf == false && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        click(loc_oper);
                    }
                    else if (oper_window.activeSelf == true && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        click(loc_assign_);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        Vector2 loc = new Vector2(490, 259);
                        drag(loc_oper, loc);
                    }
                    else if (const_int_btn.transform.GetChild(0).GetComponent<Text>().text != "0328" && int_window.activeSelf == false && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        click(loc_const_int);
                    }
                    else if (int_window.activeSelf == true && Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(267, 55);
                        click(loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(607, 259);
                        drag(loc_const_int, loc);
                    }
                    else if (oper_btn.GetComponent<MoveBtn>().btn_numbering != 25 && oper_window.activeSelf == false && first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 25)
                    {
                        click(loc_oper);
                    }
                    else if (oper_window.activeSelf == true && first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 25)
                    {
                        click(loc_minus_);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 25)
                    {
                        Vector2 loc = new Vector2(620, 259);
                        drag(loc_oper, loc);
                    }
                    else if (const_int_btn.transform.GetChild(0).GetComponent<Text>().text != "0307" && int_window.activeSelf == false && first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        click(loc_const_int);
                    }
                    else if (int_window.activeSelf == true && first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(300, 55);
                        click(loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(700, 259);
                        drag(loc_const_int, loc);
                    }
                    else if(var_btn.transform.GetChild(0).GetComponent<Text>().text != "2" && first_blk.GetComponent<BlkOnNoteComp>().DownBlk != 32)
                    {
                        click(loc_var);
                    }
                    else if(first_blk.GetComponent<BlkOnNoteComp>().DownBlk != 32)
                    {
                        Vector2 loc = new Vector2(364, 201);
                        drag(loc_var, loc);
                    }
                    else if(oper_window.activeSelf == false && oper_btn.GetComponent<MoveBtn>().btn_numbering != 23 && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        click(loc_oper);
                    }
                    else if(oper_window.activeSelf == true && oper_btn.GetComponent<MoveBtn>().btn_numbering != 23 && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        click(loc_assign_);
                    }
                    else if(first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 23)
                    {
                        Vector2 loc = new Vector2(490, 201);
                        drag(loc_oper, loc);
                    }
                    else if(int_window.activeSelf == false && const_int_btn.transform.GetChild(0).GetComponent<Text>().text != "21000")
                    {
                        click(loc_const_int);
                    }
                    else if(int_window.activeSelf == true && const_int_btn.transform.GetChild(0).GetComponent<Text>().text != "21000" && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(384, 55);
                        click(loc);
                    }
                    else if(first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 21)
                    {
                        Vector2 loc = new Vector2(607, 201);
                        drag(loc_const_int, loc);
                    }
                    else if(oper_window.activeSelf == false && oper_btn.GetComponent<MoveBtn>().btn_numbering != 27)
                    {
                        click(loc_oper);
                    }
                    else if(oper_window.activeSelf == true && oper_btn.GetComponent<MoveBtn>().btn_numbering != 27 && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 27)
                    {
                        click(loc_divide_);
                    }
                    else if(first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 27)
                    {
                        Vector2 loc = new Vector2(620, 201);
                        drag(loc_oper, loc);
                    }
                    else if(var_btn.transform.GetChild(0).GetComponent<Text>().text != "1" && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        click(loc_var);
                    }
                    else if(first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        Vector2 loc = new Vector2(700, 201);
                        drag(loc_var, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().DownBlk != 1)
                    {
                        Vector2 loc = new Vector2(364, 116);
                        drag(loc_printf, loc);
                    }
                    else if (var_btn.transform.GetChild(0).GetComponent<Text>().text != "2")
                    {
                        click(loc_var);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 32)
                    {
                        Vector2 loc = new Vector2(490, 116);
                        drag(loc_var, loc);
                    }
                    else if (Play.GetComponent<Language_script>().iscleared == true)
                    {
                        click(loc_next);
                    }
                    else
                    {
                        click(loc_play);
                    }
                    break;
                case 8:
                    first_blk = Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn;
                    if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk != 2)
                    {
                        Vector2 loc = new Vector2(364, 259);
                        drag(loc_scanf, loc);
                    }
                    else if (Objs[0].objs[0].GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        Vector2 loc = new Vector2(490, 259);
                        drag(loc_var, loc);
                    }
                    else if (if_btn.GetComponent<MoveBtn>().btn_numbering != 3 && first_blk.GetComponent<BlkOnNoteComp>().DownBlk != 3)
                    {
                        click(loc_if);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk != 3)
                    {
                        Vector2 loc = new Vector2(364, 201);
                        drag(loc_if, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 31)
                    {
                        Vector2 loc = new Vector2(490, 201);
                        drag(loc_var, loc);
                    }
                    else if (oper_window.activeSelf == false && oper_btn.GetComponent<MoveBtn>().btn_numbering != 28)
                    {
                        click(loc_oper);
                    }
                    else if (oper_window.activeSelf == true && oper_btn.GetComponent<MoveBtn>().btn_numbering != 28 && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 28)
                    {
                        click(loc_compare_);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 28)
                    {
                        Vector2 loc = new Vector2(607, 201);
                        drag(loc_oper, loc);
                    }
                    else if (string_window.activeSelf == false && const_string_btn.transform.GetChild(0).GetComponent<Text>().text != "If I disappear..." && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 22)
                    {
                        click(loc_const_string);
                    }
                    else if(string_window.activeSelf == true && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 22)
                    {
                        Vector2 loc = new Vector2(227, -15);
                        click(loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 22)
                    {
                        Vector2 loc = new Vector2(690, 201);
                        drag(loc_const_string, loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().DownBlk != 1)
                    {
                        Vector2 loc = new Vector2(364, 116);
                        drag(loc_printf, loc);
                    }
                    else if (string_window.activeSelf == false && const_string_btn.transform.GetChild(0).GetComponent<Text>().text != "I must find you." && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 22)
                    {
                        click(loc_const_string);
                    }
                    else if (string_window.activeSelf == true && first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 22)
                    {
                        Vector2 loc = new Vector2(527, -15);
                        click(loc);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk != 22)
                    {
                        Vector2 loc = new Vector2(490, 116);
                        drag(loc_const_string, loc);
                    }
                    else if (if_btn.GetComponent<MoveBtn>().btn_numbering != 103)
                    {
                        click(loc_if);
                    }
                    else if (first_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().DownBlk_btn.GetComponent<BlkOnNoteComp>().DownBlk != 103)
                    {
                        Vector2 loc = new Vector2(364, 49);
                        drag(loc_if, loc);
                    }
                    else if (Play.GetComponent<Language_script>().iscleared == true)
                    {
                        click(loc_next);
                    }
                    else
                    {
                        click(loc_play);
                    }
                    break;
                case 9:
                    this.gameObject.SetActive(false);
                    break;
                default:
                    this.gameObject.SetActive(false);
                    break;
            }
    }



    public void click_play()
    {
        ison = true;
    }

    // 코딩 노트 위의 상세 설명 버튼을 누르게 하는 튜토리얼입니다. 모든 스테이지에서 제일 먼저 실행됩니다.
    void Qtext_tuto()
    {
        if (Qtext.activeSelf == true)
        {
            Qtext_tuto_done = true;
            click(loc_Qtext);
        }

        if (Qtext.activeSelf == false)
        {
            if (Qtext_tuto_done != true)
                click(loc_Qtext_before);

        }

    }

    // start 위치에서 end 위치로 _time 단위로 1초 동안 이동합니다.
    void drag(Vector2 start, Vector2 end, float _time = 0.01f)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = start + (end - start) * time;
        time += _time;
        if (time >= 1f)
            time = 0;
        if (end == loc_Qtext)
            gameObject.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, -1f, 0);
        else
            gameObject.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);

    }

    // end 위치에서 클릭하는 모션을 취합니다.
    void click(Vector2 end)
    {
        Vector2 offset = new Vector2(-5f, 5f);
        drag(end, end + offset, 0.05f);
        if (end == loc_Qtext)
            gameObject.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, -1f, 0);
        else
            gameObject.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
    }
}
