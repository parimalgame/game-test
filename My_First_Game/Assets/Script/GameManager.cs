using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GridLayoutGroup glp_game;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("layout") == 3)
        {
            glp_game.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            glp_game.constraintCount = 5;
            glp_game.cellSize = new Vector2(175, 262);
        }else if(PlayerPrefs.GetInt("layout") == 2)
        {
            glp_game.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            glp_game.constraintCount = 4;
            glp_game.cellSize = new Vector2(210, 315);
        }
        else if (PlayerPrefs.GetInt("layout") == 1)
        {
            glp_game.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            glp_game.constraintCount = 3;
            glp_game.cellSize = new Vector2(210, 315);
        }
        else if (PlayerPrefs.GetInt("layout") == 0)
        {
            glp_game.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            glp_game.constraintCount = 2;
            glp_game.cellSize = new Vector2(210, 315);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
