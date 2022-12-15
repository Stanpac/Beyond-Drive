using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] Tiles;
    private int i;
    private int PreviousIndex;
    private GameObject PreviousTile;
    private Renderer previousRend;
    
    private Renderer NextRend;

    public float TerrainDistance;
    
    public float TerrainSpeed;

    Vector3 Pos;
    

    private void Start()
    {
        i = Random.Range(0, Tiles.Length);
        SpawnTile(new Vector3(0, 0, TerrainDistance));
    }

    private void Update()
    {
        float d = PreviousTile.transform.position.z + previousRend.bounds.extents.z - (TerrainSpeed * Time.deltaTime);
        Pos = new Vector3(0, 0, d);

        if (d < TerrainDistance)
        {
            SpawnTile(Pos);
        }
    }

    void SpawnTile(Vector3 Point)
    {
        Vector3 pos = Point;
        pos.z += Tiles[i].GetComponentInChildren<Renderer>().bounds.extents.z;

        PreviousTile = Instantiate(Tiles[i], pos, Quaternion.identity, transform);
        PreviousIndex = i;

        previousRend = PreviousTile.GetComponentInChildren<Renderer>();
        PreviousTile.GetComponent<Tile>().Speed = TerrainSpeed;
        PreviousTile.GetComponent<Tile>().Distance = TerrainDistance;


        if (Tiles.Length > 1)
        {
            while (i == PreviousIndex)
            {
                i = Random.Range(0, Tiles.Length);
            }
        }
        else
            i = Random.Range(0, Tiles.Length);

        NextRend = Tiles[i].GetComponentInChildren<Renderer>();
    }
    
    private void OnDrawGizmos()
    {
        if (NextRend == null) return;
                
        Vector3 pos = Pos;
        pos.z += NextRend.bounds.extents.z;
        
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(Pos, .2f);
        Gizmos.DrawWireCube(pos, NextRend.bounds.size);
    }
}
