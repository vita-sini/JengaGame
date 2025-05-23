using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manipulation : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private Material _ghostMaterial;

    private ProjectionGhost _projectionGhost;
    private Pick _pick;
    private Release _release;
    private MouseWorldPosition _mouseWorldPosition;
    private Rotate _rotate;
    private Movement _movement;
    private ScoreUI _scoreUI;
    private Deck _deck;
    private IPauseManager _pauseManager;

    private bool _blockScored = false;
    private Vector3 _offset; // Смещение мыши относительно центра блока
    private Plane _movementPlane; // Плоскость движения для блока
    private Rigidbody _selectedBlock;
    private Vector3 _initialBlockPosition;
    private float _previousMaxHeight;

    public bool IsBlockHeld { get; private set; } = false;

    private void Awake()
    {
        _pauseManager = _pauseMenu;
        _mouseWorldPosition = new MouseWorldPosition();
        _pick = new Pick(_mouseWorldPosition);
        _release = new Release();
        _rotate = new Rotate();
        _movement = new Movement(_mouseWorldPosition, this);
        _scoreUI = GameObject.FindObjectOfType<ScoreUI>();
        _deck = GameObject.FindObjectOfType<Deck>();
        _projectionGhost = gameObject.AddComponent<ProjectionGhost>();
    }

    private void Update()
    {
        if (_pauseManager.IsPaused)
            return;

        ClickLeftMouseButton();
        ClickRightMouseButton();
    }

    private void ClickLeftMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _blockScored = false;
            _pick.Select(ref _selectedBlock, ref _offset, ref _initialBlockPosition);

            if (_selectedBlock != null)
            {
                if (_blockSpawner.CurrentSpawnedBlock != null && _selectedBlock.gameObject == _blockSpawner.CurrentSpawnedBlock)
                    _blockSpawner.ReleaseBlock();

                _previousMaxHeight = GetCurrentTowerHeight();

                IsBlockHeld = true;
                _projectionGhost.Initialize(_selectedBlock, _ghostMaterial);
            }
        }

        if (_selectedBlock != null && Input.GetMouseButton(0))
        {
            _movement.MoveMouse(_selectedBlock, _offset);
            _rotate.Twist(_selectedBlock);
        }

        if (Input.GetMouseButtonUp(0) && _selectedBlock != null)
        {
            _release.FreeBlock(_selectedBlock);
            StartCoroutine(WaitForBlockToSettle(_selectedBlock, _initialBlockPosition));

            _selectedBlock = null;
            IsBlockHeld = false;
            _projectionGhost.DestroyGhost();
        }
    }

    private void ClickRightMouseButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Rigidbody hitBlock = hit.rigidbody;

                if (hitBlock != null)
                {
                    Vector3 forceDirection = Camera.main.transform.forward;
                    forceDirection.y = 0.5f;
                    forceDirection.Normalize();
                    Vector3 force = forceDirection * 3000f;
                    hitBlock.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }

    private IEnumerator WaitForBlockToSettle(Rigidbody block, Vector3 initialPosition)
    {
        ContactMonitor monitor = block.GetComponent<ContactMonitor>();

        while (!monitor.IsOnValidSurface())
            yield return null;

        BlockState blockState = block.GetComponent<BlockState>();

        if (blockState != null)
            blockState.SetPlaced();

        float blockTopY = block.GetComponent<Renderer>().bounds.max.y;

        if (blockTopY > _previousMaxHeight + 0.01f && !_blockScored)
        {
            Debug.Log("Условие для начисления очков выполнено.");
            _scoreUI.CalculateScore(initialPosition, block.transform.position, block);

            if (SceneManager.GetActiveScene().name == Scenes.GAMEPLAYNEWCHALLENGES)
                _deck.OnTurnEnd();

            _blockScored = true;

            // Обновляем высоту башни в спавнере
            _blockSpawner.UpdateTowerHeight(blockTopY);
        }

        _blockSpawner.SpawnBlock();
    }

    private float GetCurrentTowerHeight()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length == 0) return 0f;

        float maxY = float.MinValue;

        foreach (GameObject block in blocks)
        {
            BlockState blockState = block.GetComponent<BlockState>();

            if (blockState == null || blockState.CurrentState != BlockState.State.Placed)
                continue;

            Renderer renderer = block.GetComponent<Renderer>();

            if (renderer != null)
            {
                float topY = renderer.bounds.max.y;
                if (topY > maxY)
                    maxY = topY;
            }
        }

        return maxY == float.MinValue ? 0f : maxY;
    }
}
