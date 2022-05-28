/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.dal.RepoFactory;
import hr.algebra.model.Course;
import hr.algebra.model.CourseStudent;
import hr.algebra.model.Student;
import hr.algebra.utilities.FileUtils;
import hr.algebra.utilities.ImageUtils;
import hr.algebra.utilities.ValidationUtils;
import hr.algebra.viewmodel.CourseStudentViewModel;
import hr.algebra.viewmodel.CourseViewModel;
import hr.algebra.viewmodel.StudentViewModel;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
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
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.util.converter.IntegerStringConverter;

/**
 * FXML Controller class
 *
 * @author Marta
 */
public class StudentsController implements Initializable {

    private Map<TextField, Label> validationMap;
    private final ObservableList<StudentViewModel> students = FXCollections.observableArrayList();
    private StudentViewModel selectedStudentViewModel;
    private final ObservableList<CourseStudentViewModel> studentCourses = FXCollections.observableArrayList();
    private CourseStudentViewModel selectedStudentCourseViewModel;
    private final ObservableList<CourseViewModel> allCourses = FXCollections.observableArrayList();
    private CourseViewModel selectedAllCourseViewModel;

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private Button btnEdit;
    @FXML
    private Button btnDelete;
    @FXML
    private Tab tabEdit;
    @FXML
    private TableView<StudentViewModel> tvStudents;
    @FXML
    private TableColumn<StudentViewModel, String> tcFirstName;
    @FXML
    private TableColumn<StudentViewModel, String> tcLastName;
    @FXML
    private TableColumn<StudentViewModel, String> tcEmail;
    @FXML
    private TableColumn<StudentViewModel, Integer> tcEcts;
    @FXML
    private ImageView ivStudent;
    @FXML
    private Button btnCommit;
    @FXML
    private TextField tfFirstName;
    @FXML
    private TextField tfLastName;
    @FXML
    private TextField tfEmail;
    @FXML
    private TextField tfEcts;
    @FXML
    private Label lbFirstNameError;
    @FXML
    private Label lbLastNameError;
    @FXML
    private Label lbEmailError;
    @FXML
    private Label lbEctsError;
    @FXML
    private Label lbIconError;
    @FXML
    private TableView<CourseStudentViewModel> tvStudentCourses;
    @FXML
    private TableView<CourseViewModel> tvAllCourses;
    @FXML
    private Button btnAddCourse;
    @FXML
    private Button btnRemoveCourse;
    @FXML
    private TableColumn<CourseStudentViewModel, String> tcStudentCoursesCourseName;
    @FXML
    private TableColumn<CourseStudentViewModel, Integer> tcStudentCoursesEcts;
    @FXML
    private TableColumn<CourseViewModel, String> tcAllCoursesCourseName;
    @FXML
    private TableColumn<CourseViewModel, Integer> tcAllCoursesEcts;
    @FXML
    private Button btnManageStudentCourses;
    @FXML
    private Tab tabStudentCourses;
    @FXML
    private Label lbStudentName;

