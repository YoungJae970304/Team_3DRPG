using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserData
{
    //기본 값으로 데이터를 초기화 시켜줌
    void SetDefaultData();
    //데이터 로드
    bool LoadData();
    //데이터 세이브
    bool SaveData();
}
