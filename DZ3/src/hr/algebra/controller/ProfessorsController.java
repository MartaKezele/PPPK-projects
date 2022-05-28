/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.controller;

import hr.algebra.dal.RepoFactory;
import hr.algebra.model.Course;
import hr.algebra.model.CourseProfessor;
import hr.algebra.model.Professor;
import hr.algebra.utilities.FileUtils;
import hr.algebra.utilities.ImageUtils;
import hr.algebra.utilities.ValidationUtils;
import hr.algebra.viewmodel.CourseProfessorViewModel;
import hr.algebra.viewmodel.CourseViewModel;
import hr.algebra.viewmodel.ProfessorViewModel;
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
public class ProfessorsController implements Initializable {

    private Map<TextField, Label> validationMap;
    private final ObservableList<ProfessorViewModel> professors = FXCollections.observableArrayList();
    private ProfessorViewModel selectedProfessorViewModel;
    private final ObservableList<CourseProfessorViewModel> professorCourses = FXCollections.observableArrayList();
    private CourseProfessorViewModel selectedProfessorCourseViewModel;
    private final ObservableList<CourseViewModel> allCourses = FXCollections.observableArrayList();
    private CourseViewModel selectedAllCourseViewModel;

    @FXML
    private TabPane tpContent;
    @FXML
    private Tab tabList;
    @FXML
    private TableColumn<ProfessorViewModel, String> tcFirstName;
    @FXML
    private TableColumn<ProfessorViewModel, String> tcLastName;
    @FXML
    private TableColumn<ProfessorViewModel, String> tcEmail;
    @FXML
    private Button btnEdit;
    @FXML
    private Button btnDelete;
    @FXML
    private Tab tabEdit;
    @FXML
    private Button btnCommit;
    @FXML
    private TextField tfFirstName;
    @FXML
    private TextField tfLastName;
    @FXML
    private TextField tfEmail;
    @FXML
    private Label lbFirstNameError;
    @FXML
    private Label lbLastNameError;
    @FXML
    private Label lbEmailError;
    @FXML
    private Label lbIconError;
    @FXML
    private TableView<CourseViewModel> tvAllCourses;
    @FXML
    private TableColumn<CourseViewModel, String> tcAllCoursesCourseName;
    @FXML
    private TableColumn<CourseViewModel, Integer> tcAllCoursesEcts;
    @FXML
    private Button btnAddCourse;
    @FXML
    private Button btnRemoveCourse;
    @FXML
    private Label lbStudentName;
    @FXML
    private TableView<ProfessorViewModel> tvProfessors;
    @FXML
    private ImageView ivProfessor;
    @FXML
    private TableView<CourseProfessorViewModel> tvProfessorCourses;
    @FXML
    private TableColumn<CourseProfessorViewModel, String> tcProfessorCoursesCourseName;
    @FXML
    private TableColumn<CourseProfessorViewModel, Integer> tcProfessorCoursesEcts;
    @FXML
    private Tab tabProfessorCourses;

