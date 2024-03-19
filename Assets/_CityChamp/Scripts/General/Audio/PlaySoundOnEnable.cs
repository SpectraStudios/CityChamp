using UnityEngine;

namespace SpectraStudios.CityChamp
{
    public class PlaySoundOnEnable : MonoBehaviour
    {
        public GameObject SoundPrefab;
        [SerializeField] [Range(0.01f, 10f)] private float PitchRandomMultiplier = 1f;

        private AudioClip _audioClip;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = SoundPrefab.GetComponent<AudioSource>();
            _audioClip = _audioSource.clip;

            if (PitchRandomMultiplier != 1)
            {
                if (Random.value < .5)
                    _audioSource.pitch *= Random.Range(1 / PitchRandomMultiplier, 1);
                else
                    _audioSource.pitch *= Random.Range(1, PitchRandomMultiplier);
            }
        }

        private void OnEnable()
        {
            _audioSource.Play();
        }
    }
}
