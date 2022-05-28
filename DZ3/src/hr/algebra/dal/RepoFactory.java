/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.algebra.dal;

import hr.algebra.dal.sql.HibernateRepo;

/**
 *
 * @author Marta
 */
public class RepoFactory {

    private RepoFactory() {
    }

    private static final Repo REPO = new HibernateRepo();

    public static Repo getRepo() {
        return REPO;
    }
}
