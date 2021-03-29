using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public Sound[] music;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
        }
        PlayMusic(2);
    }

    public void ChooseTrack()
    {

    }

    public void PlayMusic(int index)
    {
        
        if (index > 0 || index <= music.Length)
        {
            Sound m = music[index];
            m.source.Play();
        }

    }

    private void Update()
    {
        
    }

}
