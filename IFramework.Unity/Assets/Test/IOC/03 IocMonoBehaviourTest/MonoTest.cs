using System.Collections;
using System.Collections.Generic;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.IOC
{
    public class MonoTest : IocMonoBehaviour
    {
        [Autowired("Student")] public IPerson Student;

        [Autowired("Teacher")] private IPerson Teacher { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            Student.print("MonoTest");
            Teacher.print("MonoTest");
        }
    }
}