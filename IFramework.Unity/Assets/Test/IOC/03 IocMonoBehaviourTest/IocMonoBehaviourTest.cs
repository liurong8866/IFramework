using IFramework.Core;
using UnityEngine;

public class IocMonoBehaviourTest : IocMonoBehaviour
{
    [Autowired("Teacher")] private IPerson teacher;
    [Autowired("Student")] private IPerson sutdent;

    // protected override void Init()
    // {
    //     Register<IPerson, Teacher>("Teacher");
    //     Register<IPerson, Student>("Student");
    // }

    private void Start()
    {
        teacher.print("IocMonoBehaviourTest");
        sutdent.print("IocMonoBehaviourTest");
    }
    
    // 可以把该方法放到单独的初始化类中，系统启动时加载，便于维护
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RegisterIoc()
    {
        IocContainer container = IocContainer.Instance;
        
        container.Register<IPerson, Teacher>("Teacher");
        container.Register<IPerson, Student>("Student");
    }
}

public interface IPerson
{
    string name { get; }
    string age { get; }

    void print(string str);
}

public class Teacher : IPerson
{
    public string name { get; }
    public string age { get; }

    public void print(string str)
    {
        Log.Info("我是老师: " + str);
    }

    public string clazz { get; }

    public Teacher() { }

    public Teacher(string name, string age, string clazz)
    {
        this.name = name;
        this.age = age;
        this.clazz = clazz;
    }
}

public class Student : IPerson
{
    public string name { get; }
    public string age { get; }
    public string book { get; }
    public Teacher teacher { get; }

    public Student() { }

    public Student(string name, string age, string book, Teacher teacher)
    {
        this.name = name;
        this.age = age;
        this.book = book;
        this.teacher = teacher;
    }

    public void print(string str)
    {
        Log.Info("我是学生: " + str);
    }
}
