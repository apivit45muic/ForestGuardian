using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class LevelController1 : MonoBehaviour
{
    public string nextLevelName;
    [SerializeField] public AudioClip _audioclip;
    [SerializeField] public AudioSource _audiosource;
    [SerializeField] public GameObject WinScene;

    IEnumerator LoadLevel(){
        yield return new WaitForSeconds(0.5f);
        Score.items = 0;
        SceneManager.LoadScene(nextLevelName);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
                _audiosource.PlayOneShot(_audioclip, 1);
                StartCoroutine(LoadLevel());
            
        }
    }


}
