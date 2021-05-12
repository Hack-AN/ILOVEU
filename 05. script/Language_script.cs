using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

    코딩 툴을 구현한 스크립트입니다.
    코딩 블록의 배열에 따라 알맞게 동작하도록 구현하였습니다.

    다음과 같은 구조로 동작합니다.
    
    우선 코딩 블록은 다음과 같은 구조로 연결되어있습니다.
    ㅁ은 하나의 코딩 블록입니다.
    
    <코딩 노트>
    =====================================
    ㅁ
    ㅣ
    ㅁ-ㅁ-ㅁ-...
    ㅣ
    ㅁ-ㅁ-ㅁ-...
    ㅣ
    ㅁ-ㅁ-ㅁ-...
    ㅣ
    ....

    =====================================

    맨 위의 블록은 start_point 블록으로, 아무런 동작을 수행하지 않는, 오로지 시작지점임을 나타내는 블록입니다.
    그 밑으로 플레이어가 붙이는 코딩 블록이 위와 같은 구조로 연결됩니다.

    플레이 버튼을 눌러 이 스크립트를 실행할 경우, start_point 블록을 찾아 해당 블록의 아랫 블록부터 아래의 루프를 시작합니다.


    Loop : 첫 줄 부터 끝 줄 까지 한 줄씩 검사
    {
        만약 맨 왼쪽의 블록이
            printf : 오른쪽 블록의 문자/숫자를 말풍선에 넣어 띄운다.
            scanf : 오른쪽 블록이 변수 혹은 배열이라면, 해당 변수 혹은 배열에 스테이지별로 정해진 입력값을 저장한다.
            변수 : 오른쪽 블록이 '=' 블록이라면, '=' 블록의 오른쪽 블록의 문자/숫자를 변수에 저장한다.
            ...
        
        자세한 구조는 아래에 서술합니다.
    }


*/

[System.Serializable]
public class Map2D_lang
{
    public GameObject[] Map;
}

public class Language_script : MonoBehaviour
{
    public GameObject click_more;
    public GameObject present_blk;
    public GameObject change_image;
    public Map2D_lang[] etc_objs;

    public AudioSource audiosource;
    public AudioClip success_clip;
    public AudioClip fail_clip;

    int[] var_int = new int[3];                  // 이 게임에선 int형 로컬 변수는 최대 3개까지 사용 가능합니다. 
    bool[] var_int_declared = new bool[3];       // 해당 변수가 쓰여지고 있는지 저장하는 배열입니다.

    string[] var_string = new string[3];         // 이 게임에선 string형 로컬 변수는 최대 3개까지 사용 가능합니다.
    bool[] var_string_declared = new bool[3];    // 해당 변수가 쓰여지고 있는지 저장하는 배열입니다. 

    int[,] arr_int = new int[3, 10];             // 이 게임에선 int형 로컬 배열을 최대 3개까지 사용 가능하며, 각 배열 당 10이 최대 size입니다.
    bool[] arr_int_declared = new bool[3];       // 해당 배열이 쓰여지고 있는지 저장하는 배열입니다.

    string[,] arr_string = new string[3, 10];    // 이 게임에선 string형 로컬 배열을 최대 3개까지 사용 가능하며, 각 배열 당 10이 최대 size입니다.
    bool[] arr_string_declared = new bool[3];    // 해당 배열이 쓰여지고 있는지 저장하는 배열입니다.

    ArrayList while_blk = new ArrayList();

    public GameObject[] speech_bubble;         // printf를 통해 쓰는 말풍선을 스테이지별로 저장해두는 배열입니다.
    public GameObject[] scanf_speech_bubble;   // scanf를 통해 쓰는 말풍선을 스테이지별로 저장해두는 배열입니다.
    int stage_num;

    public GameObject another_image_manager;  // 추가 묘사를 위해 넣어둔 오브젝트입니다.
    bool loop_swch = false;

    ArrayList arr_stage_6 = new ArrayList(); // 스테이지 6을 위해 따로 마련해둔 ArrayList입니다.

    public GameObject dir;  // 튜토리얼을 위해 움직일 화살표 오브젝트입니다.

    public GameObject error_message;  // 만약 코드에서 에러가 나올 경우 띄울 에러 메세지 오브젝트입니다.
    bool error_swch = false;

    public GameObject before_speech; // 스테이지 시작 직후 문제 상황 묘사를 위해 띄울 말풍선 오브젝트입니다.

