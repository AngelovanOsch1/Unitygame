using System;
using System.Collections;
using TMPro;
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


    public void OnLoginClick()
    {
        alertText.text = "Signing in...";
        ActivateButtons(false);

        StartCoroutine(Trylogin());
    }

    public void OnCreateClick()
    {
        alertText.text = "Creating account...";
        ActivateButtons(false);
        StartCoroutine(TryCreate());
    }
    private IEnumerator Trylogin()
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
                    alertText.text = "Welcome";
                    ActivateButtons(false);
                    GameAccount returnedAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                    SceneManager.LoadScene(1);
                }
                else
                {
                    Debug.Log("2");
                    ActivateButtons(true);
                }
            }
            else
            {
                Debug.Log("3");
                loginButton.interactable = true;
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
                    GameAccount returnedAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                    Debug.Log("Account has been created");
                }
                else
                {
                    Debug.Log("fuck");
                }
            }
            else
            {
                Debug.Log("Error connecting to the server");
            }

        ActivateButtons(true);

            yield return null;
    }

    private void ActivateButtons(bool toggle)
    {
        loginButton.interactable = toggle;
        createButton.interactable = toggle;
    }
}