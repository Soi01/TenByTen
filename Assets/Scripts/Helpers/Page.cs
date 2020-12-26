
using UnityEngine;
using Helper;
using UnityEditor;

public class Page : MonoBehaviour
{
    [SerializeField] private PageType pageType;
    [SerializeField] private int order;

    [HideInInspector] public bool isInit = false;
    [HideInInspector] public bool isBackAble = true;

    public PageType GetPageType() { return this.pageType; }
    public int GetOrder() { return this.order; }

    public virtual void InitData( params object[] args )
    {

    }

    public virtual void UpdateData( params object[] args )
    {

    }

    public virtual void OnBackPage()
    {

    }
}
