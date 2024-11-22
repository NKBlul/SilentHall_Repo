Shader "Custom/DoubleSidedLitShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        // Double-sided rendering
        Cull Off

        CGPROGRAM
        #pragma surface surf Lambert doubleSided

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Sample the texture
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb; // Set the surface color
            o.Alpha = c.a;    // Set the alpha (optional, for transparency)
        }
        ENDCG
    }
        FallBack "Diffuse"
}
