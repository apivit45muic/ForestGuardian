using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_staff : MonoBehaviour
{
    [SerializeField] private GameObject quiver;
    [SerializeField] private AudioClip audioChest;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator anim;
    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player") && !isOpen)
        {
            audioSource.PlayOneShot(audioChest, 0.5f);
            anim.SetBool("open", true);
            quiver.SetActive(true);
            isOpen = true;
        }
    }
}
