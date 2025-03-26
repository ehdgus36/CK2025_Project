Shader "Custom/BottomToTopRevealWithEdgeColor"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}  // 원본 이미지
        _RevealAmount("Reveal Amount", Range(0, 1)) = 0  // 아래에서 위로 보이는 정도
        _FadeSmoothness("Fade Smoothness", Range(0.01, 0.2)) = 0.05 // 페이드 부드러움
        _EdgeColor("Edge Color", Color) = (1, 0, 0, 1) // 경계선 색상 (기본 빨강)
        _EdgeWidth("Edge Width", Range(0.01, 0.1)) = 0.03 // 경계선 두께
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
                float _RevealAmount;
                float _FadeSmoothness;
                float4 _EdgeColor;
                float _EdgeWidth;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);

                // ✅ 아래에서 위로 점점 나타나도록 수정 (기존 반대)
                float revealThreshold = smoothstep(_RevealAmount - _FadeSmoothness, _RevealAmount, 1.0 - i.uv.y);

                // ✅ 경계선 감지 (아래에서 위로)
                float edge = smoothstep(_RevealAmount - _EdgeWidth, _RevealAmount, 1.0 - i.uv.y) - revealThreshold;

                // ✅ 경계선 색상 추가
                col.rgb = lerp(col.rgb, _EdgeColor.rgb, edge);

                // ✅ 알파값 적용
                col.a *= revealThreshold + edge;

                return col;
            }
            ENDCG
        }
        }
}
