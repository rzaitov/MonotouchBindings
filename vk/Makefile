XBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT_ROOT=.
PROJECT=$(PROJECT_ROOT)/vk-ios-sdk/sdk/sdk.xcodeproj
TARGET=VKSdk

fat: libVKSdk.a

createdir:
	mkdir bin

libVKSdk-i386.a: createdir
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv $(PROJECT_ROOT)/vk-ios-sdk/sdk/build/Release-iphonesimulator/lib$(TARGET).a ./bin/$@

libVKSdk-armv7.a: createdir
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch armv7 -configuration Release clean build
	-mv $(PROJECT_ROOT)/vk-ios-sdk/sdk/build/Release-iphoneos/lib$(TARGET).a ./bin/$@

libVKSdk.a:	libVKSdk-i386.a libVKSdk-armv7.a
	cd bin; lipo -create -output $@ $^

clean:
	-rm -f bin/*.a bin/*.dll
	-rmdir bin