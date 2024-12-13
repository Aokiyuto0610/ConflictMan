Shader "Custom/ArrowGradient"
{
    Properties
    {
        _GradientTex("Gradient Texture", 2D) = "white" {} // グラデーション用のテクスチャ
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _GradientTex; // グラデーションテクスチャ

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // UV座標に基づいてテクスチャの色を取得
                fixed4 col = tex2D(_GradientTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
