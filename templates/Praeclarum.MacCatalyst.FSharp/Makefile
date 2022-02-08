CONFIGURATION=Debug

all: app testapp
	
app: MacCatApp/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/MacCatApp.app/Contents/MacOS/MacCatApp

MacCatApp/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/MacCatApp.app/Contents/MacOS/MacCatApp:
	dotnet build -c $(CONFIGURATION) MacCatApp/MacCatApp.fsproj

Tests/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Tests.app/Contents/MacOS/Tests:
	dotnet build -c $(CONFIGURATION) Tests/Tests.fsproj

testapp: Tests/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Tests.app/Contents/MacOS/Tests

test: testapp
	Tests/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Tests.app/Contents/MacOS/Tests

clean:
	rm -rf MacCatApp/obj
	rm -rf MacCatApp/bin
	rm -rf Tests/obj
	rm -rf Tests/bin
