using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public Sound[] music; //Array of music to be played
    float timeLeftInSong = 0.0f; //A countdown to play the next song.
    int previousSong = -1; //Used to make sure the same song doesn't play twice in a row. Initialised as an invalid index, so whatever is chosen will be played.

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound m in music) //Sets up music inputted in the inspector.
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
        }
    }

    private void PlayMusic() //Chooses and plays the next song.
    {
        int index; //index to music array.
        do
        {
            index = Random.Range(0, music.Length); //Chooses a song.
        } while (index == previousSong); //Checks to make sure it's not the last song that played.

        previousSong = index;
        timeLeftInSong = music[index].clip.length; //Sets song length.

        Sound m = music[index]; //The song to be played.
        m.source.Play(); //Actually plays the music.
    }

    private void Update()
    {
        if (timeLeftInSong <= 0.0f) //Timer has run out.
        {
            PlayMusic();
        }
        else
        {
            timeLeftInSong -= Time.deltaTime; //Counts down.
        }
    }

}
