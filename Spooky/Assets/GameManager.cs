using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IObservable {

    private List<IObserver> observers = new List<IObserver>();
    private static GameManager instance;
    public GameState state;

    private void Awake() {
        state = GameState.WalkMode;

        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeGameState(GameState newState) {
        state = newState;
        NotifyObservers(state);
    }



    //Singleton
    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameManager>();
                if (instance == null) {
                    GameObject gameObject = new GameObject("GameManager");
                    instance = gameObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    /** Observer stuff **/
    public void AddObserver(IObserver observer) {
        print("Adding " + observer + "...");
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer) {
        print("Removing " + observer + "...");
        observers.Remove(observer);
    }

    public void NotifyObservers<T>(T data) {
        print("Notifying Observers with " + data);
        foreach (IObserver idk in observers) {
            idk.OnNotify(data);
        }
    }
}

public enum GameState {
    Paused,
    WalkMode
}
