using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static void SetBgmVolume( float pVolume )
    {
        PlayerPrefs.SetFloat( "BgmVolume", pVolume );
    }

    public static float GetBgmVolume()
    {
        float volume = 1;
        if( PlayerPrefs.HasKey( "BgmVolume" ) )
            volume = PlayerPrefs.GetFloat( "BgmVolume" );
        else
            SetBgmVolume( volume );

       return volume;
    }

    public static void SetSfxVolume( float pVolume )
    {
        PlayerPrefs.SetFloat( "SfxVolume", pVolume );
    }

    public static float GetSfxVolume()
    {
        float volume = 1;
        if( PlayerPrefs.HasKey( "SfxVolume" ) )
            volume = PlayerPrefs.GetFloat( "SfxVolume" );
        else
            SetSfxVolume( volume );

        return volume;
    }
}
