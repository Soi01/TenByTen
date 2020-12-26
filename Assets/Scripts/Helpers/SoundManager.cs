using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SfxType { PUT, SCORE }

    [SerializeField] AudioSource audioBgm, audioSfx;
    [SerializeField] AudioClip[] clips;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;

        SetBgmVolume( PlayerPrefsManager.GetBgmVolume() );
        SetSfxVolume( PlayerPrefsManager.GetSfxVolume() );
    }

    public void PlayBgm()
    {
        if( this.audioBgm.volume == 0 )
            return;

        this.audioBgm.Play();
    }

    public void StopBgm()
    {
        if (this.audioBgm.volume == 0)
            return;

        this.audioBgm.Stop();
    }

    public void PauseBgm()
    {
        if (this.audioBgm.volume == 0)
            return;

        this.audioBgm.Pause();
    }

    public void UnPauseBgm()
    {
        if (this.audioBgm.volume == 0)
            return;

        this.audioBgm.UnPause();
    }

    public void PlaySfx( SfxType pType )
    {
        if( this.audioSfx.volume == 0 ) 
            return;

        this.audioSfx.clip = this.clips[GetSfxTypeIdx(pType)];
        this.audioSfx.Play();
    }

    public void SetBgmVolume( float pVolume )
    {
        this.audioBgm.volume = pVolume;
    }

    public void SetSfxVolume( float pVolume )
    {
        this.audioSfx.volume = pVolume;
    }

    private int GetSfxTypeIdx(SfxType pType)
    {
        switch( pType)
        {
            case SfxType.PUT:
                return 0;
            case SfxType.SCORE:
                return 1;
            default:
                return 0;
        }
    }
}
