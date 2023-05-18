using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for playing dialogue audio clips concurrently with on screen text
public class audioHandler : MonoBehaviour
{
    [Header("Arrays of Audio Clips")]
    public AudioClip[] greetingClips;
    public AudioClip[] negativeClips;
    public AudioClip[] positiveClips;
    public AudioClip[] currentClips;
    public int clipCount = 0;
    private void Start() {
        SetClips("greeting"); // set currentClips to greetingClips
    }
    public void SetClips(string type) { // for setting the current array of clips
        switch (type) { // set currentClips based on supplied string
            case "greeting":
                currentClips = greetingClips;
                GetComponent<dialogueTrigger>().inkJSON = GetComponent<dialogueTrigger>().greeting; // set inkJSON to greeting
                break;
            case "negative":
                currentClips = negativeClips;
                GetComponent<dialogueTrigger>().inkJSON = GetComponent<dialogueTrigger>().negative; // set inkJSON to negative
                break;
            case "positive":
                currentClips = positiveClips;
                GetComponent<dialogueTrigger>().inkJSON = GetComponent<dialogueTrigger>().positive; // set inkJSON to positive
                break;
            default:
                Debug.Log("No clips found");
                break;
        }
        clipCount = 0; // reset clipCount
    }
    public void PlayNext() { // for playing the next clip in the currentClips array
        if (GetComponent<AudioSource>().isPlaying) { // if a clip is currently playing, stop it
            GetComponent<AudioSource>().Stop();
        }
        if (clipCount < currentClips.Length) { // if there are more clips to play
            GetComponent<AudioSource>().PlayOneShot(currentClips[clipCount]); // play the next clip
            clipCount++; // increment clipCount
        } else { // if there are no more clips to play
            clipCount = 0; // reset clipCount
        }
    }
}