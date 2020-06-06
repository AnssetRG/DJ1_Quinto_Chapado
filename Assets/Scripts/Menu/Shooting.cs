using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shooting : MonoBehaviour
{
    public float power = 2.0f;
    public float life = 1.0f;
    public float dead_sense = 25f;
    public int dots = 30;
    private Vector2 startPosition;
    private bool shoot = false, aiming = false, hit_ground = false;
    private GameObject Dots;
    private List<GameObject> projectilePath;
    private Rigidbody2D body;
    private Collider2D myCollider;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }
    void Start()
    {
        Dots = GameObject.Find("dots");
        body.isKinematic = true;
        myCollider.enabled = false;
        startPosition = transform.position;
        //Sacas todos los transforms childs, se convierte en una lista y se consigue los gameobjects de cada child (versión pro sin usar child count)
        projectilePath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        for (int i = 0; i < projectilePath.Count; i++)
        {
            projectilePath[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void Aim()
    {
        if (shoot)
        {
            return;
        }
        if (Input.GetAxis("Fire1") == 1)
        {
            if (!aiming)
            {
                aiming = true;
                startPosition = Input.mousePosition;
                CalculatePath();
                ShowPath(true);
            }
            else
            {
                CalculatePath();
            }
        }
        else if (aiming && !shoot)
        {
            if (inDeadZone(Input.mousePosition) || inReleaseZone(Input.mousePosition))
            {
                aiming = false;
                ShowPath(false);
                return;
            }
            aiming = false;
            shoot = true;
            body.isKinematic = false;
            myCollider.enabled = true;
            body.AddForce(GetForce(Input.mousePosition));
            ShowPath(false);
            //TODO restar pelotas
        }
    }

    //Calcular la trayectoria y separación de los puntos
    void CalculatePath()
    {
        Vector2 vel = GetForce(Input.mousePosition) * Time.fixedDeltaTime / body.mass;
        for (int i = 0; i < projectilePath.Count; i++)
        {
            projectilePath[i].GetComponent<Renderer>().enabled = true;
            float t = i / 30f;
            Vector3 point = PathPoint(transform.position, vel, t);
            point.z = 1.0f;
            projectilePath[i].transform.position = point;
        }
    }

    //Ecuación parabólica MRUV
    Vector2 PathPoint(Vector2 sartP, Vector2 starVel, float t)
    {
        return sartP + starVel * t + 0.5f * Physics2D.gravity * t * t;
    }

    Vector2 GetForce(Vector3 mousePosition)
    {
        return (new Vector2(startPosition.x, startPosition.y) - new Vector2(mousePosition.x, mousePosition.y)) * power;
    }


    bool inReleaseZone(Vector2 mouse)
    {
        return (mouse.x <= 70);
    }


    //Mostrar los puntos o no en el mapa para la trayectoria.
    void ShowPath(bool hide)
    {
        foreach (GameObject dot in projectilePath)
        {
            dot.GetComponent<SpriteRenderer>().enabled = hide;
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground")
        {
            hit_ground = true;
        }
    }

    bool inDeadZone(Vector3 mousePosition)
    {
        return Mathf.Abs(startPosition.x - mousePosition.x) <= dead_sense && Mathf.Abs(startPosition.y - mousePosition.y) <= dead_sense;
    }

    bool inReleaseZone(Vector3 mousePosition)
    {
        return mousePosition.x <= 70;
    }

    void Update()
    {
        Aim();
        //Desaparecer con el tiempo una vez toque el piso
        if (hit_ground)
        {
            life -= Time.deltaTime;
            Color c = GetComponent<Renderer>().material.GetColor("_Color");
            GetComponent<Renderer>().material.SetColor("_Color", new Color(c.r, c.g, c.b, life));
            if (life < 0)
            {
                //crear nueva pelota
            }
        }
    }
}
