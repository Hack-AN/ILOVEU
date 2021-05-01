using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    게임을 종료합니다.

*/

public class quit_game : MonoBehaviour
{

    public void Quit()
    {
        //UnityEditor.EditorApplication.isPlaying = false; // 유니티 에디터에서 작동시킬 때 켜기

        Application.Quit();
           
    }

}

