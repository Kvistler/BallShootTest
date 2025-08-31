using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Controllers")]
    [SerializeField] private UIController ui;
    [SerializeField] private CameraController camera;
    [SerializeField] private InputController input;
    [SerializeField] private PlayerController player;
    [SerializeField] private ObstacleController obstacles;
    [Header("Game Elements")]
    [SerializeField] private LevelEnd levelEnd;

    public UIController UI => ui;
    public CameraController Camera => camera;
    public InputController Input => input;
    public PlayerController Player => player;
    public ObstacleController Obstacles => obstacles;
    public LevelEnd LevelEnd => levelEnd;

    protected override void Awake()
    {
        base.Awake();
        
        Initialize();
    }

    private void OnDestroy()
    {
        Deinitialize();
    }

    private void Initialize()
    {
        Debug.Log("[GameController] Initialize");
        
        UI.Initialize();
        Input.Initialize();
        Camera.Initialize();
        Obstacles.Initialize();
        Player.Initialize();
        Player.Moved += OnMoved;
        
        LevelEnd.Initialize();
    }

    private void Deinitialize()
    {
        Debug.Log("[GameController] Deinitialize");
        
        UI.Deinitialize();
        Input.Deinitialize();
        Camera.Deinitialize();
        Obstacles.Deinitialize();
        Player.Deinitialize();
        Player.Moved -= OnMoved;
        
        LevelEnd.Deinitialize();
    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        Deinitialize();
        Initialize();
    }

    public void OnDeath()
    {
        Debug.Log("[GameController] OnDeath");

        var screen = ui.OpenScreen<RestartScreen>();
        screen.SetHeader(RestartScreenType.Lose);
    }

    public void OnWin()
    {
        Debug.Log("[GameController] OnWin");
        
        var screen = ui.OpenScreen<RestartScreen>();
        screen.SetHeader(RestartScreenType.Win);
        
        LevelEnd.Open();
    }

    private void OnMoved(float position)
    {
        Camera.Move(position);
    }
}
