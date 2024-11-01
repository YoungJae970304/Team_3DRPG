using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용 아이템 인터페이스(포션)
public interface IUsableItem
{
    //아이템 사용 성공 여부로
    bool Use(IDamageAlbe target);
}
