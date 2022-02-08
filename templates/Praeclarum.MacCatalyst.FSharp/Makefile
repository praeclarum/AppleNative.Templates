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
