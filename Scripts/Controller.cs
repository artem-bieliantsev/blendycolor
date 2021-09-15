using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Controller : MonoBehaviour
{
    public static Controller _instance;

    [SerializeField] private Vector3[] _spawnPoint;
    [SerializeField] private Transform[] _plates;

    [SerializeField] private Transform _ingredientParent;

    [SerializeField] private GameObject _camera;

    [SerializeField] private Jug _jug;

    [SerializeField] private LevelParameters _level;

    [SerializeField] private Audio _audioManager = new Audio();

    private int _currentLevel = 1;

    private int _compareValue;


    public static Controller Instance
    {
        get
        {
            return _instance;
        }
    }

    public LevelParameters Level { get => _level; set => _level = value; }
    public int CompareValue { get => _compareValue; set => _compareValue = value; }
    public Transform IngredientParent { get => _ingredientParent; set => _ingredientParent = value; }
    public Audio AudioManager { get => _audioManager; set => _audioManager = value; }

    private void Awake()
    {
        _instance = this;

        _audioManager.SourceSFX = gameObject.AddComponent<AudioSource>();
        _audioManager.SourceMusic = gameObject.AddComponent<AudioSource>();

        _audioManager.PlayMusic();
    }

    private void Start()
    {
        Hud.Instance.LoadAudioSettings();

        ResetLevel();
        InitializeLevel();
    }

    public void RestartLevel()
    {
        ClearLevel();

        _level.ClearLevel();

        InitializeLevel();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        _currentLevel++;

        if (_currentLevel > _level.LevelData.Count)
        {
            _currentLevel = 1;
        }

        _level.ClearLevel();

        RestartLevel();
    }


    private void ClearLevel()
    {
        foreach (Transform cup in _plates)
        {
            if (cup.childCount != 0)
            {
                Transform child = cup.transform.GetChild(0);

                Destroy(child.gameObject);
            }
        }
        foreach (Transform child in _ingredientParent)
        {
            Destroy(child.gameObject);
        }

        Destroy(_level.CurrentVisitor.gameObject);

        ResetLevel();
    }

    private void ResetLevel()
    {
        _jug.ClearJug();
        Hud.Instance.HideCloud();
        Hud.Instance.MixButton.SetActive(false);
    }

    private void MoveCamera()
    {
        _camera.transform.position = new Vector3(0f, 1.7f, -1f);
        _camera.transform.DOMove(new Vector3(0f, 1.9f, -2f), 2f).OnComplete(Hud.Instance.ShowCloud);
    }

    private void InitializeLevel()
    {
        _level.Initialize(_level.LevelData[_currentLevel - 1]);

        MoveCamera();
    }   

    public void SpawnFruit(GameObject ingredientPrefab)
    {
        float randomX = UnityEngine.Random.Range(0f, 360f);
        float randomY = UnityEngine.Random.Range(0f, 360f);
        float randomZ = UnityEngine.Random.Range(0f, 360f);

        Quaternion ingredientRotation = new Quaternion(randomX, randomY, randomZ, 0);

        GameObject ingredient = Instantiate(ingredientPrefab, _ingredientParent.transform.position, ingredientRotation);
        ingredient.gameObject.AddComponent<MeshCollider>().convex = true;
        ingredient.gameObject.AddComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;         
        ingredient.transform.SetParent(_ingredientParent);
    }

    public void Mix()
    {
        _jug.MixIngredients();

        CompareColors(_jug.MixedColor, _level.ReferenceColor);

        Hud.Instance.CountCompareValue();
    }

    private void CompareColors(Color mixed, Color request)
    {
        float distance = CalculateColorDistance(mixed, request);

        float delta = 2f;

        //distance -= 5f;

        _compareValue = (int)Mathf.Round(100f - distance);

        if (distance <= delta)
        {
            _compareValue = 100;
        }
        else if (distance >= 100f)
        {
            _compareValue = 0;
        }
    }

    private float CalculateColorDistance(Color firstColor, Color secondColor)
    {
        float[] first = ColorConverter.RGBtoLab(firstColor);
        float[] second = ColorConverter.RGBtoLab(secondColor);

        float distance = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(first[0] - second[0], 2)
            + Mathf.Pow(first[1] - second[1], 2)
            + Mathf.Pow(first[2] - second[2], 2)));

        return distance;
    }

}

