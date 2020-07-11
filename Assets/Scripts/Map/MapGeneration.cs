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
        foreach(Vector2 road in findRandomPath(new Vector2(startPointVector.x,startPointVector.y),new Vector2(endPointVector.x,endPointVector.y))) {
            roadTilemap.SetTile(new Vector3Int((int)road.x,(int)road.y,0), roads[0]);
        }
    }

    List<Vector2> findRandomPath(Vector2 start, Vector2 end) {
        Vector2[] offsets = {new Vector2(0,-1), new Vector2(-1,0), new Vector2(0,1), new Vector2(1,0)};
        int possible_moves = offsets.Length;

        Vector2 current_position = start;
        List<Vector2> path = new List<Vector2>();
        path.Add(current_position);

        while(current_position!=end) {
            bool valid = false;
            Vector2 candidate = new Vector2();
            while(!valid) {
                int move = Random.Range(0,4);
                candidate = new Vector2(current_position.x+offsets[move].x,current_position.y+offsets[move].y);
                valid = isInBounds(candidate);
            }
            current_position=candidate;
            path.Add(current_position);
        }
        return path;
    }

    bool isInBounds(Vector2 coordinate) {
        if(coordinate.x>=0&&coordinate.x<this.size.x&&coordinate.y>=0&&coordinate.y<this.size.y) {
            return true;
        }
        return false;
    }
}
