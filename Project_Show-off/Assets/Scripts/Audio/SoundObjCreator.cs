using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObjCreator : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] AudioClip clip;

    public void PlayClip()
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.position = transform.position; //set pos
        //play sound
        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
    }
}