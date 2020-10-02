using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorTop, doorDown;

    public bool roomLeft, roomRight, roomUp, roomDown;

    public Text text;
    public int stepTostart;
    public int doorNumber;

    // Start is called before the first frame update
    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorTop.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRoom(float x, float y)
    {
        stepTostart = (int)(math.abs(transform.position.x / x) + math.abs(transform.position.y / y));

        text.text = stepTostart + "";

        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomLeft)
            doorNumber++;
        if (roomRight)
            doorNumber++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);
        }
    }
}
