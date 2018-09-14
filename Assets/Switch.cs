using UnityEngine;
using UnityEngine.Tilemaps;

public class Switch : MonoBehaviour {

    public float radius;
    public Transform player;
    public bool switched;
    public bool switchOn = true; //true if switch is going to enable, false if it is going to disable tiles
    public Vector2Int[] TilesToSwitch;
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
        foreach(Vector2Int pos in TilesToSwitch)
        {
            if (switchOn)
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), tile);
            else
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
        }

    }
}
