using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    [SerializeField] float coinAudioVolume = 1f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position, coinAudioVolume);
            Destroy(gameObject);
        }
    }

}
