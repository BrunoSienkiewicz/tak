using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] AvailableRooms;
    public int gridX;
    public int gridZ;
    public int retryCount = 5;
    [SerializeField]
    public int MapSizeX = 500;
    [SerializeField]
    public int MapSizeZ = 500;
    [SerializeField]
    public int RoomQuantity = 5;

    public Vector3 gridOrigin = Vector3.zero;
    List<Vector2> CentersList = new List<Vector2>();
    Triangulator triangulator;

    void Start()
    {
        SpawnGrid();
        CreateMesh();
    }

    public void SpawnGrid()
    {
        for(int i=0;i<RoomQuantity;i++)
        {
            int objBefore = GameObject.FindGameObjectsWithTag("Map").Length;
            int objAfter = objBefore;
            int randomizedX = Random.Range(0, MapSizeX);
            int randomizedZ = Random.Range(0, MapSizeZ);
            Vector3 SpawnPosition = new Vector3(randomizedX, 0, randomizedZ) + gridOrigin;
            for (int j = 0; j < retryCount; j++)
            {
                if (objAfter == objBefore)
                    PickAndSpawn(SpawnPosition, Quaternion.identity);
                else
                    break;
                randomizedX = Random.Range(0, MapSizeX);
                randomizedZ = Random.Range(0, MapSizeZ);
                SpawnPosition = new Vector3(randomizedX, 0, randomizedZ) + gridOrigin;
                objAfter = GameObject.FindGameObjectsWithTag("Map").Length;
            }
            CentersList.Add(new Vector2(randomizedX,randomizedZ));
        }
    }

    public void PickAndSpawn(Vector3 PositionToSpawn, Quaternion rotationToSpawn)
    {
        int RandomIndex = Random.Range(0, AvailableRooms.Length);
        //GameObject clone = Instantiate(AvailableRooms[RandomIndex], PositionToSpawn, rotationToSpawn);
        GameObject clone = Instantiate(AvailableRooms[RandomIndex], PositionToSpawn, rotationToSpawn);
    }

    public void CreateMesh()
    {
        triangulator = new Triangulator();
        GameObject obj = triangulator.CreateInfluencePolygon(CentersList.ToArray());
        obj.transform.position = gridOrigin;
    }
}
