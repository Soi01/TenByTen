using UnityEngine;

public class Vector2X {
    /// <summary>
    /// (0, 0)
    /// </summary>
    public static Vector2 zero = Vector2.zero;
    /// <summary>
    /// (0, 1)
    /// </summary>
    public static Vector2 up = Vector2.up;
    /// <summary>
    /// (0, -1)
    /// </summary>
    public static Vector2 down = Vector2.down;
    /// <summary>
    /// (-1, 0)
    /// </summary>
    public static Vector2 left = Vector2.left;
    /// <summary>
    /// (1, 0)
    /// </summary>
    public static Vector2 right = Vector2.right;
    /// <summary>
    /// (1, 1)
    /// </summary>
    public static Vector2 one = Vector2.one;

    public static Vector2 SetX( float pX ) { return right * pX; }
    public static Vector2 SetY( float pY ) { return up * pY; }
}

public class Vector3X
{
    /// <summary>
    /// (0, 0, 0)
    /// </summary>
    public static Vector3 zero = Vector3.zero;
    /// <summary>
    /// (0, 1, 0)
    /// </summary>
    public static Vector3 up = Vector3.up;
    /// <summary>
    /// (0, -1, 0)
    /// </summary>
    public static Vector3 down = Vector3.down;
    /// <summary>
    /// (-1, 0, 0)
    /// </summary>
    public static Vector3 left = Vector3.left;
    /// <summary>
    /// (1, 0, 0)
    /// </summary>
    public static Vector3 right = Vector3.right;
    /// <summary>
    /// (1, 1, 1)
    /// </summary>
    public static Vector3 one = Vector3.one;
    /// <summary>
    /// (0, 0, 1)
    /// </summary>
    public static Vector3 forward = Vector3.forward;
    /// <summary>
    /// (0, 0, -1)
    /// </summary>
    public static Vector3 back = Vector3.back;

    public static Vector3 SetX( float pX ) { return right * pX; }
    public static Vector2 SetY( float pY ) { return up * pY; }
    public static Vector2 SetZ( float pY ) { return forward * pY; }
}

public class ColorX
{
    public static Color white = Color.white;
    public static Color white70 = new Color( 1, 1, 1, 0.7f );
}
