// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Post Outline"
{
    Properties
    {
        _MainTex("Main Texture",2D)="white"{}
    }
    SubShader 
    {
    Blend SrcAlpha OneMinusSrcAlpha
        Pass 
        {
            CGPROGRAM
     
            sampler2D _MainTex;
 
            //<SamplerName>_TexelSize is a float2 that says how much screen space a texel occupies.
            float2 _MainTex_TexelSize;
 
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
             
            struct v2f 
            {
                float4 pos : SV_POSITION;
                float2 uvs : TEXCOORD0;
                float4 color : COLOR;
            };
             
            v2f vert (appdata_full v) 
            {
                v2f o;
                 
                //Despite the fact that we are only drawing a quad to the screen, Unity requires us to multiply vertices by our MVP matrix, presumably to keep things working when inexperienced people try copying code from other shaders.
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color; 
                //Also, we need to fix the UVs to match our screen space coordinates. There is a Unity define for this that should normally be used.
                o.uvs = float2( o.pos.x/2  + 0.5,-o.pos.y/2  + 0.5);
                
                return o;
            }
             
             
            half4 frag(v2f i) : COLOR 
            {
                //arbitrary number of iterations for now
                int NumberOfIterations=9;
 
                //split texel size into smaller words
                float TX_x=_MainTex_TexelSize.x;
                float TX_y=_MainTex_TexelSize.y;
 
                //and a final intensity that increments based on surrounding intensities.
                float ColorIntensityInRadius;
 
                //if something already exists underneath the fragment, discard the fragment.
                if(tex2D(_MainTex,i.uvs.xy).r>0)
                {
                    discard;
                }
 
                //for every iteration we need to do horizontally
                for(int k=0;k<NumberOfIterations;k+=1)
                {
                    //for every iteration we need to do vertically
                    for(int j=0;j<NumberOfIterations;j+=1)
                    {
                        //increase our output color by the pixels in the area
                        ColorIntensityInRadius+=tex2D(
                                                      _MainTex, 
                                                      i.uvs.xy+float2
                                                                   (
                                                                        (k-NumberOfIterations/2)*TX_x,
                                                                        (j-NumberOfIterations/2)*TX_y
                                                                   )
                                                     ).r;
                    }
                }
 
                //output some intensity of teal
                return ColorIntensityInRadius*i.color/**half4(1,1,1,1)*/;
            }
             
            ENDCG
 
        }
        //end pass        
    }
    //end subshader
}
//end shader