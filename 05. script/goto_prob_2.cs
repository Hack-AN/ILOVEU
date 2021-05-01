using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

    씬을 이동하고 스테이지 넘버링을 이동한 씬에 넘겨주는 스크립트

*/

public class goto_prob_2 : MonoBehaviour
{
    public string scenename;

    public void go_prob_2()
    {
        Debug.Log("호출");
        // 다음 씬으로 stage_number 보내기.
        SceneManager.LoadScene(scenename);

    }
}
