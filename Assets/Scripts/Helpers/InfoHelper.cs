using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public enum PlaySelectType { NONE = -1, RESUME, RESTART, EXIT }

    [Serializable]
    public class TenByTenData
    {
        [Serializable]
        public class Piece
        {
            public int row, col;

            public Piece( int pRow, int pCol)
            {
                this.row = pRow;
                this.col = pCol;
            }
        }

        public Piece pivotExam;
        public List<Piece> checkList = new List<Piece>();
        public List<Piece> examList = new List<Piece>();
    }

    [Serializable]
    public class Exam
    {
        public GameObject[] arrColumn;
    }
}

public class InfoHelper : MonoBehaviour {

    public static readonly Color COLOR_WHITE_HALF = new Color( 1, 1, 1, 0.5f );
    public static readonly Vector3 VEC_HALF = new Vector3(0.5f, 0.5f, 0.5f);

    private static DataTableMaker dataTable;
    public static DataTableMaker DataTable {
        set { dataTable = value; }
        get { return dataTable; }
    }

    private static int layerMaskPlace;
    public static int LayerMaskPlace {
        get {
            layerMaskPlace = LayerMask.GetMask("CanvasPlace");
            return layerMaskPlace;
        }
    }
}