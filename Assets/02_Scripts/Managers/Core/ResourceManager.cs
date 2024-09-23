using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    //  Resources.Load<T>(path); ��� Load<T>(path)�� ����ϱ� ���� ����
    public T Load<T>(string path) where T : Object
    {
        // 1. original �̹� ��� ������ �ٷ� ���

        // ���� �������� ��� ���������� Ǯ���� �ѹ� ã�Ƽ� �װ��� �ٷ� ��ȯ
        if (typeof(T) == typeof(GameObject))    // �̷��� �������� Ȯ���� ����
        {
            // ���� �ε忡 Load<GameObject>($"Prefabs/{path}"); �̷� ������
            // ��ü ��θ� �Ѱ��־��µ� Ǯ�� �׳� �������� �̸��� ���۰� ������
            // /(name)���� �Ǿ� ������ (name)�� ����ؾ� �ϴϱ�
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
            {
                name = name.Substring(index + 1);   // �̷��� ('/') ���� ���Ͱ� ��
            }

            // (name)�� ã�Ɣf�µ� ������ �̰��� ��ȯ
            GameObject go = Managers.Pool.GetOriginal(name);
            // �ٵ� ���ٸ� ����ó�� �׳� return Resources.Load<T>(path);�� ����ǰ�
            if (go != null)
            {
                return go as T;
            }
        }
        // ���ٸ� ���� Loadó�� ���
        return Resources.Load<T>(path);
    }
    
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // ��θ� �����ؼ� ���Ŀ��� Instantiate("��ξ��� ������ �̸�")���� �ذ� ��������

        // 1. original�� ������ �ٷ� ���, ������ �Ʒ�ó�� ���
        GameObject original = Load<GameObject>($"Prefabs/{path}");  // �ǹ̻� ȥ���� �� �־ ������ ����

        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // 2. Ȥ�� Ǯ���� ������Ʈ�� ������ �װ��� ��ȯ
        if (original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent);
        int index = go.name.IndexOf("(Clone)"); // Clone ���ڿ��� ã�Ƽ� �ε����� ��ȯ
        if (index > 0 )
        {
            go.name = go.name.Substring(0, index);  // UI_Inven_Item//(Clone)
        }

        return go;
        // Object�� ������ ������ ����Ϸ��� �� �Ŷ� // ������ ���������� ������ �׳� �̷��� ���
        //return Object.Instantiate(prefab, parent);
    }

    // Object.Destroy(go);�� Destroy()�� ���� �غ����� �� �����δ� �ʿ� ����
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 3. ���࿡ Ǯ���� �ʿ��� ������Ʈ��� �ٷ� �����ϴ� ���� �ƴ϶� Ǯ�� �Ŵ������� ��Ź
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
