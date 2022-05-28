/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.dal.RepoFactory;
import hr.algebra.utilities.ValidationUtils;
import hr.algebra.viewmodel.CourseViewModel;
import java.net.URL;
import java.util.AbstractMap;
import java.util.Map;
import java.util.ResourceBundle;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.function.UnaryOperator;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import javafx.collections.FXCollections;
import javafx.collections.ListChangeListener;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import javafx.scene.control.TextFormatter;
import javafx.util.converter.IntegerStringConverter;

/**
 * FXML Controller class
 *
 * @author Marta
 */
public class CoursesController implements Initializable {

    private Map<TextField, Label> validationMap;
    private final ObservableList<CourseViewModel> courses = FXCollections.observableArrayList();
    private CourseViewModel selectedCourseViewModel;

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private TableColumn<CourseViewModel, String> tcCourseName;
    @FXML
    private TableColumn<CourseViewModel, Integer> tcEcts;
    @FXML
    private Button btnEdit;
    @FXML
    private Button btnDelete;
    @FXML
    private Tab tabEdit;
    @FXML
    private TextField tfCourseName;
    @FXML
    private TextField tfEcts;
    @FXML
    private Label lbCourseNameError;
    @FXML
    private Label lbEctsError;
    @FXML
    private Button btnCommit;
    @FXML
    private TableView<CourseViewModel> tvCourses;

    /**
     * Initializes the controller class.
     *
     * @param url
     * @param rb
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initCourses();
        initCoursesTable();
        addIntegerMask(tfEcts);
        setupListeners();
    }

    private void initValidation() {
        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfCourseName, lbCourseNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfEcts, lbEctsError))
                .collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
    }

    // init entities
    private void initCourses() {
        try {
            RepoFactory.getRepo().getCourses().forEach(course -> courses.add(new CourseViewModel(course)));
        } catch (Exception ex) {
            Logger.getLogger(CoursesController.class.getName()).log(Level.SEVERE, null, ex);
            new Alert(Alert.AlertType.ERROR, "Unable to load the form. Exiting...").show();
        }
    }

    // init tables
    private void initCoursesTable() {
        tcCourseName.setCellValueFactory(course -> course.getValue().getCourseNameProperty());
        tcEcts.setCellValueFactory(course -> course.getValue().getEctsProperty().asObject());
        tvCourses.setItems(courses);
    }

    private void addIntegerMask(TextField tf) {
        UnaryOperator<TextFormatter.Change> integerFilter = change -> {
            String input = change.getText();
            if (input.matches("\\d*")) {
                return change;
            }
            return null;
        };
        // first we must convert integer to string, and then we apply filter
        tf.setTextFormatter(new TextFormatter<>(new IntegerStringConverter(), 0, integerFilter));
    }

    private void setupListeners() {
        tpContent.setOnMouseClicked(event -> {
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabEdit)) {
                bindCourse(null);
            }
        });
        courses.addListener((ListChangeListener.Change<? extends CourseViewModel> change) -> {
            if (change.next()) {
                if (change.wasRemoved()) {
                    change.getRemoved().forEach(pvm -> {
                        try {
                            RepoFactory.getRepo().deleteCourse(pvm.getCourse());
                        } catch (Exception ex) {
                            Logger.getLogger(CoursesController.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                } else if (change.wasAdded()) {
                    change.getAddedSubList().forEach(pvm -> {
                        try {
                            int idCourse = RepoFactory.getRepo().addCourse(pvm.getCourse());
                            pvm.getCourse().setIDCourse(idCourse);
                        } catch (Exception ex) {
                            Logger.getLogger(CoursesController.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                }
            }
        });
    }

    private void bindCourse(CourseViewModel viewModel) {
        resetForm();

        selectedCourseViewModel = viewModel != null ? viewModel : new CourseViewModel(null);
        tfCourseName.setText(selectedCourseViewModel.getCourseNameProperty().get());
        tfEcts.setText(String.valueOf(selectedCourseViewModel.getEctsProperty().get()));
    }

    private void bindCourseCourses(CourseViewModel viewModel) {
        if (viewModel != null) {
            selectedCourseViewModel = viewModel;
            initCourses();
            initCoursesTable();
        }
    }

    private void resetForm() {
        validationMap.values().forEach(lb -> lb.setVisible(false));
    }

    private boolean formValid() {
        // discouraged but ok!
        final AtomicBoolean ok = new AtomicBoolean(true);
        validationMap.keySet().forEach(tf -> {
            if (tf.getText().trim().isEmpty() || tf.getId().contains("Email") && !ValidationUtils.isValidEmail(tf.getText().trim())) {
                validationMap.get(tf).setVisible(true);
                ok.set(false);
            } else {
                validationMap.get(tf).setVisible(false);
            }
        });

        return ok.get();
    }

    @FXML
    private void edit(ActionEvent event) {
        if (tvCourses.getSelectionModel().getSelectedItem() != null) {
            bindCourse(tvCourses.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEdit);
        }
    }

    @FXML
    private void delete(ActionEvent event) {
        if (tvCourses.getSelectionModel().getSelectedItem() != null) {
            courses.remove(tvCourses.getSelectionModel().getSelectedItem());
        }
    }

    @FXML
    private void commit(ActionEvent event) {
        if (formValid()) {
            selectedCourseViewModel.getCourse().setCourseName(tfCourseName.getText().trim());
            selectedCourseViewModel.getCourse().setEcts(Integer.valueOf(tfEcts.getText().trim()));
            if (selectedCourseViewModel.getCourse().getIDCourse() == 0) {
                courses.add(selectedCourseViewModel);
            } else {
                try {
                    // we cannot listen to the upates
                    RepoFactory.getRepo().updateCourse(selectedCourseViewModel.getCourse());
                    tvCourses.refresh();
                } catch (Exception ex) {
                    Logger.getLogger(CoursesController.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
            selectedCourseViewModel = null;
            tpContent.getSelectionModel().select(tabList);
            resetForm();
        }
    }
}
