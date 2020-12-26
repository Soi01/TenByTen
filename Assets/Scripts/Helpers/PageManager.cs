using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helper;

namespace Helper
{
    public enum PageType { START, TEN_BY_TEN }
    public enum PopType { SETTING, PAUSED }
    public enum SelectType { YES, NO }
}

public class PageManager : MonoBehaviour
{
    [SerializeField] private List<Page> pageList = new List<Page>();
    [SerializeField] private List<Pop> popList = new List<Pop>();
    [SerializeField] private PopMessage popMessage;
    [SerializeField] private GameObject objPopPrevent;

    private Stack<Page> stackPage = new Stack<Page>();
    private Dictionary<PageType, Page> dicPage = new Dictionary<PageType, Page>();
    private Dictionary<PopType, Pop> dicPop = new Dictionary<PopType, Pop>();
    private Pop nowPop = null;
    private bool isPopShow = false;

    public static PageManager Instance;
    public PopMessage PopMessage { get { return this.popMessage; } }

    private void Awake()
    {
        Instance = this;

        for( int i = 0; i < this.pageList.Count; ++i )
        {
            this.dicPage.Add( this.pageList[i].GetPageType(), this.pageList[i] );
        }

        for( int i = 0; i < this.popList.Count; ++i )
        {
            this.dicPop.Add( this.popList[i].popType, this.popList[i] );
        }

        this.stackPage.Push( this.dicPage[PageType.START] );
    }

    private void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            if (this.popMessage.gameObject.activeSelf)
            {
                this.popMessage.Hide();
                return;
            }

            if ( this.isPopShow )
            {
                HidePop();
                return;
            }

            OnBackPage();
        }
    }

    public void OnMovePage( PageType pType )
    {
        if( !this.dicPage.ContainsKey( pType ) )
            return;

        Page nowPage = this.stackPage.Peek();
        if( nowPage.GetPageType() == pType )
            return;

        Page nextPage = this.dicPage[pType];
        if( nowPage.GetOrder() < nextPage.GetOrder())
        {
            this.stackPage.Push( nextPage );
        }
        else if( nowPage.GetOrder() > nextPage.GetOrder())
        {
            this.stackPage.Pop();
        }
        else
        {
            this.stackPage.Pop();
            this.stackPage.Push( nextPage );
        }

        nextPage.gameObject.SetActive( true );
        nowPage.gameObject.SetActive( false );
    }

    public void OnBackPage()
    {
        Page nowPage = this.stackPage.Peek();
        if( nowPage.GetPageType() == PageType.START)
        {
            return;
        }

        if( !nowPage.isBackAble )
        {
            nowPage.OnBackPage();
            return;
        }

        this.stackPage.Pop();
        Page backPage = this.stackPage.Peek();

        backPage.gameObject.SetActive( true );
        nowPage.gameObject.SetActive( false );
    }

    public void ShowPop( PopType pType )
    {
        if( this.isPopShow || !this.dicPop.ContainsKey( pType ) )
            return;

        this.isPopShow = true;
        this.objPopPrevent.SetActive( true );
        this.nowPop = this.dicPop[pType];
        this.nowPop.gameObject.SetActive( true );
    }

    public void HidePop()
    {
        if( !this.isPopShow )
            return;

        Pop temp = this.nowPop;
        this.isPopShow = false;
        this.nowPop = null;
        this.objPopPrevent.SetActive( false );

        temp.gameObject.SetActive( false );
    }

    public Page GetNowPage()
    {
        return this.stackPage.Peek();
    }

    public Pop GetNowPop()
    {
        return this.nowPop;
    }
}
