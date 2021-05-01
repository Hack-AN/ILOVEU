using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

    스테이지별 상세 설명을 활성화합니다.

*/


public class QTextManage : MonoBehaviour
{

    int stage_num;
    public string[] QText;

    // Start is called before the first frame update
    void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = QText[stage_num];
    }
}
