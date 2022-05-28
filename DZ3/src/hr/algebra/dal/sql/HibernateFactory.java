/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dal.sql;

import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

/**
 *
 * @author Marta
 */
public class HibernateFactory {

    private static final String PERSISTENCE_UNIT = "PPPK_DZ3PU";
    public static final String SELECT_ALL_COURSES = "Course.findAll";
    public static final String SELECT_ALL_STUDENTS = "Student.findAll";
    public static final String SELECT_ALL_PROFESSORS = "Professor.findAll";
    public static final String SELECT_STUDENT_COURSES = "CourseStudent.findByIDStudent";
    public static final String SELECT_PROFESSOR_COURSES = "CourseProfessor.findByIDProfessor";
    public static final String SELECT_COURSE_STUDENTS = "CourseStudent.findByIDCourse";
    public static final String SELECT_COURSE_PROFESSORS = "CourseProfessor.findByIDCourse";

    private static final EntityManagerFactory EMF
            = Persistence.createEntityManagerFactory(PERSISTENCE_UNIT);

    public static EntityManagerWrapper getEntityManager() {
        return new EntityManagerWrapper(EMF.createEntityManager());
    }

    public static void release() {
        EMF.close();
    }
}
