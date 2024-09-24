using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    //�������� �������� ����� ������
    public enum ItemType
    {
        Equipment,//0, 1, 2
        Potion,//3
        Booty,//4
    }

    public int ID => _id;
    public string Name => _name;
    public Sprite IconSprite => _iconSprite;

    [SerializeField] ItemType _itemType;
    //������ ��ȣ(type)
    [SerializeField] int _id;
    //������ �̸�
    [SerializeField] string _name;
    //������ ������
    [SerializeField] Sprite _iconSprite;
}
