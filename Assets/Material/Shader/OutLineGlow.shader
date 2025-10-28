Shader "Custom/TwoLayerSprite"
{
    Properties
    {
        _MainTex("Background Texture", 2D) = "white" {}
        _BgColor("Background Tint", Color) = (1,1,1,1)

        _OverlayTex("Main Image Texture", 2D) = "white" {}
        _OverlayColor("Main Image Tint", Color) = (1,1,1,1)
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                sampler2D _OverlayTex;
                float4 _MainTex_ST;
                float4 _OverlayTex_ST;
                float4 _BgColor;
                float4 _OverlayColor;

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float2 uvBg : TEXCOORD0;
                    float2 uvOverlay : TEXCOORD1;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uvBg = TRANSFORM_TEX(v.uv, _MainTex);
                    o.uvOverlay = TRANSFORM_TEX(v.uv, _OverlayTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // 1번: 배경
                    fixed4 bg = tex2D(_MainTex, i.uvBg) * _BgColor;

                // 2번: 메인 이미지
                fixed4 overlay = tex2D(_OverlayTex, i.uvOverlay) * _OverlayColor;

                // 알파 블렌딩: 메인이 위
                fixed4 result;
                result.rgb = lerp(bg.rgb, overlay.rgb, overlay.a);
                result.a = max(bg.a, overlay.a);

                return result;
            }
            ENDCG
        }
        }
            FallBack "Sprites/Default"
}
