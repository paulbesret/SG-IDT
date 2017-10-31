
Shader "StandardAvecNoise2" {
Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_Color2 ("Main Color2", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_effetpapier ("effetpapier", 2D) = "white" {}
	 _TailleTextures ("_TailleTextures", Float) = 1
	 _effetpapierQuantite ("_effetpapierQuantite", Float) = 1
}
SubShader {
	Tags { "RenderType"="Opaque" }  
	Cull back
	LOD 200

        
     
CGPROGRAM
#pragma surface surf Lambert // vertex:vert
//#pragma target 4.0
sampler2D _MainTex;
sampler2D _effetpapier;
fixed4 _Color;
fixed4 _Color2;
 float _TailleTextures;
 float _effetpapierQuantite;
struct Input {
	float2 uv_MainTex;
	 float3 worldPos;
	  float3 worldNormal;
	   float TailleTextures; 
	   float effetpapierQuantite; 
	   /*half4 vColor;
			half2 tex;*/
		
};
/*
void vert (inout appdata_full v, out Input o){
			UNITY_INITIALIZE_OUTPUT(Input,o);
			o.vColor = v.color;
			o.tex = v.texcoord.xy;
		}
 */      
       
void surf (Input IN, inout SurfaceOutput o) {
	 IN.worldPos*=_TailleTextures;
	  float3 worldNormal = normalize(IN.worldNormal);
      float3 projNormal = saturate(pow(worldNormal*1.5, 4));

      float3 albedopapier;
       	half3 albedo0 = tex2D(_effetpapier, IN.worldPos.xy).rgb;
        half3 albedo1 = tex2D(_effetpapier, IN.worldPos.zx).rgb;
        half3 albedo2 = tex2D(_effetpapier, IN.worldPos.zy).rgb;
       
        albedopapier = lerp(albedo1, albedo0, projNormal.z);
        albedopapier = lerp(albedopapier, albedo2, projNormal.x);
        albedopapier+=_effetpapierQuantite;

	fixed4 c = (tex2D(_MainTex, IN.uv_MainTex)) * _Color*albedopapier.r+ _Color2;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}

ENDCG
/*
 Pass{
      CGPROGRAM
         #pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
        float4 _Color2;
		float _Scale;

		struct appdata
		{
			float4 vertex : POSITION;
			float4 color : COLOR;
		};

		struct v2f
		{
			float4 pos : SV_POSITION;
			float4 color : COLOR;
		};

		v2f vert (appdata v)
		{
			v2f o;

			o.pos = UnityObjectToClipPos(v.vertex);
			o.color = v.color;

			return o;
		}

		half4 frag (v2f i) : COLOR
		{
			return i.color;
		}

         ENDCG
  	}

}*/
}
Fallback "Mobile/VertexLit"
}
