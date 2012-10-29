using System;

namespace AlexTouch.Applifier
{
	public enum AFApplifierState
	{
		Init = 0,
		Loading,
		Loaded,
		Initializing,
		Initialized
	}

	public enum AFViewType
	{
		None = 0,
		Banner,
		Interstitial,
		CustomInterstitial,
		FeaturedGames,
		Animated,
		ViewButton
	}

	public enum AFDefinitions
	{
		APPLIFIER_URL_CACHE_DISK_CAPACITY = (1024 * 1024 * 10),
		APPLIFIER_URL_CACHE_MEMORY_CAPACITY = (1024 * 1024),
		APPLIFIER_SDK_VERSION = 4
	}

	public enum AFViewDefinitions
	{
		BannerWidth = 310,
		BannerHeight = 50,
		ButtonWidth = 150,
		ButtonHeight = 35,
		CornerWidth = 234,
		CornerHeight = 122,
		ViewTag = 10298305
	}

	public enum AFFrameState
	{
		None, // Hidden
		Banner,
		Button,
		CornerTopLeft,
		CornerTopRight,
		CornerBottomLeft,
		CornerBottomRight,
		Fullscreen
	}

	public enum AFCornerPosition
	{
		TopLeft = AFFrameState.CornerTopLeft,
		TopRight = AFFrameState.CornerTopRight,
		BottomLeft = AFFrameState.CornerBottomLeft,
		BottomRight = AFFrameState.CornerBottomRight
	}
}

