#!/bin/bash
set -x
set -e

# Set the version
VERSION_MAJOR_MINOR=1.0

# Get the patch number
VERSION_PATCH=$1
if [ -z "$VERSION_PATCH" ]; then
    echo "Usage: $0 <version patch>"
    exit 1
fi

# This is the major.minor.patch version.
VERSION=$VERSION_MAJOR_MINOR.$VERSION_PATCH

# Update the version in the source files
sed -E -i .bak "s:<Version>[0-9]+\\.[0-9]+\\.[0-9]+</Version>:<Version>$VERSION</Version>:g" MacCatApp/MacCatApp.fsproj
/usr/libexec/PlistBuddy -x -c "Set CFBundleShortVersionString $VERSION_MAJOR_MINOR" MacCatApp/Info.plist
/usr/libexec/PlistBuddy -x -c "Set CFBundleVersion $VERSION" MacCatApp/Info.plist
