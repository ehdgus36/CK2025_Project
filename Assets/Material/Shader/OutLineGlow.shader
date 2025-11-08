Shader "Custom/TwoLayerSprite_UI_Fixed"
{
    Properties
    {
        _MainTex("Background", 2D) = "white" {}
        _OverlayTex("Overlay", 2D) = "white" {}
        _BgColor("Background Tint", Color) = (1,1,1,1)
        _OverlayColor("Overlay Tint", Color) = (1,1,1,1)
        _Color("Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }

        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex, _OverlayTex;
            float4 _MainTex_ST, _OverlayTex_ST;
            fixed4 _BgColor, _OverlayColor, _Color;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv1 = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv, _OverlayTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 bg = tex2D(_MainTex, i.uv1) * _BgColor;
                fixed4 overlay = tex2D(_OverlayTex, i.uv2) * _OverlayColor;

                fixed4 c;
                c.rgb = lerp(bg.rgb, overlay.rgb, overlay.a);
                c.a = max(bg.a, overlay.a);

                // ? 이 한 줄이 핵심: CanvasGroup / Image.color 반영
                return c * _Color;
            }
            ENDCG
        }
    }

    FallBack "UI/Default"
}
