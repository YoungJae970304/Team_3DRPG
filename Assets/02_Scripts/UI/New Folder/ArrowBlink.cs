using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBlink : MonoBehaviour
{
    [SerializeField]
    float fadeTime;//���̵� �Ǵ� �ð�
    Image fadeImage;//���̵� ȿ���� ���Ǵ� Image UI

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        //Fade ȿ���� In -> out ���� �ݺ��Ѵ�
        StartCoroutine("FadeInOut");
    }

    private void OnDisable()
    {
        StopCoroutine("FadeInOut");
    }

    IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0)); //Fade In

            yield return StartCoroutine(Fade(0, 1)); //Fade Out

        }
    }

    IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;

            yield return null;
        }
    }
}
