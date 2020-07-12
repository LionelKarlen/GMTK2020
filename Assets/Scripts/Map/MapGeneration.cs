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
    public RuleTile roads;
    public Tile[] obstacles;

    public int distanceModifier;
    public GameObject CameraRig;

    private bool startPoint=false;
    private bool endPoint=false;

    private Vector3 startPointVector;
    private Vector3 endPointVector;

    private Vector2[] offsets = {new Vector2(0,-1), new Vector2(-1,0), new Vector2(0,1), new Vector2(1,0)};

    // Start is called before the first frame update
    void Start() {
        for(int x=0;x<size.x;x++) {
            for(int y=0;y<size.y;y++) {
                Vector3Int tile = new Vector3Int(x,y,0);
                grassTilemap.SetTile(tile, grass);
                if(x<size.x*.25f && Random.Range(0f,1f)>.75f && !startPoint) {
                    roadTilemap.SetTile(tile, roads);
                    startPointVector=tile;
                    startPoint=true;
                    CameraRig.transform.position = roadTilemap.CellToWorld(tile)+new Vector3(3.5f,3.5f,0);
                } else if(x>size.x*.9f && Random.Range(0f,1f)>.75f && !endPoint && y>startPointVector.y+distanceModifier) {
                    roadTilemap.SetTile(tile, roads);
                    endPointVector=tile;
                    obstacleTilemap.SetTile(tile, obstacles[0]);
                    endPoint=true;
                }
                if(!endPoint) {
                    endPointVector=new Vector3(size.x,size.y,0);
                }
            }
        }
        List<Vector2> path = findRandomPath(new Vector2(startPointVector.x,startPointVector.y),new Vector2(endPointVector.x,endPointVector.y));
        foreach(Vector2 road in path) {
            Debug.Log(road);
            roadTilemap.SetTile(new Vector3Int((int)road.x,(int)road.y,0), roads);
        }
    }

    List<Vector2> findRandomPath(Vector2 start, Vector2 end) {
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
                valid = isLegalCandidate(candidate,current_position,end);
            }
            current_position=candidate;
            path.Add(current_position);
        }
        return path;
    }

    bool isLegalCandidate(Vector2 candidate, Vector2 current, Vector2 end) {
        if(candidate.x>=0&&candidate.x<this.size.x&&candidate.y>=0&&candidate.y<this.size.y) {
            if(Vector2.Distance(candidate,end)<Vector2.Distance(current,end)) {
                return true;
            }
            return false;
        }
        return false;
    }

    // Tile getTileType(Vector2 coordinate, Tilemap tilemap) {
    //     Debug.Log(tilemap.GetTile(new Vector3Int((int)coordinate.x+1,(int)coordinate.y,0)));
    //     if(tilemap.HasTile(tilemap.WorldToCell(tilemap.CellToWorld(new Vector3Int((int)coordinate.x+1,(int)coordinate.y,0))))) {
    //         if(tilemap.HasTile(new Vector3Int((int)coordinate.x,(int)coordinate.y+1,0))) {
    //             return roads[1];
    //         } else if(tilemap.HasTile(new Vector3Int((int)coordinate.x,(int)coordinate.y-1,0))) {
    //             return roads[1];
    //         }
    //         return roads[2];
    //     } else if(tilemap.HasTile(tilemap.WorldToCell(tilemap.CellToWorld(new Vector3Int((int)coordinate.x-1,(int)coordinate.y,0))))) {
    //         if(tilemap.HasTile(new Vector3Int((int)coordinate.x,(int)coordinate.y+1,0))) {
    //             return roads[1];
    //         } else if(tilemap.HasTile(new Vector3Int((int)coordinate.x,(int)coordinate.y-1,0))) {
    //             return roads[1];
    //         }
    //         return roads[2];
    //     }
    //     // } else if(tilemap.HasTile(new Vector3Int((int)coordinate.x,(int)coordinate.y+1,0))) {
            
        //     return roads[0];
        // } else if(tilemap.HasTile(new Vector3Int((int)coordinate.x,(int)coordinate.y-1,0))) {
            
        //     return roads[0];
        // }
        // return roads[0];
    // }
}
