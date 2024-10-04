using System;
using System.Collections.Generic;
using UnityEngine;
public class DropDataListWrapper
{
    public List<DropData> DropDataList { get; set; } = new List<DropData>();
}
public class DropManager
{
    //경로 설정
    const string DATA_PATH = "CSVData";
    //저장용 키
    const string _MONSTER_PREFS_KEY = "ItemDataList";
    //데이터 테이블 CSV파일
    const string MONSTER_DROP_DATA_TABLE = "Monster_Drop_Data_Table";

    public List<DropData> _MonsterDropData = new List<DropData>();
    public void Init()
    {
        LoadItemDataTable();
    }

    
    void DropDataTable(string dataPath, string monsterDropTable)
    {
        var parsedDropdataTable = CSVReader.Read($"{dataPath}/{monsterDropTable}");
        foreach(var data in parsedDropdataTable)
        {
            DropData itemData = null;
            itemData = new DropData
            {
                //아이디
                ID = Convert.ToInt32(data["ID"]),
                //이름
                Name = data["Name"].ToString(),
                //아이템 타입1 - 이지
                DropType1 = Convert.ToInt32(data["DropType1"]),
                //시작 값1 - 이지
                StartValue1 = Convert.ToInt32(data["StartVaule1"]),
                //종료 값1 - 이지
                EndValue1 = Convert.ToInt32(data["EndVaule1"]),
                //아이템 타입2 - 노말
                DropType2 = Convert.ToInt32(data["DropType2"]),
                //시작 값2 - 노말
                StartValue2 = Convert.ToInt32(data["StartVaule2"]),
                //종료 값2 - 노말
                EndValue2 = Convert.ToInt32(data["EndVaule2"]),
                //아이템 타입3 - 하드
                DropType3 = Convert.ToInt32(data["DropType3"]),
                //시작 값3 - 하드
                StartValue3 = Convert.ToInt32(data["StartVaule3"]),
                //종료 값3 - 하드
                EndValue3 = Convert.ToInt32(data["EndVaule3"]),
                //아이템 타입4 - 골드
                DropType4 = Convert.ToInt32(data["DropType4"]),
                //시작 값4 - 골드
                StartValue4 = Convert.ToInt32(data["StartVaule4"]),
                //종료 값4 - 골드
                EndValue4 = Convert.ToInt32(data["EndVaule4"]),
                //경험치
                ItemValue5 = Convert.ToInt32(data["ItemType5"]),
                //경험치 값
                Value5 = Convert.ToInt32(data["Vaule5"]),
                //기타템
                ItemValue6 = Convert.ToInt32(data["ItemType6"]),
                //기타템 종류
                Value6 = Convert.ToInt32(data["Vaule6"]),

            };
            if (itemData != null)
            {
                Logger.Log($"{itemData} 저장됨");
                _MonsterDropData.Add(itemData);

            }
        }
    }
    public void LoadItemDataTable()
    {
        DropDataTable(DATA_PATH, MONSTER_DROP_DATA_TABLE);
    }
    //모든 데이터 플레이어프랩스로 제이슨저장
    public void SaveAllItemData()
    {
        DropDataListWrapper savedData = new DropDataListWrapper { DropDataList = _MonsterDropData };
        //합친 데이터를 Json으로 변환
        string itemJson = JsonUtility.ToJson(savedData);
        //Json데이터를 플레이어프랩스에 저장
        PlayerPrefs.SetString(_MONSTER_PREFS_KEY, itemJson);
        PlayerPrefs.Save();
        Logger.Log("저장 완료 : " + itemJson);
    }
    //모든 데이터를 플레이어프랩스로 제이슨 로드
    public void LoadAllItemData()
    {
        //저장된 제이슨 문자열 가져오기
        string itemDataJson = PlayerPrefs.GetString(_MONSTER_PREFS_KEY);
        if (!string.IsNullOrEmpty(itemDataJson))
        {
            //Json을 다시 객체로 변환시킴
            DropDataListWrapper loadedData = JsonUtility.FromJson<DropDataListWrapper>(itemDataJson);
            //기존 데이터 비우기
            _MonsterDropData.Clear();
            //타입에 맞춰 데이터를 다시 리스트에 추가
            foreach (var item in loadedData.DropDataList)
            {
                _MonsterDropData.Add(item);
            }
        }
        else
        {
            Logger.LogError("저장된 데이터가 없음");
        }
    }
}
