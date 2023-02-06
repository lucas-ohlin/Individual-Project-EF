using Labb3EF_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Labb3EF_Final.Src {

    internal class DBReading {

        //Connection to the application database
        private static AppDBContext context = new AppDBContext();

        public static void CoursesTeachers() {

            //First chooses the first element of a sequence (if there are duplicates it only chooses the first instance of it)
            var courses = context.Courses.GroupBy(x => x.CourseName).Select(y => y.First()).ToList();

            //Layout : ID | Course Name 
            Console.WriteLine("Choose Course (with id):");
            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine($"{i} | Course name : {courses[i].CourseName}");

            bool run = true;
            do {
                Console.WriteLine($"\nSelect which Course to print teachers from : (0-{courses.Count - 1}) | Quit with ({courses.Count}+)");

                byte choice;
                if (!byte.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine($"\nNumber 0-{courses.Count}.");

                //Incase the choice isn't contained in any element, set run to false and break the loop
                if (courses.ElementAtOrDefault(choice) == null) {
                    Console.WriteLine("Not a valid class index, quitting back to main menu.");
                    run = false;
                }
                //Incase the choice is contained we print out each teacher in that course
                if (courses.ElementAtOrDefault(choice) != null) {

                    //Order by First Name where the staff has the same course as the one choosen previously
                    var staffs = context.VwStaffInformations.OrderBy(x => x.Fname).Where(x => x.CourseName == courses[choice].CourseName).ToList();

                    //Print out amount of teachers in the course
                    Console.WriteLine($"There are {staffs.Count} teachers in {courses[choice].CourseName}.");

                    //Print out the actual teachers in the course
                    foreach (VwStaffInformation staff in staffs)
                        Console.WriteLine($"ID : {staff.Id} | Course: {staff.CourseName} | Name : {staff.Fname + " " + staff.Lname}");

                }

            } while (run);

        }

        public static void ActiveCourses() {

            //First chooses the first element of a sequence (if there are duplicates it only chooses the first instance of it)
            var courses = context.Courses.GroupBy(x => x.CourseName).Select(y => y.First()).ToList();

            Console.WriteLine("Current Courses:");
            for (int i = 0; i < courses.Count; i++)
                Console.WriteLine($"{i} | Course name : {courses[i].CourseName}");

        }

        public static void StudentClass() {

            //First chooses the first element of a sequence (if there are duplicates it only chooses the first instance of it)
            var classes = context.VwStudentInformations.GroupBy(x => x.Class).Select(j => j.First()).ToList();

            Console.WriteLine("Current Classes:");
            for (int i = 0; i < classes.Count; i++) 
                Console.WriteLine($"{i} | Class name : {classes[i].Class}"); 
            
            bool run = true;
            do {
                Console.WriteLine($"\nSelect which class to print : (0-{classes.Count - 1}) | Quit with ({classes.Count + 1}+)");

                byte choice;
                if (!byte.TryParse(Console.ReadLine(), out choice))
                    Console.WriteLine($"\nNumber 0-{classes.Count}.");

                if (classes.ElementAtOrDefault(choice) == null) {
                    Console.WriteLine("Not a valid class index, quitting back to main menu.");
                    run = false;
                }  
                if (classes.ElementAtOrDefault(choice) != null) {
                    //Makes a lists of all students in the choosen class
                    var students = context.VwStudentInformations.OrderBy(x => x.Fname).Where(x => x.Class == classes[choice].Class).ToList();

                    foreach (VwStudentInformation student in students)
                        Console.WriteLine($"ID : {student.Id} | Class: {student.Class} | Name : {student.Fname + " " + student.Lname} | SSN : {student.PersonNumber}");
                }

            } while(run);

        }

        public static void AllStudents() {

            Console.WriteLine(
                "\n1. Sort by first name | decending\r\n" +
                "2. Sort by first name | ascending\r\n" +
                "3. Sort by last name  | decending\r\n" +
                "4. Sort by last name  | ascending\r\n"
            );

            byte choice;
            if (!byte.TryParse(Console.ReadLine(), out choice))
                Console.WriteLine("\nNumber 1-4.");

            switch (choice) {
                default: //If not a valid choice
                    Console.WriteLine("Not a valid choice.");
                    break;
                case 1:
                    FirstNameDecending();
                    break;
                case 2:
                    FirstNameAscending();
                    break;
                case 3:
                    LastNameDecending();
                    break;
                case 4:
                    LastNameAscending();
                    break;
            }

        }

        private static void FirstNameDecending() {
            var students = context.VwStudentInformations.OrderByDescending(x => x.Fname).ToList();
            foreach (VwStudentInformation student in students)
                Console.WriteLine($"ID : {student.Id} | Class: {student.Class} | Name : {student.Fname + " " + student.Lname} | SSN : {student.PersonNumber}");
        }

        private static void FirstNameAscending() {
            var students = context.VwStudentInformations.OrderBy(x => x.Fname).ToList();
            foreach (VwStudentInformation student in students)
                Console.WriteLine($"ID : {student.Id} | Class: {student.Class} | Name : {student.Fname + " " + student.Lname} | SSN : {student.PersonNumber}");
        }

        private static void LastNameDecending() {
            var students = context.VwStudentInformations.OrderByDescending(x => x.Lname).ToList();
            foreach (VwStudentInformation student in students)
                Console.WriteLine($"ID : {student.Id} | Class: {student.Class} | Name : {student.Fname + " " + student.Lname} | SSN : {student.PersonNumber}");
        }

        private static void LastNameAscending() {
            var students = context.VwStudentInformations.OrderBy(x => x.Lname).ToList();
            foreach (VwStudentInformation student in students)
                Console.WriteLine($"ID : {student.Id} | Class: {student.Class} | Name : {student.Fname + " " + student.Lname} | SSN : {student.PersonNumber}");
        }

    }

}
