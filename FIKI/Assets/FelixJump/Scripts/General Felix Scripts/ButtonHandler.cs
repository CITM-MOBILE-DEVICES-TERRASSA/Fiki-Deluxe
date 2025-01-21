using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button myButton; // Referencia al botón
    private LevelSelector levelSelector; // Referencia al LevelSelector

    private void Start()
    {
        // Obtener la referencia a LevelSelector (asegúrate de que haya una instancia en la escena)
        levelSelector = LevelSelector.Instance;

        if (myButton != null && levelSelector != null)
        {
            // Asignar el evento OnClick del botón para que llame a la función ChangeToLevelSelector
            myButton.onClick.AddListener(levelSelector.ChangeToLevelSelector);
        }
        else
        {
            Debug.LogError("No se ha asignado un botón o LevelSelector no está presente en la escena.");
        }
    }
}
