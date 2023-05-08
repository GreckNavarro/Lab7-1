using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRGB2D;
    [SerializeField] private float velocityMultiplier;
    [SerializeField] private int damage;
    [SerializeField] private PlayerController player;
    public event Action<int, HealthBarController> onCollision;

    

    public void SetUpVelocity(Vector2 velocity, int newlayer, SoundScriptableObject myAudioSO){
        myRGB2D.velocity = velocity * velocityMultiplier;
        gameObject.layer = newlayer;

        myAudioSO.CreateSound();
        DamageManager.instance.SubscribeFunction(this);
    }

    private void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag(gameObject.tag) && (other.CompareTag("Player") || other.CompareTag("Enemy"))){
            if(other.GetComponent<HealthBarController>()){
                onCollision?.Invoke(damage,other.GetComponent<HealthBarController>());
            }
            Destroy(this.gameObject);
        }
    }
}
