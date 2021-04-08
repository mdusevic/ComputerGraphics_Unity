Shader "PostProcessing/BlurPost"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Distortion ("Distortion", Range(-5.0, 5.0)) = 1.0
        _Blur ("Blur", Range(0.0, 1.0)) = 1.0
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent"}
        ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float _Distortion;
            float _Blur;

            fixed4 frag (v2f i) : SV_Target
            {
                float Pi = 6.28318530718; // Pi*2

                float Directions = 16.0; // BLUR DIRECTIONS (Default 16.0 - More is better but slower)
                float Quality = 3.0; // BLUR QUALITY (Default 4.0 - More is better but slower)
                float Size = 8.0; // BLUR SIZE (Radius)

                float2 Radius = Size / _ScreenParams.xy;

                // Pixel colour
                fixed4 col = tex2D(_MainTex, i.uv);

                // Blur calculations
                for (float d = 0.0; d < Pi; d += Pi / Directions)
                {
                    for (float j = 1.0 / Quality; j <= 1.0; j += 1.0 / Quality)
                    {
                        col += tex2D(_CameraDepthTexture, i.uv + float2(cos(d), sin(d)) * Radius * j);
                    }
                }

                // Output to screen
                col /= Quality * Directions - 15.0;
                return col;
            }
            ENDCG
        }
    }
}
