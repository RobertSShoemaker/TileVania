using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    [SerializeField] float coinAudioVolume = 1f;
    [SerializeField] int coinValue = 100;

    //When the player collides with the coin, play coin audio, add to the player's score, and destroy gameObject
    void OnTriggerEnter2D(Collider2D collision)
    {
        //only the player can pick up coins
        //only the capsule collider will be able to pick up coins; this prevents us from picking up the coins twice with our second player collider
        if (collision.tag == "Player" && collision.GetType().Equals(typeof(CapsuleCollider2D)))
        {
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position, coinAudioVolume);
            FindObjectOfType<GameSession>().AddScore(coinValue);
            Destroy(gameObject);
            
        }
    }

}
