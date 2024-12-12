using System.IO;
using UnityEngine;

public class Capture : MonoBehaviour
{
    public RenderTexture renderTexture;
    public Camera renderCamera;

    void Start()
    {
        CaptureImage();
    }

    void CaptureImage()
    {
        // Ensure RenderTexture is correctly set
        if (renderTexture == null || renderCamera == null)
        {
            Debug.LogError("RenderTexture or Camera not assigned.");
            return;
        }

        // Set the camera's target texture to the RenderTexture
        renderCamera.targetTexture = renderTexture;

        // Create a temporary texture to copy the render texture to
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        // Copy the RenderTexture to the Texture2D
        RenderTexture.active = renderTexture;
        renderCamera.Render();  // Ensure the camera renders to the target texture
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // Specify the path for saving the image
        string folderPath = "Assets/Textures";  // Change as needed

        // Create the folder if it doesn't exist
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = folderPath + "/captured_image.png";

        // Save the texture as a PNG file
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        // Clean up
        RenderTexture.active = null;
        renderCamera.targetTexture = null;  // Reset the camera target texture
        Destroy(texture);

        Debug.Log("Image saved to: " + filePath);
    }
}
