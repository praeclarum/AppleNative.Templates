CONFIGURATION=Debug

all: app testapp
	
app:
	dotnet build -c $(CONFIGURATION) MacCatApp/MacCatApp.fsproj

testapp:
	dotnet build -c $(CONFIGURATION) Tests/Tests.fsproj

run:
	dotnet build -c $(CONFIGURATION) MacCatApp/MacCatApp.fsproj
	MacCatApp/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/MacCatApp.app/Contents/MacOS/MacCatApp

test:
	dotnet build -c $(CONFIGURATION) Tests/Tests.fsproj
	Tests/bin/$(CONFIGURATION)/net6.0-maccatalyst/maccatalyst-x64/Tests.app/Contents/MacOS/Tests

clean:
	rm -rf MacCatApp/obj
	rm -rf MacCatApp/bin
	rm -rf Tests/obj
	rm -rf Tests/bin
