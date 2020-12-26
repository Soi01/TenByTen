using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helper;

public class PageTenByTen : Page
{
    public delegate bool IsAblePutDownCallback( int pCellIdx, int pPivotNumber);

    private static readonly int SIZE_PIECE  = 10;
    private static readonly int SIZE_EXAM = 3;
    private static readonly float TIEM_SHORT_ANIM = 0.3f;
    private static readonly float TIEM_MIDDLE_ANIM = 0.5f;

    [SerializeField] private Transform transParentPiece;
    [SerializeField] private Text txtScore, txtAddScore;
    [SerializeField] private GameObject objCellPiece;
    [SerializeField] private Button btnPause;
    [SerializeField] private CellExam[] arrExam;

    private Image[,] arrPiece;
    private HashSet<int> hashSetPiece, hashSetCheckRow, hashSetCheckCol;
    private List<int> fullListRow, fullListCol;
    private TenByTenData nowData;
    private int[] arrExamIdx;
    private int nowRemainExam, nowPivotRow, nowPivotCol;
    private int nowSocre, addScore;
    
    private void Awake()
    {
        this.arrPiece = new Image[SIZE_PIECE, SIZE_PIECE];
        this.hashSetPiece = new HashSet<int>();
        this.hashSetCheckRow = new HashSet<int>();
        this.hashSetCheckCol = new HashSet<int>();
        this.fullListRow = new List<int>();
        this.fullListCol = new List<int>();
        this.arrExamIdx = new int[SIZE_EXAM];

        InitButtons();
        InitPiece();
        InitExam();
    }

    private void OnDisable()
    {
        SoundManager.Instance.StopBgm();
        ResetPiece();
    }

    private void OnEnable()
    {
        this.isBackAble = false;
        SoundManager.Instance.PlayBgm();

        this.nowSocre = 0;
        this.addScore = 0;
        this.txtScore.text = string.Format(StringHelper.FORMAT_NUMBER, this.nowSocre);
        UpdateExam();
    }

    public override void OnBackPage()
    {
        OnGamePaused();
    }

    private void InitButtons()
    {
        this.btnPause.onClick.AddListener(OnGamePaused);
    }

    private void InitPiece()
    {
        bool isSuccess = true;
        int idx = 0;
        for (int i = 0; i < SIZE_PIECE ; ++i)
        {
            for (int j = 0; j < SIZE_PIECE ; ++j)
            {
                GameObject objTemp = Instantiate(this.objCellPiece, this.transParentPiece, false);
                if( objTemp == null)
                {
                    isSuccess = false;
                    break;
                }
                objTemp.name = idx.ToString();

                Image imgTemp = objTemp.GetComponent<Image>();
                if (imgTemp == null)
                {
                    isSuccess = false;
                    break;
                }
                this.arrPiece[i, j] = imgTemp;

                this.hashSetPiece.Add(idx);
                idx += 1;
            }

            if( !isSuccess )
            {
                DebugX.Log("Fail Init Piece");
                break;
            }
        }
    }

    private void InitExam()
    {
        for (int i = 0; i < SIZE_EXAM; ++i)
        {
            this.arrExam[i].SetCallback( i, IsAblePutDown, OnPutDown);
        }
    }

    private void ResetPiece()
    {
        for (int i = 0; i < SIZE_PIECE; ++i)
        {
            for (int j = 0; j < SIZE_PIECE; ++j)
            {
                OnDisusePiece(i, j);
            }
        }
    }

    private void UpdateExam()
    {
        this.nowRemainExam = SIZE_EXAM;

        for (int i = 0; i < SIZE_EXAM; ++i)
        {
            this.arrExamIdx[i] = Random.Range(0, InfoHelper.DataTable.TenByTenDataList.Count);
            this.arrExam[i].SetData(InfoHelper.DataTable.TenByTenDataList[this.arrExamIdx[i]]);
        }
    }

