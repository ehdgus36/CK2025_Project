Shader "Custom/GlitchEffect"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _GlitchIntensity("Glitch Intensity", Range(0, 1)) = 0
        _GlitchSpeed("Glitch Speed", Range(0, 10)) = 5
        _GlitchFrequency("Glitch Frequency", Range(0, 1)) = 0.3
        _GlitchLineThickness("Glitch Line Thickness", Range(0.01, 0.1)) = 0.03
        _GlitchOverflow("Glitch Overflow", Range(0, 0.2)) = 0.05
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" }
            Pass
            {
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
                float _GlitchIntensity;
                float _GlitchSpeed;
                float _GlitchFrequency;
                float _GlitchLineThickness;
                float _GlitchOverflow;

                // 랜덤 값을 생성하는 함수
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
                    // Glitch Intensity가 0이면 원본 이미지 그대로 반환
                    if (_GlitchIntensity <= 0)
                    {
                        return tex2D(_MainTex, i.uv);
                    }

                    float timeFactor = _Time.y * _GlitchSpeed;
                    float glitchChance = rand(float2(i.uv.y, timeFactor));

                    // Glitch Frequency 값에 따라 글리치가 적용될 확률 결정
                    if (glitchChance > _GlitchFrequency)
                    {
                        return tex2D(_MainTex, i.uv); // 특정 확률 미만이면 원본 유지
                    }

                    // 🎯 글리치 선의 두께 조절 (_GlitchLineThickness 사용)
                    float glitchMask = step(frac(i.uv.y / _GlitchLineThickness), 0.5);
                    float randOffset = (rand(float2(timeFactor, i.uv.y)) - 0.5) * _GlitchIntensity * glitchMask;

                    // 🎯 글리치 오버플로우 (이미지 바깥으로 벗어남)
                    float2 glitchOffset = float2(randOffset, 0) + float2(_GlitchOverflow * (randOffset > 0 ? 1 : -1), 0);

                    // 특정 순간 강한 글리치 효과 추가
                    if (glitchChance > 0.8)
                    {
                        glitchOffset.x *= 5;
                    }

                    float2 newUV = i.uv + glitchOffset;

                    // 🎨 RGB 색상 겹침 효과 유지
                    float2 rOffset = float2(0.02 * rand(float2(i.uv.y, timeFactor)), 0);
                    float2 bOffset = float2(-0.02 * rand(float2(i.uv.y, timeFactor)), 0);

                    float r = tex2D(_MainTex, newUV + rOffset).r;
                    float g = tex2D(_MainTex, newUV).g;
                    float b = tex2D(_MainTex, newUV + bOffset).b;

                    return fixed4(r, g, b, 1.0);
                }
                ENDCG
            }
        }
}
