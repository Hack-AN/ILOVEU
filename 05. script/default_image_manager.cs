using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

    스테이지에 따라 화면에 깔릴 default image(변하지 않는 이미지)를 설정합니다.

*/
public class default_image_manager : MonoBehaviour
{
    public Image default_image;
    int stage_number;
    public Sprite[] default_images;
    public bool[] active;
    Color color;

    public Sprite[] default_images_2;
    // Start is called before the first frame update
    void Start()
    {
        stage_number = GameObject.Find("StageNum").GetComponent<stageNum>().stage_number;
        if (active[stage_number])
            default_image.sprite = default_images[stage_number];
        else
        {
            color.a = 0;
            default_image.color = color;
        }
    }

    // 경우에 따라 버튼을 누르거나 코드를 실행시킬 때 default image를 바꾸는 경우가 있습니다. 그 때 쓰는 코드입니다.
    public void change_default_image()
    {
        switch(stage_number)
        {
            case 3:
                default_image.sprite = default_images_2[stage_number];
                break;
        }
    }
}
