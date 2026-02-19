using UnityEngine;
using System.Collections.Generic;

public class FloorGenerator : MonoBehaviour
{
    [Header("Префабы комнат")]
    public GameObject roomStartPrefab;
    public GameObject roomCombatPrefab;
    public GameObject roomRewardPrefab;      // наградная
    public GameObject roomShopPrefab;        // магазин

    [Header("Настройки")]
    public int minRooms = 8;
    public int maxRooms = 12;
    public float roomSize = 16f;

    private List<Vector2Int> usedPositions = new List<Vector2Int>();
    private List<GameObject> spawnedRooms = new List<GameObject>();

    void Start()
    {
        GenerateFloor();
    }

    void GenerateFloor()
    {
        // Стартовая комната в центре (0,0)
        SpawnRoom(roomStartPrefab, new Vector2Int(0, 0));
        usedPositions.Add(new Vector2Int(0, 0));

        int roomCount = Random.Range(minRooms, maxRooms + 1);
        Vector2Int currentPos = new Vector2Int(0, 0);

        for (int i = 1; i < roomCount; i++)
        {
            // Куда идём: вперёд (0,-1), влево (-1,0), вправо (1,0)
            // Назад не идём (0,1) чтобы не зацикливаться
            List<Vector2Int> directions = new List<Vector2Int>
            {
                new Vector2Int(0, -1),   // вперёд (вниз по Z)
                new Vector2Int(-1, 0),   // влево
                new Vector2Int(1, 0)     // вправо
            };

            // Перемешиваем направления
            Shuffle(directions);

            Vector2Int nextPos = currentPos;
            bool found = false;

            // Ищем свободное место
            foreach (var dir in directions)
            {
                Vector2Int testPos = currentPos + dir;
                if (!usedPositions.Contains(testPos))
                {
                    nextPos = testPos;
                    found = true;
                    break;
                }
            }

            if (!found) break; // Некуда идти

            // Выбираем тип комнаты
            GameObject prefab = roomCombatPrefab;
            if (i == roomCount - 1) prefab = roomRewardPrefab; // последняя — награда

            SpawnRoom(prefab, nextPos);
            usedPositions.Add(nextPos);
            currentPos = nextPos;
        }
    }

    void SpawnRoom(GameObject prefab, Vector2Int gridPos)
    {
        Vector3 worldPos = new Vector3(gridPos.x * roomSize, 0, gridPos.y * roomSize);
        GameObject room = Instantiate(prefab, worldPos, Quaternion.identity);
        room.name = prefab.name + "_" + gridPos.x + "_" + gridPos.y;
        spawnedRooms.Add(room);
    }

    void Shuffle(List<Vector2Int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}