using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObject/Audio/SoundScriptableObject", order =1)]
public class SoundScriptableObject : ScriptableObject
{
    [SerializeField] private AudioClip myAudio;
    [SerializeField] private AudioMixerGroup myGroup;

    public void CreateSound()
    {
        GameObject audioGameObject = new GameObject();
        AudioSource myAudioSource = audioGameObject.AddComponent<AudioSource>();

        myAudioSource.outputAudioMixerGroup = myGroup;
        myAudioSource.PlayOneShot(myAudio);
        Destroy(audioGameObject, 2f);
    }

}
