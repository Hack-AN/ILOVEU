using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    스테이지에 맞는 BGM을 재생한다.

*/

public class bgm_manager : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip[] bgm;
    public int stage_number;

    // Start is called before the first frame update
    void Start()
    {
        stage_number = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
        audiosource.clip = bgm[stage_number];
        audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
