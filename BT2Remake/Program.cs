using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BT2Remake
{
    public class Program
    {
        static void Main(string[] args)
        {
            handles handles = new handles();
            Console.OutputEncoding = Encoding.UTF8;
            // sau khi làm xong t hỏi phần này kỹ đấy :>
            // Functions.gI().show("Test kỹ thuật lập trình singleton pattern mà không phải tạo ra toán tử new");

            var classroom1 = new ClassRoom() { id = 1, name = "23DTHA5", description = "A5" };
            var classroom2 = new ClassRoom() { id = 2, name = "23DTHA6", description = "A6" };
            var classroom3 = new ClassRoom() { id = 3, name = "23DTHA7", description = "A7" };

            List<Subject> sub = new List<Subject>();

            for (int i = 0; i < 4; i++)
            {
                sub.Add(new Subject());
            }

            sub[0].AddSubjects("Toán");
            sub[1].AddSubjects("Lý");
            sub[2].AddSubjects("Hóa");
            sub[3].AddSubjects("Anh");
            
            var SinhVien1 = new Student(new List<Subject>() { sub[0] }) { id = 2380602440, name = "Nguyen Cong Quang", age = 20, room = classroom1};
            var SinhVien2 = new Student(new List<Subject>() { sub[3] }) { id = 2380602441, name = "Nguyen Cong Tuan", age = 20, room = classroom1 };
            var GiaoVien1 = new Teacher() { name = "Nguyen Van Nam" , age = 40};
            classroom1.AddStudent(SinhVien1);
            classroom1.AddStudent(SinhVien2);
            classroom1.AddTeacher(GiaoVien1);

            var SinhVien3 = new Student(new List<Subject>() { sub[1] , sub[2] }) { id = 2380541234, name = "Huỳnh Ngọc Anh Tuấn", age = 20, room = classroom2 };
            var GiaoVien2 = new Teacher() { name = "Nguyễn Dzac Du Trinh", age = 45 };
            classroom2.AddStudent(SinhVien2);
            classroom2.AddTeacher(GiaoVien2);

            var SinhVien4 = new Student(new List<Subject>() { sub[2] , sub[3] }) { id = 2381234568, name = "Lê Huỳnh Ngọc", age = 20, room = classroom3 };
            var GiaoVien3 = new Teacher() { name = "Kim Phụng", age = 48 };
            classroom3.AddStudent(SinhVien3);
            classroom3.AddTeacher(GiaoVien3);

            string[] Classes = new string[]
            {
                "23DTHA5",
                "23DTHA6",
                "23DTHA7",
                "Thoat"
            };
            handles.ShowMenu(Classes, classroom1, classroom2, classroom3);
        }
    }
    public class handles
    {
        int postition = 0;
        bool checkKey;
        public void ClearConsole()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
        public int MoveMenu(string[] array, ClassRoom classRoom1, ClassRoom classRoom2, ClassRoom classRoom3)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (postition == 0)
                {
                    postition = array.Length;
                }
                postition--;
            }
            if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                postition++;

                if (postition == array.Length)
                {
                    postition = 0;
                }
            }
            if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Spacebar)
            {
                if (postition == 0)
                {
                    checkKey = false;
                    ClearConsole();
                    DisplayClassDetails(classRoom1);
                    ShowMenu(array, classRoom1, classRoom2, classRoom3);
                }
                else if (postition == 1)
                {
                    checkKey = false;
                    ClearConsole();
                    DisplayClassDetails(classRoom2);
                    ShowMenu(array, classRoom1, classRoom2, classRoom3);
                }
                else if (postition == 2)
                {
                    checkKey = false;
                    ClearConsole();
                    DisplayClassDetails(classRoom3);
                    ShowMenu(array, classRoom1, classRoom2, classRoom3);
                }
                else if (postition == 3)
                {
                    checkKey = false;
                    ClearConsole();
                }
            }
            return postition;
        }
        public int ShowMenu(string[] array, ClassRoom classRoom1, ClassRoom classRoom2, ClassRoom classRoom3)
        {
            checkKey = true;
            while (checkKey)
            {
                ClearConsole();
                for (int i = 0; i < array.Length; i++)
                {
                    Functions.gI().printf_Positions(array[i], 0, i);
                    if (i == postition)
                    {
                        Functions.gI().printf_Positions_Color(" " + array[i], 0, i, ConsoleColor.Green);
                        Functions.gI().printf_Positions_Color("<", array[i].Length + 1, i, ConsoleColor.Red);
                        Functions.gI().printf_Positions_Color(">", 0, i, ConsoleColor.Red);
                    }
                }
                MoveMenu(array, classRoom1, classRoom2, classRoom3);
            }
            return postition;
        }
        public void DisplayClassDetails(ClassRoom classRoom)
        {
            ClearConsole();
            Functions.gI().printf($"Thông tin lớp học: {classRoom.name}");
            Functions.gI().printf($"Mô tả: {classRoom.description}");
            Functions.gI().printf("Sinh viên:");
            foreach (var student in classRoom.students)
            {
                Functions.gI().printf($"- {student.name}, Tuổi: {student.age}, ID: {student.id}");
                Functions.gI().printf("  Môn học:");
                //
                foreach (var st in student.subject)             
                {
                        Functions.gI().printf($"  - {st.subjectName}");
                }

            }
            Functions.gI().printf("Giáo viên:");
            foreach (var teacher in classRoom.teachers)
            {
                Functions.gI().printf($"- {teacher.name}, Tuổi: {teacher.age}");
            }
            Functions.gI().printf("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
    }
    public class Functions
    {
        // tài liệu đây https://www.youtube.com/watch?v=r6Y0SmbufmU
        //        Singleton Pattern 
        // kỹ thuật này trương trình nào lớn đều có, đi làm chắc chắn có, mà ko bt trường mình dạy ko, nên th t chỉ luôn V:
        private Functions() { }
        private static Functions gi;
        private readonly static object key = new object();
        public static Functions gI()
        {
            if (gi == null)
            {
                lock (key)
                {
                    if(gi == null)
                    {
                        gi = new Functions();
                    }
                }
            }
            return gi;
        }

        //  
        //public void show(string message)
        //{
        //    Console.WriteLine(message);
        //}
        public void printf_Positions(string s , int x ,int y)
        {
            Console.SetCursorPosition(x,y);
            Console.WriteLine(s);
        }
        public void printf_Positions_Color(string s , int x ,int y , ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x,y);
            Console.WriteLine(s);
            Console.ResetColor();
        }

        public void printf(string s)
        {
            Console.WriteLine(s);
        }
    }
    public class people
    {
        public string name;
        public int age;
    }

    // Sinh viên bảo gồm id , lớp nào , và môn học của tụi nó
    public class Student : people
    {
        public long  id { get; set; }
        public ClassRoom room { get; set; }
        public List<Subject> subject { get; set; } // Danh sách môn học đó của tk sinh viên ( đối tượng là subject)

        public Student(List<Subject> subject)
        {
            this.subject = subject;
        }
    }
    public class Teacher : people
    {
        public List<Student> students { get; set; }
    }
    // Giáo Viên Dự Dờ
    public class guardian : people
    {

    }
    // Danh sách lưu các môn học của từng lớp
    public class Subject
    {
        //public string subjectName { get; set; }
        public string subjectName { get; set; }
        public void AddSubjects(string subjectName)
        {
            this.subjectName = subjectName;
        }
    }
    // Lớp học bao gồm sinh viên nào, id phòng , tên phòng , mô tả phòng
    public class ClassRoom : Room
    {
        // vị trí của cái phòng học trong trường
        //public void positions()
        //{
        //}
        // 
        public List<Student> students = new List<Student>();// Danh sach sinh viên ( hay la so luong sinh vien) trong 1 lop hoc

        public void AddStudent(Student student) // Lấy sinh viên đã có đưa vào cái lớp học 
        {
            students.Add(student);
        }
        public List<Teacher> teachers  = new List<Teacher>();// Ong thay chu nhiem cua lop do
        public void AddTeacher(Teacher teacher) // tương tự
        {
           teachers.Add(teacher);
        }
    }
    public class Room
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    public class Helper
    {
        public static void Delay(int time)
        {
            Thread.Sleep(TimeSpan.FromSeconds(time));
        }
    }
}
