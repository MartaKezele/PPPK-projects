/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.model;

import java.io.Serializable;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author Marta
 */
@Entity
@Table(name = "CourseStudent")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "CourseStudent.findAll", query = "SELECT c FROM CourseStudent c")
    , @NamedQuery(name = "CourseStudent.findByIDCourseStudent", query = "SELECT c FROM CourseStudent c WHERE c.iDCourseStudent = :iDCourseStudent")
    , @NamedQuery(name = "CourseStudent.findByIDStudent", query = "SELECT c FROM CourseStudent c WHERE c.student = :student")
    , @NamedQuery(name = "CourseStudent.findByIDCourse", query = "SELECT c FROM CourseStudent c WHERE c.course = :course")})
public class CourseStudent implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDCourseStudent")
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer iDCourseStudent;
    @JoinColumn(name = "CourseID", referencedColumnName = "IDCourse")
    @ManyToOne
    private Course course;
    @JoinColumn(name = "StudentID", referencedColumnName = "IDStudent")
    @ManyToOne
    private Student student;

    public CourseStudent() {
    }

    public CourseStudent(CourseStudent data) {
        updateDetails(data);
    }

    private void updateDetails(CourseStudent data) {
        this.course = data.getCourse();
        this.student = data.getStudent();
    }

    public CourseStudent(Integer iDCourseStudent) {
        this.iDCourseStudent = iDCourseStudent;
    }

    public Integer getIDCourseStudent() {
        return iDCourseStudent;
    }

    public void setIDCourseStudent(Integer iDCourseStudent) {
        this.iDCourseStudent = iDCourseStudent;
    }

    public Course getCourse() {
        return course;
    }

    public void setCourse(Course course) {
        this.course = course;
    }

    public Student getStudent() {
        return student;
    }

    public void setStudent(Student student) {
        this.student = student;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDCourseStudent != null ? iDCourseStudent.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof CourseStudent)) {
            return false;
        }
        CourseStudent other = (CourseStudent) object;
        if ((this.iDCourseStudent == null && other.iDCourseStudent != null) || (this.iDCourseStudent != null && !this.iDCourseStudent.equals(other.iDCourseStudent))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "hr.algebra.model.CourseStudent[ iDCourseStudent=" + iDCourseStudent + " ]";
    }

}
