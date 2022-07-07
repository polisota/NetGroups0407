using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviourPunCallbacks//, IConnectionCallbacks, IInRoomCallbacks
{
    private const string PLAYFAB_ID = "E49F0";
    private const string GAME_VERSION = "dev";

    //private void Awake()
    //{
    //    PhotonNetwork.AddCallbackTarget(this);
    //}

    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = PLAYFAB_ID;

        var request = new LoginWithCustomIDRequest
        {
            CustomId = "ProgrammerLamer",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, Success, Fail);
    }

    private void Success(LoginResult result)
    {
        Debug.Log(result.PlayFabId);
        Connect();
    }

    private void Fail(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError(errorMessage);
    }

    private void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = GAME_VERSION;
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }

    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        foreach (KeyValuePair<string, object> kvp in data)
            Debug.Log($"key: {kvp.Key}/value: {kvp.Value}");
    }

    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log(debugMessage);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName);
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        //throw new System.NotImplementedException();
    }
}
