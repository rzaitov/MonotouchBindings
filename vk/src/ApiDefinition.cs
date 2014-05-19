using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace VK
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
	// For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_libraries
	//

	[BaseType (typeof (VKObject))]
	public partial interface VKAccessToken {

		[Export ("accessToken", ArgumentSemantic.Retain)]
		string AccessToken { get; set; }

		[Export ("expiresIn", ArgumentSemantic.Retain)]
		string ExpiresIn { get; set; }

		[Export ("userId", ArgumentSemantic.Retain)]
		string UserId { get; set; }

		[Export ("secret", ArgumentSemantic.Retain)]
		string Secret { get; set; }

		[Export ("httpsRequired")]
		bool HttpsRequired { get; set; }

		[Export ("created")]
		double Created { get; }

		[Export ("isExpired")]
		bool IsExpired { get; }

		[Static, Export ("tokenFromUrlString:")]
		VKAccessToken TokenFromUrlString (string urlString);

		[Static, Export ("tokenFromFile:")]
		VKAccessToken TokenFromFile (string filePath);

		[Static, Export ("tokenFromDefaults:")]
		VKAccessToken TokenFromDefaults (string defaultsKey);

		[Export ("saveTokenToFile:")]
		void SaveTokenToFile (string filePath);

		[Export ("saveTokenToDefaults:")]
		void SaveTokenToDefaults (string defaultsKey);
	}

	[BaseType (typeof (VKObject))]
	public partial interface VKError {

		[Export ("httpError", ArgumentSemantic.Retain)]
		NSError HttpError { get; set; }

		[Export ("apiError", ArgumentSemantic.Retain)]
		VKError ApiError { get; set; }

		[Export ("request", ArgumentSemantic.Retain)]
		VKRequest Request { get; set; }

		[Export ("errorCode")]
		int ErrorCode { get; set; }

		[Export ("errorMessage", ArgumentSemantic.Retain)]
		string ErrorMessage { get; set; }

		[Export ("errorReason", ArgumentSemantic.Retain)]
		string ErrorReason { get; set; }

		[Export ("requestParams", ArgumentSemantic.Retain)]
		NSDictionary RequestParams { get; set; }

		[Export ("captchaSid", ArgumentSemantic.Retain)]
		string CaptchaSid { get; set; }

		[Export ("captchaImg", ArgumentSemantic.Retain)]
		string CaptchaImg { get; set; }

		[Export ("redirectUri", ArgumentSemantic.Retain)]
		string RedirectUri { get; set; }

		[Static, Export ("errorWithCode:")]
		VKError ErrorWithCode (int errorCode);

		[Static, Export ("errorWithJson:")]
		VKError ErrorWithJson (NSObject JSON);

		[Static, Export ("errorWithQuery:")]
		VKError ErrorWithQuery (NSDictionary queryParams);

		[Export ("answerCaptcha:")]
		void AnswerCaptcha (string userEnteredCode);
	}

	[Model, BaseType (typeof (NSObject))]
	public partial interface VKSdkDelegate {

		[Export ("vkSdkNeedCaptchaEnter:")]
		void  SdkNeedCaptchaEnter(VKError captchaError);

		[Export ("vkSdkTokenHasExpired:")]
		void SdkTokenHasExpired (VKAccessToken expiredToken);

		[Export ("vkSdkUserDeniedAccess:")]
		void  SdkUserDeniedAccess(VKError authorizationError);

		[Export ("vkSdkShouldPresentViewController:")]
		void  SdkShouldPresentViewController(UIViewController controller);

		[Export ("vkSdkDidReceiveNewToken:")]
		void  SdkDidReceiveNewToken(VKAccessToken newToken);

		[Export ("vkSdkDidAcceptUserToken:")]
		void SdkDidAcceptUserToken (VKAccessToken token);
	}

	[BaseType (typeof (NSObject))]
	public partial interface VKSdk {

		[Export ("delegate", ArgumentSemantic.Assign)]
		VKSdkDelegate Delegate { get; set; }

		[Static, Export ("instance"), /*Verify ("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKSdk.h", Line = 103)*/]
		VKSdk Instance { get; }

		[Static, Export ("initializeWithDelegate:andAppId:")]
		void InitializeWithDelegate (VKSdkDelegate del, string appId);

		[Static, Export ("initializeWithDelegate:andAppId:andCustomToken:")]
		void InitializeWithDelegate (VKSdkDelegate del, string appId, VKAccessToken token);

		[Static, Export ("authorize:")]
		void Authorize (/*[Verify ("NSArray may be reliably typed, check the documentation", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKSdk.h", Line = 131)]*/ NSObject [] permissions);

		[Static, Export ("authorize:revokeAccess:")]
		void Authorize (/*[Verify ("NSArray may be reliably typed, check the documentation", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKSdk.h", Line = 139)]*/ NSObject [] permissions, bool revokeAccess);

		[Static, Export ("authorize:revokeAccess:forceOAuth:")]
		void Authorize (/*[Verify ("NSArray may be reliably typed, check the documentation", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKSdk.h", Line = 147)]*/ NSObject [] permissions, bool revokeAccess, bool forceOAuth);

		[Static, Export ("authorize:revokeAccess:forceOAuth:inApp:")]
		void Authorize (/*[Verify ("NSArray may be reliably typed, check the documentation", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKSdk.h", Line = 157)]*/ NSObject [] permissions, bool revokeAccess, bool forceOAuth, bool inApp);

		[Static, Export ("accessToken"), /*Verify ("ObjC method massaged into setter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKSdk.h", Line = 167)*/]
		VKAccessToken AccessToken { set; }

		[Static, Export ("accessTokenError"), /*Verify ("ObjC method massaged into setter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKSdk.h", Line = 173)*/]
		VKError AccessTokenError { set; }

		[Static, Export ("getAccessToken"), /*Verify ("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKSdk.h", Line = 179)*/]
		VKAccessToken GetAccessToken { get; }

		[Static, Export ("processOpenURL:fromApplication:")]
		bool ProcessOpenURL (NSUrl passedUrl, string sourceApplication);

		[Static, Export ("forceLogout")]
		void ForceLogout ();
	}

	[BaseType(typeof(NSObject))]
	public partial interface VKObject
	{
	}

	[BaseType(typeof(VKObject))]
	public partial interface VKResponse
	{
		[Export("request", ArgumentSemantic.Assign)]
		VKRequest Request { get; set; }

		[Export("json", ArgumentSemantic.Retain)]
		NSObject Json { get; set; }

		[Export("parsedModel", ArgumentSemantic.Retain)]
		NSObject ParsedModel { get; set; }
	}

	public enum VKProgressType
	{
		Upload,
		Download
	}

	[BaseType(typeof(NSMutableDictionary))]
	public partial interface OrderedDictionary
	{
		[Export("insertObject:forKey:atIndex:")]
		void InsertObject(NSObject anObject, NSObject aKey, uint anIndex);

		[Export("keyAtIndex:")]
		NSObject KeyAtIndex(uint anIndex);

		[Export("reverseKeyEnumerator"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/OrderedDictionary.h", Line = 34)*/]
		NSEnumerator ReverseKeyEnumerator { get; }
	}

	[BaseType(typeof(NSOperation))]
	public partial interface VKOperation
	{
		[Export("state")]
		VKOperationState State { get; set; }
		/*		[Export("lock", ArgumentSemantic.Retain)]
		object Lock { get; }*/
	}

	[BaseType(typeof(VKOperation))]
	public partial interface VKHTTPOperation : INSUrlConnectionDelegate
	{
		[Export("loadingRequest", ArgumentSemantic.Retain)]
		VKRequest LoadingRequest { get; set; }

		[Static, Export("operationWithRequest:")]
		VKHTTPOperation OperationWithRequest(VKRequest request);

		[Export("runLoopModes", ArgumentSemantic.Retain)]
		NSSet RunLoopModes { get; set; }

		[Export("vkRequest", ArgumentSemantic.Assign)]
		VKRequest VkRequest { get; }

		[Export("request", ArgumentSemantic.Retain)]
		NSUrlRequest Request { get; }

		[Export("error", ArgumentSemantic.Retain)]
		NSError Error { get; }

		[Export("responseData", ArgumentSemantic.Retain)]
		NSData ResponseData { get; }

		[Export("responseString", ArgumentSemantic.Copy)]
		string ResponseString { get; }

		[Export("responseJson", ArgumentSemantic.Copy)]
		NSObject ResponseJson { get; }

		[Export("response", ArgumentSemantic.Retain)]
		NSHttpUrlResponse Response { get; }

		[Export("successCallbackQueue", ArgumentSemantic.Assign)]
		NSObject SuccessCallbackQueue { get; set; }

		[Export("failureCallbackQueue", ArgumentSemantic.Assign)]
		NSObject FailureCallbackQueue { get; set; }

		[Export("initWithURLRequest:")]
		IntPtr Constructor(NSUrlRequest urlRequest);

		[Export("pause")]
		void Pause();

		[Export("isPaused"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKHTTPOperation.h", Line = 123)*/]
		bool IsPaused { get; }

		[Export("resume")]
		void Resume();

		[Export("shouldExecuteAsBackgroundTaskWithExpirationHandler"), /*		Verify("ObjC method massaged into setter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKHTTPOperation.h", Line = 141)*/]
		Action ShouldExecuteAsBackgroundTaskWithExpirationHandler { set; }

		[Export("uploadProgressBlock"), /*		Verify("ObjC method massaged into setter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKHTTPOperation.h", Line = 148)*/]
		Action UploadProgressBlock { set; }

		[Export("downloadProgressBlock"), /*		Verify("ObjC method massaged into setter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKHTTPOperation.h", Line = 155)*/]
		Action DownloadProgressBlock { set; }

		[Export("setCompletionBlockWithSuccess:failure:")]
		void SetCompletionBlockWithSuccess(Action success, Action failure);
	}

	[BaseType(typeof(VKObject))]
	public partial interface VKRequest
	{
		[Export("preferredLang", ArgumentSemantic.Retain)]
		string PreferredLang { get; set; }

		[Export("progressBlock", ArgumentSemantic.Copy)]
		Action ProgressBlock { get; set; }

		[Export("completeBlock", ArgumentSemantic.Copy)]
		Action CompleteBlock { get; set; }

		[Export("errorBlock", ArgumentSemantic.Copy)]
		Action ErrorBlock { get; set; }

		[Export("attempts")]
		int Attempts { get; set; }

		[Export("secure")]
		bool Secure { get; set; }

		[Export("useSystemLanguage")]
		bool UseSystemLanguage { get; set; }

		[Export("parseModel")]
		bool ParseModel { get; set; }

		[Export("methodName")]
		string MethodName { get; }

		[Export("httpMethod")]
		string HttpMethod { get; }

		[Export("methodParameters")]
		NSDictionary MethodParameters { get; }

		[Export("executionOperation")]
		NSOperation ExecutionOperation { get; }

		[Static, Export("requestWithMethod:andParameters:andHttpMethod:")]
		VKRequest RequestWithMethod(string method, NSDictionary parameters, string httpMethod);

		[Static, Export("requestWithMethod:andParameters:andHttpMethod:classOfModel:")]
		VKRequest RequestWithMethod(string method, NSDictionary parameters, string httpMethod, Class modelClass);

		[Static, Export("photoRequestWithPostUrl:withPhotos:")]
		VKRequest PhotoRequestWithPostUrl(string url, /*		[Verify("NSArray may be reliably typed, check the documentation", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKRequest.h", Line = 117)]*/NSObject[] photoObjects);

		[Export("getPreparedRequest"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKRequest.h", Line = 124)*/]
		NSUrlRequest GetPreparedRequest { get; }

		[Export("executeWithResultBlock:errorBlock:")]
		void ExecuteWithResultBlock(Action completeBlock, Action errorBlock);

		[Export("executeAfter:withResultBlock:errorBlock:")]
		void ExecuteAfter(VKRequest request, Action completeBlock, Action errorBlock);

		[Export("start")]
		void Start();

		[Export("repeat")]
		void Repeat();

		[Export("cancel")]
		void Cancel();

		[Export("addExtraParameters:")]
		void AddExtraParameters(NSDictionary extraParameters);
	}

	[BaseType(typeof(VKObject))]
	public partial interface VKApiBase
	{
		[Export("getMethodGroup"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiBase.h", Line = 39)*/]
		string GetMethodGroup { get; }

		[Export("prepareRequestWithMethodName:andParameters:")]
		VKRequest PrepareRequestWithMethodName(string methodName, NSDictionary methodParameters);

		[Export("prepareRequestWithMethodName:andParameters:andHttpMethod:")]
		VKRequest PrepareRequestWithMethodName(string methodName, NSDictionary methodParameters, string httpMethod);

		[Export("prepareRequestWithMethodName:andParameters:andHttpMethod:andClassOfModel:")]
		VKRequest PrepareRequestWithMethodName(string methodName, NSDictionary methodParameters, string httpMethod, Class modelClass);
	}

	[BaseType(typeof(VKApiBase))]
	public partial interface VKApiUsers
	{
		[Export("get"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiUsers.h", Line = 33)*/]
		VKRequest Get();

		[Export("get:")]
		VKRequest Get(NSDictionary parameters);

		[Export("search:")]
		VKRequest Search(NSDictionary parameters);

		[Export("isAppUser"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiUsers.h", Line = 51)*/]
		VKRequest IsAppUser();

		[Export("isAppUser:")]
		VKRequest IsAppUser(int userID);

		[Export("getSubscriptions"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiUsers.h", Line = 63)*/]
		VKRequest GetSubscriptions();

		[Export("getSubscriptions:")]
		VKRequest GetSubscriptions(NSDictionary parameters);

		[Export("getFollowers"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiUsers.h", Line = 75)*/]
		VKRequest GetFollowers();

		[Export("getFollowers:")]
		VKRequest GetFollowers(NSDictionary parameters);
	}

	[BaseType(typeof(VKApiBase))]
	public partial interface VKApiFriends
	{
		[Export("get"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiFriends.h", Line = 28)*/]
		VKRequest Get();

		[Export("get:")]
		VKRequest Get(NSDictionary parameters);
	}

	[BaseType(typeof(VKApiBase))]
	public partial interface VKApiPhotos
	{
		[Export("getUploadServer:")]
		VKRequest GetUploadServer(int albumId);

		[Export("getUploadServer:andGroupId:")]
		VKRequest GetUploadServer(int albumId, int groupId);

		[Export("getWallUploadServer"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiPhotos.h", Line = 46)*/]
		VKRequest GetWallUploadServer();

		[Export("getWallUploadServer:")]
		VKRequest GetWallUploadServer(int groupId);

		[Export("save:")]
		VKRequest Save(NSDictionary parameters);

		[Export("saveWallPhoto:")]
		VKRequest SaveWallPhoto(NSDictionary parameters);
	}

	[BaseType(typeof(VKApiBase))]
	public partial interface VKApiWall
	{
		[Export("post:")]
		VKRequest Post(NSDictionary parameters);
	}

	[BaseType(typeof(VKApiBase))]
	public partial interface VKApiCaptcha
	{
		[Export("force"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiCaptcha.h", Line = 33)*/]
		VKRequest Force { get; }
	}

	[BaseType(typeof(VKObject))]
	public partial interface VKImageParameters
	{
		[Export("imageType")]
		VKImageType ImageType { get; set; }

		[Export("jpegQuality")]
		float JpegQuality { get; set; }

		[Static, Export("pngImage"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKImageParameters.h", Line = 49)*/]
		VKImageParameters PngImage { get; }

		[Static, Export("jpegImageWithQuality:")]
		VKImageParameters JpegImageWithQuality(float quality);

		[Export("fileExtension"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKImageParameters.h", Line = 61)*/]
		string FileExtension { get; }

		[Export("mimeType"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKImageParameters.h", Line = 66)*/]
		string MimeType { get; }
	}

	[BaseType(typeof(VKObject))]
	public partial interface VKUploadImage
	{
		[Export("imageData", ArgumentSemantic.Retain)]
		NSData ImageData { get; set; }

		[Export("parameters", ArgumentSemantic.Retain)]
		VKImageParameters Parameters { get; set; }

		[Static, Export("objectWithData:andParams:")]
		VKUploadImage ObjectWithData(NSData data, VKImageParameters parameters);
	}

	[BaseType(typeof(VKObject))]
	public partial interface VKApiObject
	{
		[Export("fields", ArgumentSemantic.Retain)]
		NSDictionary Fields { get; set; }

		[Export("initWithDictionary:")]
		IntPtr Constructor(NSDictionary dict);

		[Export("serialize"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApiObject.h", Line = 43)*/]
		NSDictionary Serialize { get; }
	}

	[BaseType(typeof(VKApiObject))]
	public partial interface VKUser
	{
		[Export("id", ArgumentSemantic.Retain)]
		NSNumber Id { get; set; }

		[Export("first_name", ArgumentSemantic.Retain)]
		string First_name { get; set; }

		[Export("last_name", ArgumentSemantic.Retain)]
		string Last_name { get; set; }

		[Export("sex", ArgumentSemantic.Retain)]
		NSNumber Sex { get; set; }

		[Export("bdate", ArgumentSemantic.Retain)]
		string Bdate { get; set; }

		[Export("city", ArgumentSemantic.Retain)]
		string City { get; set; }

		[Export("country", ArgumentSemantic.Retain)]
		string Country { get; set; }

		[Export("photo_50", ArgumentSemantic.Retain)]
		string Photo_50 { get; set; }

		[Export("photo_100", ArgumentSemantic.Retain)]
		string Photo_100 { get; set; }

		[Export("photo_200_orig", ArgumentSemantic.Retain)]
		string Photo_200_orig { get; set; }

		[Export("photo_200", ArgumentSemantic.Retain)]
		string Photo_200 { get; set; }

		[Export("photo_400_orig", ArgumentSemantic.Retain)]
		string Photo_400_orig { get; set; }

		[Export("photo_max", ArgumentSemantic.Retain)]
		string Photo_max { get; set; }

		[Export("photo_max_orig", ArgumentSemantic.Retain)]
		string Photo_max_orig { get; set; }

		[Export("online", ArgumentSemantic.Retain)]
		NSNumber Online { get; set; }

		[Export("online_mobile", ArgumentSemantic.Retain)]
		NSNumber Online_mobile { get; set; }

		[Export("lists", ArgumentSemantic.Retain)]
		string Lists { get; set; }

		[Export("domain", ArgumentSemantic.Retain)]
		string Domain { get; set; }

		[Export("has_mobile", ArgumentSemantic.Retain)]
		NSNumber Has_mobile { get; set; }

		[Export("contacts", ArgumentSemantic.Retain)]
		NSDictionary Contacts { get; set; }

		[Export("connections", ArgumentSemantic.Retain)]
		string Connections { get; set; }

		[Export("site", ArgumentSemantic.Retain)]
		string Site { get; set; }

		[Export("education", ArgumentSemantic.Retain)]
		NSDictionary Education { get; set; }

		[Export("universities", ArgumentSemantic.Retain), /*		Verify("NSArray may be reliably typed, check the documentation", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKUser.h", Line = 55)*/]
		NSObject [] Universities { get; set; }

		[Export("schools", ArgumentSemantic.Retain), /*		Verify("NSArray may be reliably typed, check the documentation", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKUser.h", Line = 56)*/]
		NSObject [] Schools { get; set; }

		[Export("can_post", ArgumentSemantic.Retain)]
		NSNumber Can_post { get; set; }

		[Export("can_see_all_posts", ArgumentSemantic.Retain)]
		NSNumber Can_see_all_posts { get; set; }

		[Export("can_see_audio", ArgumentSemantic.Retain)]
		NSNumber Can_see_audio { get; set; }

		[Export("can_write_private_message", ArgumentSemantic.Retain)]
		NSNumber Can_write_private_message { get; set; }

		[Export("status", ArgumentSemantic.Retain)]
		string Status { get; set; }

		[Export("last_seen", ArgumentSemantic.Retain)]
		NSDictionary Last_seen { get; set; }

		[Export("common_count", ArgumentSemantic.Retain)]
		NSNumber Common_count { get; set; }

		[Export("relation", ArgumentSemantic.Retain)]
		string Relation { get; set; }

		[Export("relatives", ArgumentSemantic.Retain), /*		Verify("NSArray may be reliably typed, check the documentation", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKUser.h", Line = 65)*/]
		NSObject [] Relatives { get; set; }

		[Export("counters", ArgumentSemantic.Retain)]
		NSDictionary Counters { get; set; }
	}

	[BaseType(typeof(NSObject))]
	public partial interface VKApi
	{
		[Static, Export("users"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApi.h", Line = 42)*/]
		VKApiUsers Users { get; }

		[Static, Export("wall"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApi.h", Line = 47)*/]
		VKApiWall Wall { get; }

		[Static, Export("photos"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApi.h", Line = 52)*/]
		VKApiPhotos Photos { get; }

		[Static, Export("friends"), /*		Verify("ObjC method massaged into getter property", "/Users/diego/Projects/vk-ios-sdk/sdk/sdk/VKApi.h", Line = 57)*/]
		VKApiFriends Friends { get; }

		[Static, Export("requestWithMethod:andParameters:andHttpMethod:")]
		VKRequest RequestWithMethod(string method, NSDictionary parameters, string httpMethod);

		[Static, Export("uploadWallPhotoRequest:parameters:userId:groupId:")]
		VKRequest UploadWallPhotoRequest(UIImage image, VKImageParameters parameters, long userId, int groupId);

		[Static, Export("uploadAlbumPhotoRequest:parameters:albumId:groupId:")]
		VKRequest UploadAlbumPhotoRequest(UIImage image, VKImageParameters parameters, int albumId, int groupId);
	}
}
