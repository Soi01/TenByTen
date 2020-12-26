using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using UnityEditor;

/// <summary>
/// .csv 파일을 에셋으로 읽어옴
/// </summary>
[CreateAssetMenu(menuName = "Scriptable/DataTable")]
public class DataTableMaker : ScriptableObject
{
    [SerializeField] private List<TenByTenData> tenByTenDataList = new List<TenByTenData>();
    public List<TenByTenData> TenByTenDataList {
        get { return this.tenByTenDataList; }
    }

#if UNITY_EDITOR
    private void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        List<Dictionary<string, object>> dicList;

        dicList = CSVReader.Read("TenByTen");
        UpdateTenByTenDataList( dicList );

    }

    private void UpdateTenByTenDataList( List<Dictionary<string, object>> pDicList )
    {
        this.tenByTenDataList.Clear();

        for( int i = 0; i < pDicList.Count; ++i )
        {
            TenByTenData data = new TenByTenData();

            string[] strsCheckList = pDicList[i]["check_list"].ToString().Split( '/' );
            int row, col;
            for (int j=0; j<strsCheckList.Length; ++j)
            {
                string[] strsCheck = strsCheckList[j].Split('&');
                if( strsCheck.Length != 2)
                {
                    DebugX.Log($"Error Check List : {j} type");
                    break;
                }

                if( !int.TryParse(strsCheck[0], out col) )
                {
                    DebugX.Log($"Error Check x : {j} type");
                    break;
                }

                if (!int.TryParse(strsCheck[1], out row))
                {
                    DebugX.Log($"Error Check y : {j} type");
                    break;
                }

                data.checkList.Add(new TenByTenData.Piece(row, col));
            }

            string[] strsExamList = pDicList[i]["exam_list"].ToString().Split('/');
            for (int j = 0; j < strsExamList.Length; ++j)
            {
                string[] strsCheck = strsExamList[j].Split('&');
                if (strsCheck.Length != 2)
                {
                    DebugX.Log($"Error Exam List : {j} type");
                    break;
                }

                if (!int.TryParse(strsCheck[0], out col))
                {
                    DebugX.Log($"Error Exam x : {j} type");
                    break;
                }

                if (!int.TryParse(strsCheck[1], out row))
                {
                    DebugX.Log($"Error Exam y : {j} type");
                    break;
                }

                data.examList.Add(new TenByTenData.Piece(row, col));
            }

            string[] strsExamPivot = pDicList[i]["exam_pivot"].ToString().Split('&');
            if (!int.TryParse(strsExamPivot[0], out col))
            {
                DebugX.Log("Error Exam Pivot x");
                break;
            }

            if (!int.TryParse(strsExamPivot[1], out row))
            {
                DebugX.Log("Error Exam Pivot y");
                break;
            }

            data.pivotExam = new TenByTenData.Piece(row, col);
            this.tenByTenDataList.Add(data);
        }
    }
 
#endif
}
