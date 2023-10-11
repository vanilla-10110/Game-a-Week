using UnityEngine;
using TMPro;

public class enemyDeathCount : MonoBehaviour
{
    public int deadCount = 0;
    int deadCountTemp = 0;
    public int chestChance = 10;
    public GameObject chest;
    GameObject player;
    Vector3 chestSpawn;
    TextMeshProUGUI killCount;



    public void Awake()
    {
        player = GameObject.Find("Platyer");
        killCount = GameObject.Find("killCount").GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        

        if (deadCount > deadCountTemp)
        {
            
            deadCountTemp ++;
            IncreaseCount(deadCount);

            int temp = Random.Range(0, chestChance);
            if (temp == 0 || chestChance == 0)
            {
                chestChance = 10;

                chestSpawn = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                GameObject chestClone = Instantiate(chest, chestSpawn, Quaternion.identity);
                chestClone.transform.position += player.transform.forward; 

                Vector3 direction = (player.transform.position - chestClone.transform.position).normalized;
                direction.y = 0;
                chestClone.transform.rotation = Quaternion.LookRotation(direction);
                Debug.Log("and dey say");
            }
            else
            {
                chestChance --;
                Debug.Log("chest chance = " + chestChance);
            }
        }





    }

    void IncreaseCount(int number)
    {
        killCount.text = "Kills: " + number.ToString();
    }

}
