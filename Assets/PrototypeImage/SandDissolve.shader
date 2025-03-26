Shader "Custom/SandDissolve"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0, 1)) = 0
        _NoiseScale("Noise Scale", Range(1, 100)) = 50
        _FadeSharpness("Fade Sharpness", Range(1, 10)) = 5
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" }
            Pass
            {
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _DissolveAmount;
                float _NoiseScale;
                float _FadeSharpness;

                // 랜덤 노이즈 생성 함수
                float rand(float2 co)
                {
                    return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
                }

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // 기본 텍스처 색상
                    fixed4 col = tex2D(_MainTex, i.uv);

                // 위에서 아래로 사라지는 효과를 강화하기 위해 세밀한 곡선 조정
                float dissolveThreshold = smoothstep(0.0, 1.0, (1.0 - i.uv.y) * _FadeSharpness);

                // 랜덤 노이즈를 생성하여 좀 더 자연스럽게 사라지는 느낌 추가
                float noise = rand(i.uv * _NoiseScale);

                // DissolveAmount 값이 커질수록 위쪽부터 점진적으로 사라짐
                float dissolveFactor = dissolveThreshold * (1.0 - _DissolveAmount);

                // 노이즈 값이 일정 임계값보다 크면 해당 픽셀을 투명하게 만듦
                float alpha = noise > dissolveFactor ? 0.0 : 1.0;

                // 최종 색상 적용
                col.a *= alpha;

                return col;
            }
            ENDCG
        }
        }
}
