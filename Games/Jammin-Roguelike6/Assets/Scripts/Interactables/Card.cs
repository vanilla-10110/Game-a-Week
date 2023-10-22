using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : Interactable
{
    [Header("Objects")]
    public ParticleSystem collectPS;
    public TextMeshProUGUI title;
    public TextMeshProUGUI playerDesc;
    public TextMeshProUGUI enemyDesc;
    GameObject player;
    AttackHandler attackHandler;
    PlayerMovement playerMovement;
    public enemyController enemyController;

    [Header("Upgrades")]
    private float[] upgradeStats =
    {
        1f,
        0.3f,
        5f,
        15f,
        //5f,
        //5f,
        //5f,
        //5f
    };

    private string[] upgradeStatNames =
    {
        "Movement Speed", 
        "Attack Speed",
        "Attack Damage",
        "Health",
        //"Fire Damage",
        //"Shock Damage",
        //"Acid Damage",
        //"Void Damage"
    };
    int playerUpgrade1;
    int playerUpgrade2;
    int enemyUpgrade1;
    int enemyUpgrade2;

    private void Awake()
    {
        player = GameObject.Find("Platyer");
        playerMovement = player.GetComponent<PlayerMovement>();
        attackHandler = player.GetComponent<AttackHandler>();
    }

    private void Start()
    {
        SetCardStatsAndText();
    }

    protected override void Interact()
    {
        base.Interact();
        Debug.Log("You got it!");
        Vector3 particlePos = gameObject.transform.position;
        FMODUnity.RuntimeManager.PlayOneShot("event:/PICKUP/PU_CARD");
        ParticleSystem cardSmoke = Instantiate(collectPS, particlePos, gameObject.transform.rotation);
        cardSmoke.Play();
        Destroy(cardSmoke, 5f);
        ChangeStats();
        Destroy(gameObject);
    }

    void SetCardStatsAndText()
    {
        playerUpgrade1 = Random.Range(0, 3);
        playerUpgrade2 = Random.Range(0, 3);
        enemyUpgrade1 = Random.Range(0, 4);
        while (enemyUpgrade1 == 2) enemyUpgrade1 = Random.Range(0, 4);
        
        enemyUpgrade2 = Random.Range(0, 4);
        while (enemyUpgrade2 == 2) enemyUpgrade2 = Random.Range(0, 4);

        playerDesc.text = "You:\n+ " + upgradeStats[playerUpgrade1] + " " + upgradeStatNames[playerUpgrade1] + "\n+ " + upgradeStats[playerUpgrade2] + " " + upgradeStatNames[playerUpgrade2];
        enemyDesc.text = "Skellies:\n+ " + upgradeStats[enemyUpgrade1] + " " + upgradeStatNames[enemyUpgrade1] + "\n+ " + upgradeStats[enemyUpgrade2] + " " + upgradeStatNames[enemyUpgrade2];
        

    }


    
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T)) SetCardStatsAndText();
    }

    void ChangeStats()
    {

        attackHandler.Stats[playerUpgrade1] += upgradeStats[playerUpgrade1];
        attackHandler.Stats[playerUpgrade2] += upgradeStats[playerUpgrade2];
        //attackHandler.Stats[enemyUpgrade1] += upgradeStats[enemyUpgrade1];
        //attackHandler.Stats[enemyUpgrade2] += upgradeStats[enemyUpgrade2];
        
        playerMovement.moveSpeed = attackHandler.Stats[0];

        attackHandler.enemStats[enemyUpgrade1] += upgradeStats[enemyUpgrade1];
        attackHandler.enemStats[enemyUpgrade2] += upgradeStats[enemyUpgrade2];
        
       // Debug.Log(enemyController.Stats[enemyUpgrade1]);
    }
}
