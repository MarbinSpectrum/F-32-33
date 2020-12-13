using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class SoundModule_Base : SerializedMonoBehaviour
{
    protected AudioSource module_audsource;

    [Title("RandomPitch")]
    public bool RandomPitch;
    [Range(-3f,3f)]
    public float MaxPitch = 1.2f;
    [Range(-3f, 3f)]
    public float MinPitch = 0.8f;

    [Title("RandomVolume")]
    public bool RandomVolume;
    [Range(0f, 1f)]
    public float MaxVol = 1f;
    [Range(0f, 1f)]
    public float MinVol;

    private void Awake()
    {
        module_audsource = GetComponent<AudioSource>();
    }

    public virtual void Play(string base_soundKey)
    {
        module_audsource = GetComponent<AudioSource>();

        if (RandomPitch)
        module_audsource.pitch = Random.Range(MinPitch, MaxPitch);

        if(RandomVolume)
        module_audsource.volume = Random.Range(MinVol, MaxVol);
    }

    public virtual void Stop()
    {
        module_audsource.Stop();
    }

    public void Fadeout(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Cor_Fadeout(time));
    }

    IEnumerator Cor_Fadeout(float time)
    {
        var t = time;
        var initVol = module_audsource.volume;
        while (t >= 0f)
        {
            yield return new WaitForFixedUpdate();
            module_audsource.volume = Mathf.Lerp(0f, initVol, t / time);
            t -= Time.fixedDeltaTime;
        }
        module_audsource.Stop();
        module_audsource.volume = initVol;
    }
}
