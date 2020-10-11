using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum PlayerTurn
    {
        PLAYER1,
        PLAYER2,
    }
    public Text Player1Points, Player2Points;
    public Text WinnerText;
    static int Player1Score;
    static int Player2Score;
    public PlayerTurn playerTurn;
    public GameObject P1Indication,P2Indication;
    public Sprite Player1_vertical, Player1_horizontal, Player1_Box;
    public Sprite Player2_vertical, Player2_horizontal, Player2_Box;

    int BoxCount = Gamesetup.board_Height * Gamesetup.board_Width;
    void Start()
    {
        Player1Score = 0;
        Player2Score = 0;
        Player1Points.text = "Player 1 point: " + Player1Score;
        Player2Points.text = "Player 1 point: " + Player2Score;

        if (Random.Range(0f, 1f) < 0.5f)
        {
            P1Indication.SetActive(true);
            P2Indication.SetActive(false);
            playerTurn = PlayerTurn.PLAYER1;
        }
        else
        {
            P2Indication.SetActive(true);
            P1Indication.SetActive(false);
            playerTurn = PlayerTurn.PLAYER2;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        play();
        if(Player1Score + Player2Score == BoxCount)
        {
            if(Player1Score == Player2Score)
            {
                WinnerText.text = "GAME DRAW";
            }else
            {
                if (Player1Score > Player2Score)
                {
                    WinnerText.text = "Player 1 is winner";

                }
                else
                {
                    WinnerText.text = "Player 2 is winner";
                }
            }
        }
    }


    void play()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float xpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float ypoint = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            bool is_empty_vertical = false;
            bool is_empty_horizontal = false;
            bool extra_turn = false;
            Vector2 originPT = new Vector2(xpoint, ypoint);
            RaycastHit2D hit = Physics2D.Raycast(originPT, Vector2.zero, 0);


            if (hit.collider != null)
            {
                is_empty_vertical = hit.transform.gameObject.tag.Equals("empty vertical");
                is_empty_horizontal = hit.transform.gameObject.tag.Equals("empty horizontal");
                if (is_empty_vertical || is_empty_horizontal)
                {
                    int x_index = 0;
                    int y_index = 0;
                    // find index of transform in array matching hit.transform

                    for (int y = 0; y <= Gamesetup.board_Height; y++)

                        for (int x = 0; x <= Gamesetup.board_Width; x++)
                        {
                            if (y != Gamesetup.board_Height && hit.transform == Gamesetup.vertical_line[x, y])
                            {
                                x_index = x;
                                y_index = y;
                            }

                            else
                           if (x != Gamesetup.board_Width && hit.transform == Gamesetup.horizontal_line[x, y])
                            {
                                x_index = x;
                                y_index = y;
                            }
                        }

                    //Rule Checking code

                    if (this.playerTurn == PlayerTurn.PLAYER1)
                    {
                        if (is_empty_horizontal)
                        {
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Player1_horizontal;
                            //player has played this Line
                            hit.transform.tag = "Is play";
                            // y_index = 0 do not have the top bot
                            if (y_index != 0)
                            {
                                if (CheckTopBox(x_index, y_index))
                                {
                                    Player1Score++;
                                    Player1Points.text = "Player1 Point: " + Player1Score;
                                    Gamesetup.boxs[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = Player1_Box;
                                    Gamesetup.boxs[x_index, y_index - 1].tag = "mark box";
                                    Gamesetup.boxs[x_index, y_index - 1].transform.GetChild(0).GetComponent<TextMesh>().text = "P1";
                                    extra_turn = true;
                                }
                            }

                            if (y_index != Gamesetup.board_Height)

                            {
                                if (CheckDownBox(x_index, y_index))
                                {
                                    Player1Score++;
                                    Player1Points.text = "Player1 Point: " + Player1Score;
                                    Gamesetup.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Player1_Box;
                                    Gamesetup.boxs[x_index, y_index].tag = "mark box";
                                    Gamesetup.boxs[x_index, y_index].transform.GetChild(0).GetComponent<TextMesh>().text = "P1";
                                    extra_turn = true;
                                }
                            }
                        }

                        else
                        if (is_empty_vertical)
                        {
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Player1_vertical;
                            hit.transform.tag = "Is play";
                            if (x_index != 0)
                            {
                                if (CheckLeftBox(x_index, y_index))
                                {
                                    Player1Score++;
                                    Player1Points.text = "Player1 Point: " + Player1Score;
                                    Gamesetup.boxs[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = Player1_Box;
                                    Gamesetup.boxs[x_index - 1, y_index].tag = "mark box";
                                    Gamesetup.boxs[x_index - 1, y_index].transform.GetChild(0).GetComponent<TextMesh>().text = "P1";
                                    extra_turn = true;
                                }
                            }

                            if (x_index != Gamesetup.board_Width)
                            {
                                if (CheckRightBox(x_index, y_index))
                                {
                                    Player1Score++;
                                    Player1Points.text = "Player1 Point: " + Player1Score;
                                    Gamesetup.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Player1_Box;
                                    Gamesetup.boxs[x_index, y_index].tag = "mark box";
                                    Gamesetup.boxs[x_index, y_index].transform.GetChild(0).GetComponent<TextMesh>().text = "P1";
                                    extra_turn = true;
                                }
                            }
                        }

                        if (!extra_turn)
                        {
                            playerTurn = PlayerTurn.PLAYER2;
                            P1Indication.SetActive(false);
                            P2Indication.SetActive(true);
                        }
                    }

                    // behavior like player1

                    else if(this.playerTurn == PlayerTurn.PLAYER2)

                   {
                        Debug.Log(x_index + "hit" + y_index);
                        if (is_empty_horizontal)
                        {
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Player2_horizontal;
                            hit.transform.tag = "Is play";
                            if (y_index != 0)
                            {
                                if (CheckTopBox(x_index, y_index))
                                {
                                    Player2Score++;
                                    Player2Points.text = "Player2 Point: " + Player2Score;
                                    Gamesetup.boxs[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = Player2_Box;
                                    Gamesetup.boxs[x_index, y_index - 1].tag = "mark box";
                                    Gamesetup.boxs[x_index, y_index - 1].transform.GetChild(0).GetComponent<TextMesh>().text = "P2";
                                    extra_turn = true;
                                }
                            }

                            if (y_index != Gamesetup.board_Height)
                            {
                                if (CheckDownBox(x_index, y_index))
                                {
                                    Player2Score++;
                                    Player2Points.text = "Player2 Point: " + Player2Score;
                                    Gamesetup.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Player2_Box;
                                    Gamesetup.boxs[x_index, y_index].tag = "mark box";
                                    Gamesetup.boxs[x_index, y_index].transform.GetChild(0).GetComponent<TextMesh>().text = "P2";
                                    extra_turn = true;
                                }
                            }
                        }

                        else
                        if (is_empty_vertical)
                        {
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Player2_vertical;
                            hit.transform.tag = "Is play";
                            if (x_index != 0)
                            {
                                if (CheckLeftBox(x_index, y_index))
                                {
                                    Player2Score++;
                                    Player2Points.text = "Player2 Point: " + Player2Score;
                                    Gamesetup.boxs[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = Player2_Box;
                                    Gamesetup.boxs[x_index - 1, y_index].tag = "mark box";
                                    Gamesetup.boxs[x_index - 1, y_index].transform.GetChild(0).GetComponent<TextMesh>().text = "P2";
                                    extra_turn = true;
                                }
                            }

                            if (x_index != Gamesetup.board_Width)
                            {
                                if (CheckRightBox(x_index, y_index))
                                {
                                    Player2Score++;
                                    Player2Points.text = "Player2 Point: " + Player2Score;
                                    Gamesetup.boxs[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Player2_Box;
                                    Gamesetup.boxs[x_index, y_index].tag = "mark box";
                                    Gamesetup.boxs[x_index, y_index].transform.GetChild(0).GetComponent<TextMesh>().text = "P2";
                                    extra_turn = true;
                                }
                            }
                        }
                        if (!extra_turn)
                        {
                            playerTurn = PlayerTurn.PLAYER1;
                            P1Indication.SetActive(true);
                            P2Indication.SetActive(false);
                        }
                    }
                }
            }
        }
    }
    
    bool CheckRightBox(int x_tile, int y_tile)
    {
        bool top, down, right;
        bool result = false;
        top = Gamesetup.horizontal_line[x_tile, y_tile].CompareTag("Is play");
        down = Gamesetup.horizontal_line[x_tile, y_tile + 1].CompareTag("Is play");
        right = Gamesetup.vertical_line[x_tile + 1, y_tile].CompareTag("Is play");
        if (top && down && right)
        {
            result = true;
        }

        return (false || result);
    }

    bool CheckLeftBox(int x_tile, int y_tile)
    {
        bool top, down, left;
        bool result = false;
        top = Gamesetup.horizontal_line[x_tile - 1, y_tile].CompareTag("Is play");
        down = Gamesetup.horizontal_line[x_tile - 1, y_tile + 1].CompareTag("Is play");
        left = Gamesetup.vertical_line[x_tile - 1, y_tile].CompareTag("Is play");
        if (top && down && left)
        {
            result = true;
        }

        return (false || result);
    }

    bool CheckTopBox(int x_tile,int y_tile)
    {
        bool top, right, left;
        bool result = false;
        top = Gamesetup.horizontal_line[x_tile, y_tile-1].CompareTag("Is play");
        left = Gamesetup.vertical_line[x_tile, y_tile - 1].CompareTag("Is play");
        right = Gamesetup.vertical_line[x_tile + 1, y_tile-1].CompareTag("Is play");
        if (top && left && right)
        {
            result = true;
        }

        return (false || result);
    }
    bool CheckDownBox(int x_tile, int y_tile)
    {
        bool down, right, left;
        bool result = false;
        down = Gamesetup.horizontal_line[x_tile, y_tile+1].CompareTag("Is play");
        left = Gamesetup.vertical_line[x_tile, y_tile ].CompareTag("Is play");
        right = Gamesetup.vertical_line[x_tile + 1, y_tile].CompareTag("Is play");
        if (left && down && right)
        {
            result = true;
        }
        return (false || result);
    }

 
}
