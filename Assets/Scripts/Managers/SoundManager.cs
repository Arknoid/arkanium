using System;
using System.Collections;
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
        public AudioSource basicSource;
        public AudioSource loopSource;
        public AudioSource musicSource;
        public float lowPitchRange = .95f;
        public float highPitchRange = 1.05f;


        private bool _canPlay = true;
        [SerializeField] private float _canPlayDelay = 0.1f;
        
        protected void Start()
        {
            musicSource.Stop();
        }

        public void PlayLoop(AudioClip clip)
        {
            loopSource.clip = clip;
            if (!loopSource.isPlaying)
            {
             
                loopSource.Play();
            }
           
        }

        public void StopLoop()
        {
            loopSource.Stop();
        }
        
        public void PlayMusic(AudioClip clip)
        {
            
            musicSource.clip = clip;
            musicSource.Play();
        }
        public void StopMusic()
        {
            musicSource.Stop();
        }


        public void Play(AudioClip clip)
        {
            basicSource.clip = clip;
            basicSource.Play();
        }
        
        public void PlaySingle(AudioClip clip, bool forcePlay = false)
        {
            if (_canPlay || forcePlay)
            {
                efxSource.PlayOneShot(clip);
                _canPlay = false;
            }
            else
            {
                StartCoroutine(ResetCanPlay());
            }
            
        }

        private IEnumerator ResetCanPlay()
        {
            yield return new WaitForSeconds(_canPlayDelay);
            _canPlay = true;
        }

        public void RandomizeSfx(AudioClip clip)
        {
            var randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);
            {
                efxSource.pitch = randomPitch;
                efxSource.clip = clip;
                efxSource.Play();
            }

        }
    }
    
    
}