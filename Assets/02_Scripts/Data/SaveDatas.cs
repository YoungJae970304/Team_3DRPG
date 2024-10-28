using System;
using System.Collections.Generic;
using System.Numerics;

[Serializable]
public class SaveDatas : IData
{
    public List<InventorySaveData> _InventorySaveDatas;
    public List<SkillSaveData> _SkillSaveDatas;
    public List<EquipmentSaveData> _EquipmentSaveDatas;
    public List<QuestClearData> _QuestClearDatas;
    public Vector3 _PlayerPosition;
    Define.PlayerType _PlayerTypes;

    int _level;
    int _exp;
    int _sp;

    public bool LoadData()
    {
        throw new NotImplementedException();
    }

    public bool SaveData()
    {
        throw new NotImplementedException();
    }

    public void SetDefaultData()
    {
        throw new NotImplementedException();
    }
}

public class PlayerPosSaveData : IData
{
    int x;
    int y;
    int z;

    public bool SaveData()
    {
        throw new NotImplementedException();
    }

    public bool LoadData()
    {
        throw new NotImplementedException();
    }

    public void SetDefaultData()
    {
        throw new NotImplementedException();
    }
}

public class InventorySaveData : IData
{
    int _id;
    int _index;
    int _amount;

    public bool SaveData()
    {
        throw new NotImplementedException();
    }

    public bool LoadData()
    {
        throw new NotImplementedException();
    }

    public void SetDefaultData()
    {
        throw new NotImplementedException();
    }
}

public class SkillSaveData : IData
{
    string _name;
    int _level;

    public bool SaveData()
    {
        throw new NotImplementedException();
    }

    public bool LoadData()
    {
        throw new NotImplementedException();
    }

    public void SetDefaultData()
    {
        throw new NotImplementedException();
    }
}

public class EquipmentSaveData : IData
{
    int _id;
    int _type;

    public bool SaveData()
    {
        throw new NotImplementedException();
    }

    public bool LoadData()
    {
        throw new NotImplementedException();
    }

    public void SetDefaultData()
    {
        throw new NotImplementedException();
    }
}

public class QuestClearData : IData
{
    string _name;
    int _amount1;
    int _amount2;

    public bool SaveData()
    {
        throw new NotImplementedException();
    }

    public bool LoadData()
    {
        throw new NotImplementedException();
    }

    public void SetDefaultData()
    {
        throw new NotImplementedException();
    }
}
