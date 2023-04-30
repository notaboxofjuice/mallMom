// A Vertex and Fragment Shader (aka Pixel shader)
Shader "ClassResources/09_GrabPass/AnimatedGrabPass"
{
    Properties
    {
        
        _BumpMap("Bump map", 2D) = "bump" {}
        _Magnitude("Magnitude", float) = 0.05
    }
        SubShader
        {
                Tags {"Queue" = "Transparent"}
                GrabPass{}

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                //#pragma fragment frag
                #pragma fragment frag_animate_static
                #include "UnityCG.cginc"

            sampler2D _BumpMap;
            float _Magnitude;
            sampler2D _GrabTexture;
            half4 _Color;
            sampler2D _MainTex;


            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
            }
            struct Input
            {
                float2 uv_MainTex;
            };

            struct vertInput
            {
                // The naming convention in these structs is called a "binding semantic".
                // This means the variables are marked so that they will be initialized
                // with certain data.

                // Position of current vertex, relative to the 3D object
                float4 pos : POSITION;
                // Gets UV data of the first texture
                float2 texcoord : TEXCOORD0;
            };

            // Even though we initialize this later, still need
            // binding semantics
            struct vertOutput
            {
                float4 pos : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float4 uvgrab : TEXCOORD1;
            };

            // Step 1: model is passed through here
            // This function is more necessary for fragment shader than surface
            // shader, as in the former, it's used to project coordinates of the model
            // on the screen.
            // In the latter, it's only used to alter the geometry.
            vertOutput vert(vertInput input)
            {
                // Need to initialize vertOutput, unlike with vertInput
                vertOutput o;
                
                // Projects the coordinates of the model to the screen...
                // the math behind which is beyond our scope
                o.pos = UnityObjectToClipPos(input.pos);

                //returns data we can use later to sample
                //the grab texture correctly
                o.uvgrab = ComputeGrabScreenPos(o.pos);

                // Simply uses the texture coordinates from the model as-is
                o.texcoord = input.texcoord;

                // All this will be passed to the fragment function
                return o;
            }

            // Step 2: Result of vert function is inputted here
            // The term "fragment" often used to refer to collecting necessary
            // data to draw a pixel.
            half4 frag(vertOutput output) : COLOR
            {
                //texcoords of bumpmap
               half4 bump = tex2D(_BumpMap, output.texcoord);
               //getting normals from text information
               half2 distortion = UnpackNormal(bump).rg;
               output.uvgrab.xy += distortion * _Magnitude;

               fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(output.uvgrab));
               return col;
            }



                half4 frag_animate_down(vertOutput output) : COLOR
            {

                float2 movingCoords = output.texcoord;
                movingCoords.y += _Time;

                //texcoords of bumpmap
               half4 bump = tex2D(_BumpMap, movingCoords);
               //getting normals from text information
               half2 distortion = UnpackNormal(bump).rg;
               output.uvgrab.xy += distortion * _Magnitude;

               fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(output.uvgrab));
               return col;
            }



                half4 frag_animate_scale(vertOutput output) : COLOR
            {

                float2 movingCoords = output.texcoord;
                movingCoords.x *= abs(_SinTime);
                movingCoords.x -= abs(_SinTime) / 2;

                movingCoords.y *= abs(_SinTime);
                movingCoords.y -= abs(_SinTime) / 2;
                //texcoords of bumpmap
               half4 bump = tex2D(_BumpMap, movingCoords);
               //getting normals from text information
               half2 distortion = UnpackNormal(bump).rg;
               output.uvgrab.xy += distortion * _Magnitude;

               fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(output.uvgrab));
               return col;
            }

                half4 frag_animate_static(vertOutput output) : COLOR
            {

                float2 movingCoords = output.texcoord;
                float2 randomseed = (movingCoords)*_Time;
                float randomNum = random(randomseed);
                movingCoords.xy /= randomNum;


                //texcoords of bumpmap
               half4 bump = tex2D(_BumpMap, movingCoords);
               //getting normals from text information
               half2 distortion = UnpackNormal(bump).rg;
               output.uvgrab.xy += distortion * _Magnitude;

               fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(output.uvgrab));
               return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
