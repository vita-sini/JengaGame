using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manipulation : MonoBehaviour
{
    private Pick _pick;
    private Release _release;
    private MouseWorldPosition _mouseWorldPosition;
    private Rotate _rotate;
    private Movement _movement;
    private CheckingUpperBlock _checkingUpperBlock;
    private ScoreUI _scoreUI;
    private Deck _deck;
    private IPauseManager _pauseManager;

    private bool _blockScored = false;
    private Vector3 _offset; // �������� ���� ������������ ������ �����
    private Plane _movementPlane; // ��������� �������� ��� �����
    private Rigidbody _selectedBlock;
    private Vector3 _initialBlockPosition;

    private void Awake()
    {
        _pauseManager = FindObjectOfType<PauseMenu>();
        _checkingUpperBlock = new CheckingUpperBlock();
        _mouseWorldPosition = new MouseWorldPosition();
        _pick = new Pick(_mouseWorldPosition, _checkingUpperBlock);
        _release = new Release();
        _rotate = new Rotate();
        _movement = new Movement(_mouseWorldPosition, this);
        _scoreUI = GameObject.FindObjectOfType<ScoreUI>();
        _deck = GameObject.FindObjectOfType<Deck>();
    }

    private void Update()
    {
        if (_pauseManager.IsPaused)
        {
            return; 
        }

        ClickLeftMouseButton();
        ClickRightMouseButton();
    }

    private void ClickLeftMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _blockScored = false;
            _pick.Select(ref _selectedBlock, ref _offset, ref _movementPlane, ref _initialBlockPosition);
        }

        if (_selectedBlock != null && Input.GetMouseButton(0))
        {
            _movement.MoveMouse(_selectedBlock, _offset, _movementPlane, _initialBlockPosition);
            _rotate.Twist(_selectedBlock);
        }

        if (Input.GetMouseButtonUp(0) && _selectedBlock != null)
        {
            _release.FreeBlock(_selectedBlock);
            _checkingUpperBlock.UpdateTopBlock();
            // ��������� ��������, ����� ��������� ��������� �����
            StartCoroutine(WaitForBlockToSettle(_selectedBlock, _initialBlockPosition));

            _selectedBlock = null;
        }
    }

    // �������� ��� ��������, ���� ���� ���������� ���������
    private IEnumerator WaitForBlockToSettle(Rigidbody block, Vector3 initialPosition)
    {
        // ������������� ���, ���� ���� �� �������� �� ������ �����
        ContactMonitor monitor = block.GetComponent<ContactMonitor>();

        while (!monitor.IsOnBlock())
        {
            yield return null; // ��� ���� ����
        }

        //yield return new WaitForSeconds(1f);

        // ���������, ��� �� ���� ���������� ���� ��������� �������
        if (block.transform.position.y > initialPosition.y + 1 && !_blockScored)
        {
            //��������� ���� ������ ���� ���� ���� ��������� �������
            _scoreUI.CalculateScore(initialPosition, block.transform.position, block);

            if (SceneManager.GetActiveScene().name == Scenes.GAMEPLAYNEWCHALLENGES)
            {
                _deck.OnTurnEnd();
            }

            _blockScored = true;
        }
        else
        {
            Debug.Log("���� �� ��� ���������� ���� ��������� �������. ���� �� �����������.");
        }
    }

    private void ClickRightMouseButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // ���������� ��� ��� ����������� �����, �� ������� ���������� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Rigidbody hitBlock = hit.rigidbody;
                if (hitBlock != null)
                {
                    // ��������� ������ �� ���� Z � X, �������� ����������� ������
                    Vector3 forceDirection = Camera.main.transform.forward;
                    forceDirection.y = 0.5f; // ������� ��������� �� ��� Y
                    forceDirection.Normalize(); // ����������� ������
                    Vector3 force = forceDirection * 3000f;
                    hitBlock.AddForce(force, ForceMode.Impulse);
                }
            }
        }
    }
}
