using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallChooseController : MonoBehaviour
{
    [SerializeField]
    void Start()
    {
        GameObject[] btn = GameObject.FindGameObjectsWithTag("Balls");
        Debug.Log(btn.Length);
        Button ballbtn;
        foreach (GameObject item in btn)
        {
            ballbtn = item.GetComponent<Button>();
            ballbtn.onClick.AddListener(() => ChooseBall());
        }
    }

    void ChooseBall()
    {
        int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.gameObject.name);
        PlayerPrefs.SetInt("ball", index);
        Debug.Log(index);
    }
}
