using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MainMenuButtonMethods : MonoBehaviour
{
    [SerializeField] private Button continueButton, startButton, quitButton;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "inventory"))
        {
            Debug.Log("Save Exists");
        }
        else
        {
            Debug.Log("No Save Exists");
            continueButton.interactable = false;
        }
    }

    public void StartGame(string gameScene)
    {
        SceneManager.LoadScene(gameScene);
    }
    public void ContinueGame(string gameScene)
    {
        SavingManager.LoadInventoryAtStart = true;
        SceneManager.LoadScene(gameScene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
