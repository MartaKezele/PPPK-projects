/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.viewmodel;

import hr.algebra.model.Course;
import hr.algebra.model.CourseProfessor;
import hr.algebra.model.Professor;
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
public class CourseProfessorViewModel {

    private final CourseProfessor courseProfessor;

    public CourseProfessorViewModel(CourseProfessor courseProfessor) {
        if (courseProfessor == null) {
            courseProfessor = new CourseProfessor(courseProfessor);
        }
        this.courseProfessor = courseProfessor;
    }

    public CourseProfessor getCourseProfessor() {
        return courseProfessor;
    }

    public IntegerProperty getIdCourseProfessorProperty() {
        return new SimpleIntegerProperty(courseProfessor.getIDCourseProfessor());
    }

    public ObjectProperty<Course> getCourseProperty() {
        return new SimpleObjectProperty<>(courseProfessor.getCourse());
    }

    public ObjectProperty<Professor> getProfessorProperty() {
        return new SimpleObjectProperty<>(courseProfessor.getProfessor());
    }

    public StringProperty getCourseNameProperty() {
        return new SimpleStringProperty(courseProfessor.getCourse().getCourseName());
    }

    public IntegerProperty getEctsProperty() {
        return new SimpleIntegerProperty(courseProfessor.getCourse().getEcts());
    }
}
