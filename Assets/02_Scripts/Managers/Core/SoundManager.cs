using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager
{
    //AudioSource�� BGM, ����Ʈ������ ����� ����
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        // ����� ����������� �־�� �ϴµ� Ȥ�� ��θ� ��԰ų� �����ߴٸ� �ڵ����� �߰�
        if (!path.Contains("Sounds/"))
        {
            path = $"Sounds/{path}";
        }

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            // ���� ������ �н��� ���ٸ� Resource.Load�� �����ͼ� ��ųʸ��� �߰�
            if (!_audioClips.TryGetValue(path, out audioClip))
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing {path}");
        }

        // �������� �����ͼ� ����, �־����� �ִ��� ����
        return audioClip;
    }

    // ��θ� ���� ã�� ���
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        /*  ////////////////// ����� ////////////////
        // ����� ����������� �־�� �ϴµ� Ȥ�� ��θ� ��԰ų� �����ߴٸ� �ڵ����� �߰�
        if (!path.Contains("Sounds/"))
        {
            path = $"Sounds/{path}";
        }

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing {path}");
                return;
            }

            /* �̹� ������ �κ��̴ϱ� ��ü ����
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            // ���� �̹� �������� BGM�� �ִٸ� ����
            if (audioSource.isPlaying) { audioSource.Stop(); }

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
            
        }
        else
        {
            //AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing {path}");
                return;
            }

            /*
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
            
        }
        */
        AudioClip audioClip = GetOrAddAudioClip(path, type);

        Play(audioClip, type, pitch);
    }

    // AudioClip�� ���� �Է��ϴ� ���
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            return;
        }

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            // ���� �̹� �������� BGM�� �ִٸ� ����
            if (audioSource.isPlaying) { audioSource.Stop(); }

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length-1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear();
    }
}
