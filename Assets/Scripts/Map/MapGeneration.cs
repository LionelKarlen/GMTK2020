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
    public PrefabTile[] obstacles;

    public int barricades;
    public int buildings;

    public int distanceModifier;
    public GameObject CameraRig;
    public GameObject timerHandler;

    private bool startPoint=false;
    private bool endPoint=false;

    private Vector3 startPointVector;
    private Vector3 endPointVector;

    private Vector2[] offsets = {new Vector2(0,-1), new Vector2(-1,0), new Vector2(0,1), new Vector2(1,0)};

    // Start is called before the first frame update
    void Start() {
        generateMap();
        timerHandler.GetComponent<Timer>().start=true;
    }

    public void resetMap() {
        startPoint=false;
        endPoint=false;
        startPointVector=new Vector3();
        endPointVector=new Vector3();
        
        grassTilemap.ClearAllTiles();
        roadTilemap.ClearAllTiles();
        obstacleTilemap.ClearAllTiles();
    }

    public void generateMap() {
        resetMap();
        obstacleTilemap.SetTile(new Vector3Int(0,0,0), obstacles[2]);
        for(int x=1;x<size.x+1;x++) {
            for(int y=1;y<size.y+1;y++) {
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
            }
        }
        if(!endPoint) {
            endPointVector=new Vector3(size.x,size.y,0);
        }
        if(!startPoint) {
            startPointVector=new Vector3(0,0,0);
        }
        List<Vector2> path = findRandomPath(new Vector2(startPointVector.x,startPointVector.y),new Vector2(endPointVector.x,endPointVector.y));
        foreach(Vector2 road in path) {
            Vector3Int tile = new Vector3Int((int)road.x,(int)road.y,0);
            roadTilemap.SetTile(tile, roads);
            if(Random.Range(0f,1f)>0.75f&&barricades>0&&tile!=endPointVector&&tile!=startPointVector) {
                obstacleTilemap.SetTile(tile, obstacles[1]);
                barricades--;
            }
        }

        for(int x=0;x<=size.x+1;x++) {
            for(int y=0;y<=size.y+1;y++) {
                Vector3Int tile = new Vector3Int(x,y,0);
                if(Random.Range(0f,1f)>0.75f&&buildings>0&&!roadTilemap.HasTile(tile)) {
                    obstacleTilemap.SetTile(tile, obstacles[2]);
                    buildings--;
                }
                if(x==0 || y==0 || x==size.x+1 || y==size.y+1) {
                    obstacleTilemap.SetTile(tile, obstacles[2]);
                }
            }
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
            if(Vector2.Distance(candidate,end)<=Vector2.Distance(current,end)) {
                return true;
            }
            return false;
        }
        return false;
    }
}
