using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

    스테이지 버튼을 누르면 페이지 넘기는 소리를 내게끔 리스너를 추가합니다.

*/

public class page_chnge : MonoBehaviour
{

    public Button[] btn;
    public int num_btn;

    public AudioSource audioSource_page;
    public AudioClip bgm_page;

    // Start is called before the first frame update
    void Start()
    {
        audioSource_page.clip = bgm_page;

        for(int i = 0; i < num_btn; i++)
        {
            btn[i] = this.transform.GetComponent<Button>();
            btn[i].onClick.AddListener(fClick);
        }
            
    }

    void fClick()
    {
        audioSource_page.Play();
    }

}
