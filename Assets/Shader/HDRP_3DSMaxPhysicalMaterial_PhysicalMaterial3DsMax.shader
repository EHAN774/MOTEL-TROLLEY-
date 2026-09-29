Shader "HDRP/3DSMaxPhysicalMaterial/PhysicalMaterial3DsMax" {
	Properties {
		_BASE_COLOR_WEIGHT ("BaseColorWeight", Range(0, 1)) = 1
		_BASE_COLOR ("BaseColor", Vector) = (1,1,1,1)
		[NoScaleOffset] _BASE_COLOR_MAP ("BaseColorMap", 2D) = "white" {}
		_REFLECTIONS_WEIGHT ("ReflectionsWeight", Float) = 1
		_REFLECTIONS_COLOR ("ReflectionsColor", Vector) = (1,1,1,1)
		[NoScaleOffset] _REFLECTIONS_COLOR_MAP ("ReflectionsColorMap", 2D) = "white" {}
		_REFLECTIONS_ROUGHNESS ("ReflectionsRoughness", Range(0, 1)) = 1
		[NoScaleOffset] _REFLECTIONS_ROUGHNESS_MAP ("ReflectionsRoughnessMap", 2D) = "white" {}
		_METALNESS ("Metalness", Range(0, 1)) = 0
		[NoScaleOffset] _METALNESS_MAP ("MetalnessMap", 2D) = "white" {}
		_REFLECTIONS_IOR ("ReflectionsIOR", Range(0, 50)) = 1.52
		[NoScaleOffset] _REFLECTIONS_IOR_MAP ("ReflectionsIORMap", 2D) = "white" {}
		_TRANSPARENCY_WEIGHT ("TransparencyWeight", Range(0, 1)) = 1
		_TRANSPARENCY ("Transparency", Vector) = (1,1,1,1)
		[NoScaleOffset] _TRANSPARENCY_MAP ("TransparencyMap", 2D) = "white" {}
		_EMISSION_WEIGHT ("EmissionWeight", Range(0, 1)) = 1
		_EMISSION_COLOR ("Emission", Vector) = (0,0,0,0)
		[NoScaleOffset] _EMISSION_COLOR_MAP ("EmissionMap", 2D) = "white" {}
		_BUMP_MAP_STRENGTH ("BumpMapStrength", Range(0, 10)) = 0.3
		[NoScaleOffset] _BUMP_MAP ("BumpMap", 2D) = "grey" {}
		_ANISOTROPY ("Anisotropy", Range(0, 1)) = 1
		[NoScaleOffset] _ANISOTROPY_MAP ("AnisotropyMap", 2D) = "white" {}
		[NoScaleOffset] [Normal] _COAT_NORMAL ("CoatNormal", 2D) = "bump" {}
		_COAT_ROUGHNESS ("CoatRoughness", Range(0, 1)) = 0
		_COAT_THICKNESS ("CoatThickness", Float) = 5
		_COAT_WEIGHT ("CoatWeight", Range(0, 1)) = 0
		_COAT_COLOR ("CoatColor", Vector) = (0,0,0,0)
		_COAT_IOR ("CoatIOR", Range(0, 30)) = 1.4
		[HideInInspector] _EmissionColor ("Color", Vector) = (1,1,1,1)
		[HideInInspector] _RenderQueueType ("Float", Float) = 1
		[ToggleUI] [HideInInspector] _AddPrecomputedVelocity ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _DepthOffsetEnable ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _ConservativeDepthOffsetEnable ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _TransparentWritingMotionVec ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _AlphaCutoffEnable ("Boolean", Float) = 0
		[HideInInspector] _TransparentSortPriority ("_TransparentSortPriority", Float) = 0
		[ToggleUI] [HideInInspector] _UseShadowThreshold ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _DoubleSidedEnable ("Boolean", Float) = 1
		[Enum(Flip, 0, Mirror, 1, None, 2)] [HideInInspector] _DoubleSidedNormalMode ("Float", Float) = 2
		[HideInInspector] _DoubleSidedConstants ("Vector4", Vector) = (1,1,-1,0)
		[Enum(Auto, 0, On, 1, Off, 2)] [HideInInspector] _DoubleSidedGIMode ("Float", Float) = 0
		[ToggleUI] [HideInInspector] _TransparentDepthPrepassEnable ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _TransparentDepthPostpassEnable ("Boolean", Float) = 0
		[HideInInspector] _SurfaceType ("Float", Float) = 0
		[HideInInspector] _BlendMode ("Float", Float) = 0
		[HideInInspector] _SrcBlend ("Float", Float) = 1
		[HideInInspector] _DstBlend ("Float", Float) = 0
		[HideInInspector] _AlphaSrcBlend ("Float", Float) = 1
		[HideInInspector] _AlphaDstBlend ("Float", Float) = 0
		[ToggleUI] [HideInInspector] _ZWrite ("Boolean", Float) = 1
		[ToggleUI] [HideInInspector] _TransparentZWrite ("Boolean", Float) = 0
		[HideInInspector] _CullMode ("Float", Float) = 2
		[ToggleUI] [HideInInspector] _EnableFogOnTransparent ("Boolean", Float) = 1
		[HideInInspector] _CullModeForward ("Float", Float) = 2
		[Enum(Front, 1, Back, 2)] [HideInInspector] _TransparentCullMode ("Float", Float) = 2
		[Enum(UnityEditor.Rendering.HighDefinition.OpaqueCullMode)] [HideInInspector] _OpaqueCullMode ("Float", Float) = 2
		[HideInInspector] _ZTestDepthEqualForOpaque ("Float", Float) = 3
		[Enum(UnityEngine.Rendering.CompareFunction)] [HideInInspector] _ZTestTransparent ("Float", Float) = 4
		[ToggleUI] [HideInInspector] _TransparentBackfaceEnable ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _RequireSplitLighting ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _ReceivesSSR ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _ReceivesSSRTransparent ("Boolean", Float) = 0
		[ToggleUI] [HideInInspector] _EnableBlendModePreserveSpecularLighting ("Boolean", Float) = 1
		[ToggleUI] [HideInInspector] _SupportDecals ("Boolean", Float) = 0
		[HideInInspector] _StencilRef ("Float", Float) = 0
		[HideInInspector] _StencilWriteMask ("Float", Float) = 6
		[HideInInspector] _StencilRefDepth ("Float", Float) = 0
		[HideInInspector] _StencilWriteMaskDepth ("Float", Float) = 9
		[HideInInspector] _StencilRefMV ("Float", Float) = 32
		[HideInInspector] _StencilWriteMaskMV ("Float", Float) = 43
		[HideInInspector] _StencilRefDistortionVec ("Float", Float) = 4
		[HideInInspector] _StencilWriteMaskDistortionVec ("Float", Float) = 4
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}