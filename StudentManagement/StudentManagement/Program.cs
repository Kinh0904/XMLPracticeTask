using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StudentManagement
{
    
    class Program
    {
        struct Student
        {
            public string Id;
            public string Name;
            public int Age;
            public string Email;
            public string Major;
            public Student(string id, string name, int age, string email, string major)
            {
                Id = id;
                Name = name;
                Age = age;
                Email = email;
                Major = major;
            }
        }
        static void Main(string[] args)
        {

            //Set console output encoding to UTF-8
            Console.OutputEncoding = Encoding.UTF8;

            //New XML doc reader
            XmlDocument doc = new XmlDocument();

            //Document path
            doc.Load("D:\\UniVS\\126\\XML\\KinhTestPJ\\StudentMPJ\\student.xml");
            //Select all student nodes
            XmlNodeList studentList = doc.SelectNodes("//student");

            //Iterate through each student node and print their information

            Console.WriteLine("1.In ra danh sách sinh viên:");

            Student student02 = new Student();

            ArrayList studentAO20S = new ArrayList();
            foreach (XmlNode student in studentList)
            {
                string id = student.Attributes["id"]?.Value;

                string name = student["name"]?.InnerText;
                string age = student["age"]?.InnerText;
                string email = student["email"]?.InnerText;
                string major = student["major"]?.InnerText;

                if (id.Equals("SV02"))
                {
                    student02.Id = id;
                    student02.Name = name;
                    student02.Age = int.Parse(age);
                    student02.Email = email;
                    student02.Major = major;
                }

                if (int.Parse(age) > 20)
                {
                    studentAO20S.Add(new Student(id, name, int.Parse(age), email, major));
                }
                Console.WriteLine($"Họ và tên   : {name}");
                Console.WriteLine($"Tuổi    : {age}");
                Console.WriteLine($"Email  : {email}");
                Console.WriteLine($"Ngành  : {major}");
                Console.WriteLine(new string('-', 30));
            }
            Console.WriteLine("2.In ra tên tất cả sinh viên:");
            foreach (XmlNode student in studentList)
            {
                string name = student["name"]?.InnerText;
                Console.WriteLine($"Họ và tên   : {name}");
            }
            Console.WriteLine(new string('-', 30));

            Console.WriteLine("3.Tìm kiếm và in ra thông tin sinh viên có id là SV02:");
            Console.WriteLine("Thông tin sinh viên id SV02:");
            Console.WriteLine($"Họ và tên   : {student02.Name}");
            Console.WriteLine($"Tuổi    : {student02.Age}");
            Console.WriteLine($"Email  : {student02.Email}");
            Console.WriteLine($"Ngành  : {student02.Major}");
            Console.WriteLine(new string('-', 30));

            Console.WriteLine("4.Tổng số sinh viên:");
            Console.WriteLine($"Tổng số sinh viên: {studentList.Count}");

            Console.WriteLine("5.In ra danh sách sinh viên có tuổi lớn hơn 20:");
            foreach (Student student in studentAO20S)
            {
                Console.WriteLine($"Họ và tên   : {student.Name}");
                Console.WriteLine($"Tuổi    : {student.Age}");
                Console.WriteLine($"Email  : {student.Email}");
                Console.WriteLine($"Ngành  : {student.Major}");
                Console.WriteLine(new string('-', 30));
            }

            Console.WriteLine("Nhấn enter để tắt console ");
            //Wait for user input
            Console.ReadLine();
        }
    }
}
