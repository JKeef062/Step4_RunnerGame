using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    public Button playBtn;
    public string inputText;
    public GameObject textField;

    void Start()
    {
        playBtn.onClick.AddListener(LoadOnClick);
    }

    void LoadOnClick()
    {
        inputText = textField.GetComponent<Text>().text;
        GameController.screenName = inputText;
        Debug.Log("Username = " + GameController.screenName);
        SceneManager.LoadScene(2);
    }
}