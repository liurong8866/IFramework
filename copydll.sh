#!/bin/bash
# 拷贝dll到unity目录
# by authors liurong 2021-08-11

echo "构建解决方案"
# /t:rebuild  重新生成
# /t:build  生成
# /t:clean   清理
# /p:Configuration=Debug 编译模式：debug
# /p:Configuration=release 编译模式：release
msbuild "IFramework.Core/IFramework.Core.csproj" -t:rebuild -p:Configuration=Debug -v:m

framework="IFramework.Unity/Assets/IFramework/"
environment="${framework}Environment/"

if [ ! -d "$framework" ]; then
    echo "创建 IFramework"
    mkdir $framework
fi

if [ ! -d "$environment" ]; then
    echo "创建 environment"
    mkdir $environment
fi

echo '拷贝 IFramework.Core'
cp -f IFramework.Core/obj/Debug/net48/IFramework.Core.dll "$framework"
cp -f IFramework.Core/obj/Debug/net48/IFramework.Core.pdb "$framework"

echo '拷贝 Settings/Environment'
cp -f IFramework.Core/bin/Debug/net48/Environment/Environment.cs "$environment"
cp -f IFramework.Core/bin/Debug/net48/Environment/Zip.cs "$environment"

# echo '拷贝 IFramework.Editor'
# cp -f IFramework.Editor/obj/Debug/net48/IFramework.Editor.dll "$framework"
# cp -f IFramework.Editor/obj/Debug/net48/IFramework.Editor.pdb "$framework"

# echo '拷贝 IFramework.Engine'
# cp -f IFramework.Engine/obj/Debug/net48/IFramework.Engine.dll "$framework"
# cp -f IFramework.Engine/obj/Debug/net48/IFramework.Engine.pdb "$framework"

echo '拷贝完毕'
