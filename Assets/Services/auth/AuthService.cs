using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public static class AuthServices
{
    public static async Task<BaseResponse<RequestSignatureResponse>> RequestSignature(RequestSignature payload)
    {
        var response =
            await ServicesManager.PostRequestAsync<RequestSignatureResponse, RequestSignature>(
                ENDPOINT_CONFIG.PostRequestSignature, payload, true, false);
        return response;
    }

    public static async Task<BaseResponse<VerifySignatureResponse>> VerifySignature(VerifySignature payload)
    {
        var response =
            await ServicesManager.PostRequestAsync<VerifySignatureResponse, VerifySignature>(
                ENDPOINT_CONFIG.PostVerifySignature, payload, true, false);
        return response;
    }
}