using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    // public GameObject RoomLeftPrefab;
    // public GameObject RoomRightPrefab;
    // public GameObject RoomStraightPrefab;
    // public GameObject RoomTPrefab;
    public GameObject RoomPrefab;
    public GameObject StartRoomPrefab;

    public float RoomSize = 1f;
    public int RoomCount = 3;

    private Stack<Vector2Int> _branchingPositions = new();
    private List<Vector2Int> _usedPositions = new();

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        GenerateRoom(StartRoomPrefab, new(0, 0)); // спавн старта в 0, 0
        GenerateRoom(RoomPrefab, new(0, 1));
        // TODO: нужен прямой коридор

        Vector2Int currentPos = new(1,1);



        for (int i = 0; i < RoomCount; i++)
        {
            GenerateRoom(RoomPrefab, new(0, i));
        }
    }

    private void GenerateRoom(GameObject roomPrefab, Vector2Int pos)
    {
        Vector3 worldPos = new Vector3(pos.x * RoomSize, 0, pos.y * RoomSize);
        GameObject room = Instantiate(roomPrefab, worldPos, Quaternion.identity);
        room.name = roomPrefab.name + "_" + pos.x + "_" + pos.y;
        _usedPositions.Add(pos);
        // spawnedRooms.Add(room);
    }

    void Shuffle(List<Vector2Int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }
}
