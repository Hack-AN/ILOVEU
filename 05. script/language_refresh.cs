using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    리플레이 버튼을 눌렀을 때 코딩 블록을 모두 삭제하고 말풍선 등을 모두 초기 상태로 리셋합니다.

*/

[System.Serializable]
public class Map2D
{
    public GameObject[] Map;
}

public class language_refresh : MonoBehaviour
{
    public GameObject start_point;
    public GameObject change_image;
    public Map2D[] off_resources;
    int stage_num;
    public GameObject another_image_manager;
    public GameObject before_speech;

    // Start is called before the first frame update
    void Start()
    {
        stage_num = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
    }

    public void refresh()
    {
        change_image.SetActive(true);
        before_speech.SetActive(true);
        another_image_manager.GetComponent<default_image_manager>().change_default_image();
        for (int i = 0; i < off_resources[stage_num].Map.Length; i++)
            off_resources[stage_num].Map[i].SetActive(false);

        // 코딩 블록을 모두 삭제합니다.
        Destroy(start_point.GetComponent<BlkOnNoteComp>().DownBlk_btn);
        start_point.GetComponent<BlkOnNoteComp>().DownBlk = 0;
        start_point.GetComponent<BlkOnNoteComp>().DownBlk_btn = null;
    }

}
