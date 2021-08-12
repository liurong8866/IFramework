/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    /// <summary>
    /// GUI生命周期展示
    /// </summary>
    public class GUIWindowLiftCircle : EditorWindow
    {
        static void Open()
        {
            //创建窗口
            Rect wr = new Rect(0, 0, 500, 500);
            AssetBundleWindow window = (AssetBundleWindow) EditorWindow.GetWindowWithRect(typeof(AssetBundleWindow), wr, true, "资源管理器");
            window.Show();
        }

        //输入文字的内容
        private string text;

        //选择贴图的对象
        private Texture texture;

        private Vector3 startPoint; //起点
        private Vector3 endPoint; //终点
        private float distance = 0f; //起点到终点的距离

        private bool val;
        private Color color = Color.red;

        private AnimationCurve curveX = AnimationCurve.Linear(0, 0, 1, 0);
        private AnimationCurve curveY = AnimationCurve.Linear(0, 0, 1, 1);
        private AnimationCurve curveZ = AnimationCurve.Linear(0, 0, 1, 0);

        private int sliderValue = 1;
        private bool showClose = true;
        private bool showToggleLeft = true;
        private string textfieldtest = "textfieldtest";
        private string password = "pwd123456";

        private float minVal = -10.0f;
        private float minLimit = -20.0f;
        private float maxVal = 10.0f;
        private float maxLimit = 20.0f;

        public void Awake()
        {
            //在资源中读取一张贴图
            texture = Resources.Load("1") as Texture;
        }

        //绘制窗口时调用
        void OnGUI()
        {
            //输入框控件
            text = EditorGUILayout.TextField("输入文字:", text);

            if (GUILayout.Button("打开通知", GUILayout.Width(200)))
            {
                //打开一个通知栏
                this.ShowNotification(new GUIContent("This is a Notification"));
            }

            if (GUILayout.Button("关闭通知", GUILayout.Width(200)))
            {
                //关闭通知栏
                this.RemoveNotification();
            }

            //文本框显示鼠标在窗口的位置
            EditorGUILayout.LabelField("鼠标在窗口的位置", Event.current.mousePosition.ToString());

            //选择贴图
            texture = EditorGUILayout.ObjectField("添加贴图", texture, typeof(Texture), true) as Texture;

            if (GUILayout.Button("关闭窗口", GUILayout.Width(200)))
            {
                //关闭窗口
                this.Close();
            }


            //Vector3类型数据的显示
            startPoint = EditorGUILayout.Vector3Field("Start Point:", startPoint);
            endPoint = EditorGUILayout.Vector3Field("End Point:", endPoint);
            //只读的标签
            EditorGUILayout.LabelField("Distance:", Vector3.Distance(startPoint, endPoint).ToString("f2"));
            //勾选框
            val = EditorGUILayout.Toggle("Can Jump", val);
            //是否开启禁用功能。false表示禁用关闭，true表示开启禁用--灰色状态
            EditorGUI.BeginDisabledGroup(val); //
            //float类型文本Text
            EditorGUILayout.FloatField("跳跃高度：", 100.0f);
            //结束禁用
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.FloatField("跳跃频率：", 1.85f);
            //Bounds输入框
            EditorGUILayout.BoundsField("BoundsField", new Bounds(new Vector3(50, 50, 50), new Vector3(150, 150, 150)));
            //颜色选择框
            color = EditorGUILayout.ColorField("颜色:", color);
            //按钮
            if (GUILayout.Button("Close"))
            {
                this.Close();
            }

            //带有阴影的Label
            EditorGUI.DropShadowLabel(new Rect(0, 250, position.width, 20), "带有阴影的Label");
            //动画曲线
            curveX = EditorGUI.CurveField(new Rect(3, 320, position.width - 6, 50), "Animation on X", curveX);
            curveY = EditorGUI.CurveField(new Rect(3, 370, position.width - 6, 50), "Animation on Y", curveY);
            curveZ = EditorGUI.CurveField(new Rect(3, 420, position.width - 6, 50), "Animation on Z", curveZ);
            //数字输入
            EditorGUI.DelayedDoubleField(new Rect(3, 480, position.width - 6, 20), "DelayedDoubleField1", 25.0);
            EditorGUI.DelayedFloatField(new Rect(3, 500, position.width - 6, 20), "DelayedFloatField", 25.0f);
            EditorGUI.DelayedIntField(new Rect(3, 520, position.width - 6, 20), "DelayedIntField", 25);
            EditorGUI.DelayedTextField(new Rect(3, 540, position.width - 6, 20), "DelayedTextField");
            //绘画矩形
            EditorGUI.DrawRect(new Rect(3, 570, position.width - 60, 20), Color.green);
            //滑动条。输入框(值不能滑动): 注意左边必须要有值接收这个值，否则不能滑动
            sliderValue = EditorGUI.IntSlider(new Rect(3, 600, position.width - 60, 30), sliderValue, 0, 100);
            //帮助盒子信息框
            EditorGUI.HelpBox(new Rect(3, 645, position.width - 60, 40), "HelpBox帮助盒子", MessageType.Info);
            //Toggle 开关
            showClose = EditorGUI.Toggle(new Rect(3, 685, position.width - 60, 20), "Toggle", showClose);
            showToggleLeft = EditorGUI.ToggleLeft(new Rect(3, 710, position.width - 60, 20), "ToggleLeft", showToggleLeft);
            textfieldtest = EditorGUI.TextField(new Rect(3, 735, position.width - 60, 20), "TextField", textfieldtest);
            password = EditorGUI.PasswordField(new Rect(3, 760, position.width - 60, 20), "密码框:", password);
            //最大值和最小值滑块
            EditorGUI.MinMaxSlider(new Rect(3, 790, position.width - 60, 20), ref minVal, ref maxVal, minLimit, maxLimit);
        }

        //更新
        void Update() { }

        void OnFocus()
        {
            Debug.Log("当窗口获得焦点时调用一次");
        }

        void OnLostFocus()
        {
            Debug.Log("当窗口丢失焦点时调用一次");
        }

        void OnHierarchyChange()
        {
            Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
        }

        void OnProjectChange()
        {
            Debug.Log("当Project视图中的资源发生改变时调用一次");
        }

        void OnInspectorUpdate()
        {
            //Debug.Log("窗口面板的更新");
            //这里开启窗口的重绘，不然窗口信息不会刷新
            this.Repaint();
        }

        void OnSelectionChange()
        {
            //当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
            foreach (Transform t in Selection.transforms)
            {
                //有可能是多选，这里开启一个循环打印选中游戏对象的名称
                Debug.Log("OnSelectionChange" + t.name);
            }
        }

        void OnDestroy()
        {
            Debug.Log("当窗口关闭时调用");
        }
    }
}