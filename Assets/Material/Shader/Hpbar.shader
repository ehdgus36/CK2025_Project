Shader "Custom/RoundedDiagonalHealthBar"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Health("Health", Range(0,1)) = 1.0
        _CutAngleSize("Cut Angle Size", Float) = 0.2
        _Radius("Corner Radius", Float) = 0.05
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100

            Pass
            {
                Cull Off
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float _Health;
                float _CutAngleSize;
                float _Radius;

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

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 uv = i.uv;

                    // 체력 비율 넘어가면 투명 처리
                    if (uv.x > _Health)
                        discard;

                    // 대각선 커팅 시작점
                    float cutStart = _Health - _CutAngleSize;

                    // 대각선 영역일 때만 처리
                    if (uv.x > cutStart)
                    {
                        // 대각선 영역 내 로컬 좌표
                        float localX = uv.x - cutStart;
                        float localY = uv.y;

                        // 둥근 모서리 원 중심 좌표
                        float2 circleCenter = float2(_Radius, 1.0 - _Radius);

                        // 원과 픽셀간 거리
                        float dist = distance(float2(localX, localY), circleCenter);

                        // 모서리 둥글기 처리
                        bool insideCornerRect = (localX > 0 && localX < _Radius * 2) && (localY > 1.0 - _Radius * 2);

                        if (insideCornerRect)
                        {
                            if (dist > _Radius)
                            {
                                discard; // 원 밖 → 투명 (컷)
                            }
                            // 원 안쪽은 보여짐 (둥근 코너)
                        }
                        else
                        {
                            // 원 바깥 영역은 기존 대각선 컷
                            float diagonalThreshold = localX / _CutAngleSize;
                            if (localY < diagonalThreshold)
                            {
                                discard;
                            }
                        }
                    }

                    fixed4 col = tex2D(_MainTex, uv);
                    return col;
                }
                ENDCG
            }
        }
}
