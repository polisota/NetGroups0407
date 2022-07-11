using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Authorization : MonoBehaviour
{
    [SerializeField] private InputField _userNameField;
    [SerializeField] private InputField _userPasswordField;
    [SerializeField] private Button _registrationButton;
    [SerializeField] private Text _errorText;

    private string _userName;
    private string _userPassword;

    private void Awake()
    {
        _userNameField.onValueChanged.AddListener(SetUserName);
        _userPasswordField.onValueChanged.AddListener(SetUserPassword);
        _registrationButton.onClick.AddListener(Submit);
    }

    private void SetUserName(string value)
    {
        _userName = value;
    }

    private void SetUserPassword(string value)
    {
        _userPassword = value;
    }

    private void Submit()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _userName,
            Password = _userPassword
        }, result =>
        {
            _errorText.gameObject.SetActive(false);
            Debug.Log($"User enter: {result.LastLoginTime}");
        }, error =>
        {
            _errorText.gameObject.SetActive(true);
            _errorText.text = error.ErrorDetails.FirstOrDefault().Value.FirstOrDefault() ?? "";
            Debug.LogError(error);
        });
    }
}
