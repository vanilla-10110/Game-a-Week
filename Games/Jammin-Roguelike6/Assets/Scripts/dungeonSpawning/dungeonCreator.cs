using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public int dungeonWidth, dungeonLength;
    public int roomWidthMin, roomLengthMin;
    public int corridorWidth;
    public int maxIterations;
    




    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    private void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(dungeonWidth, dungeonLength);
        var listOfRooms = generator.CalculateRooms(maxIterations, roomWidthMin, roomLengthMin);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
