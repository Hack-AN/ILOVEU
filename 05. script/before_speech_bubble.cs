using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    스테이지 시작 전 상황 설명에 도움이 되는 말풍선을 활성화한다.     

*/

public class before_speech_bubble : MonoBehaviour
{
    int stage_num;
    public GameObject[] bubbles;

    // Start is called before the first frame update
    void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
        if (bubbles[stage_num])
            bubbles[stage_num].SetActive(true);
    }
}
