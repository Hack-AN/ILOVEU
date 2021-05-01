using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    스테이지 버튼을 바꾸는 버튼의 집합인 page1과 page2를 왔다갔다 할 수 있게 하는 버튼을 구현한 스크립트입니다,

*/
public class levelchange : MonoBehaviour
{

    public bool ispage;
    public GameObject page1;
    public GameObject page2;

    public int stage_number;
    public AudioSource audioSource_page;

    public void level_change()
    {
        audioSource_page.Play();

        if(ispage)
        {
            if(page1.activeSelf)
            {
                page2.SetActive(true);
                page1.SetActive(false);
            }
            else
            {
                page1.SetActive(true);
                page2.SetActive(false);
            }
        }
        else
        {
            GameObject.Find("StageManager").GetComponent<title_anima>().stage_number = stage_number;
            GameObject.Find("StageNum").GetComponent<stageNum>().stage_number = stage_number;
        }


    }
}
