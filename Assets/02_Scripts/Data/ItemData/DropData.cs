using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropData
{
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] int _dropType1;
    [SerializeField] int _startValue1;
    [SerializeField] int _endValue1;
    [SerializeField] int _dropType2;
    [SerializeField] int _startValue2;
    [SerializeField] int _endValue2;
    [SerializeField] int _dropType3;
    [SerializeField] int _startValue3;
    [SerializeField] int _endValue3;
    [SerializeField] int _dropType4;
    [SerializeField] int _startValue4;
    [SerializeField] int _endValue4;
    [SerializeField] int _itemType5;
    [SerializeField] int _value5;
    [SerializeField] int _itemType6;
    [SerializeField] int _value6;
    
    
    
    public int ID { get { return _id; } set { _id = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int DropType1 { get { return _dropType1; } set { _dropType1 = value; } }
    public int StartValue1 { get { return _startValue1; } set { _startValue1 = value; } }
    public int EndValue1 { get { return _endValue1; } set { _endValue1 = value; } }
    public int DropType2 { get { return _dropType2; } set { _dropType2 = value; } }
    public int StartValue2 { get { return _startValue2; } set { _startValue2 = value; } }
    public int EndValue2 { get { return _endValue2; } set { _endValue2 = value; } }
    public int DropType3 { get { return _dropType3; } set { _dropType3 = value; } }
    public int StartValue3 { get { return _startValue3; } set { _startValue3 = value; } }
    public int EndValue3 { get { return _endValue3; } set { _endValue3 = value; } }
    public int DropType4 { get { return _dropType4; } set { _dropType4 = value; } }
    public int StartValue4 { get { return _startValue4; } set { _startValue4 = value; } }
    public int EndValue4 { get { return _endValue4; } set { _endValue4 = value; } }
    public int ItemValue5 { get { return _itemType5; } set { _itemType5 = value; } }
    public int Value5 { get { return _value5; } set { _value5 = value; } }
    public int ItemValue6 { get { return _itemType6; } set { _itemType6 = value; } }
    public int Value6 { get { return _value6; } set { _value6 = value; } }
}
