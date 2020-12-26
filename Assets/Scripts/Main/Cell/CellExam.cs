using System;
using UnityEngine;
using Helper;

public class CellExam : MonoBehaviour
{
    [SerializeField] private RectTransform rectTrans;
    [SerializeField] private Exam[] arrRow;

    private PageTenByTen.IsAblePutDownCallback isAblePutDownCallback;
    private Action onPutDownCallback;
    private TenByTenData data;
    private Vector3 vecTemp = Vector3X.zero;
    private Collider2D coll;
    private int cellIdx;

    private void OnMouseDown()
    {
        this.rectTrans.localScale = Vector3X.one;
    }

    private void OnMouseDrag()
    {
        this.vecTemp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.vecTemp.z = 0;

        this.rectTrans.position = this.vecTemp;
    }

    private void OnMouseUp()
    {
        this.coll = Physics2D.OverlapPoint(this.arrRow[this.data.pivotExam.row].arrColumn[this.data.pivotExam.col].transform.position, InfoHelper.LayerMaskPlace);
        bool isSuccess = this.coll != null;

        if( isSuccess )
        {
            int idxColl;
            isSuccess = int.TryParse(this.coll.name, out idxColl);
            if( isSuccess )
            {
                isSuccess = this.isAblePutDownCallback( this.cellIdx, idxColl);
            }
        }

        if (isSuccess )
        {
            this.gameObject.SetActive(false);
            this.onPutDownCallback();
        }
        else{
            OnResetExamLocation();
        }
    }

    public void SetCallback( int pIdx, PageTenByTen.IsAblePutDownCallback pIsAblePutDownCallback, Action pOnPutDownCallback )
    {
        this.cellIdx = pIdx;
        this.isAblePutDownCallback = pIsAblePutDownCallback;
        this.onPutDownCallback = pOnPutDownCallback;
    }

    public void SetData( TenByTenData pData )
    {
        this.data = pData;

        OnResetExamContent();

        int row, col;
        for (int i = 0; i < pData.examList.Count; ++i)
        {
            row = pData.examList[i].row;
            col = pData.examList[i].col;
            if (IsUseExam(row, col))
                this.arrRow[row].arrColumn[col].SetActive(true);
        }

        OnResetExamLocation();
    }

    private void OnResetExamLocation()
    {
        this.rectTrans.localPosition = Vector3X.zero;
        this.rectTrans.localScale = InfoHelper.VEC_HALF;
        this.gameObject.SetActive(true);
    }

    private void OnResetExamContent()
    {
        for (int i = 0; i < this.arrRow.Length; ++i)
        {
            for(int j=0; j<this.arrRow[i].arrColumn.Length; ++j)
            {
                if( this.arrRow[i].arrColumn[j] != null)
                {
                    this.arrRow[i].arrColumn[j].SetActive(false);
                }
            }
        }
    }

    private bool IsUseExam( int pRow, int pCol )
    {
        return pRow >= 0 && pCol >= 0 && pRow < this.arrRow.Length && pCol < this.arrRow[pRow].arrColumn.Length
            && this.arrRow[pRow].arrColumn[pCol] != null;
    }
}
