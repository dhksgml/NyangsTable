Shader "Custom/SpriteOutlineGlow"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.02
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
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
                float4 _OutlineColor;
                float _OutlineWidth;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 offsets[8] = {
                        float2(-_OutlineWidth, 0), float2(_OutlineWidth, 0),
                        float2(0, -_OutlineWidth), float2(0, _OutlineWidth),
                        float2(-_OutlineWidth, -_OutlineWidth), float2(-_OutlineWidth, _OutlineWidth),
                        float2(_OutlineWidth, -_OutlineWidth), float2(_OutlineWidth, _OutlineWidth)
                    };

                    float4 texColor = tex2D(_MainTex, i.uv);
                    float maxAlpha = texColor.a;

                    for (int j = 0; j < 8; j++)
                    {
                        maxAlpha = max(maxAlpha, tex2D(_MainTex, i.uv + offsets[j]).a);
                    }

                    float4 outline = _OutlineColor * step(0.1, maxAlpha);
                    return texColor + (1 - texColor.a) * outline;
                }
                ENDCG
            }
        }
}
