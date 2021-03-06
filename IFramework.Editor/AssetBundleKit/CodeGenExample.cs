using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace IFramework.Editor
{
    public class CodeGenExample
    {
        /// <summary>
        /// 代码生成示例
        /// </summary>
        private void GenerateCode()
        {
            //准备一个代码编译器单元
            CodeCompileUnit unit = new CodeCompileUnit();

            //准备必要的命名空间（这个是指要生成的类的空间）
            CodeNamespace sampleNamespace = new CodeNamespace("Xizhang.com");

            //导入必要的命名空间
            sampleNamespace.Imports.Add(new CodeNamespaceImport("System"));

            //准备要生成的类的定义
            CodeTypeDeclaration customerClass = new CodeTypeDeclaration("Customer") {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
            };

            //指定这是一个Class

            //把这个类放在这个命名空间下
            sampleNamespace.Types.Add(customerClass);

            //把该命名空间加入到编译器单元的命名空间集合中
            unit.Namespaces.Add(sampleNamespace);

            //这是输出文件
            string outputFile = "Customer.cs";

            //添加字段
            CodeMemberField field = new CodeMemberField(typeof(string), "_Id") {
                Attributes = MemberAttributes.Private
            };
            customerClass.Members.Add(field);

            //添加属性
            CodeMemberProperty property = new CodeMemberProperty {
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Name = "Id",
                HasGet = true,
                HasSet = true,
                Type = new CodeTypeReference(typeof(string))
            };
            property.Comments.Add(new CodeCommentStatement("这是Id属性"));
            property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_Id")));
            property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_Id"), new CodePropertySetValueReferenceExpression()));
            customerClass.Members.Add(property);

            //添加方法（使用CodeMemberMethod)--此处略
            //添加构造器(使用CodeConstructor) --此处略
            //添加程序入口点（使用CodeEntryPointMethod） --此处略
            //添加事件（使用CodeMemberEvent) --此处略
            //添加特征(使用 CodeAttributeDeclaration)
            customerClass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(SerializableAttribute))));

            //生成代码
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions {
                BracingStyle = "C",
                BlankLinesBetweenMembers = true
            };
            using StreamWriter sw = new StreamWriter(outputFile);
            provider.GenerateCodeFromCompileUnit(unit, sw, options);
        }
    }
}
