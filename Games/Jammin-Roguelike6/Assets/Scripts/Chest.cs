using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Animations;

public class Chest : Interactable
{
    [Header("Objects")]
    GameObject player;
    Animator animator;
    public GameObject card;
    public GameObject spawnPos;


    private void Awake()
    {
        player = GameObject.Find("Platyer");
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
    }

    protected override void Interact()
    {
        base.Interact();
        gameObject.GetComponent<Collider>().enabled = false;
        Debug.Log("*opens*");
        animator.Play("chestOpen");


        GameObject cardClone = Instantiate(card, spawnPos.transform.position, Quaternion.identity, gameObject.transform);
        Animator cardAnim = cardClone.GetComponent<Animator>();
        card.SetActive(true);
        cardAnim.Play("cardUppies");



    }







}
