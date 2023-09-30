using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] GameObject particleVFX;

    private bool _dragging;

    private Vector2 _offset;


    [SerializeField]private float min_X = -2.3f;

    [SerializeField]private float max_X = 2.3f;

    [SerializeField]private float min_Y = -4.7f;

    [SerializeField]private float max_Y = 4.7f;

    private Vector3 startPos;
    private void OnMouseDown()
    {
        _offset = new Vector2(GetMousePos().x - transform.position.x,GetMousePos().z - transform.position.z);
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnEnable()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (transform.position.x < min_X)
        {
            Vector3 moveDirX = new Vector3(min_X,0, transform.position.z);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X)
        {
            Vector3 moveDirX = new Vector3(max_X,0, transform.position.z);
            transform.position = moveDirX;
        }
        else if (transform.position.z < min_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x,0, min_Y);
            transform.position = moveDirX;
        }
        else if (transform.position.z > max_Y)
        {
            Vector3 moveDirX = new Vector3(transform.position.x,0, max_Y);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.z < min_Y)
        {
            Vector3 moveDirX = new Vector3(min_X,0, min_Y);
            transform.position = moveDirX;
        }
        else if (transform.position.x < min_X && transform.position.z > max_Y)
        {
            Vector3 moveDirX = new Vector3(min_X,0, max_Y);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.z > max_Y)
        {
            Vector3 moveDirX = new Vector3(max_X,0, max_Y);
            transform.position = moveDirX;
        }
        else if (transform.position.x > max_X && transform.position.z < min_Y)
        {
            Vector3 moveDirX = new Vector3(max_X,0, min_Y);
            transform.position = moveDirX;
        }
    }

    private void OnMouseDrag()
    {
        var mousePosition =new Vector3(GetMousePos().x,0,GetMousePos().y);

        transform.position =new Vector3(GetMousePos().x-_offset.x,0,GetMousePos().z-_offset.y);// mousePosition - new Vector3(_offset.x,0,_offset.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.CompareTag("notUse")) return;
        if (gameObject.tag == collision.gameObject.tag)
        {
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
            GameManager.Instance.CheckLevelUp();
            collision.gameObject.GetComponent<Panel>()?.Show();
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameObject explosion = Instantiate(particleVFX, transform.position, transform.rotation);
            Destroy(explosion, .75f);
        }
    }

    private void OnMouseUp()
    {
        transform.position = startPos;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    Vector3 mousePos;
    private Vector3 GetMousePos()
    {
        return mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}