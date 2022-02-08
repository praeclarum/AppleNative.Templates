CONFIGURATION=Debug

all: app testapp
	
app:
	dotnet build -c $(CONFIGURATION) AppleNativeApp/AppleNativeApp.fsproj

testapp:
	dotnet build -c $(CONFIGURATION) Tests/Tests.fsproj

run:
	dotnet build -c $(CONFIGURATION) AppleNativeApp/AppleNativeApp.fsproj
	AppleNativeApp/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/AppleNativeApp.app/Contents/MacOS/AppleNativeApp

test:
	dotnet build -c $(CONFIGURATION) Tests/Tests.fsproj
	Tests/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Tests.app/Contents/MacOS/Tests

clean:
	rm -rf AppleNativeApp/obj
	rm -rf AppleNativeApp/bin
	rm -rf Tests/obj
	rm -rf Tests/bin

iosdist:
#	Set TargetFrameworks
	sed -E -i .bak "s:<TargetFrameworks>[^<]+</TargetFrameworks>:<TargetFrameworks>net6.0-ios</TargetFrameworks>:g" AppleNativeApp/AppleNativeApp.fsproj
#	Build iOS
	dotnet build -c Release -f net6.0-ios "/p:BuildIpa=true" "/p:CodesignKey=Apple Distribution" "/p:CodesignProvision=AppleNativeApp iOS Distribution" AppleNativeApp/AppleNativeApp.fsproj

macdist:
#	Remove iPhone from Info.plist
	/usr/libexec/PlistBuddy -x -c "Delete :UIDeviceFamily:0" AppleNativeApp/Info.plist
#	Set TargetFrameworks
	sed -E -i .bak "s:<TargetFrameworks>[^<]+</TargetFrameworks>:<TargetFrameworks>net6.0-maccatalyst</TargetFrameworks>:g" AppleNativeApp/AppleNativeApp.fsproj
#	Build Mac Catalyst
	dotnet build -c Release -f net6.0-maccatalyst "/p:CreatePackage=true" "/p:EnablePackageSigning=true" "/p:EnableCodeSigning=true" "/p:CodeSignEntitlements=Entitlements.plist" "/p:CodesignKey=Apple Distribution" "/p:CodesignProvision=AppleNativeApp Mac Distribution" AppleNativeApp/AppleNativeApp.fsproj
