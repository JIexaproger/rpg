using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }  // Глобальный доступ!

    public GameInput InputActions { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InputActions = new GameInput();
            InputActions.Enable();
        }
        else
        {
            Destroy(gameObject);  // Только один экземпляр!
        }
    }

    private void OnDestroy()
    {
        InputActions?.Disable();
    }
}