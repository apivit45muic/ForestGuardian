using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuScreen;
    [SerializeField] private AudioClip MainMenuBGM;
    [SerializeField] public AudioSource _audiosource;

    private void Awake()
    {
        _audiosource.PlayOneShot(MainMenuBGM,1f);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
        #endif
    }

}