using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Helper;

public class PopPaused : Pop
{
    [SerializeField] Button btnResume, btnReStart, btnExit;

    private PageTenByTen pageTenByTen;
    private PlaySelectType callbackType;

    private void Awake()
    {
        InitButtons();
    }

    private void OnDisable()
    {
        if (this.callbackType == PlaySelectType.NONE)
            this.pageTenByTen.OnPausedCallback(PlaySelectType.RESUME);
    }

    private void InitButtons()
    {
        this.btnResume.onClick.AddListener(() => OnClick(PlaySelectType.RESUME));
        this.btnReStart.onClick.AddListener(() => OnClick(PlaySelectType.RESTART));
        this.btnExit.onClick.AddListener(() => OnClick(PlaySelectType.EXIT));
    }

    public override void InitData(params object[] args)
    {
        this.pageTenByTen = (PageTenByTen)args[0];
        this.callbackType = PlaySelectType.NONE;
    }

    private void OnClick( PlaySelectType pType )
    {
        PageManager.Instance.HidePop();
        this.pageTenByTen.OnPausedCallback(pType);
    }
}
