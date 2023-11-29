using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private MusicArea hubArea;
    [SerializeField] private MusicArea exploreArea;
    
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag.Equals("Player"))
        {
            AudioManager.instance.SetMusicArea(hubArea);
        }        
    }
    
    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.tag.Equals("Player"))
        {
            AudioManager.instance.SetMusicArea(exploreArea);
        }
    }

}
