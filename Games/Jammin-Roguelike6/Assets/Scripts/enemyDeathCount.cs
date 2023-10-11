using UnityEngine;


public class enemyDeathCount : MonoBehaviour
{
    public int deadCount = 0;
    int deadCountTemp = 0;
    public int chestChance = 10;
    public GameObject chest;
    GameObject player;
    Vector3 chestSpawn;


    public void Start()
    {
        player = GameObject.Find("Platyer");
    }

    public void Update()
    {
        chestSpawn = player.transform.position;

        if (deadCount > deadCountTemp)
        {
            Debug.Log("tehe");
            deadCountTemp ++;
            IncreaseCount(deadCount);

            int temp = Random.Range(0, chestChance);
            if (temp == 0)
            {
                chestChance = 10;
                //GameObject chestClone = Instantiate(chest, chestSpawn, Quaternion.identity);
                //chestClone.transform.position = Vector3.zero;

                GameObject chestClone = Instantiate(chest);
                chest.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);


            }
            else
            {
                chestChance --;
                Debug.Log(chestChance);
            }
        }





    }

    void IncreaseCount(int number)
    {

    }

}
