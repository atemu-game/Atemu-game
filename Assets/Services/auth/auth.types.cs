using UnityEngine;


[System.Serializable]
public class RequestSignature
{
    public string address;

    public RequestSignature(string address)
    {
        this.address = address;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

}

[System.Serializable]
public class VerifySignature
{
    public string address;
    public string referralCode;

    public string signature;

    public VerifySignature(string address, string signature, string referralCode = null)
    {
        this.address = address;
        this.signature = signature;
        this.referralCode = referralCode;
    }
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

}


[System.Serializable]
public class RequestSignatureResponse
{
    public string address { get; set; }
    public string message { get; set; }
    public string referral { get; set; }
}

[System.Serializable]
public class VerifySignatureResponse
{
    public bool success;
    public string token { get; set; }
}
