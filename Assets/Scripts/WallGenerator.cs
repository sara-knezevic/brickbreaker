using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=UnityEngine.Random;

public class WallGenerator : MonoBehaviour
{
    public GameObject brick;
    public GameObject twoXbrick;
    public GameObject fourXbrick;

    private static int mapX = 65;
    private static int mapY = 65;
    private int[, ] map = new int[mapX, mapY];

    private Spiral spiral = new Spiral();
    private Vector2 currentPosition = new Vector2(0, 0);

    bool[] ULDR = new bool[4];
    String dir = null;

    void Update() {}

    // Start is called before the first frame update
    public void generateWall()
    {
        for (int i = 0; i < 100; i++) {
            generateBrick();
        }
    }

    void generateBrick() {
        int mapPositionX = (mapX - 1) / 2 + (int)currentPosition.x;
        int mapPositionY = (mapY - 1) / 2 + (int)currentPosition.y;

        // Check if there is an object at current position
        if (!ContainsObject(mapPositionX, mapPositionY))
        {
            ULDR = CheckNeighbors(mapPositionX, mapPositionY);

            int emptyCount = 0;
            foreach (bool d in ULDR)
            {
                if (d == true) emptyCount++;
            }

            if (emptyCount == 0) { // only 1x1
                generateSmall(currentPosition);
            } else if (emptyCount == 1) { // 1x1 or 2x1
                generateSmallAndMedium(currentPosition, ULDR);
            } else if (emptyCount >= 2) { // 1x1 or 2x1 or 2x2
                generateAny(currentPosition, ULDR);
            }
        }

        currentPosition = spiral.NextPoint();
    }

    private bool[] CheckNeighbors(int mapPositionX, int mapPositionY) 
    {
        ULDR = new bool[4];

        // Check if cell above is available
        if (InRange(mapPositionX, mapPositionY + 1) && !ContainsObject(mapPositionX, mapPositionY + 1)) ULDR[0] = true;
        
        // Check if cell left is available
        if (InRange(mapPositionX + 1, mapPositionY) && !ContainsObject(mapPositionX + 1, mapPositionY)) ULDR[1] = true;

        // Check if cell down is available
        if (InRange(mapPositionX, mapPositionY - 1) && !ContainsObject(mapPositionX, mapPositionY - 1)) ULDR[2] = true;

        // Check if cell right is available
        if (InRange(mapPositionX - 1, mapPositionY) && !ContainsObject(mapPositionX - 1, mapPositionY)) ULDR[3] = true;

        return ULDR;
    }

    private bool ContainsObject(int x, int y) 
    {
       return map[x, y] == 1; // true if there is an object
    }

    private bool InRange(int x, int y)
    {
       return (x >= 0 && y >= 0 && x <= map.GetUpperBound(0) && y <= map.GetUpperBound(1));
    }

    private void generateSmall(Vector2 position)
    {
        Instantiate(brick, position, Quaternion.identity);

        int mapPositionX = (mapX - 1) / 2 + (int)position.x;
        int mapPositionY = (mapY - 1) / 2 + (int)position.y;

        map[mapPositionX, mapPositionY] = 1;
    }

    private void generateMedium(Vector2 position, String direction)
    {
        int mapPositionX = (mapX - 1) / 2 + (int)position.x;
        int mapPositionY = (mapY - 1) / 2 + (int)position.y;

        switch (direction)
        {
            case "U":
                Instantiate(twoXbrick, new Vector2(position.x, position.y + 0.5f), Quaternion.Euler(0, 180, -90));

                map[mapPositionX, mapPositionY] = 1;
                map[mapPositionX, mapPositionY + 1] = 1;
                break;
            case "L":
                Instantiate(twoXbrick, new Vector2(position.x + 0.5f, position.y), Quaternion.Euler(0, 0, 0));

                map[mapPositionX, mapPositionY] = 1;
                map[mapPositionX + 1, mapPositionY] = 1;
                break;
            case "D":
                Instantiate(twoXbrick, new Vector2(position.x, position.y - 0.5f), Quaternion.Euler(0, 180, -90));

                map[mapPositionX, mapPositionY] = 1;
                map[mapPositionX, mapPositionY - 1] = 1;
                break;
            case "R":
                Instantiate(twoXbrick, new Vector2(position.x - 0.5f, position.y), Quaternion.Euler(0, 0, 0));

                map[mapPositionX, mapPositionY] = 1;
                map[mapPositionX - 1, mapPositionY] = 1;
                break;
        }
    }

