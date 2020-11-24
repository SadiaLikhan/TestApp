using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Main_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("To Log In, Choose an option\nFor Admin Log In Press: A\nFor Employee Log In Press: E\nTo Exit Press: X\n ");
            string select = Convert.ToString(Console.ReadLine());
            Admin accessAdmin = new Admin();
            DataManipulation value = new DataManipulation();
            Employee accessEmployee = new Employee();
            if (select == "A" || select == "E")
            {
                Choice(select);
            }
            else if (select == "X" )
            {
                Choice(select);
            }
            else
            { 
                do
                {
                    Console.WriteLine("Wrong Choice.");
                    Console.WriteLine("To Log In, Choose an option\nFor Admin Log In Press: A\nFor Employee Log In Press: E\nTo Exit Press: X\n ");
                    select = Convert.ToString(Console.ReadLine());
                } while (select != "A" && select != "E" && select != "X");
             
                Choice(select);
            }

        }

        static void Choice(string choose)
        {
            DataManipulation value = new DataManipulation();
            if (choose == "A")
            {
                value.Readcsv();
                // accessAdmin.AdminLogIn();
            }
            else if (choose == "E")
            {
                value.Readcsv();
                //accessEmployee.EmployeeLogIn();
            }
            else if (choose == "X")
            {
                Console.WriteLine("You have exited. Press Enter to close.");
            }
        }
    }

    class Admin
    {

        public void AdminLogIn()
        {
            Console.WriteLine("To create new employee press: C\nTo edit employee press: R\nTo delete employee press: D\nTo Log Out press: X ");
            string select = Convert.ToString(Console.ReadLine());
            if (select == "C")
            {

                CreateUser();
                Console.WriteLine("New user created successfully.");
            }
            else if (select == "R")
            {
                DataManipulation edit = new DataManipulation();
                edit.Edit();
                Console.WriteLine("Employee details changed successful.");
            }
            else if (select == "D")
            {
                RemoveEmployee();
            }
            else if (select == "X")
            {
                Console.WriteLine("You have Loged Out successful. Press Enter to close the program");
            }
            else
            {
                Console.WriteLine("Wrong Choice.");
            }
        }

        static List<string> Details()
        {
            Console.WriteLine("Enter username: ");
            string name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter employee password (Password must contain at least 8 digit): ");
            string passcheck = Convert.ToString(Console.ReadLine());
            string pass;
            if (passcheck.Length>=8)
            {
                Console.WriteLine("Password is ok.\n");
                pass = passcheck;
            }
            else 
            {
                do
                {
                    int str = passcheck.Length;
                    Console.WriteLine("Password is not ok. Entered password contains {0} digit.\n", str);
                    Console.WriteLine("Enter a password that contains at least 8 digit): ");
                    passcheck = Convert.ToString(Console.ReadLine());
                } while (passcheck.Length < 8);

                pass = passcheck;
            }
            Console.WriteLine("Enter address: ");
            string address = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter status: ");
            string status = Convert.ToString(Console.ReadLine());
            List<string> details = new List<string>();
            details.Add(name);
            details.Add(pass);
            details.Add(address);
            details.Add(status);
            return details;
        }

        static Dictionary<string, List<string>> Collection(string id, List<string>details)
        {
            var collection = new Dictionary<string, List<string>>();
            collection.Add(id, details);
            return collection;
        }

        public void CreateUser()
        {
            string csvalter = "TestFile.csv";
            if (!File.Exists(csvalter) == true)
            {
                CreateCsvNew();
                DataManipulate(csvalter);
            }
            else 
            {
                DataManipulate(csvalter);
            }

        }

        static void CreateCsvNew()
        {
            string csvpath = "TestFile.csv";
            using (StreamWriter sr = File.AppendText(csvpath))
            {
                sr.Write("ID");
                sr.Write(",");
                sr.Write("Name");
                sr.Write(",");
                sr.Write("Password");
                sr.Write(",");
                sr.Write("Address");
                sr.Write(",");
                sr.Write("Status\n");
            }

        }
        static void RemoveEmployee()
        {
            DataManipulation delete = new DataManipulation();
            var collect = delete.ForUse();
            Console.WriteLine(string.Join(", ", collect.Keys));
            Console.WriteLine(string.Join(", ", collect["2"]));
            Console.WriteLine("Enter employee id: ");
            string empID = Convert.ToString(Console.ReadLine());
            var edit = delete.idList();
            if (collect.ContainsKey(empID))
            {
                collect.Remove(empID);
                Console.WriteLine(string.Join(", ", collect.Keys));
                Console.WriteLine("Employee details of Employee ID {0} successfully deleted.", empID);
            }
            else if (!edit.Contains(empID))
            {
                Console.WriteLine("Employee details of Employee ID {0} does not exist.", empID);
            }
        }
        static void DataManipulate(string csvpath)
        {
            
            string id = CheckIdExists();
            var collection = new Dictionary<string, List<string>>();
            List<string> details = new List<string>();
            details = Details();
            collection = Collection(id, details);
            foreach (String key in collection.Keys)
            {
                using (StreamWriter sr = File.AppendText(csvpath))
                {
                    sr.Write(key);
                    sr.Write(",");
                    for (int i = 0; i < details.Count; i++)
                    {

                        sr.Write(details[i]);
                        sr.Write(",");

                    }
                    sr.Write("\n");
                }

            }
        }
        static string CheckIdExists()
        {
            Console.WriteLine("Enter employee id: ");
            string idcheck = Convert.ToString(Console.ReadLine());
            string idfinal;
            DataManipulation check = new DataManipulation();
            var idlist = check.idList();
            int count = idlist.Count;
            var list= check.ForUse();
            if(!list.ContainsKey(idcheck))
            {
                idfinal= idcheck;
            }
            else
            {
                do
                {
                    Console.WriteLine("Employee id already exist\nPlease enter a new Employee id:(Suggested Employee ID: {0})", count);
                    idcheck = Convert.ToString(Console.ReadLine());
                } while (list.ContainsKey(idcheck));
                
                idfinal = idcheck;
            }
            return idfinal;
        }



    }

    class Employee
    {
        public void EmployeeLogIn()
        {
            Console.WriteLine("To profile press: R\nTo Log Out press: X ");
            string select = Convert.ToString(Console.ReadLine());
            
            if (select == "R")
            {
               
                Console.WriteLine("Employee Log In successful.");
            }
            
            else if (select == "X")
            {
                Console.WriteLine("You have Loged Out successful. Press Enter to close the program");
            }
            else
            {
                Console.WriteLine("Wrong Choice.");
            }
        }

    }

    class DataManipulation
    {
        public List<string> listOfId;
        public List<string> listOfPass;
        public Dictionary<string, List<string>> collectionDetails;
        
        public void Collection()
        {
            string csvpath = "TestFile.csv";
            var collection = new Dictionary<string, List<string>>();
            var details = new List<string>();
            var idList = new List<string>();
            var passList = new List<string>();
            string id;
            string pass;
            var subdetails = new List<string>();
            using (var reader = new StreamReader(csvpath))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    details = line.Split(',').ToList();
                    id = details[0];
                    pass =details[2];
                    subdetails = details.Skip(1).ToList();
                    collection.Add(details[0], subdetails);
                    idList.Add(id);
                    passList.Add(pass);
                }
            }
            listOfPass =passList;
            listOfId = idList;
            collectionDetails= collection;
        }

        public List<string> idList ()
        {
            Collection();
            var list= listOfId;
            return list;
        }

        public Dictionary<string, List<string>> ForUse()
        {
            string csvpath = "TestFile.csv";
            var collection = new Dictionary<string, List<string>>();
            var details = new List<string>();
            var idList = new List<string>();
            var passList = new List<string>();
            string id;
            string pass;
            var subdetails = new List<string>();
            using (var reader = new StreamReader(csvpath))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    details = line.Split(',').ToList();
                    id = details[0];
                    pass = details[2];
                    subdetails = details.Skip(1).ToList();
                    collection.Add(details[0], subdetails);
                    idList.Add(id);
                    passList.Add(pass);
                }
            }
            return collection;
        }
        
        public void Readcsv()
        {
            Collection();
            Console.WriteLine("Enter employee id: ");
            string empID = Convert.ToString(Console.ReadLine());
            var dic = new Dictionary<string, string>();
            var first = new List<string>();
            first = collectionDetails["ID"];
            var second = new List<string>();
            Admin enter = new Admin();
            Employee enterEmp = new Employee();
            if (listOfId.Contains(empID))

            {
                second = collectionDetails[empID];

                for (int i = 0; i < first.Count; i++)
                {
                    for (int j = 0; j < second.Count; j++)
                    {
                        if (i == j)
                        {
                            dic.Add(first[i], second[j]);

                        }
                    }
                }
                string status =dic["Status"];
                Console.WriteLine("Enter Password: ");
                string pass = Convert.ToString(Console.ReadLine());
                if(pass== dic["Password"] && status=="Admin")
                {
                    Console.WriteLine("Admin Log In successful.");
                    enter.AdminLogIn();
                }
                else if (pass != dic["Password"] && status == "Admin")
                {
                    do
                    {
                        Console.WriteLine("Wrong Password, Enter correct Password ");
                        pass= Convert.ToString(Console.ReadLine());
                    } while (pass != dic["Password"]);
                    Console.WriteLine("Admin Log In successful.");
                    enter.AdminLogIn();
                }
                else if (pass == dic["Password"] && status == "Employee")
                {
                    Console.WriteLine("Employee Log In successful.");
                    enterEmp.EmployeeLogIn();
                }
                else if (pass != dic["Password"] && status == "Employee")
                {
                    do
                    {
                        Console.WriteLine("Wrong Password, Enter correct Password ");
                        pass = Convert.ToString(Console.ReadLine());
                    } while (pass != dic["Password"]);
                    Console.WriteLine("Employee Log In successful.");
                    enterEmp.EmployeeLogIn(); 
                }
                
                else
                {
                    Console.WriteLine("Log In failed due to unauthentic Log In attempt.");
                }
                    
                
            }
            else if (!listOfId.Contains(empID))
            {
                Console.WriteLine("Employee ID {0} Does not exists in the Employee List.", empID);
            }
            
        }
        public void Edit()
        {
            Collection();
            Console.WriteLine("Enter employee id to be edited: ");
            string empID = Convert.ToString(Console.ReadLine());
            string choice;
            var dic = new Dictionary<string, string>();
            var first = new List<string>();
            first = collectionDetails["ID"];
            var second = new List<string>();
            Admin enter = new Admin();
            Employee enterEmp = new Employee();
            if (listOfId.Contains(empID))

            {
                second = collectionDetails[empID];

                for (int i = 0; i < first.Count; i++)
                {
                    for (int j = 0; j < second.Count; j++)
                    {
                        if (i == j)
                        {
                            dic.Add(first[i], second[j]);

                        }
                    }
                }
            }
            else
            {
                do
                {
                    Console.WriteLine("Wrong Employee id. Pleasee Enter correct employee id to be edited: ");
                    empID = Convert.ToString(Console.ReadLine());
                } while (!listOfId.Contains(empID));
                
                second = collectionDetails[empID];

                for (int i = 0; i < first.Count; i++)
                {
                    for (int j = 0; j < second.Count; j++)
                    {
                        if (i == j)
                        {
                            dic.Add(first[i], second[j]);

                        }
                    }
                }
            }

            Console.WriteLine("Do you want to change your Name:\nIf YES Press Y\nIf No Press N");
            choice = Convert.ToString(Console.ReadLine());
            if (choice=="Y")
            {
                Console.WriteLine("Enter new Name: ");
                string name = Convert.ToString(Console.ReadLine());
                dic["Name"] = name;
                Console.WriteLine("Name changed to: {0}", dic["Name"]);
            }
            else
            {
                Console.WriteLine("Your Name:{0}", dic["Name"]);
            }
            
            Console.WriteLine("Do you want to change your Password:\nIf YES Press Y\nIf No Press Y");
            choice = Convert.ToString(Console.ReadLine());
            if (choice == "Y")
            {
                Console.WriteLine("Enter new Password: ");
                string pass = Convert.ToString(Console.ReadLine());
                dic["Password"] = pass;
                Console.WriteLine("Password changed to: {0}", dic["Password"]);
            }
            else
            {
                Console.WriteLine("Your Password:{0}", dic["Password"]);
            }
            
            Console.WriteLine("Do you want to change your Address:\nIf YES Press Y\nIf No Press N");
            choice = Convert.ToString(Console.ReadLine());
            if (choice == "Y")
            {
                Console.WriteLine("Enter new Address: ");
                string add = Convert.ToString(Console.ReadLine());
                dic["Address"] = add;
                Console.WriteLine("Address changed to: {0}", dic["Address"]);
            }
            else
            {
                Console.WriteLine("Your Address:{0}", dic["Address"]);
            }

            if (dic["Status"] == "Employee")
            {
                Console.WriteLine("Do you want to change your Status:\nIf YES Press Y\nIf No Press N");
                choice = Convert.ToString(Console.ReadLine());
                if (choice == "Y")
                {
                    Console.WriteLine("Enter new Status (Write Admin): ");
                    string status = Convert.ToString(Console.ReadLine());
                    dic["Status"] = status;
                    Console.WriteLine("Status changed to: {0}", dic["Status"]);
                }
                else
                {
                    Console.WriteLine("Your Status:{0}", dic["Status"]);
                }
            }
            
            else if (dic["Status"] == "Admin")
            {
                Console.WriteLine("Your Status:{0}", dic["Status"]);
            }
            Replace(dic, empID);
        }

        public void Replace(Dictionary<string, string> change, string empID)
        {
            Collection();
            string csvpath = "TestFile.csv";
            var point = new List<string>();
            point.Add(change["Name"]);
            point.Add(change["Password"]);
            point.Add(change["Address"]);
            point.Add(change["Status"]);
            List<string> details1= new List<string>();
            List<string> subdetails1 = new List<string>();
            using (var reader = new StreamReader(csvpath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == empID + 1)
                    {
                        details1= line.Split(',').ToList();
                        subdetails1 = details1.Skip(1).ToList();
                        using (StreamWriter sw = new StreamWriter(csvpath))
                        {
                            //subdetails1 = change[empID];
                            sw.Write(change[empID]);
                            sw.Write(",");
                            for (int i = 0; i < point.Count; i++)
                            {
                                subdetails1[i] = point[i];
                                sw.Write(subdetails1[i]);
                                sw.Write(",");

                            }
                            sw.Write("\n");
                        }
                    }
                }
            }
            
        }
    }
}
