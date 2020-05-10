using System;
using ScoreSpace.Patterns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpace.Managers
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoSingleton<SoundManager> 
    {
    
        public AudioSource efxSource;
        public AudioSource shotSource;
        public AudioSource musicSource;
        public float lowPitchRange = .95f;
        public float highPitchRange = 1.05f;
        [SerializeField] private AudioClip[] _musics;

        protected void Start()
        {
            musicSource.Stop();
            PlayMusic(_musics[Random.Range(0,_musics.Length)]);
        }

        protected override void Init()
        {
            DontDestroyOnLoad (gameObject);
        }
        
        public void PlayMusic(AudioClip clip)
        {
            
            musicSource.clip = clip;
            musicSource.Play ();
        }
        public void StopMusic()
        {
            musicSource.Stop();
        }
        
        public void PlaySingle(AudioClip clip)
        {
            efxSource.PlayOneShot(clip);
        }


        public void RandomizeSfx(AudioClip clip, bool secondChannel = false)
        {
            var randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);
            if (secondChannel)
            {
                shotSource.pitch = randomPitch;
                shotSource.clip = clip;
                shotSource.Play();
            }
            else
            {
                efxSource.pitch = randomPitch;
                efxSource.clip = clip;
                efxSource.Play();
            }

        }
    }
    
    
}