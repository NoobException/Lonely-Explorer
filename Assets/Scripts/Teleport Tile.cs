using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TeleportTile : Tile {

    new public Sprite sprite;
    public int switchIndex;

    [MenuItem("Assets/Create/Teleport Tile")]
    public static void CreateTeleportTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save TP Tile", "New TP Tile", "Asset", "Save TP Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TeleportTile>(), path);
    }

    
}
