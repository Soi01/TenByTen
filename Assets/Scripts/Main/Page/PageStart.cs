using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageStart : Page
{
    [SerializeField] private DataTableMaker dataTable;
    [SerializeField] Button btnPlay, btnSetting;

    private void Awake()
    {
        InitData();
        InitButtons();
    }

    private void InitData()
    {
        InfoHelper.DataTable = this.dataTable;
    }

    private void InitButtons()
    {
        this.btnPlay.onClick.AddListener(() => PageManager.Instance.OnMovePage(Helper.PageType.TEN_BY_TEN));
        this.btnSetting.onClick.AddListener(() => { PageManager.Instance.ShowPop(Helper.PopType.SETTING );  });
    }
}
