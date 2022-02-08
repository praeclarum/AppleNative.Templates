CONFIGURATION=Debug

all: app testapp
	
app: Application/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Application.app/Contents/MacOS/Application

Application/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Application.app/Contents/MacOS/Application:
	dotnet build -c $(CONFIGURATION) Application/Application.fsproj

Tests/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Tests.app/Contents/MacOS/Tests:
	dotnet build -c $(CONFIGURATION) Tests/Tests.fsproj

testapp: Tests/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Tests.app/Contents/MacOS/Tests

test: testapp
	Tests/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Tests.app/Contents/MacOS/Tests

clean:
	rm -rf Application/obj
	rm -rf Application/bin
	rm -rf Tests/obj
	rm -rf Tests/bin
