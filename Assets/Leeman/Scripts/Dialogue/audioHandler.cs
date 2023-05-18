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
    private AudioClip[] currentClips;
    private int clipCount = 0;
}