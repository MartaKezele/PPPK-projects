/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.model;

import java.io.Serializable;
import java.util.Collection;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Lob;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlTransient;

/**
 *
 * @author Marta
 */
@Entity
@Table(name = "Student")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Student.findAll", query = "SELECT s FROM Student s")
    , @NamedQuery(name = "Student.findByIDStudent", query = "SELECT s FROM Student s WHERE s.iDStudent = :iDStudent")
    , @NamedQuery(name = "Student.findByFirstName", query = "SELECT s FROM Student s WHERE s.firstName = :firstName")
    , @NamedQuery(name = "Student.findByLastName", query = "SELECT s FROM Student s WHERE s.lastName = :lastName")
    , @NamedQuery(name = "Student.findByEcts", query = "SELECT s FROM Student s WHERE s.ects = :ects")
    , @NamedQuery(name = "Student.findByEmail", query = "SELECT s FROM Student s WHERE s.email = :email")})
public class Student implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDStudent")
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer iDStudent;
    @Basic(optional = false)
    @Column(name = "FirstName")
    private String firstName;
    @Basic(optional = false)
    @Column(name = "LastName")
    private String lastName;
    @Basic(optional = false)
    @Column(name = "Ects")
    private int ects;
    @Basic(optional = false)
    @Column(name = "Email")
    private String email;
    @Lob
    @Column(name = "Picture")
    private byte[] picture;
    @OneToMany(mappedBy = "student")
    private Collection<CourseStudent> courseStudentCollection;

    public Student() {
    }

    public Student(Student data) {
        updateDetails(data);
    }

    public void updateDetails(Student data) {
        this.firstName = data.getFirstName();
        this.lastName = data.getLastName();
        this.email = data.getEmail();
        this.ects = data.getEcts();
        this.picture = data.getPicture();
    }

    public Student(Integer iDStudent) {
        this.iDStudent = iDStudent;
    }

    public Student(Integer iDStudent, String firstName, String lastName, int ects, String email) {
        this.iDStudent = iDStudent;
        this.firstName = firstName;
        this.lastName = lastName;
        this.ects = ects;
        this.email = email;
    }

    public Integer getIDStudent() {
        return iDStudent;
    }

    public void setIDStudent(Integer iDStudent) {
        this.iDStudent = iDStudent;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public int getEcts() {
        return ects;
    }

    public void setEcts(int ects) {
        this.ects = ects;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public byte[] getPicture() {
        return picture;
    }

    public void setPicture(byte[] picture) {
        this.picture = picture;
    }

    @XmlTransient
    public Collection<CourseStudent> getCourseStudentCollection() {
        return courseStudentCollection;
    }

    public void setCourseStudentCollection(Collection<CourseStudent> courseStudentCollection) {
        this.courseStudentCollection = courseStudentCollection;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDStudent != null ? iDStudent.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Student)) {
            return false;
        }
        Student other = (Student) object;
        if ((this.iDStudent == null && other.iDStudent != null) || (this.iDStudent != null && !this.iDStudent.equals(other.iDStudent))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "hr.algebra.model.Student[ iDStudent=" + iDStudent + " ]";
    }

}
