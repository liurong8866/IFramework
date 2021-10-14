using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using IFramework.Core;
using IFramework.Engine;
using Microsoft.CSharp;
using UnityEngine;

namespace IFramework.Editor
{
    public class AssetBundleScript
    {
        /// <summary>
        /// 生成常量名
        /// </summary>
        public static void GenerateConstScript()
        {
            Log.Info("生成脚本: 开始！");
            // 生成文件路径
            string path = DirectoryUtils.CombinePath(Application.dataPath, Constant.ENVIRONMENT_PATH, Constant.ASSET_BUNDLE_SCRIPT_FILE);
            // 完成组装
            Generate("IFramework.Engine", path);
            Log.Info("生成脚本: 完成！");
        }

        /// <summary>
        /// 生成脚本
        /// </summary>
        private static void Generate(string nameSpace, string outputPath)
        {
            // 初始化要生成的AssetBundle
            List<AssetBundleScriptModel> assetModelList = InitAssetBundleModel();
            // 创建一个编译单元
            CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
            // 生成命名空间
            CodeNamespace codeNamespace = new CodeNamespace(nameSpace);
            // 取消当前文件代码格式检查
            codeNamespace.Comments.Add(new CodeCommentStatement("ReSharper disable All"));

            // 添加命名空间到编译单元
            codeCompileUnit.Namespaces.Add(codeNamespace);

            //AssetsName 主类
            CodeTypeDeclaration assetClass = new CodeTypeDeclaration(Constant.ASSET_BUNDLE_SCRIPT_FILE.Replace(".cs", ""));
            assetClass.Comments.Add(new CodeCommentStatement("AssetBundle资源名称常量，自动生成，请勿修改"));
            assetClass.TypeAttributes = TypeAttributes.Public;
            assetClass.IsPartial = true;
            // 把类添加到命名空间下
            codeNamespace.Types.Add(assetClass);

            // 循环每个AssetBundle，每个AssetBundle生成一个类
            foreach (AssetBundleScriptModel assetModle in assetModelList) {
                // 驼峰格式
                string bundleName = assetModle.Name;

                // 首字母不能是数字
                if (bundleName[0].IsNumeric()) continue;

                // 定义类名称
                string className = assetModle.Name.Replace("/", "_").Replace("@", "_").Replace("!", "_").Replace("-", "_").ToPascal('_');

                //准备要生成的类的定义
                CodeTypeDeclaration classCode = new CodeTypeDeclaration(className);

                // 把类添加到命名空间下
                assetClass.Members.Add(classCode);

                //添加字段 ASSET_BUNDLE_NAME
                CodeMemberField bundleNameField = new CodeMemberField {
                    Attributes = MemberAttributes.Public | MemberAttributes.Const,
                    Name = "ASSET_BUNDLE_NAME",
                    Type = new CodeTypeReference(typeof(string))
                };
                bundleNameField.InitExpression = new CodePrimitiveExpression(bundleName.ToLowerInvariant());
                classCode.Members.Add(bundleNameField);

                // 循环所有资源，生成字段
                GenerateFields(assetModle, classCode);
            }
            // 设置编译器
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CodeGeneratorOptions options = new CodeGeneratorOptions {
                BlankLinesBetweenMembers = false,
                BracingStyle = "CS"
            };

            // 写入文件
            using StreamWriter sw = new StreamWriter(outputPath);
            provider.GenerateCodeFromCompileUnit(codeCompileUnit, sw, options);
        }

        /// <summary>
        /// 初始化要生成的AssetBundle
        /// </summary>
        private static List<AssetBundleScriptModel> InitAssetBundleModel()
        {
            AssetBundleConfig assetBundleConfig = new AssetBundleConfig();
            Environment.Instance.InitAssetBundleConfig(assetBundleConfig);
            List<AssetBundleInfo> assetBundleList = assetBundleConfig.AssetBundleList;
            List<AssetBundleScriptModel> assetModelList = new List<AssetBundleScriptModel>();

            // 初始化要生成到AssetBundle
            foreach (AssetBundleInfo assetGroup in assetBundleList) {
                List<AssetDependence> depends = assetGroup.AssetDepends;
                foreach (AssetDependence depend in depends) {
                    AssetBundleScriptModel model = new AssetBundleScriptModel(depend.AssetBundleName);
                    model.assets = assetGroup.AssetInfos.Where(info => info.AssetBundleName == depend.AssetBundleName).Select(info => info.AssetName).ToArray();
                    assetModelList.Add(model);
                }
            }
            return assetModelList;
        }

        /// <summary>
        /// 用资源名称生成字段
        /// </summary>
        /// <param name="assetModle"></param>
        /// <param name="classCode"></param>
        private static void GenerateFields(AssetBundleScriptModel assetModle, CodeTypeDeclaration classCode)
        {
            // 用于检查是否重复
            ISet<string> checkRepeatSet = new HashSet<string>();
            foreach (string asset in assetModle.assets) {
                CodeMemberField assetField = new CodeMemberField {
                    Attributes = MemberAttributes.Public | MemberAttributes.Const
                };
                string content = FileUtils.GetFileNameByPath(asset, false);
                assetField.Name = content.ToUpperInvariant().Replace("@", "_").Replace("!", "_").Replace("-", "_");
                assetField.Type = new CodeTypeReference(typeof(string));

                // 如果不是[开头，并且不重复
                if (!assetField.Name.StartsWith("[") && !assetField.Name.StartsWith(" [") && !checkRepeatSet.Contains(assetField.Name)) {
                    classCode.Members.Add(assetField);
                    checkRepeatSet.Add(assetField.Name);
                }
                assetField.InitExpression = new CodePrimitiveExpression(content);
            }
            checkRepeatSet.Clear();
        }
    }

    /// <summary>
    /// 用于生成代码的临时类
    /// </summary>
    internal class AssetBundleScriptModel
    {
        public readonly string Name;
        public string[] assets;

        public AssetBundleScriptModel(string name)
        {
            Name = name;
        }
    }
}
