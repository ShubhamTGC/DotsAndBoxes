using UnityEngine;

using System.Collections;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Gamesetup : MonoBehaviour
{
    public Transform vertical;

    public Transform horizontal;
    public Transform VerticleDot;
    public Transform HorizontalDot;

    public Transform box;

    // Main camera
    public Camera mainCam;

    // Board Size
    // modify later to let user config
    public static int board_Width = 4;
    public static int board_Height = 3;

    // Structure of game

    public static Transform[,] vertical_line;

    public static Transform[,] horizontal_line;

    public static Transform[,] boxs;
    public static Transform[,] VerticleCircles;
    public static Transform[,] HorizontalCircles;
    private int counter = 0;


    private RaycastHit2D hit;
    private Vector2 mousepos;
    private float sideBuffer, DotBuffer;
    private float ScreenBufffer = 0f;

    public Text TouchBarText;
    private static GameMenu GameInstance;
    void Start()
    {
      
        GameInstance = GameMenu.MainGame;
        Debug.Log("board hieght " + GameInstance.Height + "width " + GameInstance.width);
        board_Height = GameInstance.Height;
        board_Width = GameInstance.width;
        if (board_Width > 7 && board_Height > 8)  // 8 x 9
        {
            ScreenBufffer = 1.25f;
            DotBuffer = 0.08f;
            SetelementsSize(0.5f,0.5f);
        }else if(board_Width == 5 && board_Height == 6) // 5 x 6
        {
            ScreenBufffer = 0.7f;
            DotBuffer = 0.1f;
            SetelementsSize(0.7f,0.8f);
        }
        else if(board_Width == 4 && board_Height == 5)  // 4 x 5
        {
            ScreenBufffer = 0.35f;
            DotBuffer = 0.1f;
            SetelementsSize(0.8f,0.8f);
        }
        else if(board_Width == 6 && board_Height == 7)  // 6 x 7
        {
            ScreenBufffer = 0.9f;
            DotBuffer = 0.1f;
            SetelementsSize(0.6f, 0.6f);
        }
        else if(board_Width == 7 && board_Height == 8)  // 7 x 8
        {
            ScreenBufffer = 1f;
            DotBuffer = 0.08f;
            SetelementsSize(0.5f, 0.6f);
        }
        else if (board_Width == 3 && board_Height == 4)    // 3 x 4
        {
            ScreenBufffer = 0f;
            DotBuffer = 0.15f;
            SetelementsSize(1f, 1f);
        }
        CreateBoard(board_Width, board_Height);
    }

    void SetelementsSize(float value,float dotvalue)
    {
        sideBuffer = value / 2;
       // DotBuffer = value / 2;
        vertical.transform.localScale = horizontal.transform.localScale =  box.transform.localScale= new Vector3(value, value, value);
        VerticleDot.transform.localScale = new Vector3(dotvalue, dotvalue, dotvalue);


    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 screenpt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mousepos = new Vector2(screenpt.x, screenpt.y);
        //    hit = Physics2D.Raycast(mousepos, Vector2.zero);
        //    Debug.Log("object name " + hit.collider.gameObject.name);
        //    TouchBarText.text = hit.collider.gameObject.name;


        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    Debug.Log("object name " + hit.collider.gameObject.name);
        //    TouchBarText.text = "";
        //}
    }

    void CreateBoard(int board_Width, int board_Height)

    {

        float vertical_Line_Width = vertical.GetComponent<SpriteRenderer>().bounds.size.x;

        float vertical_Line_Height = vertical.GetComponent<SpriteRenderer>().bounds.size.y;



        float horizontal_Line_Width = horizontal.GetComponent<SpriteRenderer>().bounds.size.x;

        float horizontal_Line_Height = horizontal.GetComponent<SpriteRenderer>().bounds.size.y;



        float box_Width = box.GetComponent<SpriteRenderer>().bounds.size.x;

        float box_Height = box.GetComponent<SpriteRenderer>().bounds.size.y;



        float startX = Screen.width / 2;

        float startY = Screen.height / 2;

        //initial game object structure

        vertical_line = new Transform[board_Width + 1, board_Height];

        horizontal_line = new Transform[board_Width, board_Height + 1];

        VerticleCircles = new Transform[board_Width + 1, board_Height + 1];

        HorizontalCircles = new Transform[board_Width, board_Height + 1];

        boxs = new Transform[board_Width, board_Height];
       
        // xCor, yCor is the center cordiante of box[0,0]

        float yCor = mainCam.ScreenToWorldPoint(new Vector3(0, startY, 0f)).y;
        float ypoint = mainCam.ScreenToWorldPoint(new Vector3(0, startY, 0f)).y;
        // make board in middle of screen by yCor

        yCor = yCor + (board_Height * vertical_Line_Height + (board_Height + 1) * horizontal_Line_Height/2 + sideBuffer) / 2;
        ypoint = ypoint + (board_Height * vertical_Line_Height + (board_Height + 1) * horizontal_Line_Height/2 + sideBuffer) /2;

        // Create the Board

        for (int y = 0; y <= board_Height; y++)
        {

            float xCor = mainCam.ScreenToWorldPoint(new Vector3(startX, 0, 0f)).x;
            float xpoint = mainCam.ScreenToWorldPoint(new Vector3(startX, 0, 0f)).x;

            // make board in middle of screen by xCor
            xCor = xCor - (board_Width * horizontal_Line_Width / 2 + ScreenBufffer + (board_Width + 1) * vertical_Line_Width) / 2;
            xpoint = xpoint - (board_Width * horizontal_Line_Width / 2 + ScreenBufffer + (board_Width + 1) * vertical_Line_Width)/2;

            // check.text = "x =  " + xCor + " y = " + yCor;

            for (int x = 0; x <= board_Width; x++)
            {
                Vector3 center_of_Box = new Vector3(xCor, yCor, 0f);

                Vector3 center_of_Vertical = new Vector3(xCor - horizontal_Line_Width / 2 - vertical_Line_Width / 2, yCor, 0f);
                Vector3 center_of_verticldot = new Vector3(xpoint - horizontal_Line_Width /2 - vertical_Line_Width / 2,ypoint + (vertical_Line_Height / 2)+ DotBuffer , 0f);

                Vector3 center_of_Horizontal = new Vector3(xCor, yCor + horizontal_Line_Height / 2 + vertical_Line_Height / 2, 0f);
                Vector3 center_of_horizontalDot = new Vector3(xpoint  + (vertical_Line_Height / 2) + DotBuffer, ypoint + horizontal_Line_Height/2  + vertical_Line_Height/2 , 0f);

                if (x != board_Width && y != board_Height)
                {
                    boxs[x, y] = Instantiate(box, center_of_Box, Quaternion.identity) as Transform;
                    boxs[x, y].name = "box " + counter;
                }

                if (y != board_Height)
                {
                    vertical_line[x, y] = Instantiate(vertical, center_of_Vertical, Quaternion.identity) as Transform;
                    vertical_line[x, y].name = "vertical" + counter;
                }
                if(y != board_Height + 1)
                {
                    VerticleCircles[x, y] = Instantiate(VerticleDot, center_of_verticldot, Quaternion.identity) as Transform;
                    VerticleCircles[x, y].name = "verticalDot" + counter;
                }

                if (x != board_Width)
                {
                    horizontal_line[x, y] = Instantiate(horizontal, center_of_Horizontal, Quaternion.identity) as Transform;
                   // HorizontalCircles[x, y] = Instantiate(HorizontalDot, center_of_horizontalDot, Quaternion.identity) as Transform;
                    horizontal_line[x, y].name = "Horizontal" + counter;
                   // HorizontalCircles[x, y].name = "HorizontalDot" + counter;
                }
                counter++;
                xCor += box_Width + vertical_Line_Width;
                xpoint += box_Width + vertical_Line_Width;
            }
            yCor -= box_Height + horizontal_Line_Height;
            ypoint -= box_Height + horizontal_Line_Height;
        }
    }


    public void Exit()
    {
        board_Height = 0;
        board_Width = 0;
        Destroy(GameInstance);
        SceneManager.LoadScene(0);
    }
}