    private void generateBig(Vector2 position, String direction)
    {
        int mapPositionX = (mapX - 1) / 2 + (int)position.x;
        int mapPositionY = (mapY - 1) / 2 + (int)position.y;

        switch (direction)
        {
            case "UL":
                Instantiate(fourXbrick, new Vector2(position.x + 0.5f, position.y + 0.5f), Quaternion.Euler(0, 0, 0));

                map[mapPositionX, mapPositionY] = 1;
                map[mapPositionX, mapPositionY + 1] = 1;
                map[mapPositionX + 1, mapPositionY] = 1;
                map[mapPositionX + 1, mapPositionY + 1] = 1;
                break;
            case "DL":
                Instantiate(fourXbrick, new Vector2(position.x + 0.5f, position.y - 0.5f), Quaternion.Euler(0, 0, 0));

                map[mapPositionX, mapPositionY] = 1;
                map[mapPositionX + 1, mapPositionY] = 1;
                map[mapPositionX, mapPositionY - 1] = 1;
                map[mapPositionX + 1, mapPositionY - 1] = 1;
                break;
            case "DR":
                Instantiate(fourXbrick, new Vector2(position.x - 0.5f, position.y - 0.5f), Quaternion.Euler(0, 0, 0));

                map[mapPositionX, mapPositionY] = 1;
                map[mapPositionX, mapPositionY - 1] = 1;
                map[mapPositionX - 1, mapPositionY] = 1;
                map[mapPositionX - 1, mapPositionY - 1] = 1;
                break;
            case "UR":
                Instantiate(fourXbrick, new Vector2(position.x - 0.5f, position.y+ 0.5f), Quaternion.Euler(0, 0, 0));

                map[mapPositionX, mapPositionY] = 1;
                map[mapPositionX - 1, mapPositionY] = 1;
                map[mapPositionX, mapPositionY + 1] = 1;
                map[mapPositionX - 1, mapPositionY + 1] = 1;
                break;
        }
    }

    private void generateSmallAndMedium(Vector2 position, bool[] ULDR)
    {
        int randBrick = Random.Range(0, 2);
        dir = null;

        if (randBrick == 0) {
            generateSmall(position);
        } else if (randBrick == 1) {
            
            if (ULDR[0] == true) {
                // upper cell is empty
                dir = "U";
            } else if (ULDR[1] == true) {
                // left cell is empty
                dir = "L";
            } else if (ULDR[2] == true) {
                // down cell is empty
                dir = "D";
            } else if (ULDR[3] == true) {
                // right cell is empty
                dir = "R";
            }

            generateMedium(position, dir);
        }
    }

    private void generateAny(Vector2 position, bool[] ULDR)
    {
        int randBrick = Random.Range(0, 3);
        dir = null;

        if (randBrick == 0) {
            generateSmall(position);
        } else if (randBrick == 1) {
            
            if (ULDR[0] == true) {
                // upper cell is empty
                dir = "U";
            } else if (ULDR[1] == true) {
                // left cell is empty
                dir = "L";
            } else if (ULDR[2] == true) {
                // down cell is empty
                dir = "D";
            } else if (ULDR[3] == true) {
                // right cell is empty
                dir = "R";
            }

            generateMedium(position, dir);
        } else if (randBrick == 2) {
            if (ULDR[0] == true && ULDR[1] == true) {
                // upper left cell is empty
                dir = "UL";
            } else if (ULDR[1] == true && ULDR[2] == true) {
                // lower left cell is empty
                dir = "DL";
            } else if (ULDR[2] == true && ULDR[3] == true) {
                // down right cell is empty
                dir = "DR";
            } else if (ULDR[3] == true && ULDR[0] == true) {
                // right upper cell is empty
                dir = "UR";
            }

            generateBig(position, dir);
        }
    }
}
