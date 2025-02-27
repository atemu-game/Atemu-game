using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[Serializable]
public class BaseResponse<T>
{
    public bool success;
    public string message;

    public T data;
}

public class ServicesManager
{
    private static readonly string baseURL = "https://dev-api.thesunnyside.cc/";

    // Generic GET request using async/await
    public static async Task<BaseResponse<T>> GetRequestAsync<T>(string endpoint)
    {
        string url = baseURL + endpoint;
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            string token = TokenManager.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", $"Bearer {token}");
            }

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("request.responseCode" + request.responseCode);
                if (request.responseCode == 401)
                {
                    HandleUnauthorizedAccess();
                }

                Debug.LogError($"Error fetching data: {request.error} " + "at api " + url);
                return new BaseResponse<T>
                {
                    success = false,
                    message = $"Error fetching data",
                    data = default
                }; // Return default value for the type
            }
            else
            {
                string json = request.downloadHandler.text;
                try
                {
                    BaseResponse<T> response = JsonConvert.DeserializeObject<BaseResponse<T>>(json);


                    return response;
                    ; // Directly return the response // Directly return the response
                }
                catch (Exception ex)
                {
                    Debug.LogError($"JSON Parsing Error: {ex.Message}" + "at api " + url);
                    return new BaseResponse<T>
                    {
                        success = false,
                        message = $"Error fetching data",
                        data = default
                    }; // Return default value for the type
                }
            }
        }
    }

    // Generic POST request using async/await
    public static async Task<BaseResponse<T>> PostRequestAsync<T, U>(string endpoint, U postData,
        bool deserializeResponse = true, bool addToken = true)
    {
        string url = baseURL + endpoint;
        string jsonData = JsonConvert.SerializeObject(postData, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            if (addToken)
            {
                string token = TokenManager.GetToken();
                if (!string.IsNullOrEmpty(token))
                {
                    request.SetRequestHeader("Authorization", $"Bearer {token}");
                }
            }
            // Attach token if available


            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                if (request.responseCode == 401)
                {
                    HandleUnauthorizedAccess();
                }

                Debug.LogError($"Error posting data: {request.error}" + "at api " + url);
                return new BaseResponse<T>
                {
                    success = false,
                    message = $"Error fetching data",
                    data = default
                }; // Return default value for the type
            }
            else
            {
                string json = request.downloadHandler.text;

                if (deserializeResponse)
                {
                    try
                    {
                        BaseResponse<T> response = JsonConvert.DeserializeObject<BaseResponse<T>>(json);

                        return response;
                        ; // Directly return the response
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"JSON Parsing Error: {ex.Message}" + "at api " + url);
                        return new BaseResponse<T>
                        {
                            success = false,
                            message = $"Error fetching data",
                            data = default
                        };
                    }
                }
                else
                {
                    // Skip deserialization and return default
                    return new BaseResponse<T>
                    {
                        success = false,
                        message = $"Error fetching data",
                        data = default
                    };
                }
            }
        }
    }

    public static async Task<BaseResponse<T>> PostRequestAsync<T>(string endpoint, bool deserializeResponse = true,
        bool addToken = true)
    {
        string url = baseURL + endpoint;

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            if (addToken)
            {
                string token = TokenManager.GetToken();
                if (!string.IsNullOrEmpty(token))
                {
                    request.SetRequestHeader("Authorization", $"Bearer {token}");
                }
            }
            // Attach token if available

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                if (request.responseCode == 401)
                {
                    HandleUnauthorizedAccess();
                }

                Debug.LogError($"Error posting data: {request.error}" + "at api " + url);
                return new BaseResponse<T>
                {
                    success = false,
                    message = $"Error fetching data",
                    data = default
                }; // Return default value for the type
            }
            else
            {
                string json = request.downloadHandler.text;

                if (deserializeResponse)
                {
                    try
                    {
                        BaseResponse<T> response = JsonConvert.DeserializeObject<BaseResponse<T>>(json);

                        return response;
                        ; // Directly return the response
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"JSON Parsing Error: {ex.Message}" + "at api " + url);
                        return new BaseResponse<T>
                        {
                            success = false,
                            message = $"Error fetching data",
                            data = default
                        };
                    }
                }
                else
                {
                    // Skip deserialization and return default
                    return new BaseResponse<T>
                    {
                        success = false,
                        message = $"Error fetching data",
                        data = default
                    };
                }
            }
        }
    }

    public static async Task<BaseResponse<T>> PutRequestAsync<T, U>(string endpoint, U putData)
    {
        string url = $"{baseURL}{endpoint}";
        string jsonData = JsonConvert.SerializeObject(putData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = UnityWebRequest.Put(url, bodyRaw))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            // Attach token if available
            string token = TokenManager.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", $"Bearer {token}");
            }

            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"PUT Request Error: {request.error}" + "at api " + url);
                return new BaseResponse<T>
                {
                    success = false,
                    message = $"Error fetching data",
                    data = default
                }; // Return default value for the type
            }

            try
            {
                string json = request.downloadHandler.text;
                BaseResponse<T> response = JsonConvert.DeserializeObject<BaseResponse<T>>(json);

                return response;
                ; // Directly return the response
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Deserialization Error: {ex.Message}" + "at api " + url);
                return new BaseResponse<T>
                {
                    success = false,
                    message = $"Error fetching data",

                    data = default
                };
            }
        }
    }

    public static async Task<BaseResponse<T>> DeleteRequestAsync<T, U>(string endpoint, U? deleteData)
    {
        string url = $"{baseURL}{endpoint}";

        // Create a UnityWebRequest with method DELETE
        using (UnityWebRequest request = new UnityWebRequest(url, "DELETE"))
        {
            // Set the Content-Type header to application/json
            request.SetRequestHeader("Content-Type", "application/json");

            // Attach token if available
            string token = TokenManager.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", $"Bearer {token}");
            }

            // If deleteData is not null, include it in the request body
            if (deleteData != null)
            {
                // Serialize the deleteData to JSON
                string jsonData = JsonConvert.SerializeObject(deleteData);
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                // Assign the UploadHandler with the serialized data
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            // Assign a DownloadHandler to receive server responses
            request.downloadHandler = new DownloadHandlerBuffer();

            // Send the request and wait for a response
            var operation = request.SendWebRequest();

            // Check for network errors or HTTP errors
            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                if (request.responseCode == 401)
                {
                    HandleUnauthorizedAccess();
                }

                Debug.LogError($"Error posting data: {request.error}" + "at api " + url);
                return new BaseResponse<T>
                {
                    success = false,
                    message = $"Error fetching data",

                    data = default
                }; // Return default value for the type
            }
            else
            {
                string json = request.downloadHandler.text;

                try
                {
                    BaseResponse<T> response = JsonConvert.DeserializeObject<BaseResponse<T>>(json);

                    return response;
                    ; // Directly return the response
                }
                catch (Exception ex)
                {
                    Debug.LogError($"JSON Parsing Error: {ex.Message}" + "at api " + url);
                    return new BaseResponse<T>
                    {
                        success = false,
                        message = $"Error fetching data",
                        data = default
                    };
                }
            }
        }
    }

    private static void HandleUnauthorizedAccess()
    {
        Debug.LogWarning("Token expired or unauthorized. Clearing token and redirecting to login.");
        TokenManager.ClearToken();
        // Implement redirection logic, possibly via event or delegate
        // SceneManager.LoadSceneAsync(0);
    }
}