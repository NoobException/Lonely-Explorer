using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct RectToSwitch
{
    //Structure that stores a rect to switch and info if it should be switched on or off;
    public Vector2Int UpperLeft;
    public Vector2Int LowerRight;
    public bool switchOn;
}
[System.Serializable]
public struct TileToSwitch
{
    public Vector2Int pos;
    public bool switchOn;
}
[System.Serializable]
public struct ObjectToSwitch
{
    public GameObject obj;
    public bool switchOn;
}

public class Switch : MonoBehaviour {

    public float radius;
    public Transform player;
    public bool switched;

    public RectToSwitch[] TilesToSwitch;
    public TileToSwitch[] OtherTilesToSwitch; //Othe tiles to switch
    public ObjectToSwitch[] OtherObjectsToSwitch; //GameObjects that are not tiles, but should be switched
    public Tilemap tilemap;
    public Tile tile;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, player.position) < radius)
            SelfSwitch();
   	}

    void SelfSwitch()
    {
        if (switched)
            return;

        switched = true;
        foreach(RectToSwitch rect in TilesToSwitch)
        {
            Debug.Log(rect.UpperLeft);
            bool on = rect.switchOn;
            for(int x = rect.UpperLeft.x; x <= rect.LowerRight.x; x++)
            {
                for (int y = rect.UpperLeft.y; y >= rect.LowerRight.y; y--)
                {
                    if (on)
                        tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                    else
                        tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
        foreach(TileToSwitch tileToSwitch in OtherTilesToSwitch)
        {
            if (tileToSwitch.switchOn)
                tilemap.SetTile(new Vector3Int(tileToSwitch.pos.x, tileToSwitch.pos.y, 0), tile);
            else
                tilemap.SetTile(new Vector3Int(tileToSwitch.pos.x, tileToSwitch.pos.y, 0),null);
        }
        foreach(ObjectToSwitch obj in OtherObjectsToSwitch)
        {
            obj.obj.SetActive(obj.switchOn);
        }

    }
}
