using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour {
    [SerializeField] private string vcaPath;
    
    private Slider _slider;
    private VCA _vca;

    private void Awake() {
        _slider = GetComponent<Slider>();
    }

    private void Start() {
        _vca = RuntimeManager.GetVCA(vcaPath);
        
        _slider.value = GetVolume();
    }

    public void OnSliderValueChanged() {
        SetVolume(_slider.value);
    }

    public void SetVolume(float volume) {
        _vca.setVolume(volume);
    }

    public float GetVolume() {
        _vca.getVolume(out float volume);
        return volume;
    }
}