Shader "Custom/ChromakeyUI"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ChromaKeyColor ("Chroma Key Color", Color) = (0, 1, 0, 1)
        _Threshold ("Threshold", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _ChromaKeyColor;
            float _Threshold;

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

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float chromaDistance = distance(col.rgb, _ChromaKeyColor.rgb);

                if (chromaDistance < _Threshold)
                {
                    discard;
                }

                return col;
            }
            ENDCG
        }
    }
    FallBack Off
}