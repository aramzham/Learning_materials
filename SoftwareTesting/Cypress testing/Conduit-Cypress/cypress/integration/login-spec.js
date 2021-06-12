/// <reference types="Cypress"/>

describe('login test suite', () => {
    it('does not work with wrong credentials', () => {
        cy.visit('http://localhost:55476/');
        cy.get('[data-cy=sign-in]').click();
        cy.get('[data-cy=username]').type('info');
        cy.get('[data-cy=password]').type('visitor');
        cy.get('[data-cy=login-form]').submit();

        cy.contains('.error-messages li', 'Error Invalid email / password.');
        cy.location('pathname').should('equal', '/login');
    });

    it('happy path test', () => {
        const email = 'myemail@docs.am';
        const password = 'strongPassword';

        cy.visit('http://localhost:55476/');
        cy.get('[data-cy=sign-in]').click();
        cy.get('[data-cy=username]').type(email);
        cy.get('[data-cy=password]').type(password);
        cy.get('[data-cy=login-form]').submit();

        cy.get('[data-cy=profile]').should('be.visible');
        cy.location('pathname').should('equal', '/');
    });
});