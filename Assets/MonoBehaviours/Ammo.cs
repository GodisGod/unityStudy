using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    public int damageInflicted;
    
    AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("子弹射击 collision");
        if (collision is BoxCollider2D) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(enemy.DamageCharacter(damageInflicted,0.0f));
            gameObject.SetActive(false);
        }
    }

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

private void OnEnable() {
     audioSource.Play();
}

private void OnDisable() {
    if(audioSource.isPlaying){
 audioSource.Stop();
}
    }
    


}
