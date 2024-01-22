using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//For the text on the noticeboard on the player ship
public class ShipUI : MonoBehaviour
{
     public TextMeshProUGUI boardText;

    private List<string> boardTextList = new List<string>();

    public bool instructionsShownDone=false;
    public bool enemyApproaching=false;
    public bool isEnemyDead = false;
    public bool atDestinationPoints = false;
    public bool atDanger = false;
    public bool outOfBound = false;
    public bool isDead = false;

    private void Start()
    {
        StartCoroutine(Delay());
    }


    private IEnumerator Delay()
    {
        boardText.text = @"
Game starts in 10s";
        boardText.alignment = TextAlignmentOptions.Center;
        boardText.fontSize = 19.15f;
        int countdown = 10;
        while (countdown > 7)
        {
            boardText.text = @"
Aircraft is getting ready";
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        StartCoroutine(showInstructions());
    }
    private IEnumerator showInstructions()
    {
        boardText.text =
        @"[Controls]:
- Push the throttle to speed up
- Move the handle adjust direction
- Activate handle to shoot enemies";
        boardText.alignment = TextAlignmentOptions.TopLeft;
        boardText.fontSize = 12f;
        yield return new WaitForSeconds(5f);
        StartCoroutine(showGoals());
    }

    private IEnumerator showGoals()
    {
        boardText.text =
  @"[Goals]:
-Travel to the destination
- Fight enemies' space craft";
        boardText.alignment = TextAlignmentOptions.TopLeft;
        boardText.fontSize = 12f;
        yield return new WaitForSeconds(5f);
        boardText.text = "\nEnjoy Piloting!";
        boardText.alignment = TextAlignmentOptions.Center;
        boardText.fontSize = 19.15f;
        instructionsShownDone = true;
    }

    public void showArriving(float estimatedDistance)
    {
        if (instructionsShownDone == true)
        {
            if (atDestinationPoints == true)
            {
                boardText.text = @"
You are almost there.
Autopilot on";
                boardText.color = Color.blue;
            }
            else if (atDanger == true)
            {
                boardText.text = "Your aircraft is at risk";
                atDanger = false;
                boardText.color = Color.red;
            }
            else if (isDead == true)
            {
                boardText.text = @"Your aircraft has fallen. 
You are dead";
                atDanger = false;
                boardText.color = Color.red;
                isDead = false;
            }
            else if (outOfBound == true)
            { 
                outOfBound = false;
                boardText.color = Color.red;
            }
            else
            {
                boardText.text = @"
"+estimatedDistance + " left";
                if (isEnemyDead == true)
                {
                    StartCoroutine(showEnemyHere());
                    return;
                }
                if (enemyApproaching == true)
                {
                    boardText.text += 
                        @"
Enemy nearby";
                    enemyApproaching = false;
                  
                }  
                boardText.color = Color.blue;
            }
            boardText.alignment = TextAlignmentOptions.Center;
            boardText.fontSize = 19.15f;
        }
    }

    private IEnumerator showEnemyHere()
    {
        boardText.text += @"

Enemy is falling";
        boardText.color = Color.red;
        yield return new WaitForSeconds(5f);
        isEnemyDead = false;
    }

}
