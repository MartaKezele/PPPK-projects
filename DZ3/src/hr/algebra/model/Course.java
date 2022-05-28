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
@Table(name = "Course")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Course.findAll", query = "SELECT c FROM Course c")
    , @NamedQuery(name = "Course.findByIDCourse", query = "SELECT c FROM Course c WHERE c.iDCourse = :iDCourse")
    , @NamedQuery(name = "Course.findByCourseName", query = "SELECT c FROM Course c WHERE c.courseName = :courseName")
    , @NamedQuery(name = "Course.findByEcts", query = "SELECT c FROM Course c WHERE c.ects = :ects")})
public class Course implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDCourse")
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer iDCourse;
    @Basic(optional = false)
    @Column(name = "CourseName")
    private String courseName;
    @Basic(optional = false)
    @Column(name = "Ects")
    private int ects;
    @OneToMany(mappedBy = "course")
    private Collection<CourseStudent> courseStudentCollection;
    @OneToMany(mappedBy = "course")
    private Collection<CourseProfessor> courseProfessorCollection;

    public Course() {
    }

    public Course(Course data) {
        updateDetails(data);
    }

    public void updateDetails(Course data) {
        this.courseName = data.getCourseName();
        this.ects = data.getEcts();
    }

    public Course(Integer iDCourse) {
        this.iDCourse = iDCourse;
    }

    public Course(Integer iDCourse, String courseName, int ects) {
        this.iDCourse = iDCourse;
        this.courseName = courseName;
        this.ects = ects;
    }

    public Integer getIDCourse() {
        return iDCourse;
    }

    public void setIDCourse(Integer iDCourse) {
        this.iDCourse = iDCourse;
    }

    public String getCourseName() {
        return courseName;
    }

    public void setCourseName(String courseName) {
        this.courseName = courseName;
    }

    public int getEcts() {
        return ects;
    }

    public void setEcts(int ects) {
        this.ects = ects;
    }

    @XmlTransient
    public Collection<CourseStudent> getCourseStudentCollection() {
        return courseStudentCollection;
    }

    public void setCourseStudentCollection(Collection<CourseStudent> courseStudentCollection) {
        this.courseStudentCollection = courseStudentCollection;
    }

    @XmlTransient
    public Collection<CourseProfessor> getCourseProfessorCollection() {
        return courseProfessorCollection;
    }

    public void setCourseProfessorCollection(Collection<CourseProfessor> courseProfessorCollection) {
        this.courseProfessorCollection = courseProfessorCollection;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDCourse != null ? iDCourse.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Course)) {
            return false;
        }
        Course other = (Course) object;
        if ((this.iDCourse == null && other.iDCourse != null) || 
                (this.iDCourse != null && !this.iDCourse.equals(other.iDCourse))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "hr.algebra.model.Course[ iDCourse=" + iDCourse + " ]";
    }

}
