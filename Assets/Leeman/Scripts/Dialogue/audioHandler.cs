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
    private int clipCount = 0;
    public void SetClips() { // for setting the current array of clips
    
    }
    public void PlayNext() {
        
    }
}