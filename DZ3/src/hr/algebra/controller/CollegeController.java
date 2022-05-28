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
import java.util.logging.Level;
import java.util.logging.Logger;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;

/**
 * FXML Controller class
 *
 * @author Marta
 */
public class CollegeController implements Initializable {

    @FXML
    private Button btnManageProfessors;
    @FXML
    private Button btnManageStudents;
    @FXML
    private Button btnManageCourses;

    /**
     * Initializes the controller class.
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }

        @FXML
    private void manageStudents(ActionEvent event) {
        try {
            Parent root = FXMLLoader.load(
                    CollegeApp.class.getResource("view/Students.fxml"));
            
            CollegeApp.getPrimaryStage().setScene(new Scene(root, CollegeApp.WIDTH, CollegeApp.HEIGHT));
        } catch (IOException ex) {
            Logger.getLogger(CollegeController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    @FXML
    private void manageProfessors(ActionEvent event) {
        try {
            Parent root = FXMLLoader.load(
                    CollegeApp.class.getResource("view/Professors.fxml"));
            
            CollegeApp.getPrimaryStage().setScene(new Scene(root, CollegeApp.WIDTH, CollegeApp.HEIGHT));
        } catch (IOException ex) {
            Logger.getLogger(CollegeController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @FXML
    private void manageCourses(ActionEvent event) {
        try {
            Parent root = FXMLLoader.load(
                    CollegeApp.class.getResource("view/Courses.fxml"));
            
            CollegeApp.getPrimaryStage().setScene(new Scene(root, CollegeApp.WIDTH, CollegeApp.HEIGHT));
        } catch (IOException ex) {
            Logger.getLogger(CollegeController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

}
