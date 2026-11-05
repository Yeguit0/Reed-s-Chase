Shader "UI/TransparentHoleMask"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _HoleRect ("Hole Rect", Vector) = (0,0,0,0)
        _HoleEnabled ("Hole Enabled", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "RenderPipeline"="UniversalPipeline"
            "IgnoreProjector"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Color;
                float4 _HoleRect;
                float _HoleEnabled;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;

                output.positionHCS =
                    TransformObjectToHClip(input.positionOS.xyz);

                output.screenPos =
                    ComputeScreenPos(output.positionHCS);

                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                output.color = input.color * _Color;

                return output;
            }

half4 frag(Varyings input) : SV_Target
{
    half4 col = SAMPLE_TEXTURE2D(
        _MainTex,
        sampler_MainTex,
        input.uv
    ) * input.color;

    float2 screenUV =
        input.screenPos.xy / input.screenPos.w;

    bool insideHole =
        screenUV.x >= _HoleRect.x &&
        screenUV.x <= _HoleRect.z &&
        screenUV.y >= _HoleRect.y &&
        screenUV.y <= _HoleRect.w;

    // Ahora hacemos transparente TODO lo que esté fuera.
    if (_HoleEnabled > 0.5 && !insideHole)
    {
        col.a = 0;
    }

    return col;
}

            ENDHLSL
        }
    }
}
