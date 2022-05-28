/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dal;

import hr.algebra.model.Course;
import hr.algebra.model.CourseProfessor;
import hr.algebra.model.CourseStudent;
import hr.algebra.model.Professor;
import hr.algebra.model.Student;
import java.util.List;

/**
 *
 * @author Marta
 */
public interface Repo {

    // student
    int addStudent(Student data) throws Exception;

    void deleteStudent(Student student) throws Exception;

    List<Student> getStudents() throws Exception;

    Student getStudent(int idStudent) throws Exception;

    void updateStudent(Student student) throws Exception;

    default void release() throws Exception {
    }

    ;
    
    // professor
    int addProfessor(Professor data) throws Exception;

    void deleteProfessor(Professor professor) throws Exception;

    List<Professor> getProfessors() throws Exception;

    Professor getProfessor(int idProfessor) throws Exception;

    void updateProfessor(Professor professor) throws Exception;

    // course
    int addCourse(Course data) throws Exception;

    void deleteCourse(Course course) throws Exception;

    List<Course> getCourses() throws Exception;

    Course getCourse(int idCourse) throws Exception;

    void updateCourse(Course course) throws Exception;

    // course student
    int addCourseStudent(CourseStudent data) throws Exception;

    void deleteCourseStudent(CourseStudent courseStudent) throws Exception;

    CourseStudent getCourseStudent(int idCourseStudent) throws Exception;

    List<CourseStudent> getStudentCourses(Student student) throws Exception;

    List<CourseStudent> getCourseStudents(Course course) throws Exception;

    // course professor
    int addCourseProfessor(CourseProfessor data) throws Exception;

    void deleteCourseProfessor(CourseProfessor courseProfessor) throws Exception;

    CourseProfessor getCourseProfessor(int idCourseProfessor) throws Exception;

    List<CourseProfessor> getProfessorCourses(Professor professor) throws Exception;

    List<CourseProfessor> getCourseProfessors(Course course) throws Exception;
}
