﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private int touchedFloor = 0;
    private bool touchedRam;
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
}