using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class DebugX {

    [Conditional("ENABLE_LOG")]
    public static void Log( object pObj )
    {
        Debug.Log( pObj );
    }

    [Conditional( "ENABLE_LOG" )]
    public static void LogWarning( object pObj )
    {
        Debug.LogWarning( pObj );
    }

    [Conditional( "ENABLE_LOG" )]
    public static void LogError( object pObj )
    {
        Debug.LogError( pObj );
    }

    [Conditional( "ENABLE_LOG" )]
    public static void LogException( Exception pObj )
    {
        Debug.LogException( pObj );
    }
}
