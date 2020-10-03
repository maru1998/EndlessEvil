using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public GameObject Panel;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            Panel.SetActive(!Panel.activeSelf);
        }
    }
}
