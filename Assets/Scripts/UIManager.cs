using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private GameObject winningScreen;
    [SerializeField] private AudioClip winningSound;

    [SerializeField] public AudioSource _audiosource;
    [SerializeField] public AudioSource _audiosource1;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        winningScreen.SetActive(false);
    }

    #region Game Over Functions
    //Game over function
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        _audiosource.PlayOneShot(gameOverSound, 1f);
    }
    public void Victory()
    {
        winningScreen.SetActive(true);
        _audiosource1.PlayOneShot(winningSound, 0.5f);
    }

    //Restart level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

        public void MainMenuClear()
    {
        SceneManager.LoadScene(4);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
        #endif
    }
    #endregion
}