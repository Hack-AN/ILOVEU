using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

/*

    이 스크립트는 Language_script, BlkOnNoteComp 코드와 함께 해당 게임의 메인 기능을 담당하는 코드입니다.
    플레이어가 드래그 앤 드롭을 통해 코딩 노트 위로 옮길 블록이 가질 기능과 특성에 대해 구현한 스크립트입니다.

*/

public class MoveBtn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 location;
    Vector3 location_offset = new Vector3(0, 10, 0);
    public Transform changeempty;
    public int btn_numbering;
    public GameObject btn_prefab;   // 대응되는 btnonNote 넣어놓기
    bool isrightcol;
    bool isdowncol;

    void Start()
    {
        isrightcol = false;
        isdowncol = false;
    }

    void SwapObjHierarchy(Transform sour, Transform dest)
    {
        Transform sourParent = sour.parent;
        Transform destParent = dest.parent;

        int sourindex = sour.GetSiblingIndex();
        int destindex = dest.GetSiblingIndex();

        sour.SetParent(destParent);
        sour.SetSiblingIndex(destindex);

        dest.SetParent(sourParent);
        dest.SetSiblingIndex(sourindex);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        location = transform.position;
        SwapObjHierarchy(changeempty, transform);
    }

    public void OnDrag(PointerEventData eventData)  // 블록 위치 이동
    {
        transform.position = eventData.position;    // 위치를 조금 변경해서 손가락이 블록을 가리지 않게 하자
        if (this.gameObject.name.CompareTo("btn_const_string") == 0)
        {
            //this.gameObject.GetComponent<RectTransform>().pivot = new Vector2(1f, -0.5f);
            //this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-this.gameObject.GetComponent<RectTransform>().rect.width / 2, this.gameObject.GetComponent<RectTransform>().rect.height/2);
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(200, 60);
        }
        else
        {
            //this.gameObject.GetComponent<RectTransform>().pivot = new Vector2(1f, -0.5f);
            //this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-this.gameObject.GetComponent<RectTransform>().rect.width / 2, this.gameObject.GetComponent<RectTransform>().rect.height / 2);
        }
    }

    public void OnEndDrag(PointerEventData eventData)   // 블록 원상 복귀
    {
        //this.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        SwapObjHierarchy(changeempty, transform);
        transform.position = location;
        if (this.gameObject.name.CompareTo("btn_const_string") == 0)
        {
            //this.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            //this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0,0);
            this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(64.77f, 68.61f);
        }
    }

    
    

    void OnTriggerStay2D(Collider2D other)  // 반투명 블록 이미지 붙여놓기
    {
        //this.gameObject.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
        if (other.CompareTag("BlkColNum"))
        {
            other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().isdrop = false;


            if (isdowncol == false && other.gameObject.name == "RightCol" && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().RightBlk == 0)    // 접촉 중인 블록이 이미 다른 블록과 합친 상태인지 검사. 
            {
                if (btn_numbering >= 21) // 여기에 놔도 되는 블록인지 조건 검사 필요
                {
                    // 반투명 이미지 띄우기
                    other.gameObject.transform.parent.transform.Find("DownCol").gameObject.SetActive(false);
                    isrightcol = true;

                    /*
                    other.gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
                    other.gameObject.GetComponent<Image>().SetNativeSize();
                    float x = other.gameObject.transform.parent.transform.position.x + other.gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2 + this.gameObject.transform.GetComponent<RectTransform>().rect.width / 2; ;
                    other.gameObject.transform.position = new Vector3(x, other.transform.position.y, other.transform.position.z);

                    */
                    other.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().rect.width * 2, this.gameObject.GetComponent<RectTransform>().rect.height * 2);


                    
                    
                    other.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
                    other.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();
                    float x = other.gameObject.transform.parent.transform.position.x + other.gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2 + this.gameObject.transform.GetComponent<RectTransform>().rect.width / 2;
                    other.transform.GetChild(0).transform.position = new Vector3(x, other.transform.position.y, other.transform.position.z);
                    
                    Debug.Log("right");
                    Debug.Log(btn_numbering);


                }
            }
            else if (isrightcol == false && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().ismostleft == true && other.gameObject.name == "DownCol" && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().DownBlk == 0)    // 접촉 중인 블록이 이미 다른 블록과 합친 상태인지 검사. 
            {
                // 제일 왼쪽의 블록인지 추가 검사 필요
                
                if (!(btn_numbering >= 21 && btn_numbering <= 30) && btn_numbering >= 1)   // 여기에 놔도 되는 블록인지 조건 검사 필요
                {
                    // 반투명 이미지 띄우기
                    if(other.gameObject.transform.parent.name.CompareTo("start point 1") != 0)
                        other.gameObject.transform.parent.transform.Find("RightCol").gameObject.SetActive(false);
                    isdowncol = true;

                    /*
                    other.gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
                    other.GetComponent<Image>().SetNativeSize();

                    float x = other.gameObject.transform.parent.transform.position.x + (this.gameObject.GetComponent<RectTransform>().rect.width / 2 - other.gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2);
                    float y = other.gameObject.transform.parent.transform.position.y - other.gameObject.transform.parent.GetComponent<RectTransform>().rect.height;

                    other.gameObject.transform.position = new Vector3(x, y, other.transform.position.z);
                    */
                    other.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().rect.width * 3 , this.gameObject.GetComponent<RectTransform>().rect.height * 3);



                    
                    other.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = this.gameObject.GetComponent<Image>().sprite;
                    other.transform.GetChild(0).gameObject.GetComponent<Image>().SetNativeSize();

                    float x = other.gameObject.transform.parent.transform.position.x + (this.gameObject.GetComponent<RectTransform>().rect.width / 2 - other.gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2);
                    float y = other.gameObject.transform.parent.transform.position.y - other.gameObject.transform.parent.GetComponent<RectTransform>().rect.height;

                    other.transform.GetChild(0).transform.position = new Vector3(x, y, other.transform.position.z);
                    
                    Debug.Log("down");
                    Debug.Log(btn_numbering);
                    
                    
                }

            }
            
        }
    }

    void OnTriggerExit2D(Collider2D other)  // 반투명 블록 이미지 제거
    {
        if (other.CompareTag("BlkColNum"))
        {
            Debug.Log("exit");

            //isdowncol = false;
            //isrightcol = false;

            /*
            other.GetComponent<Image>().sprite = null;
            other.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            */

            /*
            if(other.gameObject.name == "RightCol" && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().isdragging == false)
                other.transform.position = new Vector3(other.gameObject.transform.parent.transform.position.x + other.gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2, other.gameObject.transform.parent.transform.position.y, other.gameObject.transform.parent.transform.position.z);
            else if(other.gameObject.transform.parent.name != "start point 1" && other.gameObject.name == "DownCol" && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().isdragging == false)
                other.transform.position = new Vector3(other.gameObject.transform.parent.transform.position.x, other.gameObject.transform.parent.transform.position.y - other.gameObject.transform.parent.GetComponent<RectTransform>().rect.height / 2, other.gameObject.transform.parent.transform.position.z);
                */

            other.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0,0);

            other.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
            other.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            other.transform.GetChild(0).transform.position = new Vector3(other.gameObject.transform.parent.transform.position.x, other.gameObject.transform.parent.transform.position.y, other.gameObject.transform.parent.transform.position.z);
            
            //Color temp_color = other.gameObject.GetComponent<Image>().color;
            //temp_color.a = 0;
            /*if (other.gameObject.name == "RightCol" && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().isdrop == false)
            {

                //other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().RightBlk = 0;
            }
            */
            if (isdowncol == false && other.gameObject.name == "RightCol" && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().isdrop == true)
            {
                other.transform.position = new Vector3(other.gameObject.transform.parent.transform.position.x + other.gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2, other.gameObject.transform.parent.transform.position.y, other.gameObject.transform.parent.transform.position.z);

                if (btn_numbering >= 21)
                {
                    
                    other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().RightBlk = btn_numbering;

                    // btn_onNote 인스턴스화 하여 자식으로 하고, child의 rightCol 혹은 downCol 의 특정 위치에 붙이기(크기를 갖고 계산하기)

                    float x = other.gameObject.transform.parent.transform.position.x + other.gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2 + this.gameObject.transform.GetComponent<RectTransform>().rect.width / 2;
                    Vector3 offset = new Vector3(x, other.transform.position.y, other.transform.position.z);  // 블록 크기에 맞춰 offset 계산하기
                    GameObject btn_onNote_instance = Instantiate(btn_prefab);
                    other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().RightBlk_btn = btn_onNote_instance;
                    btn_onNote_instance.GetComponent<BlkOnNoteComp>().thisBlk = btn_numbering;

                    if (btn_numbering == 22 || btn_numbering == 21 || (btn_numbering >= 31 && btn_numbering <= 36))
                        btn_onNote_instance.transform.GetChild(0).gameObject.GetComponent<Text>().text = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;

                    btn_onNote_instance.transform.parent = other.gameObject.transform.parent.transform;
                    btn_onNote_instance.GetComponent<BlkOnNoteComp>().ismostleft = false;
                    btn_onNote_instance.transform.position = offset;
                    btn_onNote_instance.transform.localScale = new Vector3(1, 1, 1);

                    
                }
                other.gameObject.transform.parent.transform.Find("DownCol").gameObject.SetActive(true);

                return;
                
            }
            /*else if (other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().ismostleft == true && other.gameObject.name == "DownCol" && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().isdrop == false)
            {


                //other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().DownBlk = 0;
            }
            */
            else if (other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().ismostleft == true && isrightcol == false && other.gameObject.name == "DownCol" && other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().isdrop == true)
            {
                other.transform.position = new Vector3(other.gameObject.transform.parent.transform.position.x, other.gameObject.transform.parent.transform.position.y - other.gameObject.transform.parent.GetComponent<RectTransform>().rect.height / 2, other.gameObject.transform.parent.transform.position.z);
                
                if (!(btn_numbering >= 21 && btn_numbering <= 30) && btn_numbering >= 1)   // 여기에 놔도 되는 블록인지 조건 검사 필요
                {
                    
                    other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().DownBlk = btn_numbering;

                    float x = other.gameObject.transform.parent.transform.position.x + (this.gameObject.GetComponent<RectTransform>().rect.width / 2 - other.gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2);
                    float y = other.gameObject.transform.parent.transform.position.y - other.gameObject.transform.parent.GetComponent<RectTransform>().rect.height;
                    Vector3 offset = new Vector3(x, y, other.transform.position.z); // 블록 크기에 맞춰 offset 계산하기
                    GameObject btn_onNote_instance = Instantiate(btn_prefab);
                    btn_onNote_instance.GetComponent<BlkOnNoteComp>().thisBlk = btn_numbering;
                    if ((btn_numbering >= 31 && btn_numbering <= 36))
                        btn_onNote_instance.transform.GetChild(0).gameObject.GetComponent<Text>().text = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;

                    other.gameObject.transform.parent.GetComponent<BlkOnNoteComp>().DownBlk_btn = btn_onNote_instance;
                    btn_onNote_instance.transform.parent = other.gameObject.transform.parent.transform;
                    btn_onNote_instance.transform.position = offset;
                    btn_onNote_instance.transform.localScale = new Vector3(1, 1, 1);


                }

                if (other.gameObject.transform.parent.name.CompareTo("start point 1") != 0)
                    other.gameObject.transform.parent.transform.Find("RightCol").gameObject.SetActive(true);
                return;

            }
            isrightcol = false;
            isdowncol = false;

            other.gameObject.transform.parent.transform.Find("DownCol").gameObject.SetActive(true);
            if (other.gameObject.transform.parent.name.CompareTo("start point 1") != 0)
                other.gameObject.transform.parent.transform.Find("RightCol").gameObject.SetActive(true);

        }
        
    }


}
