using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager
{
    //AudioSource를 BGM, 이펙트용으로 만들기 위해
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    
    // 볼륨 변수
    private float _bgmVolume = 1.0f; // BGM 볼륨
    private float _effectVolume = 1.0f; // 효과음 볼륨

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        // 사운드는 사운즈폴더에 있어야 하는데 혹시 경로를 까먹거나 생략했다면 자동으로 추가
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
            // 만약 기존에 패스가 없다면 Resource.Load로 가져와서 딕셔너리에 추가
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

        // 없었으면 가져와서 리턴, 있었으면 있던거 리턴
        return audioClip;
    }

    // 경로를 통해 찾는 경우
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        switch (type)
        {
            case Define.Sound.Effect:
                pitch = _effectVolume;
                break;
            case Define.Sound.Bgm:
                pitch = _bgmVolume;
                break;
        }
        /*  ////////////////// 빌드업 ////////////////
        // 사운드는 사운즈폴더에 있어야 하는데 혹시 경로를 까먹거나 생략했다면 자동으로 추가
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

            /* 이미 구현된 부분이니까 대체 가능
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            // 만약 이미 실행중인 BGM이 있다면 정지
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

    // AudioClip을 직접 입력하는 경우
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            return;
        }
        switch (type)
        {
            case Define.Sound.Effect:
                pitch = _effectVolume;
                break;
            case Define.Sound.Bgm:
                pitch = _bgmVolume;
                break;
        }
        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            // 만약 이미 실행중인 BGM이 있다면 정지
            if (audioSource.isPlaying) { audioSource.Stop(); }

            //audioSource.pitch = pitch;
            audioSource.volume = pitch;
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            //audioSource.pitch = pitch;
            audioSource.volume = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void RandSoundsPlay(string path1, string path2, float volume = 1)
    {
        int randVal = Random.Range(1, 3);
        volume = _effectVolume;
        if (randVal == 1)
        {
            Managers.Sound.Play(path1, Define.Sound.Effect, volume);
        }
        else
        {
            Managers.Sound.Play(path2, Define.Sound.Effect, volume);
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

    // BGM 볼륨 설정
    public void SetBgmVolume(float volume)
    {
        _bgmVolume = Mathf.Clamp01(volume); // 0.0과 1.0 사이로 클램프
        _audioSources[(int)Define.Sound.Bgm].volume = _bgmVolume; // BGM AudioSource 볼륨 설정
    }

    // 효과음 볼륨 설정
    public void SetEffectVolume(float volume)
    {
        _effectVolume = Mathf.Clamp01(volume); // 0.0과 1.0 사이로 클램프
        foreach (AudioSource audioSource in _audioSources)
        {
            if (audioSource != _audioSources[(int)Define.Sound.Bgm]) // BGM을 제외한 모든 AudioSource에 대해
            {
                audioSource.volume = _effectVolume; // 효과음 AudioSource 볼륨 설정
            }
        }
    }

    // BGM과 효과음의 현재 볼륨을 반환하는 메서드 추가
    public float GetBgmVolume()
    {
        return _bgmVolume;
    }

    public float GetEffectVolume()
    {
        return _effectVolume;
    }
}
