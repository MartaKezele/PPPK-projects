/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.viewmodel;

import hr.algebra.model.Course;
import hr.algebra.model.CourseStudent;
import hr.algebra.model.Student;
import javafx.beans.property.IntegerProperty;
import javafx.beans.property.ObjectProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.beans.property.SimpleObjectProperty;
import javafx.beans.property.SimpleStringProperty;
import javafx.beans.property.StringProperty;

/**
 *
 * @author Marta
 */
public class CourseStudentViewModel {

    private final CourseStudent courseStudent;

    public CourseStudentViewModel(CourseStudent courseStudent) {
        if (courseStudent == null) {
            courseStudent = new CourseStudent(courseStudent);
        }
        this.courseStudent = courseStudent;
    }

    public CourseStudent getCourseStudent() {
        return courseStudent;
    }

    public IntegerProperty getIdCourseStudentProperty() {
        return new SimpleIntegerProperty(courseStudent.getIDCourseStudent());
    }

    public ObjectProperty<Course> getCourseProperty() {
        return new SimpleObjectProperty<>(courseStudent.getCourse());
    }

    public ObjectProperty<Student> getStudentProperty() {
        return new SimpleObjectProperty<>(courseStudent.getStudent());
    }

    public StringProperty getCourseNameProperty() {
        return new SimpleStringProperty(courseStudent.getCourse().getCourseName());
    }

    public IntegerProperty getEctsProperty() {
        return new SimpleIntegerProperty(courseStudent.getCourse().getEcts());
    }
}
