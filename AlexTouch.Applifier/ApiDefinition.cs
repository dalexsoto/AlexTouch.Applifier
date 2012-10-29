using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AlexTouch.Applifier
{
	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     PointF Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_types
	//

	[BaseType (typeof (NSObject),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (ApplifierGameDelegate) })]
	interface Applifier 
	{
		[Export("window", ArgumentSemantic.Retain)]
		UIWindow Window { get; set; }

		[Wrap ("WeakDelegate")]
		ApplifierGameDelegate Delegate { get; set; }
		
		[Export ("gameDelegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export("applifierView", ArgumentSemantic.Retain)]
		ApplifierView applifierView { get; set; }

		[Export("gameRendererShouldPause", ArgumentSemantic.Assign)]
		bool GameRendererShouldPause { get; set; }

		[Static, Export ("sharedInstance")]
		Applifier SharedInstance ();

		[Export ("initWithApplifierID:withWindow:supportedOrientations:"), Static] [Internal]
		Applifier InitWithApplifierId (string applifierId, UIWindow withWindow, UIDeviceOrientation firstOrientation, IntPtr orientationsPtr);
		
		[Export ("initWithApplifierID:withWindow:delegate:usingBanners:usingInterstitials:usingFeaturedGames:supportedOrientations:"), Static] [Internal]
		Applifier InitWithApplifierId (string applifierId, UIWindow withWindow, ApplifierGameDelegate gameDelegate, bool usingBanners, bool usingInterstitials, bool usingFeaturedGames, UIDeviceOrientation firstOrientation, IntPtr orientationsPtr);

		[Static, Export ("initWithApplifierID:withWindow:supportedOrientationsArray:")]
		Applifier initWithApplifierID (string applifierID, UIWindow window, NSMutableArray orientationsArray);

		[Static, Export ("showAlert:msg:")]
		void ShowAlert (string title, string message);

		[Export ("releaseResources")]
		void ReleaseResources ();

		[Export ("isViewReady:")]
		bool IsViewReady (AFViewType type);

		[Export ("isViewReadyStr:")]
		bool IsViewReady (string type);

		[Export ("setParamsForView:withParams:")]
		void SetParamsForView (AFViewType type, NSDictionary parameters);

		[Export ("prepareBanner")]
		void PrepareBanner ();

		[Export ("showBannerAt:")]
		void ShowBannerAt (PointF position);

		[Export ("moveBanner:")]
		void MoveBanner (PointF position);

		[Export ("getBannerSize")]
		SizeF BannerSize { get; }

		[Export ("prepareInterstitial")]
		void PrepareInterstitial ();

		[Export ("prepareCustomInterstitial")]
		void PrepareCustomInterstitial ();

		[Export ("showInterstitial")]
		void ShowInterstitial ();

		[Export ("showCustomInterstitial")]
		void ShowCustomInterstitial ();

		[Export ("prepareFeaturedGames")]
		void PrepareFeaturedGames ();

		[Export ("showFeaturedGames")]
		void ShowFeaturedGames ();

		[Export ("prepareAnimatedAtCorner:")]
		void PrepareAnimatedAtCorner (AFCornerPosition corner);

		[Export ("showAnimatedAtCorner:")]
		void ShowAnimatedAtCorner (AFCornerPosition corner);

		[Export ("getAnimatedSizeAtCorner:")]
		SizeF GetAnimatedSizeAtCorner (AFCornerPosition corner);

		[Export ("showButtonWithLabel:at:")]
		void ShowButtonWithLabel (string label, PointF position);

		[Export ("showLeaderboard:forGame:")]
		void ShowLeaderboard (string leaderboardId, string gameId);

		[Export ("hideView")]
		void HideView ();

		[Export ("webView:didFailLoadWithError:")]
		void WebViewDidFailLoad (UIWebView webView, NSError error);

		[Export ("webView:shouldStartLoadWithRequest:navigationType:")]
		bool WebViewShouldStartLoad (UIWebView webview, NSUrlRequest request, UIWebViewNavigationType navigationType);

		[Export ("webViewDidFinishLoad:")]
		void WebViewDidFinishLoad (UIWebView webView);

		[Export ("sendToDelegate:")]
		void SendToDelegate (Selector selectorToCall);

		[Export ("pauseGame")]
		void PauseGame ();

		[Export ("resumeGame")]
		void ResumeGame ();

		[Export ("sendCommand:")]
		void SendCommand (string cmd);

		[Export ("sendCommand:withParam:")]
		void SendCommand (string cmd, string strParam);

		[Export ("sendCommand:withParams:")]
		void SendCommand (string cmd, NSDictionary parameters);

		[Export ("getApplifierAppId")]
		string ApplifierAppId { get; }

		[Export ("getMobileURL")]
		string MobileURL { get; }

		[Export ("hexStringFromApplifierId:")]
		string HexStringFromApplifierId (string hex);

		[Export ("handleOpenURL:")]
		bool HandleOpenURL (NSUrl url);

		// Extensions

		[Bind ("ApplifierStringWithEnum:")]
		string ApplifierStringWithEnum (uint enumType);

		[Bind ("ApplifierEnumFromString:default:")]
		uint ApplifierEnumFromString (string str, uint enumType);

		[Bind ("ApplifierEnumFromString:")]
		uint ApplifierEnumFromString (string str);

		// Constants

		[Field ("kApplifierMobileDefaultURL", "__Internal")]
		NSString ApplifierMobileDefaultURL { get; }

		[Field ("kApplifierJavascriptClassName", "__Internal")]
		NSString ApplifierJavascriptClassName { get; }
	}

	[BaseType (typeof (NSObject))]
	[Model]
	interface ApplifierGameDelegate 
	{		
		//We need a fakeArg in order to make Events work

		[Export ("applifierInterstitialReady"), EventArgs ("ApplifierGameDelegateArgs")] [Abstract]
		void InterstitialReady (bool fakeArg);
		
		[Export ("applifierFeaturedGamesReady"), EventArgs ("ApplifierGameDelegateArgs")] [Abstract]
		void FeaturedGamesReady (bool fakeArg);

		[Export ("applifierBannerReady"), EventArgs ("ApplifierGameDelegateArgs")]
		void BannerReady (bool fakeArg);

		[Export ("applifierAnimatedReady"), EventArgs ("ApplifierGameDelegateArgs")]
		void AnimatedReady (bool fakeArg);

		[Export ("applifierCustomInterstitialReady"), EventArgs ("ApplifierGameDelegateArgs")]
		void CustomInterstitialReady (bool fakeArg);

		[Export ("pauseGame"), EventArgs ("ApplifierGameDelegateArgs")]
		void PausedGame (bool fakeArg);

		[Export ("resumeGame"), EventArgs ("ApplifierGameDelegateArgs")]
		void ResumedGame (bool fakeArg);
	}

	[BaseType (typeof (NSObject))]
	interface ApplifierLeaderboard
	{
		[Export("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[Export("unit", ArgumentSemantic.Copy)]
		string Unit { get; set; }

		[Export("identifier", ArgumentSemantic.Copy)]
		string Identifier { get; set; }

		[Export("results", ArgumentSemantic.Copy)]
		ApplifierLeaderboardsResult [] Results { get; set; }

		[Export ("initWithAttributes:")]
		IntPtr Constructor (NSDictionary attributes);
	}

	// Blocks
	delegate void ApplifierLeaderboardsCallback (NSMutableArray leaderboards);
	delegate void ApplifierLeaderboardsPlayerCallback (ApplifierLeaderboardsPlayer player);
	delegate void ApplifierLeaderboardsErrorCallback (NSError error);
	delegate void ApplifierLeaderboardsResultsCallback (NSArray results);
	delegate void ApplifierLeaderboardsAccessCallback (NSObject accss);
	delegate void ApplifierLeaderboardsScoreCallback (ApplifierLeaderboardsScore score);

	[BaseType (typeof (NSObject))]
	interface ApplifierLeaderboards
	{
		[Export("applifierAccessToken", ArgumentSemantic.Copy)]
		string ApplifierAccessToken { get; set; }
		
		[Export("playerPool", ArgumentSemantic.Copy)]
		NSMutableDictionary PlayerPool { get; set; }
		
		[Static, Export ("sharedInstance")]
		ApplifierLeaderboards SharedInstance ();

		[Static, Export ("initWithApplifierAppId:")]
		ApplifierLeaderboards InitWithApplifierAppId (string appId);

		[Export ("getLeaderboardsForGame:startOffset:limitTo:withCallback:")]
		void GetLeaderboardsForGame (string gameId, int startOffset, int limitTo, ApplifierLeaderboardsCallback callBack);

		[Export ("findOrCreatePlayer:game:withCallback:")]
		void FindOrCreatePlayer (string playerId, string gameId, ApplifierLeaderboardsPlayerCallback callBack);

		[Export ("getLoggedPlayerForGame:success:failure:")]
		void GetLoggedPlayerForGame (string gameId, ApplifierLeaderboardsPlayerCallback success, ApplifierLeaderboardsErrorCallback failure);
	
		[Export ("getResultsForGame:inLeaderboard:startOffset:limitTo:withCallback:")]
		void GetResultsForGame (string gameId, string leaderboardId, int offset, int limit, ApplifierLeaderboardsResultsCallback resultsCallback);

		[Export ("getAccessTokenForPlayer:forGame:withCallback:")]
		void GetAccessTokenForPlayer (string playerId, string gameId, ApplifierLeaderboardsAccessCallback accessCallback);

		[Export ("getPlayerScoreForLeaderboard:withCallback:")]
		void GetPlayerScoreForLeaderboard (string leaderboardName, ApplifierLeaderboardsScoreCallback scoreCallback);
	
		[Export ("postScore:forLeaderboard:withCallback:")]
		void PostScore (int scoreVal, string leaderboardName, ApplifierLeaderboardsScoreCallback scoreCallback);

		[Export ("postScore:withMetadata:forLeaderboard:withCallback:")]
		void PostScore (int scoreVal, NSDictionary metaData, string leaderboardName, ApplifierLeaderboardsScoreCallback scoreCallback);

		[Export("getGameId")]
		string GameId { get; }
	}

	[BaseType (typeof (NSObject))]
	interface ApplifierLeaderboardsPlayer 
	{
		[Export("playerId", ArgumentSemantic.Copy)]
		string PlayerId { get; set; }

		[Export("lastSeen", ArgumentSemantic.Copy)]
		string LastSeen { get; set; }

		[Export("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[Export("firstName", ArgumentSemantic.Copy)]
		string FirstName { get; set; }

		[Export("lastName", ArgumentSemantic.Copy)]
		string LastName { get; set; }

		[Export("pictureURL", ArgumentSemantic.Copy)]
		string PictureURL { get; set; }

		[Export("disabled")]
		bool Disabled { get; set; }

		[Export("searchable")]
		bool Searchable { get; set; }

		[Export ("initWithAttributes:")]
		IntPtr Constructor (NSDictionary attributes);

		[Export ("initWithPlayerId:name:")]
		IntPtr Constructor (string playerIdentifier, string playerName);
	}

	[BaseType (typeof (NSObject))]
	interface ApplifierLeaderboardsResult 
	{
		[Export("player", ArgumentSemantic.Retain)]
		ApplifierLeaderboardsPlayer Player { get; set; }
		
		[Export("score", ArgumentSemantic.Retain)]
		ApplifierLeaderboardsScore Score { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface ApplifierLeaderboardsScore 
	{
		[Export("value")]
		int Value { get; set; }
		
		[Export("rank")]
		int Rank { get; set; }

		[Export("units", ArgumentSemantic.Retain)]
		string Units { get; set; }

		[Export ("initWithAttributes:")]
		IntPtr Constructor (NSDictionary attributes);

		[Export ("initWithValue:rank:units:")]
		IntPtr Constructor (int scoreValue, int scoreRank, string scoreUnits);
	}

	[BaseType (typeof (UIView))]
	interface ApplifierView 
	{
		[Export("scalingFactor", ArgumentSemantic.Assign)]
		double ScalingFactor { get; set; }
		
		[Export("bannerPosition", ArgumentSemantic.Assign)]
		PointF BannerPosition { get; set; }

		[Export("buttonPosition", ArgumentSemantic.Assign)]
		PointF ButtonPosition { get; set; }

		[Export ("initWithSupportedOrientations:")]
		IntPtr Constructor (NSMutableArray orientations);

		[Export("createWebView")]
		UIWebView CreateWebView { get; }

		[Export("loadWebView")]
		void LoadWebView ();

		[Export("closeWebView")]
		void CloseWebView ();

		[Export("getFrameStateEnum:")]
		AFFrameState GetFrameStateEnum (string str);

		[Export("getFrameStateString:")]
		string GetFrameStateString (AFFrameState type);

		[Export("getFrameRect:")]
		RectangleF GetFrameRect (AFFrameState type);

		[Export("changeFrameState:")]
		void ChangeFrameState (AFFrameState nextState);

		[Export("setWebViewDelegate:")]
		void SetWebViewDelegate (NSObject oDelegate);

		[Export("loadRequest:")]
		void LoadRequest (NSUrlRequest urlRequest);

		[Export("evaluateJavascriptOnWebView:")]
		string EvaluateJavascriptOnWebView (string javaScript);

		[Export("rotateTo:")]
		void RotateTo (UIDeviceOrientation orientation);

		[Export("isSupportedOrientation:")]
		bool IsSupportedOrientation (UIDeviceOrientation orientation);
	}
}