    private void UpdateScore()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SfxType.SCORE);

        this.nowSocre += this.addScore;
        this.txtScore.text = string.Format(StringHelper.FORMAT_NUMBER, this.nowSocre);

        this.txtAddScore.text = string.Format(StringHelper.FORMAT_ADD_SCORE, this.addScore);
        this.addScore = 0;
        LeanTween.colorText(this.txtAddScore.rectTransform, Color.yellow, TIEM_MIDDLE_ANIM).setLoopPingPong(1);
    }

    private void OnPutDown()
    {
        this.nowRemainExam -= 1;
        if (this.nowRemainExam <= 0)
            UpdateExam();

        this.hashSetCheckRow.Clear();
        this.hashSetCheckCol.Clear();
        this.fullListRow.Clear();
        this.fullListCol.Clear();

        int row, col;
        for (int i = 0; i < this.nowData.checkList.Count; ++i)
        {
            row = this.nowData.checkList[i].row + this.nowPivotRow;
            col = this.nowData.checkList[i].col + this.nowPivotCol;

            OnUsePiece(row, col);

            if (!this.hashSetCheckRow.Contains(row))
                this.hashSetCheckRow.Add(row);
            if (!this.hashSetCheckCol.Contains(col))
                this.hashSetCheckCol.Add(col);
        }

        bool isFull;
        foreach (int i in this.hashSetCheckRow)
        {
            isFull = true;
            for (int j = 0; j < SIZE_PIECE; ++j)
            {
                if (IsUseAblePiece(i, j))
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                this.fullListRow.Add(i);
            }
        }

        foreach (int i in this.hashSetCheckCol)
        {
            isFull = true;
            for (int j = 0; j < SIZE_PIECE; ++j)
            {
                if (IsUseAblePiece(j, i))
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                this.fullListCol.Add(i);
            }
        }

        for (int i = 0; i < this.fullListRow.Count; ++i)
        {
            OnFullPieceRow(this.fullListRow[i], 0);
        }

        for (int i = 0; i < this.fullListCol.Count; ++i)
        {
            OnFullPieceCol(this.fullListCol[i], 0);
        }

        if (this.fullListRow.Count > 0 || this.fullListCol.Count > 0)
        {
            LeanTween.delayedCall(TIEM_MIDDLE_ANIM, UpdateScore);
        }

        SoundManager.Instance.PlaySfx(SoundManager.SfxType.PUT);

        if (IsGameOver())
            OnGameOver();
    }

    private void OnFullPieceRow( int pRow, int pIdx )
    {
        OnDisusePiece( pRow, pIdx);

        if (pIdx + 1 < SIZE_PIECE)
            LeanTween.delayedCall(TIEM_SHORT_ANIM/ SIZE_PIECE, () => OnFullPieceRow(pRow, pIdx + 1));
    }

    private void OnFullPieceCol( int pCol, int pIdx )
    {
        OnDisusePiece( pIdx, pCol);

        if (pIdx + 1 < SIZE_PIECE)
            LeanTween.delayedCall(TIEM_SHORT_ANIM/ SIZE_PIECE, () => OnFullPieceCol(pCol, pIdx + 1));
    }

    private void OnUsePiece(int pRow, int pCol)
    {
        if(!IsUseAblePiece(pRow, pCol))
        {
            return;
        }

        this.arrPiece[pRow, pCol].color = Color.white;

        int cellIdx = pRow * SIZE_EXAM + pCol;
        if (this.hashSetPiece.Contains(cellIdx))
            this.hashSetPiece.Remove(cellIdx);
    }

    private void OnDisusePiece(int pRow, int pCol)
    {
        if (IsUseAblePiece(pRow, pCol))
        {
            return;
        }

        this.addScore += 1;
        this.arrPiece[pRow, pCol].color = InfoHelper.COLOR_WHITE_HALF;

        int cellIdx = pRow * SIZE_EXAM + pCol;
        if (!this.hashSetPiece.Contains(cellIdx))
            this.hashSetPiece.Add(cellIdx);
    }

    private void OnGameOver()
    {
        SoundManager.Instance.PauseBgm();
        PageManager.Instance.PopMessage.ShowMessage(StringHelper.MSG_GAME_OVER, true, (pSelect) =>
        {
            if (pSelect == SelectType.YES)
                OnPausedCallback(PlaySelectType.RESTART);
            else
                OnPausedCallback(PlaySelectType.EXIT);
        });
    }

    private void OnGamePaused()
    {
        SoundManager.Instance.PauseBgm();

        PageManager.Instance.ShowPop(PopType.PAUSED);
        PageManager.Instance.GetNowPop().InitData(this);
    }

    public void OnPausedCallback( PlaySelectType pType )
    {
        switch( pType)
        {
            case PlaySelectType.RESUME:
                SoundManager.Instance.UnPauseBgm();
                break;
            case PlaySelectType.RESTART:
                ResetPiece();
                OnEnable();
                break;
            case PlaySelectType.EXIT:
            default:
                base.isBackAble = true;
                PageManager.Instance.OnBackPage();
                break;
        }
    }

    private bool IsAblePutDown( int pCellIdx, int pPivotNumber)
    {
        this.nowPivotRow = pPivotNumber / SIZE_PIECE;
        this.nowPivotCol = pPivotNumber % SIZE_PIECE;

        return IsAblePutDown(pCellIdx);
    }

    private bool IsAblePutDown(int pCellIdx)
    {
        this.nowData = InfoHelper.DataTable.TenByTenDataList[this.arrExamIdx[pCellIdx]];
        for (int i = 0; i < this.nowData.checkList.Count; ++i)
        {
            if (!IsUseAblePiece(this.nowData.checkList[i].row + this.nowPivotRow, this.nowData.checkList[i].col + this.nowPivotCol))
                return false;
        }

        return true;
    }

    private bool IsUseAblePiece( int pRow, int pCol )
    {
        return pRow >=0 && pCol >=0 && pRow < SIZE_PIECE && pCol < SIZE_PIECE && this.arrPiece[pRow, pCol].color.a != 1;
    }

    private bool IsGameOver()
    {
        bool isGameOver = true;
        for (int i=0; i<SIZE_EXAM; ++i)
        {
            if (!this.arrExam[i].gameObject.activeSelf)
                continue;

            foreach (int j in this.hashSetPiece)
            {
                this.nowPivotRow = j / SIZE_PIECE;
                this.nowPivotCol = j % SIZE_PIECE;

                if (IsAblePutDown(i))
                {
                    isGameOver = false;
                    break;
                }
            }

            if(!isGameOver)
            {
                return false;
            }
        }

        return true;
    }
}
