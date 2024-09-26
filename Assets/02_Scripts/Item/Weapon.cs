using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //실제 장비에 붙여줄 스크립트를 작성
    //아이템을 장착하면 플레이어 스텟에+ 시켜주는 함수 작성

    //붙여줄 스크립터블 오브젝트
    //장비의 등급에 따라 공격력 증가량 상이
    public EquipmentItem _equipmentItem;

    public PlayerStat _playerStat;

}
