using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;


public class dungeonMaker : MonoBehaviour
{
    int theme;
    public GameObject[,] rooms;

    int rows = 0;
    int columns = 0;



    void Start()
    {
        rooms = new GameObject[rows, columns];
        // Now you can assign GameObjects to the array
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                
                rooms[i, j] = GameObject.Find();
            }
        }


        //pick a theme, pick a room, build hallways off the room, build more rooms, continue until no more rooms can be built. Hallways are built off of entrances. the more rooms generated, the more likely an ending room will generate.
        theme = Random.Range(0,3);
        if (theme == 0)
        {
        }
    }
}

/*
 
 rows = theme
 columns = rooms
 
    _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
  0|0|1|2| | | | | | | | | | | | | | | | | |
    _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
  1|0|1|2| | | | | | | | | | | | | | | | | |
    _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
  2|0|1|2| | | | | | | | | | | | | | | | | |
    _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ 
  3|0|1|2| | | | | | | | | | | | | | | | | |





 */