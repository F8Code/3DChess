using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChessBoardConstructor))]
public class ChessBoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChessBoardConstructor myScript = (ChessBoardConstructor)target;
        if (GUILayout.Button("Update Chess Board"))
        {
            myScript.UpdateChessBoard();
        }
    }
}
