using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class volumeManager : MonoBehaviour
{
    public TextMeshProUGUI text; // text to change
    public void SubmitSliderValue() { // called when slider value changes
        float sliderValue = GetComponent<Slider>().value; // get slider value
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>(); // get all audio sources
        foreach (AudioSource audioSource in audioSources) { // for each audio source
            audioSource.volume = sliderValue / 100; // set volume
        }
        text.text = "Volume: " + sliderValue.ToString(); // set text to value
    }
}