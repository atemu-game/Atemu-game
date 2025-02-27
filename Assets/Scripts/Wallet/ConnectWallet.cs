using System;
using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TcgEngine;

public class ConnectWalletUI : MonoBehaviour
{
    [field: SerializeField] private Button WalletConnectButton;
    private string referralCode = "";
    private string walletAddress = "";


    [DllImport("__Internal")]
    private static extern void HandleConnectButton();
    [DllImport("__Internal")]
    private static extern void HandleRequestSign(string message);
    public void ReceiveWalletAddressData(string address)
    {
        Debug.Log("address " + address);
        if (string.IsNullOrWhiteSpace(address))
        {
            // PopupLoading.Instance.Hide();
            return;
        }
        ConnectWalletRequest(address);
        walletAddress = address;
    }

    public void ReceiveSignature(string jsonData)
    {
        if (string.IsNullOrWhiteSpace(jsonData))
        {
            // PopupLoading.Instance.Hide();
            return;
        }
        VerifySignatureRequest(jsonData);
    }

    private void Awake()
    {
        // referralCode = ReferralCode.GetRefCodeFromUrl();
    }

    private void Start()
    {

        WalletConnectButton.onClick.RemoveAllListeners();
        WalletConnectButton.onClick.AddListener(() =>
        {
            ConnectWallet();
        });
    }
    public void ConnectWallet()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            HandleConnectButton();
#else
        Login("test account long long long long long name");
#endif
    }


    private async void ConnectWalletRequest(string address)
    {
        // Connect the wallet
        try
        {
            // PopupLoading.Instance.Show();
            // var requestSignatureResponse = await AuthServices.RequestSignature(new RequestSignature(address));
            // if (!requestSignatureResponse.success) throw new Exception(requestSignatureResponse.message);
            // HandleRequestSign(requestSignatureResponse.data.message);
            Login(address);

        }
        catch (Exception e)
        {
            Debug.LogWarning($"Unable to connect to the wallet: {e.Message + " "}");
            // PopupLoading.Instance.Hide();
        }
    }

    private async void VerifySignatureRequest(string signature)
    {
        // Connect the wallet
        try
        {
            // var verifySignatureResponse =
            //     await AuthServices.VerifySignature(new VerifySignature(walletAddress, signature, referralCode));
            // if (!verifySignatureResponse.success) throw new Exception(verifySignatureResponse.message);

            // TokenManager.SaveToken(verifySignatureResponse.data.token);
            ChangeSceneInGame();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Unable to connect to the wallet: {e.Message + " "}");
            // PopupLoading.Instance.Hide();
        }
    }

    private async void Login(string userAddress)
    {
        bool success = await Authenticator.Get().Login(userAddress, "test");
        if (success)
        {
            PlayerPrefs.SetString("tcg_last_user", userAddress);
            ChangeSceneInGame();
        }

    }

    private void ChangeSceneInGame()
    {
        TransitionSettingManager.Instance.LoadScene(0.25f, () =>
         {
             //  SceneManager.LoadSceneAsync(1);
             SceneNav.GoTo("Menu");

         });

    }

}
