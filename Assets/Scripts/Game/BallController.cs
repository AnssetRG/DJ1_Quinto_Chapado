using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private int touchedFloor = 0;
    private bool touchedRam;
    private bool touchProtector;
    private bool check;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Holder")
        {
            if (Random.Range(0, 2) > 1)
            {
                GameController.instance.playSound(3);
            }
            else
            {
                GameController.instance.playSound(4);
            }
        }
        if (other.gameObject.tag == "Ram")
        {
            if (Random.Range(0, 2) > 1)
            {
                GameController.instance.playSound(2);
            }
            else
            {
                GameController.instance.playSound(5);
            }
        }
        if (other.gameObject.tag == "Ground")
        {
            touchedFloor++;
            if (touchedFloor <= 3)
            {
                if (Random.Range(0, 2) > 1)
                {
                    GameController.instance.playSound(3);
                }
                else
                {
                    GameController.instance.playSound(4);
                }
            }
        }
        if (other.gameObject.tag == "Table")
        {
            touchedRam = true;
            if (Random.Range(0, 2) > 1)
            {
                GameController.instance.playSound(2);
            }
            else
            {
                GameController.instance.playSound(5);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Protector")
        {
            touchProtector = true;
        }
        if (other.tag == "Net")
        {
            if (!touchProtector)
            {
                print("ENCESTO");
                if (touchedRam)
                {
                    //ganas 1 punto
                    GameController.instance.incrementBalls(1);
                }
                else
                {
                    //ganas 2 puntos
                    GameController.instance.incrementBalls(2);
                }
                GameController.instance.playSound(1);
            }
        }
    }

    void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            GameController.instance.checkGameOver();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void checkDestruction()
    {
        if (!check)
        {
            check = true;
            Destroy(gameObject);
            GameController.instance.checkGameOver();
        }
    }
}
