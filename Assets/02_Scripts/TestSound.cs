using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;

    private void Start()
    {
        Managers.Sound.Play(clip1, Define.Sound.Bgm);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Managers.Sound.Play("Sounds/univ0001");
        Managers.Sound.Play(clip2);
    }
}
