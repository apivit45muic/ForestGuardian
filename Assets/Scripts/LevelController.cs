using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour
{
    public string nextLevelName;
    [SerializeField] public AudioClip _audioclip;
    [SerializeField] public AudioSource _audiosource;
    [SerializeField] public GameObject ThinkingBallon;
    [SerializeField] public GameObject WinScene;

    IEnumerator LoadLevel(){
        yield return new WaitForSeconds(0.5f);
        Score.items = 0;
        SceneManager.LoadScene(nextLevelName);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player") && Score.items>2)
        {
            PlayerPrefs.SetFloat("PlayerHealth", FindObjectOfType<Health>().currentHealth);
                _audiosource.PlayOneShot(_audioclip, 1);
                StartCoroutine(LoadLevel());
            
        }
        else
        {
            ThinkingBallon.SetActive(true);
        }    
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        ThinkingBallon.SetActive(false);
    }
    
}