    bool if_tuto = false;

    public GameObject success_window;

    public bool iscleared = false;

    public Sprite solve_if;
    public Sprite not_solve_if;

    bool stage_9_isdone = false;
    bool stage_9_isdone1 = false;
    bool stage_9_isdone2 = false;

    // Start is called before the first frame update
    void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
    }


    public void Play()
    {

        // 코드 시작 전에 변수, 배열을 모두 사용 이전으로 초기화합니다.
        for(int i =0; i < 3; i ++)
        {
            var_int[i] = 0;
            var_int_declared[i] = false;
            var_string[i] = null;
            var_string_declared[i] = false;
            arr_int_declared[i] = false;
            arr_string_declared[i] = false;
            for(int j = 0; j < 10; j++)
            {
                arr_int[i, j] = 0;
                arr_string[i, j] = null;
            }
        }

        // 코드 시작 전에 scanf 말풍선을 제거하고 튜토리얼 화살표를 활성화합니다.
        speech_bubble[stage_num].transform.GetChild(0).gameObject.GetComponent<Text>().text = null;
        dir.SetActive(true);

        // 코드 실행을 위해 코루틴을 실행합니다.
        StartCoroutine(timer());
    }


    // 해당 스크립트는 기본적으로 한 줄을 실행한 후 1.5초 기다린 후 다음 줄로 넘어가도록 설계되었습니다.
    // 그러나 플레이어의 편의를 위해 특정 블록에 한해서만 1.5초를 대기하고, 그 외의 블록은 1.5초를 대기하지 않도록 구현했습니다.
    // 따라서 코루틴을 이용하여 일정 시간 마다 해당 코드를 반복하도록 하였습니다.
    IEnumerator timer()
    {
        present_blk = GameObject.Find("start point 1").gameObject;

        // 코드 시작 전에 상황 묘사를 위한 오브젝트들을 비활성화합니다.
        if (present_blk.GetComponent<BlkOnNoteComp>().DownBlk != 0)
        {
            before_speech.SetActive(false);
            change_image.SetActive(false);
            another_image_manager.GetComponent<default_image_manager>().change_default_image();
            for (int i = 0; i < etc_objs[stage_num].Map.Length; i++)
                etc_objs[stage_num].Map[i].SetActive(false);
        }

        // 마지막 줄까지 해당 코드를 반복합니다.
        while (present_blk.GetComponent<BlkOnNoteComp>().DownBlk != 0)
        {
            speech_bubble[stage_num].SetActive(false);
            scanf_speech_bubble[stage_num].SetActive(false);

            if (loop_swch == false)
            {
                present_blk = present_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn;
                dir.transform.Translate(new Vector3(0,present_blk.transform.position.y - dir.transform.position.y + present_blk.GetComponent<RectTransform>().rect.height /2, 0));
            }
            else
                loop_swch = false;
                

            // 만약 현재 줄의 맨 왼쪽 블록이...
            switch (present_blk.GetComponent<BlkOnNoteComp>().thisBlk)
            {

                // printf 블록
                case 1:
                    int right_num = present_blk.GetComponent<BlkOnNoteComp>().RightBlk;
                    if (right_num == 0)
                    {
                        error();
                        break;
                    }
                    speech_bubble[stage_num].SetActive(true);
                    switch (right_num)
                    {
                        case 21:
                        case 22:
                            
                            speech_bubble[stage_num].transform.GetChild(0).gameObject.GetComponent<Text>().text = present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).gameObject.GetComponent<Text>().text;
                            break;
                        case 31:
                        case 32:
                        case 33:

                            if (var_int_declared[right_num - 31] && !var_string_declared[right_num - 31])
                                speech_bubble[stage_num].transform.GetChild(0).gameObject.GetComponent<Text>().text = var_int[right_num - 31].ToString();
                            else if (!var_int_declared[right_num - 31] && var_string_declared[right_num - 31])
                                speech_bubble[stage_num].transform.GetChild(0).gameObject.GetComponent<Text>().text = var_string[right_num - 31];
                            break;
                        case 34:
                        case 35:
                        case 36:
                            
                            if (!arr_int_declared[right_num - 34] && arr_string_declared[right_num - 34])
                                speech_bubble[stage_num].transform.GetChild(0).gameObject.GetComponent<Text>().text = arr_string[right_num - 34, int.Parse(present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.gameObject.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)];   // 몇 번째 원소 고르는 법 다시 생각하기. getchild로는 한계가 있음
                            else if (arr_int_declared[right_num - 34] && !arr_string_declared[right_num - 34])
                                speech_bubble[stage_num].transform.GetChild(0).gameObject.GetComponent<Text>().text = arr_int[right_num - 34, int.Parse(present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.gameObject.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)].ToString();
                            break;
                    }
                    if (stage_num == 5)
                    {
                        arr_stage_6.Add(speech_bubble[5].transform.GetChild(0).gameObject.GetComponent<Text>().text);
                    }
                    break;
                // scanf 블록
                case 2:

                    right_num = present_blk.GetComponent<BlkOnNoteComp>().RightBlk;
                    if (right_num == 0)
                    {
                        error();
                        break;
                    }
                        
                    scanf_case_per_stage();
                    string temp = scanf_speech_bubble[stage_num].transform.GetChild(0).gameObject.GetComponent<Text>().text;
                    int i = 0;
                    bool isint = int.TryParse(temp, out i);

                    switch (right_num)
                    {
                        case 31:
                        case 32:
                        case 33:
                            if (isint)
                            {
                                var_int[right_num - 31] = int.Parse(temp);
                                var_int_declared[right_num - 31] = true;
                                var_string_declared[right_num - 31] = false;
                            }
                                
                            else
                            {
                                var_string[right_num - 31] = temp;
                                var_int_declared[right_num - 31] = false;
                                var_string_declared[right_num - 31] = true;
                            }
                                
                            break;
                        case 34:
                        case 35:
                        case 36:
                            if (isint)
                            {
                                arr_int[right_num - 34, int.Parse(present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.gameObject.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)] = int.Parse(temp);
                                arr_int_declared[right_num - 31] = true;
                                arr_string_declared[right_num - 31] = false;
                            }
                            else
                            {
                                arr_string[right_num - 34, int.Parse(present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.gameObject.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)] = temp;
                                arr_int_declared[right_num - 31] = false;
                                arr_string_declared[right_num - 31] = true;
                            }
                            break;

                    }

                    break;
                // if 블록
                case 3:
                    if (present_blk.GetComponent<BlkOnNoteComp>().RightBlk == 0)
                    {
                        error();
                        break;
                    }
                        

                    GameObject first = present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn;
                    i = 0;
                    isint = true;
                    switch(first.GetComponent<BlkOnNoteComp>().thisBlk)
                    {
                        case 21:
                            isint = int.TryParse(first.transform.GetChild(0).GetComponent<Text>().text, out i);
                            break;
                        case 31:
                        case 32:
                        case 33:
                            if (var_int_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 31] && !var_string_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 31])
                                isint = true;
                            else if (!var_int_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 31] && var_string_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 31])
                                isint = int.TryParse(var_string[first.GetComponent<BlkOnNoteComp>().thisBlk - 31], out i);
                            break;
                        case 34:
                        case 35:
                        case 36:
                            if (arr_int_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 34] && !arr_string_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 34])
                                isint = true;
                            else if (!arr_int_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 34] && arr_string_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 34])
                                isint = int.TryParse(arr_string[first.GetComponent<BlkOnNoteComp>().thisBlk - 34, int.Parse(first.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)], out i);
                            break;
                    }

                    

                    if (isint)
                    {
                        int front = 0;
                        switch (first.GetComponent<BlkOnNoteComp>().thisBlk)
                        {
                            case 21:
                                front = int.Parse(first.transform.GetChild(0).GetComponent<Text>().text);
                                break;
                            case 31:
                            case 32:
                            case 33:
                                front = var_int[first.GetComponent<BlkOnNoteComp>().thisBlk - 31];
                                break;
                            case 34:
                            case 35:
                            case 36:
                                front = arr_int[first.GetComponent<BlkOnNoteComp>().thisBlk - 34, int.Parse(first.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)];
                                break;
                        }
                        if (calculator(front, first.GetComponent<BlkOnNoteComp>().RightBlk, first.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn) == 0)
                        {
                            while (present_blk.GetComponent<BlkOnNoteComp>().DownBlk != 103)
                            {
                                present_blk = present_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn;
                               
                            }
                        }
                        else
                        {
                            present_blk.GetComponent<Image>().sprite = solve_if;
                        }
                    }
                    else
                    {
                        string front = "";
                        switch (first.GetComponent<BlkOnNoteComp>().thisBlk)
                        {
                            case 21:
                                front = first.transform.GetChild(0).GetComponent<Text>().text;
                                break;
                            case 31:
                            case 32:
                            case 33:
                                front = var_string[first.GetComponent<BlkOnNoteComp>().thisBlk - 31];
                                break;
                            case 34:
                            case 35:
                            case 36:
                                front = arr_string[first.GetComponent<BlkOnNoteComp>().thisBlk - 34, int.Parse(first.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)];
                                break;
                        }
                        if (calculator_string(front, first.GetComponent<BlkOnNoteComp>().RightBlk, first.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn) == false)
                        {
                            while (present_blk.GetComponent<BlkOnNoteComp>().DownBlk != 103)
                            {
                                present_blk = present_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn;
                               
                            }
                        }
                        else
                        {
                            present_blk.GetComponent<Image>().sprite = solve_if;
                        }
                    }
                    


                    break;
                // loop 블록
                case 4:
                    if (present_blk.GetComponent<BlkOnNoteComp>().RightBlk == 0)
                    {
                        error();
                        break;
                    }

                    first = present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn;
                    i = 0;
                    isint = true;
                    switch (first.GetComponent<BlkOnNoteComp>().thisBlk)
                    {
                        case 21:
                            isint = int.TryParse(first.transform.GetChild(0).GetComponent<Text>().text, out i);
                            break;
                        case 31:
                        case 32:
                        case 33:
                            if (var_int_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 31] && !var_string_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 31])
                                isint = true;
                            else if (!var_int_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 31] && var_string_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 31])
                                isint = int.TryParse(var_string[first.GetComponent<BlkOnNoteComp>().thisBlk - 31], out i);
                            break;
                        case 34:
                        case 35:
                        case 36:
                            if (arr_int_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 34] && !arr_string_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 34])
                                isint = true;
                            else if (!arr_int_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 34] && arr_string_declared[first.GetComponent<BlkOnNoteComp>().thisBlk - 34])
                                isint = int.TryParse(arr_string[first.GetComponent<BlkOnNoteComp>().thisBlk - 34, int.Parse(first.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)], out i);
                            break;
                    }

                    if (isint)
                    {
                        int front = 0;

                        if (while_blk.Count == 0)
                            while_blk.Add(present_blk);
                        else if ((object)while_blk[while_blk.Count - 1] != present_blk)
                            while_blk.Add(present_blk);

                        switch (first.GetComponent<BlkOnNoteComp>().thisBlk)
                        {
                            case 21:
                                front = int.Parse(first.transform.GetChild(0).GetComponent<Text>().text);
                                break;
                            case 31:
                            case 32:
                            case 33:
                                front = var_int[first.GetComponent<BlkOnNoteComp>().thisBlk - 31];
                                break;
                            case 34:
                            case 35:
                            case 36:
                                front = arr_int[first.GetComponent<BlkOnNoteComp>().thisBlk - 34, int.Parse(first.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)];
                                break;
                        }
                        if (calculator(front, first.GetComponent<BlkOnNoteComp>().RightBlk, first.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn) == 0)
                        {
                            while_blk.RemoveAt(while_blk.Count - 1);
                            while (present_blk.GetComponent<BlkOnNoteComp>().DownBlk != 104)
                            {
                                present_blk = present_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn;
                               
                            }
                            present_blk = present_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn;
                        }
                    }
                    else
                    {
                        string front = "";


                        if(while_blk.Count == 0)
                            while_blk.Add(present_blk);
                        else if((object)while_blk[while_blk.Count-1] != present_blk)
                            while_blk.Add(present_blk);

                        switch (first.GetComponent<BlkOnNoteComp>().thisBlk)
                        {
                            case 21:
                                front = first.transform.GetChild(0).GetComponent<Text>().text;
                                break;
                            case 31:
                            case 32:
                            case 33:
                                front = var_string[first.GetComponent<BlkOnNoteComp>().thisBlk - 31];
                                break;
                            case 34:
                            case 35:
                            case 36:
                                front = arr_string[first.GetComponent<BlkOnNoteComp>().thisBlk - 34, int.Parse(first.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)];
                                break;
                        }
                        if (calculator_string(front, first.GetComponent<BlkOnNoteComp>().RightBlk, first.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn) == false)
                        {
                            while_blk.RemoveAt(while_blk.Count - 1);
                            while (present_blk.GetComponent<BlkOnNoteComp>().DownBlk != 104)
                            {
                                present_blk = present_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn;
                               
                            }
                            present_blk = present_blk.GetComponent<BlkOnNoteComp>().DownBlk_btn;
                        }
                            
                    }
                    

                        break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                    break;

                // 변수 블록
                case 31:
                case 32:
                case 33:
                    // 대입 연산자 있을 때 뒤에 있는 놈에 따라 스위치 다르게 켜기
                    right_num = present_blk.GetComponent<BlkOnNoteComp>().RightBlk;
                    if (right_num != 23)
                    {
                        error();
                        break;
                    }
                    if (right_num == 23)
                    {
                        int assigned_blk = present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk;

                        /*
                        if(assigned_blk == 0 || !present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn || !present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn)
                        {
                            error();
                            break;
                        }
                        */
                        temp = present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text;
                        bool isonlyassign = present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk == 0 ? true : false;
                        switch (assigned_blk)
                        {
                            case 21:
                                if (isonlyassign)
                                    var_int[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = int.Parse(temp);
                                else
                                    var_int[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = calculator(int.Parse(temp), present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk, present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn);
                                var_int_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = true;
                                var_string_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = false;
                                break;
                            case 22:

                                var_string[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = temp;
                                var_int_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = false;
                                var_string_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = true;
                                break;
                            case 31:
                            case 32:
                            case 33:
                                if (var_int_declared[assigned_blk-31] && !var_string_declared[assigned_blk - 31])
                                {
                                    if (isonlyassign)
                                        var_int[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = var_int[assigned_blk - 31];
                                    else
                                        var_int[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = calculator(var_int[assigned_blk - 31], present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk, present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn);

                                    var_int_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = true;
                                    var_string_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = false;
                                }
                                else if (!var_int_declared[assigned_blk - 31] && var_string_declared[assigned_blk - 31])
                                {
                                    var_string[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = var_string[assigned_blk - 31];
                                    var_int_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = false;
                                    var_string_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = true;

                                }
                                break;
                            case 34:
                            case 35:
                            case 36:
                                if(arr_int_declared[assigned_blk - 34] && !arr_string_declared[assigned_blk - 34])
                                {
                                    if (isonlyassign)
                                        var_int[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = arr_int[assigned_blk - 34, int.Parse(present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<Text>().text)];
                                    else
                                        var_int[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = calculator(arr_int[assigned_blk - 34, int.Parse(present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<Text>().text)], present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk, present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn);
                                    var_int_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = true;
                                    var_string_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = false;
                                }
                                else if (!arr_int_declared[assigned_blk - 34] && arr_string_declared[assigned_blk - 34])
                                {
                                    var_string[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = arr_string[assigned_blk - 34, int.Parse(present_blk.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<BlkOnNoteComp>().RightBlk_btn.GetComponent<Text>().text)];
                                    var_int_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = false;
                                    var_string_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = true;
                                }
                                break;
                        }

                        

                    }
                    //var_int_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = true;
                    //var_string_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 31] = true;
                    break;
                // 배열 블록
                case 34:
                case 35:
                case 36:
                    //arr_int_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 34] = true;
                    //arr_string_declared[present_blk.GetComponent<BlkOnNoteComp>().thisBlk - 34] = true;
                    break;
                case 37:
                    break;
                
                // if 끝 블록
                case 103:
                    if (present_blk.GetComponent<BlkOnNoteComp>().RightBlk != 0)
                    {
                        error();
                        break;
                    }
                    break;
                // loop 끝 블록
                case 104:
                    if (present_blk.GetComponent<BlkOnNoteComp>().RightBlk != 0)
                    {
                        error();
                        break;
                    }
                    present_blk = (GameObject)while_blk[while_blk.Count - 1];
                    loop_swch = true;
                    break;
                default:
                    break;


            }

            Debug.Log("start");

            if(error_swch)
            {
                yield return new WaitForSeconds(1f);
                error_swch = false;
                error_message.SetActive(false);
                break;
            }

            
            // 위에서 설명한 대로 특정 블록에 한해서만 1.5초를 대기하고 다음 줄로 넘어갑니다.
            // 실제 액션이 화면에 애니메이팅 되는 printf, scanf 블록일 경우에만 1.5초 대기 후 다음 줄로 넘어갑니다.
            switch(present_blk.GetComponent<BlkOnNoteComp>().thisBlk)
            {
                
                case 1:
                case 2:
                case 3:
                case 4:
                    yield return new WaitForSeconds(1.5f);

                    if (present_blk.GetComponent<BlkOnNoteComp>().thisBlk == 3)
                        present_blk.GetComponent<Image>().sprite = not_solve_if;

                    break;

                default:
                    break;
            }
            
            Debug.Log("end");

        }
        
        // 코드가 끝난 후 스테이지 별로 다른 성공 조건을 만족했는지 확인합니다.
        switch (stage_num)
        {
            case 0:
                success_1();
                break;
            case 1:
                success_2();
                break;
            case 2:
                success_3();
                break;
            case 3:
                success_4();
                break;
            case 4:
                success_5();
                break;
            case 5:
                success_6();
                break;
            case 6:
                success_7();
                break;
            case 7:
                success_8();
                
                break;
            case 8:
                success_9();
                click_more.SetActive(true);
                break;
            case 9:
                success_10();
                break;
            case 10:
                success_11();
                break;
        }

        
        dir.SetActive(false);

    }

    void error()
    {
        error_message.SetActive(true);
        error_swch = true;
    }

    // string 상수, 변수 블록이 있고 그 사이에 연산자 블록이 낄 경우, 연산의 결과를 리턴합니다.
    bool calculator_string(string front, int oper_num, GameObject endblk)
    {
        string end = "";
        switch(endblk.GetComponent<BlkOnNoteComp>().thisBlk)
        {
            case 22:
                end = endblk.transform.GetChild(0).GetComponent<Text>().text;
                break;
            case 31:
            case 32:
            case 33:
                end = var_string[endblk.GetComponent<BlkOnNoteComp>().thisBlk - 31];
                break;
            case 34:
            case 35:
            case 36:
                end = arr_string[endblk.GetComponent<BlkOnNoteComp>().thisBlk - 34, int.Parse(endblk.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)];
                break;
        }

        switch(oper_num)
        {
            case 28:
                return front == end ? true : false;
            default:
                break;  // 오류 처리
        }

        return false;
    }


    // int형 상수, 변수 블록이 있고 그 사이에 연산자 블록이 있을 경우, 연산의 결과를 리턴합니다.
    int calculator(int front, int oper_num, GameObject endblk)
    {
        int end = 0;

        switch(endblk.GetComponent<BlkOnNoteComp>().thisBlk)
        {
            case 21:
                end = int.Parse(endblk.transform.GetChild(0).GetComponent<Text>().text);
                break;
            case 31:
            case 32:
            case 33:
                end = var_int[endblk.GetComponent<BlkOnNoteComp>().thisBlk - 31];
                break;
            case 34:
            case 35:
            case 36:
                end = arr_int[endblk.GetComponent<BlkOnNoteComp>().thisBlk - 34, int.Parse(endblk.GetComponent<BlkOnNoteComp>().RightBlk_btn.transform.GetChild(0).GetComponent<Text>().text)];
                break;
        }


        switch(oper_num)
        {
            case 0:
                return front;
            case 24:
                return front + end;
            case 25:
                return front - end;
            case 26:
                return front * end;
            case 27:
                return front / end;
            case 28:
                return front == end ? 1 : 0;
            case 29:
                return front > end ? 1 : 0;
            case 30:
                return front < end ? 1 : 0;
            default:
                break;
        }

        return front;
    }

    // 성공 시 효과음 출력
    void success_sound()    
    {
        audiosource.clip = success_clip;
        audiosource.Play();
        open_success_window();
    }

    // 실패 시 효과음 출력
    void fail_sound()   
    {
        audiosource.clip = fail_clip;
        audiosource.Play();
    }

    void open_success_window()
    {

        switch(stage_num)
        {
            case 8:
                if(stage_9_isdone)
                {
                    success_window.SetActive(true);
                    iscleared = true;
                }
                    
                break;
            default:
                success_window.SetActive(true);
                iscleared = true;
                break;
        }

        
    }

    // 스테이지별로 scanf로 입력받게 될 값을 설정하고 해당 말풍선을 활성화합니다.
    void scanf_case_per_stage()
    {
        switch(stage_num)
        {
            case 4:
                scanf_speech_bubble[4].SetActive(true);
                scanf_speech_bubble[4].transform.GetChild(0).GetComponent<Text>().text = "0328";
                break;
            case 5:
                scanf_speech_bubble[5].SetActive(true);
                scanf_speech_bubble[5].transform.GetChild(0).GetComponent<Text>().text = "Perfume";
                break;
            case 8:

                // 순서대로 출력
                
                scanf_speech_bubble[8].SetActive(true);

                if(!if_tuto)
                {
                    scanf_speech_bubble[8].transform.GetChild(0).GetComponent<Text>().text = "nothing";
                    if_tuto = true;
                }
                else
                {
                    scanf_speech_bubble[8].transform.GetChild(0).GetComponent<Text>().text = "If I disappear...";
                    if_tuto = false;
                }
                        

                break;
            case 9:

                // 순서대로 출력
                int random_num = Random.Range(0, 2);
                scanf_speech_bubble[9].SetActive(true);

                switch (random_num)
                {
                    case 0:
                        scanf_speech_bubble[9].transform.GetChild(0).GetComponent<Text>().text = "You like me?";
                        break;
                    case 1:
                        scanf_speech_bubble[9].transform.GetChild(0).GetComponent<Text>().text = "You like her?";
                        break;
                }

                break;
            case 11:
                // 랜덤하게 케이스 넣어서 띄우기
                random_num = Random.Range(0, 3);
                scanf_speech_bubble[11].SetActive(true);

                switch (random_num)
                {
                    case 0:
                        scanf_speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text = "Steak";
                        break;
                    case 1:
                        scanf_speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text = "Kimchi";
                        break;
                    case 2:
                        scanf_speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text = "Soup";
                        break;
                }
                break;
        }
    }

    // 스테이지를 클리어한 경우, GameData의 최종 클리어 스테이지를 업데이트 합니다. 
    void update_cleared_stage_num()
    {
        if (PlayerPrefs.GetInt("stageNum") == stage_num) // 14번째 스테이지 이후론 16으로 set해야 함.(화살표 버튼 때문)
            PlayerPrefs.SetInt("stageNum", stage_num + 1);
        /*
        if (GameObject.Find("StageNum").GetComponent<stageNum>().ClearedStageNumber == stage_num)
            GameObject.Find("StageNum").GetComponent<stageNum>().ClearedStageNumber = stage_num + 1;
            */
    }

    void success_1()
    {
        if(stage_num == 0)
        {
            if (speech_bubble[0].activeSelf == true && speech_bubble[0].transform.GetChild(0).gameObject.GetComponent<Text>().text == "I Love U.")
            {
                speech_bubble[0].SetActive(false);
                etc_objs[0].Map[0].SetActive(true);     // 여주 말풍선 띄우기
                success_sound();

                //update_cleared_stage_num();
                /*
                if(PlayerPrefs.GetInt("stageNum") < 1) // 14번째 스테이지 이후론 16으로 set해야 함.(화살표 버튼 때문)
                    PlayerPrefs.SetInt("stageNum", 1);
                    */

                // 최고 스테이지 정보 업데이트(게임 데이터 관리)

            }
            else
            {
                fail_sound();
            }
        }
    }

    void success_2()
    {
        if (stage_num == 1)
        {
            if (speech_bubble[1].activeSelf == true && speech_bubble[1].transform.GetChild(0).gameObject.GetComponent<Text>().text == "Me too.")
            {
                success_sound();
            }
            else
            {
                fail_sound();
            }
        }
    }


    void success_3()
    {
        if (stage_num == 2)
        {
            if (speech_bubble[2].activeSelf == true && speech_bubble[2].transform.GetChild(0).gameObject.GetComponent<Text>().text == "0307")
            {
                success_sound();
            }
            else
            {
                fail_sound();
            }
        }
    }

    void success_4()
    {
        if (stage_num == 3)
        {
            if (speech_bubble[3].activeSelf == true && speech_bubble[3].transform.GetChild(0).gameObject.GetComponent<Text>().text == "308")
            {
                success_sound();
            }
            else
            {
                fail_sound();
            }
        }
    }


    void success_5()
    {
        if(stage_num == 4)
        {
            if((var_int[0] == 328 || var_int[1] == 328 || var_int[2] == 328) && speech_bubble[4].activeSelf == true && speech_bubble[4].transform.GetChild(0).gameObject.GetComponent<Text>().text == "328")
            {
                speech_bubble[4].SetActive(false);
                etc_objs[4].Map[0].SetActive(true);     // 여주 말풍선 띄우기
                success_sound();

                //update_cleared_stage_num();
            }
            else
            {
                fail_sound();
            }
        }
    }

    void success_6()
    {
        if (stage_num == 5)
        {
            if (speech_bubble[5].activeSelf == true && speech_bubble[5].transform.GetChild(0).gameObject.GetComponent<Text>().text == "Perfume")
            {
                speech_bubble[5].SetActive(false);
                etc_objs[5].Map[0].SetActive(true);     // 여주 말풍선 띄우기
                success_sound();
            }
            else
            {
                fail_sound();
            }
        }
    }

    void success_7()
    {
        if (stage_num == 6)
        {
            if (speech_bubble[6].activeSelf == true && speech_bubble[6].transform.GetChild(0).gameObject.GetComponent<Text>().text == "21")
            {
                success_sound();
            }
            else
            {
                fail_sound();
            }
        }
    }

    void success_8()
    {
        if (stage_num == 7)
        {
            if (speech_bubble[7].activeSelf == true && speech_bubble[7].transform.GetChild(0).gameObject.GetComponent<Text>().text == "1000")
            {
                success_sound();
                //update_cleared_stage_num();
            }
            else
            {
                fail_sound();
            }
        }
    }

    void success_9()
    {
        if (stage_num == 8)
        {
            bool condition1 = scanf_speech_bubble[8].transform.GetChild(0).GetComponent<Text>().text == "nothing" && speech_bubble[8].transform.GetChild(0).GetComponent<Text>().text == "";
            bool condition2 = scanf_speech_bubble[8].transform.GetChild(0).GetComponent<Text>().text == "If I disappear..." && speech_bubble[8].transform.GetChild(0).GetComponent<Text>().text == "I must find you.";


            if (condition1 || condition2)
            {
                if (condition1)
                    stage_9_isdone1 = true;
                if (condition2)
                    stage_9_isdone2 = true;
                if (stage_9_isdone1 && stage_9_isdone2)
                    stage_9_isdone = true;
                success_sound();

                //update_cleared_stage_num();
            }
            else
            {
                fail_sound();
            }
        }

    }

    void success_10()
    {
        if (stage_num == 9)
        {
            bool condition1 = scanf_speech_bubble[9].transform.GetChild(0).GetComponent<Text>().text == "You like me?" && speech_bubble[9].transform.GetChild(0).GetComponent<Text>().text == "Of course.";
            bool condition2 = scanf_speech_bubble[9].transform.GetChild(0).GetComponent<Text>().text == "You like her?" && speech_bubble[9].transform.GetChild(0).GetComponent<Text>().text == "Never.";


            if (condition1 || condition2)
            {
                etc_objs[9].Map[0].SetActive(true); // 긍정적인 반응
                success_sound();
                //update_cleared_stage_num();
            }
            else
            {
                etc_objs[9].Map[1].SetActive(true); // 부정적인 반응
                fail_sound();
            }
        }

    }

    void success_11()
    {
        if (stage_num == 10)
        {
            bool condition = true;
            for(int i = 0; i < arr_stage_6.Count; i++)
            {
                if(int.Parse(arr_stage_6[i].ToString()) != (i + 1))
                {
                    condition = false;
                    break;
                }
            }

            if (condition)
            {
                etc_objs[10].Map[0].SetActive(true);
                success_sound();
                //update_cleared_stage_num();
            }
            else
            {
                etc_objs[10].Map[1].SetActive(true);
                fail_sound();
            }
        }
    }

    void success_12()
    {
        if (stage_num == 11)
        {
            bool condition1 = scanf_speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text == "Steak" && speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text == "10000";
            bool condition2 = scanf_speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text == "Kimchi" && speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text == "2000";
            bool condition3 = scanf_speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text == "Soup" && speech_bubble[11].transform.GetChild(0).GetComponent<Text>().text == "4000";
            if (condition1 || condition2 || condition3)
            {
                etc_objs[11].Map[0].SetActive(true);
                success_sound();
                //update_cleared_stage_num();
            }
            else
            {
                etc_objs[11].Map[1].SetActive(true);
                fail_sound();
            }
        }
    }
}
