using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : BaseScene
{
    public Slider _loadingBar;

    public TextMeshProUGUI _loadingTxt;
    public TextMeshProUGUI _skipTxt;

    private void Start()
    {
        StartCoroutine(GoNextScene(Managers.Scene._targetScene));

        if (Managers.Game._player != null) return;
        Managers.Game.PlayerCreate();
    }

    // 비동기 신
    IEnumerator GoNextScene(Define.Scene sceneType)
    {
        yield return null;

        // 지정된 씬을 비동기 형식으로 로드한다
        AsyncOperation ao = Managers.Scene.LoadSceneAsync(sceneType);

        // 준비가 완료되어도 다음 씬으로 넘어가지 않게
        // 단, 이걸 사용하면 progree는 0.9까지밖에 안됨 -> 유니티 내부 구조의 문제

        ao.allowSceneActivation = false;

        // 로딩이 완료될 때까지 반복해서 요소들을 로드하고 진행 과정을 하면에 표시한다
        while (!ao.isDone)
        {
            // 로딩 진행률을 슬라이더 바와 텍스트로 표시한다
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            _loadingBar.value = progress;
            _loadingTxt.text = (progress * 100f).ToString("F0") + "%";

            // 만일 씬 로드 진행률이 90%를 넘어가면
            if (ao.progress >= 0.9f)
            {
                _skipTxt.enabled = true;
                // 비디오가 종료되거나 엔터키를 누르면 다음씬 활성화
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ao.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    public override void Clear()
    {
        
    }
}
