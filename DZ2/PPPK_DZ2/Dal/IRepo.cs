using PPPK_DZ2.Models;
using System.Collections.Generic;

namespace PPPK_DZ2.Dal
{
    public interface IRepo
    {
        void AddStudent(Student student);
        void AddProfessor(Professor professor);
        void AddCourse(Course course);
        void AddCourseStudent(int idCourse, int idStudent);
        void AddCourseProfessor(int idCourse, int idProfessor);

        void DeleteStudent(int idStudent);
        void DeleteProfessor(int idProfessor);
        void DeleteCourse(int idCourse);
        void DeleteCourseStudent(int idCourse, int idStudent);
        void DeleteCourseProfessor(int idCourse, int idProfessor);

        IList<Person> GetStudents();
        IList<Person> GetProfessors();
        IList<Course> GetCourses();

        Person GetStudent(int idStudent);
        Person GetProfessor(int idProfessor);
        Course GetCourse(int idCourse);

        IList<Course> GetStudentCourses(int idStudent);
        IList<Course> GetProfessorCourses(int idProfessor);
        IList<Person> GetCourseStudents(int idCourse);
        IList<Person> GetCourseProfessors(int idCourse);

        void UpdateStudent(Student student);
        void UpdateProfessor(Professor professor);
        void UpdateCourse(Course course);
    }
}
