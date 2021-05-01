using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

    씬을 이동하고 스테이지 넘버링을 이동한 씬에 넘겨주는 스크립트

*/
public class goto_prob : MonoBehaviour
{
    public string scenename;
   
    public UnityEngine.UI.Image fade;


    public GameObject StageManagerObj;

    public void go_prob()
    {
        // 다음 씬으로 stage_number 보내기.
        SceneManager.LoadScene(scenename);
        DontDestroyOnLoad(StageManagerObj);
    }

}
