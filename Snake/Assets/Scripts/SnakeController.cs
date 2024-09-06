using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private Vector2 _direction = Vector2.down;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right || Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector2.right)
        {
            _direction = Vector2.left;
        }else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up || Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector2.up)
        {
            _direction = Vector2.down;
        }else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left || Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }else if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down || Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector2.down)
        {
            _direction = Vector2.up;  
        }
    }


    private IEnumerator Move()
    {
        while (true)
        {
            
            var position = transform.position;
            position +=(Vector3) _direction;
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

    }
    private void Death()
    {

    }
    
}
