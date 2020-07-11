using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node {

    public Vector3Int coordinates;
    public Tile tile;

    public Node(Vector3Int coordinates, Tile tile) {
        this.coordinates=coordinates;
        this.tile=tile;
    }
}
