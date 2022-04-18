using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneNumber(int sceneNum) {
        PlayerPrefs.SetString("CurScene", sceneNum.ToString());
        SceneManager.LoadScene("TalkingSections");
    }
}