#!/bin/zsh
# 拷贝dll到unity目录
# by authors liurong 2021-08-11

echo -e "\033[36m 开始构建解决方案 \033[0m"

# /t:rebuild  重新生成
# /t:build  生成
# /t:clean   清理
# /p:Configuration=Debug 编译模式：debug
# /p:Configuration=release 编译模式：release
#  -verbosity:<level> 在事件日志中显示此级别的信息量。
#            可用的详细程度有: q[uiet]、 m[inimal]、
#            n[ormal]、d[etailed] 和 diag[nostic]。(缩写: -v)

echo -e "\033[36m 正在构建项目: IFramework.Core \033[0m"
msbuild "IFramework.Core/IFramework.Core.csproj" -t:rebuild -p:Configuration=Debug -v:m

buildResult=$?

# 如果构建失败，退出
if [ ${buildResult} != 0 ]; then
    exit
fi

echo -e "\033[36m 正在构建项目: IFramework.Engine \033[0m"
msbuild "IFramework.Engine/IFramework.Engine.csproj" -t:rebuild -p:Configuration=Debug -v:m

buildResult=$?

# 如果构建失败，退出
if [ ${buildResult} != 0 ]; then
    exit
fi

echo -e "\033[36m 正在构建项目: IFramework.Engine \033[0m"
msbuild "IFramework.Editor/IFramework.Editor.csproj" -t:rebuild -p:Configuration=Debug -v:m

buildResult=$?

# 如果构建失败，退出
if [ ${buildResult} != 0 ]; then
    exit
fi


if [ "$1" = "sword" ]; then
    framework="/Volumes/Gamespace/Workspace/Sword/Assets/IFramework/"
else 
    framework="IFramework.Unity/Assets/IFramework/"
fi

environment="${framework}Environment/"
editor="${framework}Editor/"

if [ ! -d "$framework" ]; then
    echo -e "\033[36m Create Directory IFramework \033[0m"
    mkdir $framework
fi

if [ ! -d "$environment" ]; then
    echo -e "\033[36m Create Directory environment \033[0m"
    mkdir $environment
fi

if [ ! -d "$editor" ]; then
    echo -e "\033[36m Create Directory editor \033[0m"
    mkdir $editor
fi


echo -e "\033[36m 拷贝 IFramework.Core.dll \033[0m"
cp -f IFramework.Core/obj/Debug/net48/IFramework.Core.dll "$framework"
echo -e "\033[36m 拷贝 IFramework.Core.pdb \033[0m"
cp -f IFramework.Core/obj/Debug/net48/IFramework.Core.pdb "$framework"

echo '拷贝 IFramework.Engine -> ' "$framework" 
echo -e "\033[32m 拷贝 IFramework.Engine.dll \033[0m"
cp -f IFramework.Engine/obj/Debug/net48/IFramework.Engine.dll "$framework"
echo -e "\033[32m 拷贝 IFramework.Engine.pdb \033[0m"
cp -f IFramework.Engine/obj/Debug/net48/IFramework.Engine.pdb "$framework"

echo '拷贝 IFramework.Editor -> ' "$editor" 
echo -e "\033[32m 拷贝 IFramework.Editor.dll \033[0m"
cp -f IFramework.Editor/obj/Debug/net48/IFramework.Editor.dll "$editor"
echo -e "\033[32m 拷贝 IFramework.Editor.pdb \033[0m"
cp -f IFramework.Editor/obj/Debug/net48/IFramework.Editor.pdb "$editor"

echo '拷贝 Environment 文件 -> ' "$environment" 
echo -e "\033[36m 拷贝 Settings/Environment/IFramework.cs \033[0m"
cp -f IFramework.Core/bin/Debug/net48/Environment/IFramework.cs "$environment"

echo -e "\033[36m 拷贝 Settings/Environment/Environment.cs \033[0m"
cp -f IFramework.Core/bin/Debug/net48/Environment/Environment.cs "$environment"

echo -e "\033[36m 拷贝 Settings/Environment/Zip.cs \033[0m"
cp -f IFramework.Core/bin/Debug/net48/Environment/Zip.cs "$environment"

now=$(date +%Y-%m-%d:%H:%M:%S)
echo -e "\033[32m Build succeeded at ${now} \033[0m"
