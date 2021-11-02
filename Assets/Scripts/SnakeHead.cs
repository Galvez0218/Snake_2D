using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{

    enum Direccion{
        up, 
        down, 
        left, 
        right
        }

    Direccion direccion;

    //parala cola
    public List<Transform> Tail = new List<Transform>();

    public float frameRate = 0.2f;
    public float pasos = 0.25f;

    public GameObject TailPrefab;

    public Vector2 horizontalRange;
    public Vector2 verticalRange;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", frameRate, frameRate);
    }

    void Move()
    {
        lastPos = transform.position;
        Vector3 nextPos = Vector3.zero;
        if(direccion == Direccion.up)
        {
            nextPos = Vector3.up;
        }else if(direccion == Direccion.down)
        {
            nextPos = Vector3.down;
        }
        else if (direccion == Direccion.left)
        {
            nextPos = Vector3.left;
        }
        else if (direccion == Direccion.right)
        {
            nextPos = Vector3.right;
        }

        nextPos *= pasos;
        transform.position += nextPos;

        MoveTail();
    }

    Vector3 lastPos;
    void MoveTail()
    {
        for(int i=0; i<Tail.Count; i++)
        {
            Vector3 temp = Tail[i].position;
            //para cuando el head se mueva, se pondra justo donde estaba la cabeza
            Tail[i].position = lastPos;
            lastPos = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direccion= Direccion.up;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            direccion = Direccion.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direccion = Direccion.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direccion = Direccion.right;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bloque"))
        {
            print("Fin del juego...");
            //Reinicia la escena
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }else if (col.CompareTag("Food"))
        {
            //para que la cola crezca y lacomida cambie de posicicion aleatoriamente
            Tail.Add(Instantiate(TailPrefab, Tail[Tail.Count - 1].position, Quaternion.identity).transform);
            col.transform.position = new Vector2(Random.Range(horizontalRange.x, horizontalRange.y), Random.Range(verticalRange.x, verticalRange.y));
        }
    }
}
