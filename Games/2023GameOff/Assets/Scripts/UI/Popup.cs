using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Popup : MonoBehaviour {
    [SerializeField] private EventReference popOnSound;
    [SerializeField] private EventReference popOffSound;
    
    private Animator _animator;
    private bool _toggledOn = false;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }
    
    public void TogglePopup() {
        if (_toggledOn) {
            HidePopup();
        }
        else {
            ShowPopup();
        }
    }

    public void ShowPopup() {
        if (_toggledOn) {
            return;
        }
        
        _animator.SetTrigger("On");
        FMODUnity.RuntimeManager.PlayOneShot(popOnSound);
        _toggledOn = true;
    }
    
    public void HidePopup() {
        if (!_toggledOn) {
            return;
        }
        
        _animator.SetTrigger("Off");
        FMODUnity.RuntimeManager.PlayOneShot(popOffSound);
        _toggledOn = false;
    }
}