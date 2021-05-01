using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*

    세이브되어 관리될 게임 데이터를 저장하는 스크립트입니다.

*/

// 여기에 저장할 게임 데이터를 넣어주자. 내 게임에선 : 클리어한 스테이지 넘버, 하트 개수, (스테이지 별 배열 정보)
[Serializable]
public class GameData   
{
    public int ClearedStageNumber;
    public int hearts;
}

