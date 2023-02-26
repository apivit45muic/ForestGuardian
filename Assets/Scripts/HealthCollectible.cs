using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] public AudioClip _audioclip;
    [SerializeField] public AudioSource _audiosource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // _audiosource.PlayOneShot(_audioclip, 1f);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);

            GameObject audioObject = new GameObject("Audio");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.clip = _audioclip;
            audioSource.volume = 0.5f;
            audioSource.Play();
            Destroy(audioObject, _audioclip.length);
        }
    }
}