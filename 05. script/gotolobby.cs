using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

    씬 lobbyScene으로 이동합니다.

*/

public class gotolobby : MonoBehaviour
{
    private Touch tempTouchs;
    private Vector3 touchedPos;

    private bool touchOn;

    public void go_lobby()
    {
        // 다음 씬으로 stage_number 보내기.
        SceneManager.LoadScene("lobbyScene");
    }

}
