using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour {

    public Vector2 size;

    public Tilemap grassTilemap;
    public Tilemap roadTilemap;
    public Tilemap obstacleTilemap;

    public Tile grass;
    public Tile[] roads;
    public Tile[] obstacles;

    public int minCurves;
    public GameObject CameraRig;

    private bool startPoint=false;
    private bool endPoint=false;

    private Vector3 startPointVector;
    private Vector3 endPointVector;

    // Start is called before the first frame update
    void Start() {
        for(int x=0;x<size.x;x++) {
            for(int y=0;y<size.y;y++) {
                Vector3Int tile = new Vector3Int(x,y,0);
                grassTilemap.SetTile(tile, grass);
                if(x<size.x*.25f && Random.Range(0f,1f)>.75f && !startPoint) {
                    roadTilemap.SetTile(tile, roads[0]);
                    startPointVector=tile;
                    startPoint=true;
                    CameraRig.transform.position = roadTilemap.CellToWorld(tile)+new Vector3(3.5f,3.5f,0);
                } else if(x>size.x*.75f && Random.Range(0f,1f)>.75f && !endPoint) {
                    roadTilemap.SetTile(tile, roads[0]);
                    endPointVector=tile;
                    endPoint=true;
                }
            }
        }
        foreach(Node road in findPathUsingCurves(startPointVector,endPointVector,minCurves)) {
            roadTilemap.SetTile(road.coordinates, road.tile);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    List<Node> findPathUsingCurves(Vector3 start, Vector3 end, int minCurves) {
        float x=start.x;
        List<Node> path=new List<Node>();
        while(x<end.x) {
            path.Add(new Node(new Vector3Int((int)x,(int)start.y,0), roads[2]));  
            x++;
        }
        path.Add(new Node(new Vector3Int((int)x,(int)start.y,0), roads[1]));
        float y = start.y;
        if(end.y>start.y) {
            while(y<end.y) {
                y++;
                path.Add(new Node(new Vector3Int((int)x,(int)y,0),roads[0]));
            }
        } else if(end.y<start.y){
            while(y>end.y) {
                y--;
                path.Add(new Node(new Vector3Int((int)x,(int)y,0),roads[0]));
            }
        }
        return path;
          
    }
}
