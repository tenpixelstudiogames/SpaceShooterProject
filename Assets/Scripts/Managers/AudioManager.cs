using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioClip _laserSound;

    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    private AudioClip _powerupSound;

    [SerializeField]
    private AudioSource _playSound;

 

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLaserSound()
    {
        _playSound.PlayOneShot(_laserSound);
    }

    public void PlayExplosionSound()
    {
        _playSound.PlayOneShot(_explosionSound);
    }

    public void PlayPowerupSound()
    {
        _playSound.PlayOneShot(_powerupSound);
    }
}
