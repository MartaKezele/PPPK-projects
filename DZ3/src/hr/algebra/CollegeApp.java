/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra;

import java.io.IOException;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

/**
 *
 * @author Marta
 */
public class CollegeApp extends Application {

    private static Stage primaryStage;
    public static int WIDTH = 1200;
    public static int HEIGHT = 800;

    @Override
    public void start(Stage primaryStage) throws IOException {
        CollegeApp.primaryStage = primaryStage;
        Parent root = FXMLLoader.load(getClass().getResource("view/College.fxml"));

        Scene scene = new Scene(root, WIDTH, HEIGHT);

        primaryStage.setTitle("College manager");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }

    public static Stage getPrimaryStage() {
        return primaryStage;
    }

}
