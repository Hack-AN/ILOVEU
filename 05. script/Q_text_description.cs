using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

    스테이지별 상세 설명을 설정하는 스크립트입니다.

*/

[System.Serializable]
public class Qtext_btn
{
    public GameObject[] objs;
}

public class Q_text_description : MonoBehaviour
{
    public Qtext_btn[] btns;
    public GameObject description_note;
    int stage_num;

    private void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
    }

    public void On_description_note()
    {
        for (int i = 0; i < btns[stage_num].objs.Length; i++)
            btns[stage_num].objs[i].SetActive(true);
        switch (stage_num)
        {
            case 0:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "\"I love U.\" 문장을 말해보자.\n" +
                                                                                    "코딩이 끝나면 ▶ 버튼을 눌러서\n" +
                                                                                    "코드를 실행해보자.\n";
                break;
            case 1:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "\"Me too.\" 문장을 변수      에 담은 뒤\n" +
                                                                                    "이를 그대로 말해보자.\n" +
                                                                                    "코딩이 끝나면 ▶ 버튼을 눌러서\n" +
                                                                                    "코드를 실행해보자.\n";
                break;
            case 2:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "오늘 날짜인 0307   을 말해보자.\n" +
                                                                                    "코딩이 끝나면 ▶ 버튼을 눌러서\n" +
                                                                                    "코드를 실행해보자.\n";
                break;
            case 3:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "내일 날짜인 \"0308\"을 변수      에\n" +
                                                                                    "담은 뒤 이를 그대로 말해보자.\n" +
                                                                                    "코딩이 끝나면 ▶ 버튼을 눌러서\n" +
                                                                                    "코드를 실행해보자.\n";
                break;
            case 4:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "여자친구의 생일을 듣고 변수      에 담아\n" +
                                                                                    "그대로 생일을 말해보자.\n";
                break;
            case 5:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "여자친구가 원하는 선물을 듣고 변수      에 담아\n" +
                                                                                    "그대로 말해보자.\n";
                break;
            case 6:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "오늘(0307)부터 여친 생일(0328)까지 며칠\n" +
                                                                                    "남았는지 계산한 값을 변수      에" +
                                                                                    "담고 말해보자.\n";
                break;
            case 7:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "생일선물을 사기 위해 하루에 얼만큼\n" +
                                                                                    "돈을 벌어야 하는지 구하여\n" +
                                                                                    "변수      에 담고 말해보자.";
                break;
            case 8:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "여자친구가 할 말이 있는 듯 하다.\n" +
                                                                                    "\"If I disappear...\"라고 말하면\n" +
                                                                                    "\"I must find you.\"라고 말하자.";
                break;
            case 9:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "여자친구가 질투하는 듯 하다. 그녀가\n" +
                                                                                    "\"You like me?\"라고 물어보면\n" +
                                                                                    "\"Of course.\"라고,\n" +
                                                                                    "\"You like her?\"라고 물어보면\n" +
                                                                                    "\"Never.\"라고 말하자.";
                break;

            case 10:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "여자친구가 잠들기 전에 1부터 10까지\n" +
                                                                                    "세 달라고 한다. 수를 1씩 늘려가면서\n" +
                                                                                    "말해보자.";
                break;
            case 11:
                description_note.transform.GetChild(0).GetComponent<Text>().text = "선물을 사기 위해 알바를 하는 중이다.\n" +
                                                                                    "손님께서 말하는 음식 이름을 듣고\n" +
                                                                                    "그 이름에 맞는 가격을 말해보자.";
                break;
            
        }

        if(description_note.activeSelf == false)
            description_note.SetActive(true);
        else
            description_note.SetActive(false);
    }
}
