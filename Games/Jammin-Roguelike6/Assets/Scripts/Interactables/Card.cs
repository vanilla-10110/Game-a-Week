using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using TMPro;
using Unity.VisualScripting;

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


    [Header("Upgrades")]
    private float[] upgradeStats =
    {
        1f,
        0.3f,
        5f,
        10f,
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
        "Critical Strike Chance",
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
        ParticleSystem cardSmoke = Instantiate(collectPS, particlePos, gameObject.transform.rotation);
        cardSmoke.Play();
        Destroy(cardSmoke, 5);
        ChangeStats();
        Destroy(gameObject);
    }

    void SetCardStatsAndText()
    {
        playerUpgrade1 = Random.Range(0, 8);
        while ( playerUpgrade1 == playerUpgrade2) playerUpgrade2 = Random.Range(0, 8);
        enemyUpgrade1 = Random.Range(0, 8);
        while (enemyUpgrade1 == enemyUpgrade2) enemyUpgrade2 = Random.Range(0, 8);

        playerDesc.text = "+ " + upgradeStats[playerUpgrade1] + " " + upgradeStatNames[playerUpgrade1] + "\n+ " + upgradeStats[playerUpgrade2] + " " + upgradeStatNames[playerUpgrade2];
        enemyDesc.text = "+ " + upgradeStats[enemyUpgrade1] + " " + upgradeStatNames[enemyUpgrade1] + "\n+ " + upgradeStats[enemyUpgrade2] + " " + upgradeStatNames[enemyUpgrade2];
        

    }


    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) SetCardStatsAndText();
    }

    void ChangeStats()
    {

        attackHandler.Stats[playerUpgrade1] += upgradeStats[playerUpgrade1];
        attackHandler.Stats[playerUpgrade2] += upgradeStats[playerUpgrade2];
        //attackHandler.Stats[enemyUpgrade1] += upgradeStats[enemyUpgrade1];
        //attackHandler.Stats[enemyUpgrade2] += upgradeStats[enemyUpgrade2];
        
        playerMovement.moveSpeed = attackHandler.Stats[0];

    }
}
