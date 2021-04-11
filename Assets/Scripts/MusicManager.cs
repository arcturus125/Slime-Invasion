using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public Sound[] music; //Array of music to be played
    float timeLeftInSong = 0.0f; //A countdown to play the next song.
    int[] musicOrder; //An array of all the songs in the inspector. Ordered randomly and used as a playlist.
    int noOfSongsLeftInQueue = 0; //The number of songs left until the playlist has finished.
    int noOfLoops = 0; //The number of times the current song is going to loop before switching to something else.


    // Start is called before the first frame update
    void Awake()
    {
        musicOrder = new int[music.Length];
        foreach (Sound m in music) //Sets up music inputted in the inspector.
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
        }
    }

    private void OrderPlaylist()
    {
        for(int i = 0; i < music.Length; i++) //Makes all tracks in playlist invalid. This is necessary as the array is searched for values, so there cannot be any valid ones initially.
        {
            musicOrder[i] = -1;
        }
        for (int i = 0; i < music.Length; i++) //Loops through again, this time to add songs to the list.
        {
            int randomSong;
            do
            {
                randomSong = Random.Range(0, music.Length); //Chooses random song to put in the playlist.

            } while (!CanBeAddedToPlaylist(randomSong));

            musicOrder[i] = randomSong; //Song is added to playlist.
        }
        noOfSongsLeftInQueue = music.Length;

        Debug.Log("New Order");
        for (int i = 0; i < music.Length; i++) //Makes all tracks in playlist invalid. This is necessary as the array is searched for values, so there cannot be any valid ones initially.
        {
            Debug.Log(musicOrder[i]);
        }
    }

    private bool CanBeAddedToPlaylist(int songIndex) //Returns true if the song index is currently unused in the playlist.
    {
        for (int i = 0; i < music.Length; i++) //Loops through the playlist.
        {
            if (musicOrder[i] == songIndex) //The index exists in the playlist already.
            {
                return false; //We don't want this to be added to the list.
            }
        }
        return true; //Gone through the array and not found the song index. This is fine to be added to the playlist.
    }

    private void PlayMusic() //Chooses and plays the next song.
    {
        int index = musicOrder[music.Length - noOfSongsLeftInQueue];

        timeLeftInSong = music[index].clip.length; //Sets song length.
        if (timeLeftInSong < 90.0f) //If track is less than a minute and a half, loop it.
        {
            noOfLoops = 1;
        }

        Sound m = music[index]; //The song to be played.
        m.source.Play(); //Actually plays the music.
    }

    private void Update()
    {
        timeLeftInSong -= Time.deltaTime; //Counts down.

        if (noOfSongsLeftInQueue <= 0)
        {
            OrderPlaylist();
        }
        else
        {
            if (noOfLoops > 0) //Song will loop.
            {
                if (timeLeftInSong <= 0.0f) //Timer has run out.
                {
                    noOfLoops--; //A loop has played.
                    PlayMusic();
                }
            }
            else //Change to a different song
            {
                if (timeLeftInSong <= 0.0f) //Timer has run out.
                {
                    if (noOfSongsLeftInQueue <= 0)
                    {
                        OrderPlaylist();
                    }
                    else
                    {
                        PlayMusic();
                        noOfSongsLeftInQueue--;
                    }
                }
            }
        }
        
        
    }

}
