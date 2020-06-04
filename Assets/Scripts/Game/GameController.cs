using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> balls;
    private float minX = -4.7f, maxX = 8f, minY = -2.5f, maxY = 1.5f;
    public AudioSource audio;
    private float volume = 1.0f;
    [SerializeField]
    private AudioClip rim_hit1, rim_hit2, bounce1, bounce2, net_sound;
    private int index = 0;
    private int total_balls = 10;
    public static GameController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void setInitialBalls()
    {
        //setear balls en caja de texto
    }

    public void incrementBalls(int increment)
    {
        total_balls += increment;
        if (total_balls > 10)
        {
            total_balls = 10;
        }
    }
    void Start()
    {
        CreateBall();
    }

    void CreateBall()
    {
        int index = PlayerPrefs.GetInt("ball", 0);
        Instantiate(balls[index], new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f), Quaternion.identity);
    }

    public void decrementBalls()
    {
        total_balls--;
        //actualizar caja de texto
    }

    public void playSound(int index)
    {
        switch (index)
        {
            case 1:
                audio.PlayOneShot(net_sound, volume);
                break;
            case 2:
                if (Random.Range(0, 2) > 1)
                {
                    audio.PlayOneShot(rim_hit1, volume);
                }
                else
                {
                    audio.PlayOneShot(rim_hit2, volume);
                }
                break;
            case 3:
                if (Random.Range(0, 2) > 1)
                {
                    audio.PlayOneShot(bounce1, volume);
                }
                else
                {
                    audio.PlayOneShot(bounce2, volume);
                }
                break;
            case 4:
                if (Random.Range(0, 2) > 1)
                {
                    audio.PlayOneShot(bounce1, volume * 0.5f);
                }
                else
                {
                    audio.PlayOneShot(bounce2, volume * 0.5f);
                }
                break;
            case 5:
                if (Random.Range(0, 2) > 1)
                {
                    audio.PlayOneShot(rim_hit1, volume * 0.5f);
                }
                else
                {
                    audio.PlayOneShot(rim_hit2, volume * 0.5f);
                }
                break;
        }
    }
}
