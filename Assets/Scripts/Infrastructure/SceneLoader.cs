using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void RestartCurrent()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
