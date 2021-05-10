using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*

    인트로 연출을 위해 작성된 스크립트입니다.

*/

[System.Serializable]
public class sprite_per_intro
{
    public Sprite[] _empty;
}

[System.Serializable]
public class intro_per_stage
{
    public sprite_per_intro[] empty;
}

public class intro_manage : MonoBehaviour
{
    public Image Empty;
    public int stage_number;    // 현재 스테이지의 순서, 0부터 시작
    public intro_per_stage[] a; // 각 a는 총 3개의 이미지가 서로 돌아가면서 애니메이션을 연출합니다.
    public AudioSource page_sound;

    public GameObject next_btn;

    bool scene_swch = true;

    float fades = 0f;

    float time = 0;     // a가 바뀌는 시간을 저장합니다.
    float _time = 0;    // 각 a의 3개의 이미지가 서로 돌아가면서 바뀌는 시간을 저장합니다. 
    int count = 0;      // a가 현재 몇 번째인지 저장합니다.
    int _count = 0;     // 각 a의 3개의 이미지가 현재 몇 번째인지 저장합니다.

    // Start is called before the first frame update
    void Start()
    {
        stage_number = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;

        // 만약 intro로 설정해둔 이미지가 없다면 바로 스테이지 씬으로 넘어갑니다.
        if (a[stage_number].empty.Length != 0)
            Empty.sprite = a[stage_number].empty[0]._empty[0];
        else
            SceneManager.LoadScene("prob_iloveu");
        
    }

    // Update is called once per frame
    void Update()
    {

        if(count >= a[stage_number].empty.Length - 1)
        {
            next_btn.SetActive(false);
        }

        if (a[stage_number].empty.Length != 0)
        {
            time += Time.deltaTime;
            _time += Time.deltaTime;

            if (time >= 3f)
            {
                if (count >= a[stage_number].empty.Length - 1)
                {
                    next_btn.SetActive(false);
                    page_sound.Play();
                    SceneManager.LoadScene("prob_iloveu");
                }
                else
                {
                    Empty.sprite = a[stage_number].empty[count]._empty[0];
                    time = 0;
                    if (count < a[stage_number].empty.Length - 1)
                        count++;
                    page_sound.Play();
                    if (count > a[stage_number].empty.Length - 1)
                        scene_swch = false;

                }
            }

            if (_count > 2) _count = 0;
            else if (_time >= 0.3f)
            {
                Empty.sprite = a[stage_number].empty[count]._empty[_count];
                _time = 0;
                _count++;
            }
        }
        
    }


    public void next_page()
    {
        count++;
        _time = 0;
    }

}
