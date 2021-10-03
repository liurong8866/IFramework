namespace IFramework.Test.Model
{
    public class UserInfo
    {
        public UserInfo() { }

        public UserInfo(string userName, string password, int age, string sex)
        {
            UserName = userName;
            Age = age;
            Sex = sex;
        }

        public string UserName { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public override string ToString() { return $"username: {UserName}, age: {Age}, sex: {Sex}"; }
    }
}
