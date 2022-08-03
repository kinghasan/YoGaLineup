// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Wall"
{

    Properties {
 _MainTex ("Albedo (RGB)", 2D) = "white" {}
 _OutLineWidth("width", float) = 1.2//定义一个变量
        _Outline ("Outline", Range(0, 1)) = 0.1
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
 }
 SubShader {
		
        Pass 
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            float4 vert (float4 v : POSITION) : SV_POSITION 
            {       
                return UnityObjectToClipPos(v); 
            }

            float4 frag() : SV_Target 
            { 
                return float4(1, 1, 1, 0);
            }

            ENDCG
        }
        Pass 
        {
            Stencil
            {
                Ref 1
                Comp NotEqual
            }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            float _Outline;
            fixed4 _OutlineColor;

            struct a2v 
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            }; 

            struct v2f 
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (a2v v) 
            {
                v2f o;

                float4 pos = mul(UNITY_MATRIX_MV, v.vertex); 
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);  
                normal.z = -0.5;
                pos = pos + float4(normalize(normal), 0) * _Outline;
                o.pos = mul(UNITY_MATRIX_P, pos);

                return o;
            }

            float4 frag(v2f i) : SV_Target 
            { 
                return float4(_OutlineColor.rgb, 1);               
            }

            ENDCG
        }
  
 Pass
 {
            Blend SrcAlpha OneMinusSrcAlpha
            //ZWrite Off
 //ZTest Always
 CGPROGRAM
 #pragma vertex vert
 #pragma fragment frag
 #include "UnityCG.cginc"
  
 struct appdata {
 float4 vertex:POSITION;
 float2 uv:TEXCOORD0;
 };
  
 struct v2f
 {
 float2 uv :TEXCOORD0;
 float4 vertex:SV_POSITION;
 };
  
  
 v2f vert(appdata v)
 {
 v2f o;
 o.vertex = UnityObjectToClipPos(v.vertex);
 o.uv = v.uv;
 return o;
 }
  
 sampler2D _MainTex;
  
 fixed4 frag(v2f i) :SV_Target
 {
 fixed4 col = tex2D(_MainTex, i.uv);
 //return fixed4(0, 0, 1, 1);//返回蓝色，因为再次渲染会把第一个颜色覆盖掉
 col.a = 0;
 return col;
 }
 ENDCG
 }
 } 
 FallBack "Diffuse"
}
