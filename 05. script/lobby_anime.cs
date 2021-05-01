using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    로비 스테이지에서 애니메이션을 주기 위한 스크립트입니다.

*/

public class lobby_anime : MonoBehaviour
{
    public UnityEngine.UI.Image fade;
    float fades = 1.0f;
    float time = 0;
    public GameObject panel;    // 판넬을 비활성화하기 위한 게임오브젝트


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(fades > 0.0f && time >= 0.1f)
        {
            fades -= 0.1f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;
        }
        else if(fades <= 0.0f)
        {
            time = 0;
            panel.SetActive(false);
            
        }
    }
}
