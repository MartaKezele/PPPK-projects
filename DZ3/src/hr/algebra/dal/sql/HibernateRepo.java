/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dal.sql;

import hr.algebra.dal.Repo;
import hr.algebra.model.Course;
import hr.algebra.model.CourseProfessor;
import hr.algebra.model.CourseStudent;
import hr.algebra.model.Professor;
import hr.algebra.model.Student;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.persistence.EntityManager;
import javax.persistence.Query;

/**
 *
 * @author Marta
 */
public class HibernateRepo implements Repo {

    // student
    @Override
    public int addStudent(Student data) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            Student student = new Student(data);
            em.persist(student);
            em.getTransaction().commit();
            return student.getIDStudent();
        }
    }

    @Override
    public void deleteStudent(Student student) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            List<CourseStudent> studentCourses = getStudentCourses(student);
            studentCourses.forEach(sc -> {
                try {
                    deleteCourseStudent(sc);
                } catch (Exception ex) {
                    Logger.getLogger(HibernateRepo.class.getName()).log(Level.SEVERE, null, ex);
                }
            });

            em.remove(em.contains(student) ? student : em.merge(student));
            em.getTransaction().commit();
        }
    }

    @Override
    public List<Student> getStudents() throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            return entityManager.get().createNamedQuery(HibernateFactory.SELECT_ALL_STUDENTS).getResultList();
        }
    }

    @Override
    public Student getStudent(int idStudent) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            return em.find(Student.class, idStudent);
        }
    }

    @Override
    public void updateStudent(Student student) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            em.find(Student.class, student.getIDStudent()).updateDetails(student);
            em.getTransaction().commit();
        }
    }

    // professor
    @Override
    public int addProfessor(Professor data) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            Professor professor = new Professor(data);
            em.persist(professor);
            em.getTransaction().commit();
            return professor.getIDProfessor();
        }
    }

    @Override
    public void deleteProfessor(Professor professor) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            List<CourseProfessor> professorCourses = getProfessorCourses(professor);
            professorCourses.forEach(cp -> {
                try {
                    deleteCourseProfessor(cp);
                } catch (Exception ex) {
                    Logger.getLogger(HibernateRepo.class.getName()).log(Level.SEVERE, null, ex);
                }
            });
            em.remove(em.contains(professor) ? professor : em.merge(professor));
            em.getTransaction().commit();
        }
    }

    @Override
    public List<Professor> getProfessors() throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            return entityManager.get().createNamedQuery(HibernateFactory.SELECT_ALL_PROFESSORS).getResultList();
        }
    }

    @Override
    public Professor getProfessor(int idProfessor) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            return em.find(Professor.class, idProfessor);
        }
    }

    @Override
    public void updateProfessor(Professor professor) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            em.find(Professor.class, professor.getIDProfessor()).updateDetails(professor);
            em.getTransaction().commit();
        }
    }

    // Course
    @Override
    public int addCourse(Course data) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            Course course = new Course(data);
            em.persist(course);
            em.getTransaction().commit();
            return course.getIDCourse();
        }
    }

    @Override
    public void deleteCourse(Course course) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            
            List<CourseStudent> courseStudents = getCourseStudents(course);
            List<CourseProfessor> courseProfessors = getCourseProfessors(course);
            
            courseStudents.forEach(cs -> {
                try {
                    deleteCourseStudent(cs);
                } catch (Exception ex) {
                    Logger.getLogger(HibernateRepo.class.getName()).log(Level.SEVERE, null, ex);
                }
            });
            
            courseProfessors.forEach(cp -> {
                try {
                    deleteCourseProfessor(cp);
                } catch (Exception ex) {
                    Logger.getLogger(HibernateRepo.class.getName()).log(Level.SEVERE, null, ex);
                }
            });
            
            em.remove(em.contains(course) ? course : em.merge(course));
            em.getTransaction().commit();
        }
    }

    @Override
    public List<Course> getCourses() throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            return entityManager.get().createNamedQuery(HibernateFactory.SELECT_ALL_COURSES).getResultList();
        }
    }

    @Override
    public Course getCourse(int idCourse) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            return em.find(Course.class, idCourse);
        }
    }

    @Override
    public void updateCourse(Course course) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            em.find(Course.class, course.getIDCourse()).updateDetails(course);
            em.getTransaction().commit();
        }
    }

    // courseProfessor
    @Override
    public int addCourseStudent(CourseStudent data) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            CourseStudent courseStudent = new CourseStudent(data);
            em.persist(courseStudent);
            em.getTransaction().commit();
            return courseStudent.getIDCourseStudent();
        }
    }

    @Override
    public void deleteCourseStudent(CourseStudent courseStudent) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            em.remove(em.contains(courseStudent) ? courseStudent : em.merge(courseStudent));
            em.getTransaction().commit();
        }
    }

    @Override
    public CourseStudent getCourseStudent(int idCourseStudent) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            return em.find(CourseStudent.class, idCourseStudent);
        }
    }

    @Override
    public List<CourseStudent> getStudentCourses(Student student) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            Query query = em.createNamedQuery(HibernateFactory.SELECT_STUDENT_COURSES);
            query.setParameter("student", student);
            return query.getResultList();
        }
    }

    // courseProfessor 
    @Override
    public int addCourseProfessor(CourseProfessor data) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            CourseProfessor courseProfessor = new CourseProfessor(data);
            em.persist(courseProfessor);
            em.getTransaction().commit();
            return courseProfessor.getIDCourseProfessor();
        }
    }

    @Override
    public void deleteCourseProfessor(CourseProfessor courseProfessor) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            em.getTransaction().begin();
            em.remove(em.contains(courseProfessor) ? courseProfessor : em.merge(courseProfessor));
            em.getTransaction().commit();
        }
    }

    @Override
    public CourseProfessor getCourseProfessor(int idCourseProfessor) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            return em.find(CourseProfessor.class, idCourseProfessor);
        }
    }

    @Override
    public List<CourseProfessor> getProfessorCourses(Professor professor) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            Query query = em.createNamedQuery(HibernateFactory.SELECT_PROFESSOR_COURSES);
            query.setParameter("professor", professor);
            return query.getResultList();
        }
    }

    // release
    @Override
    public void release() throws Exception {
        HibernateFactory.release();
    }

    @Override
    public List<CourseStudent> getCourseStudents(Course course) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            Query query = em.createNamedQuery(HibernateFactory.SELECT_COURSE_STUDENTS);
            query.setParameter("course", course);
            return query.getResultList();
        }
    }

    @Override
    public List<CourseProfessor> getCourseProfessors(Course course) throws Exception {
        try (EntityManagerWrapper entityManager = HibernateFactory.getEntityManager()) {
            EntityManager em = entityManager.get();
            Query query = em.createNamedQuery(HibernateFactory.SELECT_COURSE_PROFESSORS);
            query.setParameter("course", course);
            return query.getResultList();
        }
    }
}
