using JetBrains.Annotations;
using System;
using System.Collections;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private string loginEndpoint = "http://127.0.0.1:13756/account/login";
    [SerializeField] private string createEndpoint = "http://127.0.0.1:13756/account/create";

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button createButton;

    public GameAccount gameAccount;

    public void OnLoginClick()
    {
        alertText.text = "Signing in...";
        StartCoroutine(Trylogin());
    }

    public void OnCreateClick()
    {
        alertText.text = "Creating account...";
        StartCoroutine(TryCreate());
    }


    public IEnumerator Trylogin()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);

        UnityWebRequest request = UnityWebRequest.Post(loginEndpoint, form);
            var handler = request.SendWebRequest();

            float startTime = 0.0f;

            while (!handler.isDone)
            {
                startTime += Time.deltaTime;

                if (startTime > 10.0f)
                {
                    break;
                }

                yield return null;
            }

        if (request.result == UnityWebRequest.Result.Success)
        {

            if (request.downloadHandler.text != "Invalid credentials")
            {
                GameAccount gameAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                this.gameAccount = gameAccount;
                Debug.Log(request.downloadHandler.text);

                //int gameAccountLevel = Convert.ToInt32(gameAccount.level);

                //Debug.Log(gameAccountLevel);

                Debug.Log(gameAccount.username);
                PlayerPrefs.SetInt("score", gameAccount.score);
                PlayerPrefs.SetInt("level", gameAccount.level);
                PlayerPrefs.SetString("username", gameAccount.username);

                if (gameAccount.level == 1)
                {
                    SceneManager.LoadScene(1);
                }

                else if (gameAccount.level == 1)
                {
                    SceneManager.LoadScene(2);
                }
            }
            else
            {
                Debug.Log("ERROR");
            }
        }
        else
        {
            Debug.Log("ERROR");
        }
            yield return null;
    }
    
    private IEnumerator TryCreate()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);

        UnityWebRequest request = UnityWebRequest.Post(createEndpoint, form);
            var handler = request.SendWebRequest();

            float startTime = 0.0f;

            while (!handler.isDone)
            {
                startTime += Time.deltaTime;

                if (startTime > 10.0f)
                {
                    break;
                }

                yield return null;
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.downloadHandler.text != "Invalid credentials" && request.downloadHandler.text != "Username is already taken")
                {
                    Debug.Log("Account has been created");
                }
                else
                {
                    Debug.Log("ERROR");
                }
            }
            else
            {
                Debug.Log("Error connecting to the server");
            }
            yield return null;
    }
}