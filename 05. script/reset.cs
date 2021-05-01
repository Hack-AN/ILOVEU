using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 리플레이 버튼을 눌렀을 때 필요한 처리를 합니다.
 * 
 */

public class reset : MonoBehaviour
{

    public int num_beon;
    public int num_etc_on;
    public int num_etc_off;


    public GameObject[] obj_beon;
    public GameObject[] obj_beoff;
    public GameObject[] etc_hvtobeon;
    public GameObject[] etc_hvtobeoff;

    public AudioSource audioSource_page;
    public AudioClip bgm_page;

    public void reset_all()
    {
        audioSource_page.clip = bgm_page;
        audioSource_page.Play();

        for (int i = 0; i < num_beon; i++)
        {
            obj_beon[i].SetActive(true);
            for (int j = 0; j < 2; j++)
                obj_beoff[j * num_beon + i].SetActive(false);
        }

        for (int i = 0; i < num_etc_on; i++)
            etc_hvtobeon[i].SetActive(true);


        for (int i = 0; i < num_etc_off; i++)
            etc_hvtobeoff[i].SetActive(false);
    }
}
