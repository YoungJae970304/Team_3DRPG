using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTableManager
{
    //DataTable폴더 안에 있는 csv값을 스트링으로 가져오고 csv파일로 읽어올거임
    const string DATA_PATH = "DataTable";

    #region 아이템 데이터 로드
    //데이터 테이블 폴더 안에 있는 장비아이템 데이터 csv파일
    const string EQUIPMENT_ITEM_DATA_TABLE = "Equipment_Item_Data_Table";
    //데이터 테이블 폴더 안에 있는 포션아이템 데이터 csv파일
    const string POTION_ITEM_DATA_TABLE = "";
    //데잍터 테이블 폴더 안에 있는 기타아이템 데이터 csv파일
    const string BOOTY_ITEM_DATA_TABLE = "";

    //실질적인 아이템 데이터의 리스트
    List<ItemData> _itemDataTable = new List<ItemData>();
    void LoadItemDataTable()
    {
        //csv파일 읽어오기

        var parsedDataTable = CSVReader.Read($"{DATA_PATH}/{EQUIPMENT_ITEM_DATA_TABLE}");

        foreach (var data in parsedDataTable)
        {
            ItemData itemdata;
            //아이템 데이터 안에있는 적잘한 서브클래스인스턴스를 생성
            //장비 데이터cvs파일을 불러와서 저장해주기
            string itemType = data["EquipmentItem"].ToString();
            if(itemType == "EquipmentItem")
            {
                itemdata = new WeaponItemData()
                {
                   //_TEMP
                   //ID = Convert.ToInt32(data["item_id"]),
                   
                };
            }
        }
        //불러와서 저장해준 장비cvs데이터를 리스트에 넣어서 저장해주기


    }

    #endregion
}
