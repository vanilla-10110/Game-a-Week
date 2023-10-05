using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;


public class dungeonMaker : MonoBehaviour
{
    int theme;
    public GameObject[] library;
    public GameObject[] jail;
    public GameObject[] dirt;
    public GameObject[] castle;
    public GameObject[] hallStraight;
    public GameObject[] hallCorner;

    


    //int rows = 0;
    //int columns = 0;



    void Start()
    {
        // rooms = new GameObject[rows, columns];
        // Now you can assign GameObjects to the array
        // for (int i = 0; i < rows; i++)
        // {
        //     for (int j = 0; j < columns; j++)
        //      {

        //           rooms[i, j] = GameObject.Find();
        //       }
        //   }


        //pick a theme, pick a room, build hallways off the room, build more rooms, continue until no more rooms can be built. Hallways are built off of entrances. the more rooms generated, the more likely an ending room will generate.
        theme = Random.Range(0, 0);//change to 3 later
        if (theme == 0)
        {
            //choose room
            int roomRand = Random.Range(0, 3);
            Debug.Log("roomRand = " + roomRand);

            //choose entrance
            int entranceRand = Random.Range(0, 2);
            
            Debug.Log("entranceRand = " + entranceRand);


            Instantiate(library[roomRand], new Vector3(0, 0, 0), transform.rotation);
           

            
            

            if (roomRand == 0)
            {
                GameObject Entrances = library[roomRand].transform.GetChild(3).gameObject;
                Vector3 entrancePos = Entrances.transform.GetChild(entranceRand).position;
                Debug.Log(entrancePos);

                if (entranceRand == 0)
                {
                    roomRand = Random.Range(0, 3);
                    Vector3 spawnPoint = new Vector3(entrancePos.x, entrancePos.y, entrancePos.z + 8);
                    Instantiate(library[roomRand], spawnPoint, transform.rotation);
                }

                else if (entranceRand == 1)//
                {
                    roomRand = Random.Range(0, 3);
                    Vector3 spawnPoint = new Vector3(entrancePos.x, entrancePos.y, entrancePos.z - 8);
                    Instantiate(library[roomRand], spawnPoint, transform.rotation);
                }


            }

            else if (roomRand == 1)
            {
                GameObject Entrances = library[roomRand].transform.GetChild(3).gameObject;
                Vector3 entrancePos = Entrances.transform.GetChild(entranceRand).position;
                Debug.Log(entrancePos);

                if (entranceRand == 0)
                {
                    if (roomRand == 2)
                    {
                        roomRand = Random.Range(0, 3);
                        Vector3 spawnPoint = new Vector3(entrancePos.x, entrancePos.y, entrancePos.z + 8);
                        Instantiate(library[roomRand], spawnPoint, transform.rotation);
                    }
                    else
                    {
                        roomRand = Random.Range(0, 3);
                        Vector3 spawnPoint = new Vector3(entrancePos.x, entrancePos.y, entrancePos.z + 8);
                        Instantiate(library[roomRand], spawnPoint, transform.rotation);
                    }
                }

                else if (entranceRand == 1)
                {
                    roomRand = Random.Range(0, 3);
                    Vector3 spawnPoint = new Vector3(entrancePos.x, entrancePos.y, entrancePos.z - 8);
                    Instantiate(library[roomRand], spawnPoint, transform.rotation);
                }
            }

            else if (roomRand == 2)
            {
                entranceRand = 0;
                Debug.Log("entranceRand = " + entranceRand);



                GameObject Entrances = library[roomRand].transform.GetChild(3).gameObject;
                Vector3 entrancePos = Entrances.transform.GetChild(entranceRand).position;
                Debug.Log(entrancePos);

                roomRand = Random.Range(0, 3);
                Vector3 spawnPoint = new Vector3(entrancePos.x, entrancePos.y, entrancePos.z + 8);
                Instantiate(library[roomRand], spawnPoint, transform.rotation);
            }


           /* if (roomRand == 0 && entranceRand == 0)
            {
                Instantiate(library[roomRand], new Vector3(0, 0, 0), transform.rotation);
                GameObject Entrances = library[roomRand].transform.GetChild(3).gameObject;
                Vector3 entrancePos = Entrances.transform.GetChild(entranceRand).position;
                Debug.Log(entrancePos);

                roomRand = Random.Range(0, 3);
                Debug.Log("roomRand = " + roomRand);
                if (entranceRand  == 0)
                {
                    Vector3 spawnPoint = new Vector3(entrancePos.x, entrancePos.y, entrancePos.z + 8);
                    Instantiate(library[roomRand], spawnPoint, transform.rotation);
                }
                else if (entranceRand == 1)
                {
                    Vector3 spawnPoint = new Vector3(entrancePos.x, entrancePos.y, entrancePos.z - 8);
                    Instantiate(library[roomRand], spawnPoint, transform.rotation);
                }
               

            }*/
            
            




        }
        else if (theme == 1)
        {
            Instantiate(jail[0]);
        }
        else if (theme == 2)
        {
            Instantiate(dirt[0]);
        }
        else if (theme == 3)
        {
            Instantiate(castle[0]);
        }
    }

    
}
/*
 

spawn a room, detect entrances, spawn hallways, spwan rooms at the end of the halway. After 3 rooms, increase chance of ending room. increase chance further after each new room.

, new Vector3(9,0,13), transform.rotation)
 */




