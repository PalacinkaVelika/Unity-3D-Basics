using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_handler : MonoBehaviour, IObserver {

    GameManager gameManager;
    InputHandle inputHandle;
    Canvas UI;
    public GameObject[] menus;

    void Awake() {
        UI = GetComponent<Canvas>();
        gameManager = FindObjectOfType<GameManager>();
        inputHandle = FindObjectOfType<InputHandle>();
        inputHandle.AddObserver(this);
        ToggleMenu(0);
        UI.enabled = false;
    }
    /* Pause menu */
    public void Pause() {
        gameManager.ChangeGameState(GameState.Paused);
        UI.enabled = true;
    }
    public void Resume() {
        gameManager.ChangeGameState(GameState.WalkMode);
        UI.enabled = false;
    }
    public void Settings() {
        ToggleMenu(1);
    }
    public void Quit() {
        Application.Quit();
    }
    /* Settings menu */
    public void ReturnToPause() {
        ToggleMenu(0);
    }

    /* Usefull stuff */
    public void ToggleMenu(int menuIndex) {
        for (int i = 0; i < menus.Length; i++) {
            menus[i].SetActive(i == menuIndex);
        }
    }

    public void OnNotify<T>(T data) {
        print("UI notified with " + data);
        switch (data) {
            case "Pause":
                if(gameManager.state == GameState.Paused) {
                    Resume();
                } else {
                    Pause();
                }
                break;
        }
    }
}
