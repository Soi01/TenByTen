using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopSetting : Pop
{
    [SerializeField] Text txtBgm, txtSfx;
    [SerializeField] Button btnConfirm;
    [SerializeField] Slider sliderBgm, sliderSfx;

    private void Awake()
    {
        this.btnConfirm.onClick.AddListener( () =>
         {
             PageManager.Instance.HidePop();
         } );

        this.sliderBgm.onValueChanged.AddListener( ( pValue ) =>
         {
             this.txtBgm.text = string.Format(StringHelper.FORMAT_NUMBER, this.sliderBgm.value * 100);
             SoundManager.Instance.SetBgmVolume( this.sliderBgm.value );
         } );
        this.sliderSfx.onValueChanged.AddListener( ( pValue ) =>
        {
            this.txtSfx.text = string.Format(StringHelper.FORMAT_NUMBER, this.sliderSfx.value * 100);
            SoundManager.Instance.SetSfxVolume( this.sliderSfx.value );
        } );

        this.sliderBgm.value = PlayerPrefsManager.GetBgmVolume();
        this.sliderSfx.value = PlayerPrefsManager.GetSfxVolume();
    }

    private void OnDisable()
    {
        if( PlayerPrefsManager.GetBgmVolume() != this.sliderBgm.value )
        {
            PlayerPrefsManager.SetBgmVolume( this.sliderBgm.value );
        }

        if(PlayerPrefsManager.GetSfxVolume() != this.sliderSfx.value )
        {
            PlayerPrefsManager.SetSfxVolume( this.sliderSfx.value );
        }
    }
}
