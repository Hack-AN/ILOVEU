using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

/*

    코딩 노트 위에 올라간 블록이 가질 모든 특성과 동작을 구현한 코드이다.
    MoveBtn, Language_script와 함께 게임의 메인 기능을 담당하는 코드이다.

    각 코드 별로 기능을 설명해두었다.

*/
public class BlkOnNoteComp : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int thisBlk;    // 이 블록의 넘버링을 저장한다.
    public int RightBlk;   // 이 블록의 오른쪽에 붙어있는 코딩 블록의 넘버링을 저장한다.
    public int DownBlk;    // 이 블록의 아래쪽에 붙어있는 코딩 블록의 넘버링을 저장한다.

    public GameObject RightBlk_btn;  // 이 블록의 오른쪽에 붙어있는 코딩 블록 오브젝트를 저장한다.
    public GameObject DownBlk_btn;   // 이 블록의 아래쪽에 붙어있는 코딩 블록 오브젝트를 저장한다.

    public bool isdrop = false;     // 이 블록의 위(이 블록과 겹친다는 의미)에 다른 블록이 drop 되었는지 여부를 저장한다.
    public bool ismostleft = true;  // 이 블록이 제일 맨 왼쪽, 즉 1번째 세로줄에 있는 블록인지 여부를 저장한다.

    public bool isdragging = false; // 이 블록이 현재 drag되고 있는지 저장한다.

    Vector3 location;              // 임의의 위치를 저장하기 위한 벡터 변수이다.
    public Transform changeempty;  // 오브젝트 changeempty의 위치를 저장하는 변수이다.
    
    
    /* 노트위의 블록들의 이동 시 자잘한 처리는 데모 시연 이후에 구현하자. 시연 전까진 노트 위에 블록 올리기까지만! */

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        // 상위에 연결된 블록에서 rightBlk, downBlk 변수 수정해주고 부모 벗어나기
        if(ismostleft && this.transform.parent.GetComponent<BlkOnNoteComp>().ismostleft)
        {
            this.transform.parent.GetComponent<BlkOnNoteComp>().DownBlk = 0;
            this.transform.parent.GetComponent<BlkOnNoteComp>().DownBlk_btn = null;
        }
        else if (!ismostleft && this.transform.parent.GetComponent<BlkOnNoteComp>().ismostleft)
        {
            this.transform.parent.GetComponent<BlkOnNoteComp>().RightBlk = 0;
            this.transform.parent.GetComponent<BlkOnNoteComp>().RightBlk_btn = null;
        }
        else if (!ismostleft && !this.transform.parent.GetComponent<BlkOnNoteComp>().ismostleft)
        {
            this.transform.parent.GetComponent<BlkOnNoteComp>().RightBlk = 0;
            this.transform.parent.GetComponent<BlkOnNoteComp>().RightBlk_btn = null;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        isdragging = true;
        if (changeempty != null)
        {
            transform.position = eventData.position;    
            // TODO: 손가락에 가리지 않게 offset을 더해주기
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isdragging = false;
        // 만약 어디 붙은 게 아니라면 블록 삭제, 뒤에 연결된 블록도 같이 삭제
        this.gameObject.transform.parent = null;
        if (!gameObject.transform.parent)
            Destroy(gameObject);
           
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop");
        isdrop = true;
       
    }

}
