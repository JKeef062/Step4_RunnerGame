using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Button playBtn;

    void Start()
    {
        playBtn.onClick.AddListener(LoadOnClick);
    }

    void LoadOnClick()
    {
        SceneManager.LoadScene(1);
    }
}