using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Screen> screens;
    
    public void Initialize()
    {
        Debug.Log("[UIController] Initialize");
        
        CloseAllScreens();
    }
    
    public void Deinitialize()
    {
        Debug.Log("[UIController] Deinitialize");
        
        CloseAllScreens();
    }
    
    public void CloseAllScreens()
    {
        screens.ForEach(s => s.Close());
    }
    
    public T OpenScreen<T>() where T : Screen
    {
        CloseAllScreens();
        
        var screen = GetScreen<T>();
        screen?.Open();
        
        return screen;
    }
    
    public void CloseScreen<T>() where T : Screen
    {
        GetScreen<T>()?.Close();
    }
    
    private T GetScreen<T>() where T : Screen
    {
        return (T)screens.FirstOrDefault(s => s is T);
    }

    public void RestartClicked()
    {
        GameController.Instance.Restart();
    }
}
