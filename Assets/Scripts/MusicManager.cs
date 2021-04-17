using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public Sound[] music; //Array of music to be played
    float timeLeftInSong = 0.0f; //A countdown to play the next song.
    int[] musicOrder; //An array of all the songs in the inspector. Ordered randomly and used as a playlist.
    int noOfSongsLeftInQueue = 0; //The number of songs left until the playlist has finished.
    int noOfLoops = 0; //The number of times the current song is going to loop before switching to something else.
    int index = 0;

    Sound currentSong; //The song that is currently playing.
    const float MAX_MUSIC_VOLUME = 1.0f;
    float musicVolume = MAX_MUSIC_VOLUME / 2; //Starts at half volume.
    float incrementVolumeAmount = MAX_MUSIC_VOLUME / 20; //5% of max music volume
    bool isMuted = false;


    // Start is called before the first frame update
    void Awake()
    {
        musicOrder = new int[music.Length];
        foreach (Sound m in music) //Sets up music inputted in the inspector.
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
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
        index = musicOrder[music.Length - noOfSongsLeftInQueue];

        timeLeftInSong = music[index].clip.length; //Sets song length.
        if (timeLeftInSong < 90.0f) //If track is less than a minute and a half, loop it.
        {
            noOfLoops = 1;
        }

        currentSong = music[index]; //The song to be played.
        if (!isMuted) currentSong.source.volume = musicVolume;
        else currentSong.source.volume = 0.0f;
        currentSong.source.Play(); //Actually plays the music.
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
                    PlayMusic();
                    noOfLoops--; //A loop has played.
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
        
        if (Input.GetKeyDown(KeyCode.M)) //Mutes volume.
        {
            if (currentSong.source.volume == 0.0f)
            {
                currentSong.source.volume = musicVolume;
                isMuted = false;
            }
            else
            {
                currentSong.source.volume = 0.0f;
                isMuted = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && musicVolume < MAX_MUSIC_VOLUME) //Increase volume.
        {
            if (currentSong.source.volume == 0.0f)
            {
                musicVolume = 0.0f; //If the current volume is 0 (Could be muted), Set the value to 0;
                isMuted = false;
            }
            musicVolume += incrementVolumeAmount; //Increment
            if (musicVolume > MAX_MUSIC_VOLUME) musicVolume = MAX_MUSIC_VOLUME; //If gone over, reset. Avoids floating point errors.
            currentSong.source.volume = musicVolume;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && musicVolume > 0.0f) //Lower volume.
        {
            if (currentSong.source.volume == 0.0f)
            {
                musicVolume = 0.0f;
                isMuted = false;
            }
            musicVolume -= incrementVolumeAmount;
            if (musicVolume < 0.0f) musicVolume = 0.0f;
            currentSong.source.volume = musicVolume;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) //Skip song.
        {
            noOfLoops = 0;
            index++;
            currentSong.source.volume = 0.0f;
            timeLeftInSong = 0.0f;
        }

    }

}
