/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.CollegeApp;
import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;

/**
 * FXML Controller class
 *
 * @author Marta
 */
public class MenuBarController implements Initializable {

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }

    @FXML
    public void manageStudents(ActionEvent event) throws IOException {
        Parent root = FXMLLoader.load(
                CollegeApp.class.getResource("view/Students.fxml"));

        CollegeApp.getPrimaryStage().setScene(new Scene(root, CollegeApp.WIDTH, CollegeApp.HEIGHT));
    }

    @FXML
    public void manageProfessors(ActionEvent event) throws IOException {
        Parent root = FXMLLoader.load(
                CollegeApp.class.getResource("view/Professors.fxml"));

        CollegeApp.getPrimaryStage().setScene(new Scene(root, CollegeApp.WIDTH, CollegeApp.HEIGHT));
    }

    @FXML
    public void manageCourses(ActionEvent event) throws IOException {
        Parent root = FXMLLoader.load(
                CollegeApp.class.getResource("view/Courses.fxml"));

        CollegeApp.getPrimaryStage().setScene(new Scene(root, CollegeApp.WIDTH, CollegeApp.HEIGHT));
    }
}
