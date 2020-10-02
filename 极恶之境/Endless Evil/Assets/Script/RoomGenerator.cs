using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    //枚举
    public enum Direction { up, down, left, right};
    public Direction direction;

    //房间的数据
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    GameObject endRoom;

    //房间位置控制
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;

    public walls walls;

    public List<Room> rooms = new List<Room>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //生成之后改变生成点的位置
            ChangeSpawnPoint();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        rooms[roomNumber - 1].GetComponent<SpriteRenderer>().color = endColor;
        endRoom = rooms[roomNumber - 1].gameObject;

        foreach(var room in rooms)
        {
            SetupRoom(room, room.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //改变生成点的位置
    private void ChangeSpawnPoint()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }

    private void SetupRoom(Room room, Vector3 roomPosition)
    {
        room.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        room.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        room.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        room.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);

        room.UpdateRoom(xOffset, yOffset);

        switch (room.doorNumber)
        {
            case 1:
                if (room.roomUp)
                    Instantiate(walls.singleU, roomPosition, Quaternion.identity);
                if (room.roomDown)
                    Instantiate(walls.singleD, roomPosition, Quaternion.identity);
                if (room.roomLeft)
                    Instantiate(walls.singleL, roomPosition, Quaternion.identity);
                if (room.roomRight)
                    Instantiate(walls.singleR, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (room.roomLeft && room.roomUp)
                    Instantiate(walls.doubleUL, roomPosition, Quaternion.identity);
                if (room.roomLeft && room.roomRight)
                    Instantiate(walls.doubleLR, roomPosition, Quaternion.identity);
                if (room.roomLeft && room.roomDown)
                    Instantiate(walls.doubleDL, roomPosition, Quaternion.identity);
                if (room.roomUp && room.roomRight)
                    Instantiate(walls.doubleUR, roomPosition, Quaternion.identity);
                if (room.roomUp && room.roomDown)
                    Instantiate(walls.doubleUD, roomPosition, Quaternion.identity);
                if (room.roomRight && room.roomDown)
                    Instantiate(walls.doubleDR, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (room.roomLeft && room.roomUp && room.roomRight)
                    Instantiate(walls.tripleULR, roomPosition, Quaternion.identity);
                if (room.roomLeft && room.roomRight && room.roomDown)
                    Instantiate(walls.tripleDLR, roomPosition, Quaternion.identity);
                if (room.roomDown && room.roomUp && room.roomRight)
                    Instantiate(walls.tripleUDR, roomPosition, Quaternion.identity);
                if (room.roomLeft && room.roomUp && room.roomDown)
                    Instantiate(walls.tripleUDL, roomPosition, Quaternion.identity);
                break;
            case 4:
                if (room.roomLeft && room.roomUp && room.roomRight && room.roomDown)
                    Instantiate(walls.fourUDLR, roomPosition, Quaternion.identity);
                break;
        }
    }
}

[System.Serializable]
public class walls
{
    public GameObject singleL, singleR, singleU, singleD;
    public GameObject doubleUL, doubleUR, doubleUD, doubleDL, doubleDR, doubleLR;
    public GameObject tripleUDL, tripleUDR, tripleULR, tripleDLR;
    public GameObject fourUDLR;
}
