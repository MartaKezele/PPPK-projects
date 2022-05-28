using PPPK_DZ2.Models;
using PPPK_DZ2.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PPPK_DZ2.Dal
{
    class SqlRepo : IRepo
    {
        // params
        private const string FirstNameParam = "@firstname";
        private const string LastNameParam = "@lastname";
        private const string CourseNameParam = "@coursename";
        private const string EctsParam = "@ects";
        private const string EmailParam = "@email";
        private const string PictureParam = "@picture";
        private const string IdStudentParam = "@idStudent";
        private const string IdProfessorParam = "@idProfessor";
        private const string IdCourseParam = "@idCourse";

        // constants
        private const string ID_STUDENT = "IDStudent";
        private const string ID_PROFESSOR = "IDProfessor";
        private const int STUDENT_PICTURE_COLUMN_NUMBER = 5;
        private const int PROFESSOR_PICTURE_COLUMN_NUMBER = 4;

        // cannot be const
        //private static readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString, "Pa$$w0rd");


        // add methods
        public void AddCourse(Course course)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(CourseNameParam, course.CourseName);
                    cmd.Parameters.AddWithValue(EctsParam, course.Ects);
                    SqlParameter idCourse = new SqlParameter(IdCourseParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idCourse);
                    cmd.ExecuteNonQuery();
                    course.IDCourse = (int)idCourse.Value;
                }
            }
        }

        public void AddProfessor(Professor professor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter idProfessor = AddPersonParametersToCmd(cmd, professor, IdProfessorParam);
                    cmd.ExecuteNonQuery();
                    professor.IDPerson = (int)idProfessor.Value;
                }
            }
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter idStudent = AddPersonParametersToCmd(cmd, student, IdStudentParam);
                    cmd.Parameters.AddWithValue(EctsParam, student.Ects);
                    cmd.ExecuteNonQuery();
                    student.IDPerson = (int)idStudent.Value;
                }
            }
        }

        public void AddCourseStudent(int idCourse, int idStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdStudentParam, idStudent);
                    cmd.Parameters.AddWithValue(IdCourseParam, idCourse);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddCourseProfessor(int idCourse, int idProfessor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdProfessorParam, idProfessor);
                    cmd.Parameters.AddWithValue(IdCourseParam, idCourse);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // delete methods
        public void DeleteCourse(int idCourse)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdCourseParam, idCourse);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProfessor(int idProfessor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdProfessorParam, idProfessor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int idStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdStudentParam, idStudent);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCourseStudent(int idCourse, int idStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdStudentParam, idStudent);
                    cmd.Parameters.AddWithValue(IdCourseParam, idCourse);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCourseProfessor(int idCourse, int idProfessor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdProfessorParam, idProfessor);
                    cmd.Parameters.AddWithValue(IdCourseParam, idCourse);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // get one methods
        public Course GetCourse(int idCourse)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdCourseParam, idCourse);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadCourse(dr);
                        }
                    }
                }
            }
            throw new Exception("Course does not exist");
        }

        public Person GetProfessor(int idProfessor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdProfessorParam, idProfessor);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadProfessor(dr);
                        }
                    }
                }
            }
            throw new Exception("Professor does not exist");
        }

        public Person GetStudent(int idStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdStudentParam, idStudent);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadStudent(dr);
                        }
                    }
                }
            }
            throw new Exception("Student does not exist");
        }


        // get all methods
        public IList<Course> GetCourses()
        {
            IList<Course> courses = new List<Course>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            courses.Add(ReadCourse(dr));
                        }
                    }
                }
            }
            return courses;
        }

        public IList<Person> GetProfessors()
        {
            IList<Person> professors = new List<Person>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            professors.Add(ReadProfessor(dr));
                        }
                    }
                }
            }
            return professors;
        }

        public IList<Person> GetStudents()
        {
            IList<Person> students = new List<Person>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            students.Add(ReadStudent(dr));
                        }
                    }
                }
            }
            return students;
        }

        // update methods
        public void UpdateCourse(Course course)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(CourseNameParam, course.CourseName);
                    cmd.Parameters.AddWithValue(EctsParam, course.Ects);
                    cmd.Parameters.AddWithValue(IdCourseParam, course.IDCourse);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProfessor(Professor professor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, professor.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, professor.LastName);
                    cmd.Parameters.AddWithValue(EmailParam, professor.Email);
                    cmd.Parameters.AddWithValue(IdProfessorParam, professor.IDPerson);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, professor.Picture.Length)
                    {
                        Value = professor.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, student.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, student.LastName);
                    cmd.Parameters.AddWithValue(EctsParam, student.Ects);
                    cmd.Parameters.AddWithValue(EmailParam, student.Email);
                    cmd.Parameters.AddWithValue(IdStudentParam, student.IDPerson);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, student.Picture.Length)
                    {
                        Value = student.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // get some methods

        public IList<Course> GetStudentCourses(int idStudent)
        {
            IList<Course> courses = new List<Course>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdStudentParam, idStudent);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int idCourse = (int)dr[0];
                            courses.Add(GetCourse(idCourse));
                        }
                    }
                }
            }
            return courses;
        }

        public IList<Course> GetProfessorCourses(int idProfessor)
        {
            IList<Course> courses = new List<Course>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdProfessorParam, idProfessor);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int idCourse = (int)dr[0];
                            courses.Add(GetCourse(idCourse));
                        }
                    }
                }
            }
            return courses;
        }

        public IList<Person> GetCourseStudents(int idCourse)
        {
            IList<Person> students = new List<Person>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdCourseParam, idCourse);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int idStudent = (int)dr[0];
                            students.Add(GetStudent(idStudent));
                        }
                    }
                }
            }
            return students;
        }

        public IList<Person> GetCourseProfessors(int idCourse)
        {
            IList<Person> professors = new List<Person>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdCourseParam, idCourse);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int idProfessor = (int)dr[0];
                            professors.Add(GetStudent(idProfessor));
                        }
                    }
                }
            }
            return professors;
        }

        // private methods
        private SqlParameter AddPersonParametersToCmd(SqlCommand cmd, Person person, string idPersonParam)
        {
            cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
            cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
            cmd.Parameters.AddWithValue(EmailParam, person.Email);
            cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length)
            {
                Value = person.Picture
            });
            SqlParameter idPerson = new SqlParameter(idPersonParam, SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(idPerson);
            return idPerson;
        }

        private Course ReadCourse(SqlDataReader dr) => new Course
        {
            IDCourse = (int)dr[nameof(Course.IDCourse)],
            CourseName = dr[nameof(Course.CourseName)].ToString(),
            Ects = (int)dr[nameof(Course.Ects)]
        };

        private Professor ReadProfessor(SqlDataReader dr)
        {
            Professor professor = new Professor();
            ReadPersonData(professor, ID_PROFESSOR, PROFESSOR_PICTURE_COLUMN_NUMBER, dr);
            return professor;
        }

        private Student ReadStudent(SqlDataReader dr)
        {
            Student student = new Student();
            ReadPersonData(student, ID_STUDENT, STUDENT_PICTURE_COLUMN_NUMBER, dr);
            student.Ects = (int)dr[nameof(Student.Ects)];
            return student;
        }

        private void ReadPersonData(Person person, string IDPerson, int pictureColumnNumber, SqlDataReader dr)
        {
            person.IDPerson = (int)dr[IDPerson];
            person.FirstName = dr[nameof(Person.FirstName)].ToString();
            person.LastName = dr[nameof(Person.LastName)].ToString();
            person.Email = dr[nameof(Person.Email)].ToString();
            person.Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, pictureColumnNumber);
        }
    }
}
