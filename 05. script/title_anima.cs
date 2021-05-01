using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * 현재 스테이지의 넘버에 맞는 애니메이션을 실행합니다.
 * 
 */


public class title_anima : MonoBehaviour
{

    public Image empty;
    public int stage_number;    // 현재 스테이지의 순서, 0부터 시작
    public int number;      // 스테이지의 총 개수
    public Sprite[] a;
    

    float time = 0;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        empty.sprite = a[0];
        stage_number = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;

        if (!a[3 * stage_number])
            empty.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (count > 2) count = 0;
        else if (time >= 0.3f)
        {
            empty.sprite = a[count + 3 * stage_number];
            time = 0;
            count++;
        }
    }
}
