using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    private readonly static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private readonly static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    private readonly static char[] TRIM_CHARS = { '\"' };
    private readonly static string REMOVE_STR = "\\";
    private readonly static string EMPTY_STR = "";

    public static List<Dictionary<string, object>> Read( string file )
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load( "DataTable/"+file ) as TextAsset;

        var lines = Regex.Split( data.text, LINE_SPLIT_RE );

        if( lines.Length <= 1 ) return list;

        var header = Regex.Split( lines[0], SPLIT_RE );
        for( var i = 1; i < lines.Length; i++ )
        {

            var values = Regex.Split( lines[i], SPLIT_RE );
            if( values.Length == 0 || values[0] == EMPTY_STR ) continue;

            var entry = new Dictionary<string, object>();
            for( var j = 0; j < header.Length && j < values.Length; j++ )
            {
                string value = values[j];
                value = value.TrimStart( TRIM_CHARS ).TrimEnd( TRIM_CHARS ).Replace( REMOVE_STR, EMPTY_STR );
                object finalvalue = value;
                int n;
                float f;
                if( int.TryParse( value, out n ) )
                {
                    finalvalue = n;
                }
                else if( float.TryParse( value, out f ) )
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add( entry );
        }
        return list;
    }
}