using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEditor : MonoBehaviour
{

    public Tilemap TM;
    public Tile GrassTile;
    public bool buildroad = true;
    //--------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------

    void Update()
    {
        
        Vector3 MP = Camera.main.ScreenToWorldPoint(new Vector3((Input.mousePosition.x), (Input.mousePosition.y), 0));
        Vector3Int cell = new Vector3Int(Mathf.FloorToInt(MP.x), Mathf.FloorToInt(MP.y),0);

        if (Input.GetMouseButton(0) == true && buildroad == true)
        {

            
                TM.SetTile(cell, null);
                TM.SetTile(new Vector3Int(cell.x-1,cell.y-1,0), null);
                TM.SetTile(new Vector3Int(cell.x - 1, cell.y , 0), null);
                TM.SetTile(new Vector3Int(cell.x - 1, cell.y + 1, 0), null);
                TM.SetTile(new Vector3Int(cell.x, cell.y - 1, 0), null);
                TM.SetTile(new Vector3Int(cell.x, cell.y, 0), null);
                TM.SetTile(new Vector3Int(cell.x, cell.y + 1, 0), null);
                TM.SetTile(new Vector3Int(cell.x + 1, cell.y - 1, 0), null);
                TM.SetTile(new Vector3Int(cell.x + 1, cell.y, 0), null);
                TM.SetTile(new Vector3Int(cell.x + 1, cell.y + 1, 0), null);

                
            
            
        }
        if (Input.GetMouseButton(0) == true && buildroad == false)
        {


            TM.SetTile(cell, null);
            TM.SetTile(new Vector3Int(cell.x - 1, cell.y - 1, 0), GrassTile);
            TM.SetTile(new Vector3Int(cell.x - 1, cell.y, 0), GrassTile);
            TM.SetTile(new Vector3Int(cell.x - 1, cell.y + 1, 0), GrassTile);
            TM.SetTile(new Vector3Int(cell.x, cell.y - 1, 0), GrassTile);
            TM.SetTile(new Vector3Int(cell.x, cell.y, 0), GrassTile);
            TM.SetTile(new Vector3Int(cell.x, cell.y + 1, 0), GrassTile);
            TM.SetTile(new Vector3Int(cell.x + 1, cell.y - 1, 0), GrassTile);
            TM.SetTile(new Vector3Int(cell.x + 1, cell.y, 0), GrassTile);
            TM.SetTile(new Vector3Int(cell.x + 1, cell.y + 1, 0), GrassTile);

            


        }
    }
}
