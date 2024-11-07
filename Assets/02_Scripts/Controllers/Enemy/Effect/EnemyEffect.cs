using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEffect : MonoBehaviour //monobihavior로 변경
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    public enum GoblemOrkEffects
    {
        LeftAttack,
        RightAttack,
        MonsterHit,
        Roar,
        Count,
    }
    private void OnEnable()
    {
        Bind<ParticleSystem>(typeof(GoblemOrkEffects));
        EffectOff();
    }
    private void Update()
    {
        
    }
    public void EffectOff(string ex = null)
    {
        ex = GetComponentInParent<SphereCollider>().gameObject.name;
        if(ex == "BossBear")
        {
            for (int i = 2; i < (int)GoblemOrkEffects.Count; i++)
            {
                if (Get<ParticleSystem>(i).gameObject != null)
                {
                    Get<ParticleSystem>(i).gameObject.SetActive(false);
                }
                else
                {
                    return;
                }
            }

        }
        else if(ex == "Slime")
        {
            for (int i = 2; i <= 2; i++)
            {
                if (Get<ParticleSystem>(i).gameObject != null)
                {
                    Get<ParticleSystem>(i).gameObject.SetActive(false);
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < (int)GoblemOrkEffects.Count; i++)
            {
                if (Get<ParticleSystem>(i) != null && Get<ParticleSystem>(i).gameObject.activeSelf)//내일 체크 렉이 너무심해서 도저히 안됨
                {
                    //Get<ParticleSystem>(i).Stop();
                    Get<ParticleSystem>(i).gameObject.SetActive(false);
                    
                }
                else
                {
                    return;
                }
            }
        }
        

    }
    public void MonsterAttack(GoblemOrkEffects name, Transform playerTransform = null)
    {
        if (Get<ParticleSystem>((int)name).gameObject.activeSelf) return;
        Get<ParticleSystem>((int)name).gameObject.SetActive(true);
        if (playerTransform != null)
        {
            Get<ParticleSystem>((int)name).gameObject.transform.position = playerTransform.position;
        }
        Get<ParticleSystem>((int)name).Play(); //너무 이펙트가 다터짐 수정 필요
    }
 
    #region Bind구현부
    // 컴퍼넌트에 연결해줄 함수 형태
    protected void Bind<T>(Type type) where T : UnityEngine.Object    // Type 쓰려면 using System;
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


    // 자주 사용하는 것들은 Get<T> 를 이용하지 않고 바로 사용할 수 있게 만들어 두자
    protected GameObject GetGameObject(int idx) { return Get<GameObject>(idx); }
    protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Toggle GetToggle(int idx) { return Get<Toggle>(idx); }
    protected RawImage GetRawImage(int idx) { return Get<RawImage>(idx); }
    #endregion
}
