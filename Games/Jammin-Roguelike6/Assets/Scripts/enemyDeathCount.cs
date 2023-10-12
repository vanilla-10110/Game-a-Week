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
    int highScore;
    TextMeshProUGUI highScoreCount;


    public void Awake()
    {
        player = GameObject.Find("Platyer");
        killCount = GameObject.Find("killCount").GetComponent<TextMeshProUGUI>();
        highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreCount = GameObject.Find("highScoreCount").GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        

        if (deadCount > deadCountTemp)
        {
            
            deadCountTemp ++;
            IncreaseCount();

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
            if (highScore <= deadCount)
            {
                highScore = deadCount;
                PlayerPrefs.SetInt("highScore", highScore);
                IncreaseCount() ;
            }
        }





    }

    void IncreaseCount()
    {
        killCount.text = "Kills: " + deadCount.ToString();
        highScoreCount.text = "High Score: " + highScore.ToString();
    }

}
