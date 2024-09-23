using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    //�������� �������� ����� ������
    public int ID => _id;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public Sprite IconSprite => _iconSprite;

    //������ ��ȣ
    [SerializeField] int _id;
    //������ �̸�
    [SerializeField] string _name;
    [Multiline]
    //������ ����
    [SerializeField] string _tooltip;
    //������ ������
    [SerializeField] Sprite _iconSprite;
}
