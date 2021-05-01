using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 터치를 통해 원하는 오브젝트를 활성화/비활성화하는 기능입니다.
 * 
 */

public class touch_then_active : MonoBehaviour
{

    public GameObject active_one;
    public AudioSource audioSource_page;
    public AudioClip bgm_page;


    public void touch_active()
    {
        audioSource_page.clip = bgm_page;
        audioSource_page.Play();

        if (active_one.activeSelf == false)
        {
            active_one.SetActive(true);
            gameObject.SetActive(false);
        }
        else if(active_one.activeSelf == true)
        {
            active_one.SetActive(false);
            gameObject.SetActive(true);
        }

    }

}
