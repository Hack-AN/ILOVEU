using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_next_stage : MonoBehaviour
{

    public void next_stage()
    {
        GameObject.Find("StageNum").GetComponent<stageNum>().stage_number++;
        SceneManager.LoadScene(3);
    }
}
