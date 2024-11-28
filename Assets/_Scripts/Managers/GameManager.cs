using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent GameSave = new UnityEvent();
    public static GameManager Instance;
    
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private string mainMenuScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        //Destroy starting items if loading from save
        if (SavingManager.LoadInventoryAtStart)
        {
            foreach (ItemDrop itemDrop in FindObjectsOfType<ItemDrop>())
            {
                Destroy(itemDrop.gameObject);
            }
        }
    }
    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Pause();
        }
    }
    public void Pause()
    {
        Time.timeScale = pauseMenu.activeSelf ? 1 : 0;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
    
    public void SaveGame()
    {
        Time.timeScale = 1;
        Debug.Log("Game Saved");
        GameSave?.Invoke();
        SceneManager.LoadScene(mainMenuScene);
    }
    public void GameOver()
    {
        //Show game over screen
    }
}
