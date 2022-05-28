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
@Table(name = "CourseProfessor")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "CourseProfessor.findAll", query = "SELECT c FROM CourseProfessor c")
    , @NamedQuery(name = "CourseProfessor.findByIDCourseProfessor", query = "SELECT c FROM CourseProfessor c WHERE c.iDCourseProfessor = :iDCourseProfessor")
    , @NamedQuery(name = "CourseProfessor.findByIDProfessor", query = "SELECT c FROM CourseProfessor c WHERE c.professor = :professor")
    , @NamedQuery(name = "CourseProfessor.findByIDCourse", query = "SELECT c FROM CourseProfessor c WHERE c.course = :course")})

public class CourseProfessor implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDCourseProfessor")
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer iDCourseProfessor;
    @JoinColumn(name = "CourseID", referencedColumnName = "IDCourse")
    @ManyToOne
    private Course course;
    @JoinColumn(name = "ProfessorID", referencedColumnName = "IDProfessor")
    @ManyToOne
    private Professor professor;

    public CourseProfessor() {
    }

    public CourseProfessor(CourseProfessor data) {
        updateDetails(data);
    }

    private void updateDetails(CourseProfessor data) {
        this.course = data.getCourse();
        this.professor = data.getProfessor();
    }

    public CourseProfessor(Integer iDCourseProfessor) {
        this.iDCourseProfessor = iDCourseProfessor;
    }

    public Integer getIDCourseProfessor() {
        return iDCourseProfessor;
    }

    public void setIDCourseProfessor(Integer iDCourseProfessor) {
        this.iDCourseProfessor = iDCourseProfessor;
    }

    public Course getCourse() {
        return course;
    }

    public void setCourse(Course course) {
        this.course = course;
    }

    public Professor getProfessor() {
        return professor;
    }

    public void setProfessor(Professor professor) {
        this.professor = professor;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDCourseProfessor != null ? iDCourseProfessor.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof CourseProfessor)) {
            return false;
        }
        CourseProfessor other = (CourseProfessor) object;
        if ((this.iDCourseProfessor == null && other.iDCourseProfessor != null) || (this.iDCourseProfessor != null && !this.iDCourseProfessor.equals(other.iDCourseProfessor))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "hr.algebra.model.CourseProfessor[ iDCourseProfessor=" + iDCourseProfessor + " ]";
    }

}
