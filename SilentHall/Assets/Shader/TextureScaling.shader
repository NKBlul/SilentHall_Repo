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

        struct Input
        {
            float2 uv_MainTex;
        };

        // Function to calculate object scale from unity_ObjectToWorld matrix
        float3 GetObjectScale()
        {
            float3 scaleX = unity_ObjectToWorld._m00_m10_m20; // First column
            float3 scaleY = unity_ObjectToWorld._m01_m11_m21; // Second column
            float3 scaleZ = unity_ObjectToWorld._m02_m12_m22; // Third column

            return float3(length(scaleX), length(scaleY), length(scaleZ));
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float3 objectScale = GetObjectScale();

            // Use X and Z scales for UV adjustment (assuming uniform scaling in Y isn't relevant)
            float2 scaledUV = IN.uv_MainTex * float2(objectScale.x, objectScale.z);

            o.Albedo = tex2D(_MainTex, scaledUV).rgb;
        }
        ENDCG
    }
        FallBack "Diffuse"
}
