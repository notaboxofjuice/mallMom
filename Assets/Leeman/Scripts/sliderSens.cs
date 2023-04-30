using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sliderSens : MonoBehaviour { // for changing mouse sens
    public TextMeshProUGUI text; // text to change
    public void SubmitSliderValue() { // called when slider value changes
        float sliderValue = GetComponent<Slider>().value; // get slider value
        pauseMenu.GetInstance().SetSensitivity(sliderValue); // send value to pause menu
        text.text = "Mouse Sensitivity: " + sliderValue.ToString(); // set text to value
    }
}