#!/bin/bash  
# 拷贝dll到unity目录
# by authors liurong 2021-08-11


@echo "构建解决方案"
# /t:rebuild  重新生成
# /t:build  生成
# /t:clean   清理
# /p:Configuration=Debug 编译模式：debug
# /p:Configuration=release 编译模式：release
msbuild "IFramework.Core/IFramework.Core.csproj" -t:rebuild  -p:Configuration=Debug  -v:m


echo '拷贝 IFramework.Core'
cp -f IFramework.Core/obj/Debug/net48/IFramework.Core.dll IFramework.Unity/Assets/IFramework/
cp -f IFramework.Core/obj/Debug/net48/IFramework.Core.pdb IFramework.Unity/Assets/IFramework/

echo '拷贝 Settings/Environment'
cp -f IFramework.Core/bin/Debug/net48/Environment/Environment.cs IFramework.Unity/Assets/IFramework/

echo '拷贝 IFramework.Editor'
cp -f IFramework.Editor/obj/Debug/net48/IFramework.Editor.dll IFramework.Unity/Assets/IFramework/
cp -f IFramework.Editor/obj/Debug/net48/IFramework.Editor.pdb IFramework.Unity/Assets/IFramework/

echo '拷贝 IFramework.Engine'
cp -f IFramework.Engine/obj/Debug/net48/IFramework.Engine.dll IFramework.Unity/Assets/IFramework/
cp -f IFramework.Engine/obj/Debug/net48/IFramework.Engine.pdb IFramework.Unity/Assets/IFramework/


echo '拷贝完毕'