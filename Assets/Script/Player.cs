using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum playerSide
    {
        left,
        center,
        right
    }
    public Animator animator;
    [NonSerialized]
    float speedDampTime = 0f;

    float speed = 0;

    const string speedName = "Speed";
    const string winName = "isWin";

    public Vector3 oldPosition;

    private playerSide currSide = playerSide.center;

    bool isSideChange;

    private Collider[] ragDollColider;
    private Rigidbody[] ragDollRigidBody;

    public GamePlayManager gamePlayManager;

   // private Collider myColider;
   // private Rigidbody myRigidBody;

   public bool isResultDeclare = false;

    bool isStartGame = false;

    private void Start()
    {
        ragDollColider = gameObject.GetComponentsInChildren<Collider>();
        ragDollRigidBody = gameObject.GetComponentsInChildren<Rigidbody>();

       // myColider = GetComponent<Collider>();
      //  myRigidBody = GetComponent<Rigidbody>();
       // isGameOver = true;
        setRagdollMode(false);
    }

    public void startGame()
    {
        isStartGame = true;
    }

    private void Update()
    {
        if(!isResultDeclare && isStartGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                oldPosition = Input.mousePosition;
                isSideChange = false;
            }
            else if (Input.GetMouseButton(0))
            {
               /* if (!isSideChange)
                    setSide();*/

                Vector3 diff = Input.mousePosition - oldPosition;

                oldPosition = Input.mousePosition;

                Rotating(diff.x * 0.1f);
            }
            else
            {
                Rotating(0);
            }
            setSpeed(0.01f);
        }
       
    }

    private void setSpeed(float speedVal)
    {
        speed += speedVal;
        speed = Mathf.Clamp(speed, 0, 1);

        animator.SetFloat(speedName, speed, speedDampTime, Time.deltaTime);

        if (speed > 0.1)
        {
            Vector3 pos = transform.position + transform.forward * Time.deltaTime * speed * 5;
            pos.x = Mathf.Clamp(pos.x, -1,1);
            transform.position = pos;
        }
    }

    private void setSide()
    {

        Vector3 diff = Input.mousePosition - oldPosition;
        if (diff.x > 3f)
        {
            //right

            if (currSide == playerSide.left)
            {
                Vector3 currPos = transform.position;
                currPos.x = 0;
                transform.position = currPos;

                currSide = playerSide.center;

                isSideChange = true;
            }
            else if (currSide == playerSide.center)
            {
                Vector3 currPos = transform.position;
                currPos.x = 0.8f;
                transform.position = currPos;
                currSide = playerSide.right;

                isSideChange = true;
            }
        }
        else if (diff.x < -3f)
        {
            //left

            if (currSide == playerSide.right)
            {
                Vector3 currPos = transform.position;
                currPos.x = 0;
                transform.position = currPos;

                currSide = playerSide.center;

                isSideChange = true;
            }
            else if (currSide == playerSide.center)
            {
                Vector3 currPos = transform.position;
                currPos.x = -0.8f;
                transform.position = currPos;
                currSide = playerSide.left;

                isSideChange = true;
            }
        }
    }

    private void setRagdollMode(bool isMode)
    {
       /* for (int i = 0; i < ragDollColider.Length; i++)
        {
            ragDollColider[i].enabled = isMode;
        }*/

        for (int i = 0; i < ragDollRigidBody.Length; i++)
        {
            ragDollRigidBody[i].isKinematic = !isMode;

            if (isMode)
            {
                //ragDollRigidBody[i].AddForce(new Vector3(0, 0, -200));

                ragDollRigidBody[i].AddForce(transform.forward * -200);
            }
        }

        

        animator.enabled = !isMode;
       // myColider.enabled = !isMode;
       // myRigidBody.isKinematic = isMode;
    }

    public void setGameOver(Rigidbody rigidbody)
    {
        if(!isResultDeclare)
        {
            isResultDeclare = true;
            setRagdollMode(true);

            //rigidbody.AddForce(new Vector3(0, 0, -500));

            gamePlayManager.loadResult("Try Again");
        }
    }

    public void setGameComplete()
    {
        if (!isResultDeclare)
        {
            isResultDeclare = true;

            Debug.Log("winner");

            StartCoroutine(startWinAnim());

            
        }
    }

    public IEnumerator startWinAnim()
    {
        while (speed >0)
        {
            setSpeed(-0.05f);
            yield return new WaitForFixedUpdate();
        }
        animator.SetTrigger(winName);

       // yield return new WaitForSeconds(2);

        gamePlayManager.loadResult("You Win");
    }

    void Rotating(float horizontal)
    {
        // Get camera forward direction, without vertical component.
        Vector3 forward = Vector3.forward;

        // Player is moving on ground, Y component of camera facing is not relevant.
        forward.y = 0.0f;
        forward = forward.normalized;

        // Calculate target direction based on camera forward and direction key.
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 targetDirection;
        targetDirection = right * horizontal;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime  *5); ;

    }

}
