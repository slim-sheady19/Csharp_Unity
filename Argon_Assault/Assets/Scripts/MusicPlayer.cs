using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;

        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            //DontDestroyOnLoad is a gameobject created when scene is changing.  objects can be assigned to the object with this function to be retained for the scene change
            DontDestroyOnLoad(gameObject); 
        }
    }
}
