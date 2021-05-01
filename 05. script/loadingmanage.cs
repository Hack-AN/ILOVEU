using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingmanage : MonoBehaviour
{

    private void Awake()
    {
        Application.backgroundLoadingPriority = UnityEngine.ThreadPriority.High;
    }

}
