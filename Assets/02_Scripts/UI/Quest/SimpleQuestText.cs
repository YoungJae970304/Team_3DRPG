using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class SimpleQuestText : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
    public int _questTextID;
    enum QuestText
    {
        SimpleQuestText,
        QuestRequireText,
    }
    private void Awake()
    {
        _questTextID = Managers.QuestManager._questTextID;
    }
    public void Init(Transform anchor)
    {
        Bind<TextMeshProUGUI>(typeof(QuestText));
        TextChange(_questTextID);
    }
    public void TextChange(int i)
    {
        GetText((int)QuestText.SimpleQuestText).text = $"{Managers.QuestManager._targetName[i]}";
        GetText((int)QuestText.QuestRequireText).text = $"{Managers.QuestManager._countCheck[i]} / {Managers.QuestManager._completeChecks[i]}";
    }
    void Bind<T>(Type type) where T : UnityEngine.Object    // Type 쓰려면 using System;
    {
        if (_objects.ContainsKey(typeof(T)))//이미 바인딩 되어있으면 리턴
            return;
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            // 게임 오브젝트용 전용 버전을 하나 더 만들어줌
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            // 잘 찾아주고 있는지 테스트
            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind({names[i]})");
            }
        }
    }
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)  // 값이 없으면 그냥 리턴
            return null;

        return objects[idx] as T;   // 오브젝츠에다가 인덱스 번호를 추출한 다음에 T로 캐스팅 해줌
    }
}
