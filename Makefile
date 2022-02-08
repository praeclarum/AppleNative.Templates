CONFIGURATION=Debug

all:

pack:
	find . -name '.DS_Store' -type f -delete
	dotnet pack -c $(CONFIGURATION) -p:NoDefaultExcludes=true
