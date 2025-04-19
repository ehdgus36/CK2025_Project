Shader "Custom/URPOutlineGlow3D"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0.0, 1.0)) = 0.05
        _GlowColor ("Glow Color", Color) = (0, 0, 1, 1)
        _GlowIntensity ("Glow Intensity", Range(0.0, 10.0)) = 1.0
        _MainTex ("Base Texture", 2D) = "white" { }
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "UniversalForward" }

            // Outline 패스: 외곽선 그리기
            HLSLPROGRAM
            #pragma target 3.0
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Outline 파라미터
            float _OutlineWidth;
            float4 _OutlineColor;

            struct Attributes
            {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 position : POSITION;
                float4 color : COLOR;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                // 오브젝트의 normal 방향으로 외곽선 밀기
                o.position = TransformObjectToHClip(v.position + v.normal * _OutlineWidth);
                o.color = _OutlineColor;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                return i.color; // 외곽선 색상 반환
            }

            ENDHLSL
        }

        Pass
        {
            Name "OUTLINE_GLOW"
            Tags { "LightMode" = "UniversalForward" }

            // Glow 패스: 빛나는 효과 적용
            HLSLPROGRAM
            #pragma target 3.0
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // Glow 파라미터
            float4 _GlowColor;
            float _GlowIntensity;

            struct Attributes
            {
                float4 position : POSITION;
            };

            struct Varyings
            {
                float4 position : POSITION;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.position = v.position;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                return _GlowColor * _GlowIntensity; // Glow 효과 적용
            }

            ENDHLSL
        }
    }

    Fallback "Diffuse"
}
