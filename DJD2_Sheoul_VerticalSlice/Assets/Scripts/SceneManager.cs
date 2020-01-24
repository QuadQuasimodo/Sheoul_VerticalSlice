using UnityEngine;

/// <summary>
/// Handles the Scene changing and Game quitting
/// </summary>
public class SceneManager : MonoBehaviour
{
    /// <summary>
    /// Loads the 'MainMenu' scene
    /// </summary>
    public void MainMenu()
    {
        // Loads the 'MainMenu' scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Loads the 'GameScene' scene
    /// </summary>
    public void GameScene()
    {
        // Loads the 'GameScene' scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void Quit()
    {
        // Quits the game
        Application.Quit();
    }
}
