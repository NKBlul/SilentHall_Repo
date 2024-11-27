// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/TextureScaling"
{
    Properties
    {
        _MainTex("Base Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;

    // Struct to hold input information
    struct Input
    {
        float2 uv_MainTex;
    };

    void surf(Input IN, inout SurfaceOutputStandard o)
    {
        // Get object scale from the model's transform
        float scaleX = unity_ObjectToWorld[0][0]; // Scale in X (1st row)
        float scaleZ = unity_ObjectToWorld[2][2]; // Scale in Z (3rd row)

        // Modify the UVs based on the object scale
        float2 scaledUV = IN.uv_MainTex * float2(scaleX, scaleZ);

        // Apply texture with modified UVs
        o.Albedo = tex2D(_MainTex, scaledUV).rgb;
    }
    ENDCG
    }
        FallBack "Diffuse"
}
