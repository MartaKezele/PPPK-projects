/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.viewmodel;

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
public class ProfessorViewModel {

    private final Professor professor;

    public ProfessorViewModel(Professor professor) {
        if (professor == null) {
            professor = new Professor(0, "", "", "");
        }
        this.professor = professor;
    }

    public Professor getProfessor() {
        return professor;
    }

    public IntegerProperty getIdProfessorProperty() {
        return new SimpleIntegerProperty(professor.getIDProfessor());
    }

    public StringProperty getFirstNameProperty() {
        return new SimpleStringProperty(professor.getFirstName());
    }

    public StringProperty getLastNameProperty() {
        return new SimpleStringProperty(professor.getLastName());
    }

    public StringProperty getEmailProperty() {
        return new SimpleStringProperty(professor.getEmail());
    }

    public ObjectProperty<byte[]> getPictureProperty() {
        return new SimpleObjectProperty<>(professor.getPicture());
    }
}
