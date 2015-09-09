using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

// =========================================================

// 参考：http://tkmkrocket.net/doc/?p=88
// =========================================================
public class CharControl : MonoBehaviour{

    public string inputCommands = "";
    public bool commandEnable = true;

    private int recCommandLength = 100;

    // -------------------
    // Startメソッド
    // -------------------
    void Start()
    {
        // inputCommandsを100文字まで半角スペースで埋める（PadLeftメソッド）
        inputCommands = inputCommands.PadLeft(100);

        // CommandInputControlをコルーチンでコール
        StartCoroutine("CommandInputControl");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ---------------------------------------------
    // Startメソッドからコールされる
    // ---------------------------------------------
    IEnumerator CommandInputControl()
    {
        StartCoroutine("confirmCommand");

        while (true)
        {
            //Axis
            if (commandEnable)
            {
                getAxis();
                getFire();
            }
            else
            {
                inputCommands += " ";
            }

            yield return null;
        }
    }

    void getAxis()
    {
        if (Input.GetAxisRaw("Vertical") > 0.9)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.9) { inputCommands += "9"; }
            else if (Input.GetAxisRaw("Horizontal") < -0.9) { inputCommands += "7"; }
            else if (Input.GetAxisRaw("Horizontal") == 0) { inputCommands += "8"; }
            else { inputCommands += "8"; }
        }

        else if (Input.GetAxisRaw("Vertical") < -0.9)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.9) { inputCommands += "3"; }
            else if (Input.GetAxisRaw("Horizontal") < -0.9) { inputCommands += "1"; }
            else if (Input.GetAxisRaw("Horizontal") == 0) { inputCommands += "2"; }
            else { }
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {

            if (Input.GetAxisRaw("Horizontal") > 0.9) { inputCommands += "6"; }
            else if (Input.GetAxisRaw("Horizontal") < -0.9) { inputCommands += "4"; }
            else if (Input.GetAxisRaw("Horizontal") == 0) { inputCommands += "5"; }
            else { }
        }
        else
        {

        }

        if (inputCommands.Length > recCommandLength)
        {
            inputCommands = inputCommands.Remove(0, 1);
        }

    }

    void getFire()
    {
        //fire
        if (Input.GetButton("Fire1") == true)
        {
            inputCommands += "f";
        }

        if (inputCommands.Length > recCommandLength)
        {
            inputCommands = inputCommands.Remove(0, 1);
        }
    }

    IEnumerator confirmCommand()
    {
        while (true)
        {
            int comLength = 0;
            string checkframe = " ";

            string testB = "6[0-9]*5[0-9]*2[0-9]*6[0-9]*f";
            comLength = 30;
            checkframe = inputCommands.Remove(0, recCommandLength - comLength);
            if (Regex.IsMatch(checkframe, testB))
            {
                Debug.Log("SHORYUKEN!");
                yield return new WaitForSeconds(1.5f);
            }

            string testA = "2[0-9]*6[0-9]*[f]";
            comLength = 30;
            checkframe = inputCommands.Remove(0, recCommandLength - comLength);
            if (Regex.IsMatch(checkframe, testA))
            {
                //Debug.Log("HADOUKEN!");
                GameObject.Instantiate(bullet2, sp.transform.position, sp.transform.rotation);
                yield return new WaitForSeconds(1.0f);
            }

            string testC = "f";
            comLength = 1;
            checkframe = inputCommands.Remove(0, recCommandLength - comLength);
            if (Regex.IsMatch(checkframe, testC))
            {
                GameObject.Instantiate(bullet, sp.transform.position, sp.transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }
    }
}