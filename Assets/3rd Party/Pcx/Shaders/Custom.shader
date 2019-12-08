// Pcx - Point cloud importer & renderer for Unity
// https://github.com/keijiro/Pcx

Shader "Point Cloud/Custom"
{
    Properties
    {
        _Tint("Tint", Color) = (0.5, 0.5, 0.5, 1)
        _PointSize("Point Size", Float) = 0.05
        [Toggle] _Distance("Apply Distance", Float) = 1        
        
        _OffsetX("OffsetX", Range(-30, 30)) = 0
        _SpeedX("SpeedX", Range(0, 10)) = 1
		_FrequencyX("FrequencyX", Range(0, 10)) = 1
		_AmplitudeX("AmplitudeX", Range(0, 0.1)) = 0.01
		
		_OffsetY("OffsetY", Range(-30, 30)) = 0
		_SpeedY("SpeedY", Range(0, 10)) = 1
		_FrequencyY("FrequencyY", Range(0, 10)) = 1
		_AmplitudeY("AmplitudeY", Range(0, 0.1)) = 0.01
		
		_OffsetZ("OffsetZ", Range(-30, 30)) = 0
		_SpeedZ("SpeedZ", Range(0, 10)) = 1
		_FrequencyZ("FrequencyZ", Range(0, 10)) = 1
		_AmplitudeZ("AmplitudeZ", Range(0, 0.1)) = 0.01
		
		_ScaleAmplitude("Scale Amplitude", Range(0, 10)) = 1.0
		_ScaleFrequency("Scale Frequency", Range(0, 10)) = 1.0
		_ScaleOffset("Scale Offset", Range(-2, 2)) = 1.0
		
        [Toggle] _SpherizeFirst("Spherize First", Float) = 1
        _Radius("Spherize Radius", Range(-20, 20)) = 3.0
		_Amount("Spherize Amount", Range(-2, 2)) = 0.0
		
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            CGPROGRAM

            #pragma vertex Vertex
            #pragma fragment Fragment

            #pragma multi_compile_fog
            #pragma multi_compile _ _DISTANCE_ON

            #include "UnityCG.cginc"
            #include "Common.cginc"

            struct Attributes
            {
                float4 position : POSITION;
                half3 color : COLOR;
            };

            struct Varyings
            {
                float4 position : SV_Position;
                half3 color : COLOR;
                half psize : PSIZE;
                UNITY_FOG_COORDS(0)
            };

            half4 _Tint;
            float4x4 _Transform;
            half _PointSize;
            
            float _OffsetX;
            float _SpeedX;
            float _FrequencyX;
            float _AmplitudeX;
            
            float _OffsetY;
            float _SpeedY;
            float _FrequencyY;
            float _AmplitudeY;
        
            float _OffsetZ;
            float _SpeedZ;
            float _FrequencyZ;
            float _AmplitudeZ;
                            
            float _ScaleAmplitude;
            float _ScaleFrequency;
            float _ScaleOffset;
            
            float _Radius;
            float _Amount;

            Varyings Vertex(Attributes input)
            {
                float4 pos = input.position;
                pos += float4(_OffsetX, _OffsetY, _OffsetZ, 0);
                
                float4 spherePos = float4(normalize(pos) * _Radius);
                pos = lerp(pos, spherePos, _Amount);
           
                pos.x += sin((input.position.x + _Time.y * _SpeedX) * _FrequencyX) * _AmplitudeX; // * input.position.x;		
                pos.y += cos((input.position.y + _Time.y * _SpeedY) * _FrequencyY) * _AmplitudeY; // * input.position.y;
                pos.x += sin((input.position.z + _Time.y * _SpeedZ) * _FrequencyZ) * _AmplitudeZ; // * input.position.z;
                
//                float scale = sin(_Time.y * _ScaleFrequency) * _ScaleAmplitude + _ScaleOffset;
//                pos.x *= scale;
//                pos.y *= scale;
//                pos.z *= scale;
                
                //pos = UnityObjectToClipPos(pos);
                                
                half3 col = input.color;
                col *= LinearToGammaSpace(_Tint.rgb) * 2;
                col = GammaToLinearSpace(col);
                Varyings o;
                o.position = UnityObjectToClipPos(pos);
                o.color = col;
                #ifdef _DISTANCE_ON
                    o.psize = _PointSize / o.position.w * _ScreenParams.y;
                #else
                    o.psize = _PointSize;
                #endif
                UNITY_TRANSFER_FOG(o, o.position);
                return o;
            }

            half4 Fragment(Varyings input) : SV_Target
            {
                half4 c = half4(input.color, _Tint.a);
                UNITY_APPLY_FOG(input.fogCoord, c);
                return c;
            }

            ENDCG
        }
    }
    //CustomEditor "Pcx.PointMaterialInspector"
}
