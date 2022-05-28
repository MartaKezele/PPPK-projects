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
@Table(name = "Professor")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Professor.findAll", query = "SELECT p FROM Professor p")
    , @NamedQuery(name = "Professor.findByIDProfessor", query = "SELECT p FROM Professor p WHERE p.iDProfessor = :iDProfessor")
    , @NamedQuery(name = "Professor.findByFirstName", query = "SELECT p FROM Professor p WHERE p.firstName = :firstName")
    , @NamedQuery(name = "Professor.findByLastName", query = "SELECT p FROM Professor p WHERE p.lastName = :lastName")
    , @NamedQuery(name = "Professor.findByEmail", query = "SELECT p FROM Professor p WHERE p.email = :email")})
public class Professor implements Serializable {

    private static final long serialVersionUID = 1L;
    @Id
    @Basic(optional = false)
    @Column(name = "IDProfessor")
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Integer iDProfessor;
    @Basic(optional = false)
    @Column(name = "FirstName")
    private String firstName;
    @Basic(optional = false)
    @Column(name = "LastName")
    private String lastName;
    @Basic(optional = false)
    @Column(name = "Email")
    private String email;
    @Lob
    @Column(name = "Picture")
    private byte[] picture;
    @OneToMany(mappedBy = "professor")
    private Collection<CourseProfessor> courseProfessorCollection;

    public Professor() {
    }

    public Professor(Professor data) {
        updateDetails(data);
    }

    public void updateDetails(Professor data) {
        this.firstName = data.getFirstName();
        this.lastName = data.getLastName();
        this.email = data.getEmail();
        this.picture = data.getPicture();
    }

    public Professor(Integer iDProfessor) {
        this.iDProfessor = iDProfessor;
    }

    public Professor(Integer iDProfessor, String firstName, String lastName, String email) {
        this.iDProfessor = iDProfessor;
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
    }

    public Integer getIDProfessor() {
        return iDProfessor;
    }

    public void setIDProfessor(Integer iDProfessor) {
        this.iDProfessor = iDProfessor;
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
    public Collection<CourseProfessor> getCourseProfessorCollection() {
        return courseProfessorCollection;
    }

    public void setCourseProfessorCollection(Collection<CourseProfessor> courseProfessorCollection) {
        this.courseProfessorCollection = courseProfessorCollection;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (iDProfessor != null ? iDProfessor.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Professor)) {
            return false;
        }
        Professor other = (Professor) object;
        if ((this.iDProfessor == null && other.iDProfessor != null) || (this.iDProfessor != null && !this.iDProfessor.equals(other.iDProfessor))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "hr.algebra.model.Professor[ iDProfessor=" + iDProfessor + " ]";
    }

}
