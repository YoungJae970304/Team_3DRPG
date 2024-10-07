using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBtnController : MonoBehaviour
{
    public Define.Scene _targetscene;

    public void OnClickBtn()
    {
        Managers.Scene._targetScene = _targetscene;

        Managers.Scene.LoadScene(Define.Scene.Loading);
    }
}
