using System.Collections.Generic;
using System.Linq;
using IFramework.Core;
using UnityEditor;

namespace IFramework.Editor
{
    /// <summary>
    /// 子管理
    /// </summary>
    public class AssetBundlePackage
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Folder { get; set; }
        public string NameSpace { get; set; }

        public List<AssetBundleBuild> packages = new List<AssetBundleBuild>();

        /// <summary>
        /// 查找所有类Package.asset包，确定打包空间
        /// </summary>
        public static List<AssetBundlePackage> GetPackageList()
        {
            //找到所有.asset文件，筛选出Package类型及子类生成的文件
            List<AssetBundlePackage> list = AssetDatabase.GetAllAssetPaths()
                   .Where(path => path.EndsWith(".asset"))
                   .Select(path => {
                        Package package = AssetDatabase.LoadAssetAtPath<Package>(path);
                        if (package) {
                            return new AssetBundlePackage {
                                Path = path,
                                Folder = path.RemoveString(package.name + ".asset"),
                                Name = package.name,
                                NameSpace = package.NameSpace
                            };
                        }
                        return null;
                    })
                   .Where(data => data != null)
                   .ToList();
            return list;
        }

        /// <summary>
        /// 划分默认包、子包
        /// </summary>
        public static void SplitPackage(AssetBundlePackage defaultPackage, List<AssetBundlePackage> subPackages)
        {
            // 获取所有标记的AssetBundle资源
            string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
            foreach (string assetBundleName in assetBundleNames) {
                // 生成资源信息
                AssetBundleBuild assetBundleBuild = new AssetBundleBuild {
                    assetBundleName = assetBundleName,
                    assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName)
                };
                bool idDefault = true;

                // 判断资源是否在子包目录下，如果在，则认为是子包资源
                foreach (AssetBundlePackage subPackage in subPackages) {
                    foreach (string assetName in assetBundleBuild.assetNames) {
                        // 资源路径是否包含子包路径
                        if (assetName.Contains(subPackage.Folder)) {
                            subPackage.packages.Add(assetBundleBuild);
                            idDefault = false;
                            break;
                        }
                    }
                }

                // 如果不在子包，则认为默认包资源
                if (idDefault) {
                    defaultPackage.packages.Add(assetBundleBuild);
                }
            }
        }
    }
}
