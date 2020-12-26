using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringHelper : MonoBehaviour
{
    #region 서식
    public static readonly string FORMAT_NUMBER = "{0:n0}";
    public static readonly string FORMAT_SCORE = "{0:n0}점";
    public static readonly string FORMAT_ADD_SCORE = "+{0:n0}";
    #endregion

    public static readonly string MSG_GAME_OVER = "<b>GAME OVER</b>\nPLAY AGAIN?";
}