    /**
     * Initializes the controller class.
     *
     * @param url
     * @param rb
     */
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        initValidation();
        initProfessors();
        initAllCourses();
        initProfessorsTable();
        initAllCoursesTable();
        setupListeners();
    }

    private void initValidation() {
        validationMap = Stream.of(
                new AbstractMap.SimpleImmutableEntry<>(tfFirstName, lbFirstNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfLastName, lbLastNameError),
                new AbstractMap.SimpleImmutableEntry<>(tfEmail, lbEmailError))
                .collect(Collectors.toMap(Map.Entry::getKey, Map.Entry::getValue));
    }

    // init entities
    private void initProfessors() {
        try {
            RepoFactory.getRepo().getProfessors().forEach(professor -> professors.add(new ProfessorViewModel(professor)));
        } catch (Exception ex) {
            Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
            new Alert(Alert.AlertType.ERROR, "Unable to load the form. Exiting...").show();
        }
    }

    private void initAllCourses() {
        try {
            RepoFactory.getRepo().getCourses().forEach(course -> allCourses.add(new CourseViewModel(course)));
        } catch (Exception ex) {
            Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
            new Alert(Alert.AlertType.ERROR, "Unable to load the form. Exiting...").show();
        }
    }

    private void initProfessorCourses() {
        try {
            professorCourses.removeAll();
            tvProfessorCourses.setItems(null);
            RepoFactory.getRepo().getProfessorCourses(selectedProfessorViewModel.getProfessor()).forEach(courseProfessor -> professorCourses.add(new CourseProfessorViewModel(courseProfessor)));
        } catch (Exception ex) {
            Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
            new Alert(Alert.AlertType.ERROR, "Unable to load the form. Exiting...").show();
        }
    }

    // init tables
    private void initProfessorsTable() {
        tcFirstName.setCellValueFactory(professor -> professor.getValue().getFirstNameProperty());
        tcLastName.setCellValueFactory(professor -> professor.getValue().getLastNameProperty());
        tcEmail.setCellValueFactory(professor -> professor.getValue().getEmailProperty());
        tvProfessors.setItems(professors);
    }

    private void initAllCoursesTable() {
        tcAllCoursesCourseName.setCellValueFactory(course -> course.getValue().getCourseNameProperty());
        tcAllCoursesEcts.setCellValueFactory(course -> course.getValue().getEctsProperty().asObject());
        tvAllCourses.setItems(allCourses);
    }

    private void initProfessorCoursesTable() {
        tcProfessorCoursesCourseName.setCellValueFactory(cs -> cs.getValue().getCourseNameProperty());
        tcProfessorCoursesEcts.setCellValueFactory(cs -> cs.getValue().getEctsProperty().asObject());
        tvProfessorCourses.setItems(professorCourses);
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
                bindProfessor(null);
            }
            if (tpContent.getSelectionModel().getSelectedItem().equals(tabProfessorCourses)) {
                professorCourses.remove(0, professorCourses.size());
            }
        });
        professors.addListener((ListChangeListener.Change<? extends ProfessorViewModel> change) -> {
            if (change.next()) {
                if (change.wasRemoved()) {
                    change.getRemoved().forEach(pvm -> {
                        try {
                            RepoFactory.getRepo().deleteProfessor(pvm.getProfessor());
                        } catch (Exception ex) {
                            Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                } else if (change.wasAdded()) {
                    change.getAddedSubList().forEach(pvm -> {
                        try {
                            int idProfessor = RepoFactory.getRepo().addProfessor(pvm.getProfessor());
                            pvm.getProfessor().setIDProfessor(idProfessor);
                        } catch (Exception ex) {
                            Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    });
                }
            }
        });
    }

    private void bindProfessor(ProfessorViewModel viewModel) {
        resetForm();

        selectedProfessorViewModel = viewModel != null ? viewModel : new ProfessorViewModel(null);
        tfFirstName.setText(selectedProfessorViewModel.getFirstNameProperty().get());
        tfLastName.setText(selectedProfessorViewModel.getLastNameProperty().get());
        tfEmail.setText(selectedProfessorViewModel.getEmailProperty().get());
        ivProfessor.setImage(selectedProfessorViewModel.getPictureProperty().get() != null ? new Image(new ByteArrayInputStream(selectedProfessorViewModel.getPictureProperty().get())) : new Image(new File("src/assets/no_image.png").toURI().toString()));
    }

    private void bindProfessorCourses(ProfessorViewModel viewModel) {
        if (viewModel != null) {
            selectedProfessorViewModel = viewModel;
            initProfessorCourses();
            initProfessorCoursesTable();
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

        if (selectedProfessorViewModel.getPictureProperty().get() == null) {
            lbIconError.setVisible(true);
            ok.set(false);
        } else {
            lbIconError.setVisible(false);
        }
        return ok.get();
    }

    @FXML
    private void edit(ActionEvent event) {
        if (tvProfessors.getSelectionModel().getSelectedItem() != null) {
            bindProfessor(tvProfessors.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabEdit);
        }
    }

    @FXML
    private void delete(ActionEvent event) {
        if (tvProfessors.getSelectionModel().getSelectedItem() != null) {
            professors.remove(tvProfessors.getSelectionModel().getSelectedItem());
        }
    }

    @FXML
    private void upload(ActionEvent event) {
        File file = FileUtils.uploadFileDialog(tfEmail.getScene().getWindow(), "jpg", "jpeg", "png");
        if (file != null) {
            Image image = new Image(file.toURI().toString());
            try {
                String ext = file.getName().substring(file.getName().lastIndexOf(".") + 1);
                selectedProfessorViewModel.getProfessor().setPicture(ImageUtils.imageToByteArray(image, ext));
                ivProfessor.setImage(image);
            } catch (IOException ex) {
                Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }

    @FXML
    private void commit(ActionEvent event) {
        if (formValid()) {
            selectedProfessorViewModel.getProfessor().setFirstName(tfFirstName.getText().trim());
            selectedProfessorViewModel.getProfessor().setLastName(tfLastName.getText().trim());
            selectedProfessorViewModel.getProfessor().setEmail(tfEmail.getText().trim());
            if (selectedProfessorViewModel.getProfessor().getIDProfessor() == 0) {
                professors.add(selectedProfessorViewModel);
            } else {
                try {
                    // we cannot listen to the upates
                    RepoFactory.getRepo().updateProfessor(selectedProfessorViewModel.getProfessor());
                    tvProfessors.refresh();
                } catch (Exception ex) {
                    Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
            selectedProfessorViewModel = null;
            tpContent.getSelectionModel().select(tabList);
            resetForm();
        }
    }

    @FXML
    private void addCourse(ActionEvent event) {
        try {
            Course course = tvAllCourses.getSelectionModel().getSelectedItem().getCourse();
            Professor professor = tvProfessors.getSelectionModel().getSelectedItem().getProfessor();

            boolean contains = false;
            for (CourseProfessorViewModel item : tvProfessorCourses.getItems()) {
                if (item.getCourseProperty().get().getIDCourse().equals(tvAllCourses.getSelectionModel().getSelectedItem().getCourse().getIDCourse())) {
                    contains = true;
                }
            }

            if (contains) {
                new Alert(Alert.AlertType.INFORMATION, "Professor is already assigned to this course.").show();
            } else {
                CourseProfessor courseProfessor = new CourseProfessor();
                courseProfessor.setCourse(course);
                courseProfessor.setProfessor(professor);
                int idCourseProfessor = RepoFactory.getRepo().addCourseProfessor(courseProfessor);
                CourseProfessor cs = RepoFactory.getRepo().getCourseProfessor(idCourseProfessor);
                professorCourses.add(new CourseProfessorViewModel(cs));
            }

        } catch (Exception ex) {
            Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @FXML
    private void removeCourse(ActionEvent event) {
        try {
            int idCourseProfessor = tvProfessorCourses.getSelectionModel().getSelectedItem().getIdCourseProfessorProperty().get();
            CourseProfessor courseProfessor = RepoFactory.getRepo().getCourseProfessor(idCourseProfessor);
            RepoFactory.getRepo().deleteCourseProfessor(courseProfessor);
            professorCourses.remove(tvProfessorCourses.getSelectionModel().getSelectedItem());
        } catch (Exception ex) {
            Logger.getLogger(ProfessorsController.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    @FXML
    private void manageProfessorCourses(ActionEvent event) {
        professorCourses.remove(0, professorCourses.size());
        if (tvProfessors.getSelectionModel().getSelectedItem() != null) {
            bindProfessorCourses(tvProfessors.getSelectionModel().getSelectedItem());
            tpContent.getSelectionModel().select(tabProfessorCourses);
        }
    }

}
