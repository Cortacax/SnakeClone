using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 _areaLimit = new Vector2(13, 24);


    [SerializeField] private GameObject food;
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private float speed = 1;
    [SerializeField] private TextMeshPro textScore;
    [SerializeField] private TextMeshPro textGameOver;
    private Vector2 _direction = Vector2.down;
    private List<Transform> _snake = new List<Transform> ();
    private int Score
    {
        get => _score;
        set 
        {
            _score=value;
            textScore.text = _score.ToString();
        }
    }
    private int _score;
    private bool _grow;

    private void Start()
    {
        textGameOver.enabled = false;
        Score = 0;
        ChangePositionFood();
        StartCoroutine(Move());

        _snake.Add(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right || Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector2.right)
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up || Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector2.up)
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left || Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down || Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector2.down)
        {
            _direction = Vector2.up;
        }
    }


    private IEnumerator Move()
    {
        while (true)
        {

            for (int i = _snake.Count-1; i > 0; i--)
            {
                _snake[i].position = _snake[i-1].position;
            }


            var position = transform.position;
            position += (Vector3)_direction;
            position.x = Mathf.RoundToInt(position.x);
            position.y = Mathf.RoundToInt(position.y);
            transform.position = position;
            yield return new WaitForSeconds(speed);


        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();

        }

        if (other.CompareTag("Wall"))
        {
            Death();
        }
    }
    private void Grow()
    {

        var tail = Instantiate(tailPrefab);
        ChangePositionFood();
        _snake.Add(tail.transform);
        _snake[_snake.Count - 1].position = _snake[_snake.Count - 2].position;
        

    }

    private void ChangePositionFood()
    {
        Vector2 newFoodPosition;
        
        do
        {
            var x = (int)Random.Range(1, _areaLimit.x);
            var y = (int)Random.Range(1, _areaLimit.y);
            newFoodPosition = new Vector2(x, y);
        } while (!CanFoodSpawn(newFoodPosition));



        food.transform.position = newFoodPosition;

    }
       private bool CanFoodSpawn(Vector2 newPosition)
    {
        foreach (var item in _snake)
        {
            var x = Mathf.RoundToInt(item.position.x);
            var y = Mathf.RoundToInt(item.position.y);
            if (new Vector2(x,y) == newPosition)
            {
                return false;
            }
        }
        return true;
    }

    private void Death()
    {
        textGameOver.enabled = true;
        StopAllCoroutines();
    }

}
