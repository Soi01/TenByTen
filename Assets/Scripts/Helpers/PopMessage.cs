using System;
using UnityEngine;
using UnityEngine.UI;
using Helper;
using System.Text.RegularExpressions;

public class PopMessage : MonoBehaviour
{
    private static readonly int START_BOX_X = 300;
    private static readonly int END_BOX_X = 600;

    [SerializeField] private Text txtMessage;
    [SerializeField] private Button btnCancel, btnConfirm;
    [SerializeField] private RectTransform rectTransBox;
    private Vector2 vSizeDelta = new Vector2( 300, 400 );
    private Action<SelectType> callback;
    private bool isShow = false;

    private void Awake()
    {
        this.btnCancel.onClick.AddListener( Hide );
        this.btnConfirm.onClick.AddListener( OnConfirm );
    }

    public void ShowMessage( string pMessage, bool pIsShowCancel = false, Action<SelectType> pCallback = null )
    {
        if( this.isShow )
            return;
        this.isShow = true;

        this.txtMessage.gameObject.SetActive(false);
        this.txtMessage.text = pMessage;
        this.callback = pCallback;
        this.gameObject.SetActive( true );
        this.btnCancel.gameObject.SetActive( pIsShowCancel );

        this.vSizeDelta.x = START_BOX_X;
        this.rectTransBox.sizeDelta = this.vSizeDelta;

        LeanTween.value( this.gameObject, UpdateSize, START_BOX_X, END_BOX_X, 0.1f );
    }

    public void Hide()
    {
        this.gameObject.SetActive( false );
        this.isShow = false;
        if( this.callback != null )
            this.callback( SelectType.NO );
    }

    private void UpdateSize( float pX )
    {
        this.vSizeDelta.x = pX;
        this.rectTransBox.sizeDelta = this.vSizeDelta;
        this.txtMessage.gameObject.SetActive(true);
    }

    private void OnConfirm()
    {
        this.gameObject.SetActive( false );
        this.isShow = false;
        if( this.callback != null )
            this.callback( SelectType.YES );
    }
}