    /**
     * Initializes the controller class.
     *
     * @param url
     * @param rb
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initStudents();
        initAllCourses();
        initStudentsTable();
        initAllCoursesTable();
        addIntegerMask(tfEcts);
        setupListeners();
    }

    private void initValidation() {
        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfFirstName, lbFirstNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfLastName, lbLastNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfEcts, lbEctsError),
                new AbstractMap.SimpleImmutableEntry<>(tfEmail, lbEmailError))
                .collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
    }

    // init entities
    private void initStudents() {
        try {
            RepoFactory.getRepo().getStudents().forEach(student -> students.add(new StudentViewModel(student)));
        } catch (Exception ex) {
            Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
            new Alert(Alert.AlertType.ERROR, "Unable to load the form. Exiting...").show();
        }
    }

    private void initAllCourses() {
        try {
            RepoFactory.getRepo().getCourses().forEach(course -> allCourses.add(new CourseViewModel(course)));
        } catch (Exception ex) {
            Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
            new Alert(Alert.AlertType.ERROR, "Unable to load the form. Exiting...").show();
        }
    }

    private void initStudentCourses() {
        try {
            studentCourses.removeAll();
            tvStudentCourses.setItems(null);
            RepoFactory.getRepo().getStudentCourses(selectedStudentViewModel.getStudent()).forEach(courseStudent -> studentCourses.add(new CourseStudentViewModel(courseStudent)));
        } catch (Exception ex) {
            Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
            new Alert(Alert.AlertType.ERROR, "Unable to load the form. Exiting...").show();
        }
    }

    // init tables
    private void initStudentsTable() {
        tcFirstName.setCellValueFactory(student -> student.getValue().getFirstNameProperty());
        tcLastName.setCellValueFactory(student -> student.getValue().getLastNameProperty());
        tcEcts.setCellValueFactory(student -> student.getValue().getEctsProperty().asObject());
        tcEmail.setCellValueFactory(student -> student.getValue().getEmailProperty());
        tvStudents.setItems(students);
    }

    private void initAllCoursesTable() {
        tcAllCoursesCourseName.setCellValueFactory(course -> course.getValue().getCourseNameProperty());
        tcAllCoursesEcts.setCellValueFactory(course -> course.getValue().getEctsProperty().asObject());
        tvAllCourses.setItems(allCourses);
    }

    private void initStudentCoursesTable() {
        tcStudentCoursesCourseName.setCellValueFactory(cs -> cs.getValue().getCourseNameProperty());
        tcStudentCoursesEcts.setCellValueFactory(cs -> cs.getValue().getEctsProperty().asObject());
        tvStudentCourses.setItems(studentCourses);
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
                bindStudent(null);
            }
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabStudentCourses)) {
                studentCourses.remove(0, studentCourses.size());
            }
        });
        students.addListener((ListChangeListener.Change<? extends StudentViewModel> change) -> {
            if (change.next()) {
                if (change.wasRemoved()) {
                    change.getRemoved().forEach(pvm -> {
                        try {
                            RepoFactory.getRepo().deleteStudent(pvm.getStudent());
                        } catch (Exception ex) {
                            Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                } else if (change.wasAdded()) {
                    change.getAddedSubList().forEach(pvm -> {
                        try {
                            int idStudent = RepoFactory.getRepo().addStudent(pvm.getStudent());
                            pvm.getStudent().setIDStudent(idStudent);
                        } catch (Exception ex) {
                            Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                }
            }
        });
    }

    private void bindStudent(StudentViewModel viewModel) {
        resetForm();

        selectedStudentViewModel = viewModel != null ? viewModel : new StudentViewModel(null);
        tfFirstName.setText(selectedStudentViewModel.getFirstNameProperty().get());
        tfLastName.setText(selectedStudentViewModel.getLastNameProperty().get());
        tfEcts.setText(String.valueOf(selectedStudentViewModel.getEctsProperty().get()));
        tfEmail.setText(selectedStudentViewModel.getEmailProperty().get());
        ivStudent.setImage(selectedStudentViewModel.getPictureProperty().get() != null ? new Image(new ByteArrayInputStream(selectedStudentViewModel.getPictureProperty().get())) : new Image(new File("src/assets/no_image.png").toURI().toString()));
    }

    private void bindStudentCourses(StudentViewModel viewModel) {
        if (viewModel != null) {
            selectedStudentViewModel = viewModel;
            initStudentCourses();
            initStudentCoursesTable();
        }
    }

    private void resetForm() {
        validationMap.values().forEach(lb -> lb.setVisible(false));
        lbIconError.setVisible(false);
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

        if (selectedStudentViewModel.getPictureProperty().get() == null) {
            lbIconError.setVisible(true);
            ok.set(false);
        } else {
            lbIconError.setVisible(false);
        }
        return ok.get();
    }

    @FXML
    private void edit(ActionEvent event) {
        if (tvStudents.getSelectionModel().getSelectedItem() != null) {
            bindStudent(tvStudents.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEdit);
        }
    }

    @FXML
    private void delete(ActionEvent event) {
        if (tvStudents.getSelectionModel().getSelectedItem() != null) {
            students.remove(tvStudents.getSelectionModel().getSelectedItem());
        }
    }

    @FXML
    private void upload(ActionEvent event) {
        File file = FileUtils.uploadFileDialog(tfEmail.getScene().getWindow(), "jpg", "jpeg", "png");
        if (file != null) {
            Image image = new Image(file.toURI().toString());
            try {
                String ext = file.getName().substring(file.getName().lastIndexOf(".") + 1);
                selectedStudentViewModel.getStudent().setPicture(ImageUtils.imageToByteArray(image, ext));
                ivStudent.setImage(image);
            } catch (IOException ex) {
                Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    @FXML
    private void commit(ActionEvent event) {
        if (formValid()) {
            selectedStudentViewModel.getStudent().setFirstName(tfFirstName.getText().trim());
            selectedStudentViewModel.getStudent().setLastName(tfLastName.getText().trim());
            selectedStudentViewModel.getStudent().setEcts(Integer.valueOf(tfEcts.getText().trim()));
            selectedStudentViewModel.getStudent().setEmail(tfEmail.getText().trim());
            if (selectedStudentViewModel.getStudent().getIDStudent() == 0) {
                students.add(selectedStudentViewModel);
            } else {
                try {
                    // we cannot listen to the upates
                    RepoFactory.getRepo().updateStudent(selectedStudentViewModel.getStudent());
                    tvStudents.refresh();
                } catch (Exception ex) {
                    Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
            selectedStudentViewModel = null;
            tpContent.getSelectionModel().select(tabList);
            resetForm();
        }
    }

    @FXML
    private void addCourse(ActionEvent event) {
        try {
            Course course = tvAllCourses.getSelectionModel().getSelectedItem().getCourse();
            Student student = tvStudents.getSelectionModel().getSelectedItem().getStudent();

            boolean contains = false;
            for (CourseStudentViewModel item : tvStudentCourses.getItems()) {
                if (item.getCourseProperty().get().getIDCourse().equals(tvAllCourses.getSelectionModel().getSelectedItem().getCourse().getIDCourse())) {
                    contains = true;
                }
            }

            if (contains) {
                new Alert(Alert.AlertType.INFORMATION, "Student is already assigned to this course.").show();
            } else {
                CourseStudent courseStudent = new CourseStudent();
                courseStudent.setCourse(course);
                courseStudent.setStudent(student);
                int idCourseStudent = RepoFactory.getRepo().addCourseStudent(courseStudent);
                CourseStudent cs = RepoFactory.getRepo().getCourseStudent(idCourseStudent);
                studentCourses.add(new CourseStudentViewModel(cs));
            }

        } catch (Exception ex) {
            Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @FXML
    private void removeCourse(ActionEvent event) {
        try {
            int idCourseStudent = tvStudentCourses.getSelectionModel().getSelectedItem().getIdCourseStudentProperty().get();
            CourseStudent courseStudent = RepoFactory.getRepo().getCourseStudent(idCourseStudent);
            RepoFactory.getRepo().deleteCourseStudent(courseStudent);
            studentCourses.remove(tvStudentCourses.getSelectionModel().getSelectedItem());
        } catch (Exception ex) {
            Logger.getLogger(StudentsController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @FXML
    private void manageStudentCourses(ActionEvent event) {
        studentCourses.remove(0, studentCourses.size());
        if (tvStudents.getSelectionModel().getSelectedItem() != null) {
            bindStudentCourses(tvStudents.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabStudentCourses);
        }
    }
}
