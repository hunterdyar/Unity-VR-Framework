using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSourceInterface))]
public class AudioPlaylistHandler : MonoBehaviour
{
    private AudioSourceInterface audioInterface;
    int trackIndex = 0;
    int playingIndex;
    [Header("Settings")]
    public bool startWithRandomSong;
    public bool randomSongEachTurn;
    [Header("Audio Playlist")]
    public AudioClip[] playlist;
    void Awake()
    {
        audioInterface = GetComponent<AudioSourceInterface>();
    }
    void Start()
    {
        if(startWithRandomSong)
        {
            trackIndex = Random.Range(0,playlist.Length-1);
        }else{
            trackIndex = 0;
        }
        PlaySong(trackIndex);
    }
    public void PlaySong(int indexToPlay)
    {
        audioInterface.SetClip(playlist[indexToPlay]);
        StartCoroutine(WaitForSongEnd(audioInterface.GetClip().length));
        audioInterface.Play();
        playingIndex = indexToPlay;
    }
    IEnumerator WaitForSongEnd(float songLength)
    {
        yield return new WaitForSeconds(songLength);
        if(randomSongEachTurn)
        {
            while(trackIndex == playingIndex){//wont repeat itself twice in a row.
                trackIndex = Random.Range(0,playlist.Length-1);
            }
        }
        else
        {
            trackIndex++;
            if(trackIndex >= playlist.Length)
            {
                trackIndex = 0;
            }
        }
        PlaySong(trackIndex);
    }
}
