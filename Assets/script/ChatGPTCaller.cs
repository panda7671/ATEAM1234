using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class ChatGPTCaller : MonoBehaviour
{
    public TMP_InputField userInputField;
    public TMP_Text gptOutputText;
    public Button sendButton;

    public string apiKey = "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"; // 🔐 실제 키는 보안상 숨겨두는 것이 좋음

    public void SendRequestToGPT()
    {
        sendButton.interactable = false; // 버튼 잠금
        string prompt = userInputField.text;
        StartCoroutine(SendChatGPTRequest(prompt));
    }

    IEnumerator SendChatGPTRequest(string prompt)
    {
        string url = "https://api.openai.com/v1/chat/completions";
        string requestBody = "{\"model\": \"gpt-4o\", \"messages\": [{\"role\": \"user\", \"content\": \"" + prompt + "\"}]}";

        Debug.Log("🔹 보낼 Prompt: " + prompt);
        Debug.Log("🔹 보내는 JSON: " + requestBody);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        Debug.Log("🔸 응답 상태 코드: " + request.responseCode); // ✅ 상태 코드 출력
        Debug.Log("🔸 요청 결과 상태: " + request.result);       // ✅ 결과 종류 출력
        Debug.Log("🔸 요청 에러 메시지: " + request.error);     // ✅ 에러 메시지 출력

        if (request.result == UnityWebRequest.Result.Success)
        {
            string resultJson = request.downloadHandler.text;
            Debug.Log("✅ GPT 응답 원본 JSON: " + resultJson);

            string content = ParseGPTContent(resultJson);
            Debug.Log("✅ 파싱된 응답: " + content);
            gptOutputText.text = content;
        }
        else
        {
            gptOutputText.text = "GPT 요청 실패: " + request.error;
        }

        sendButton.interactable = true; // 버튼 다시 활성화
    }

    string ParseGPTContent(string json)
    {
        try
        {
            GPTResponse response = JsonUtility.FromJson<GPTResponse>(json);
            return response.choices[0].message.content;
        }
        catch
        {
            return "응답 파싱 실패 (구조 불일치)";
        }
    }
}
