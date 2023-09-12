using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandle : MonoBehaviour, IObservable {

    private Dictionary<InputAction, bool> buttonStates = new Dictionary<InputAction, bool>();
    private List<IObserver> observers = new List<IObserver>();

    private PlayerInput playerInput;
    private InputAction move;
    private InputAction look;
    private InputAction sprint;
    private InputAction interact;
    private InputAction interact2;
    private InputAction pause;


    void Start() {
        //Input time
        playerInput = GetComponent<PlayerInput>();
        InputActionMap actionMap = playerInput.actions.FindActionMap("Player");
        move = actionMap.FindAction("Move");
        look = actionMap.FindAction("Look");
        sprint = actionMap.FindAction("Sprint");
        interact = actionMap.FindAction("Fire");
        interact2 = actionMap.FindAction("Fire2");
        pause = actionMap.FindAction("Pause");
    }

    void Update() {
        if (move.ReadValue<Vector2>().x != 0 || move.ReadValue<Vector2>().y != 0) {
        //    print("Walk action"); 
        }
        if (look.ReadValue<Vector2>().x != 0 || look.ReadValue<Vector2>().y != 0) {
        //    print("Look action");
        }
        if (sprint.ReadValue<float>() != 0f) {
        //    print("Sprint action");
        }
        if (ButtonPressedDown(interact)) {
        //    print("Click action");
        }
        if (ButtonPressedDown(interact2)) {
        //    print("Right Click action");
        }
        if (ButtonPressedDown(pause)) {
            NotifyObservers("Pause");
        }
    }

    //single click/press
    public bool ButtonPressedDown(InputAction action) {
        if (!buttonStates.ContainsKey(action)) {
            buttonStates[action] = false;
        }
        if (action.ReadValue<float>() == 1 && !buttonStates[action]) {
            buttonStates[action] = true;
            return true;
        } else if (action.ReadValue<float>() == 0 && buttonStates[action]) {
            buttonStates[action] = false;
        }
        return false;
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
