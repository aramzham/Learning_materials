/// <reference types="Cypress"/>

describe('Register', ()=>{
    it('register a new user', () => {
        const username = 'visitor';
        const email = 'myemail@docs.am';
        const password = 'strongPassword';

        cy.visit('http://localhost:55476/');
        cy.contains('a.nav-link', 'Sign up').click();

        cy.location('pathname').should('equal', '/register');

        cy.screenshot('register/screenshot1');

        cy.get('[data-cy=username]').type(username);
        cy.get('[data-cy=email]').type(email);
        cy.get('[data-cy=password]').type(password);

        cy.get('form').submit();

        cy.screenshot('register/screenshot2');

        cy.location('pathname').should('equal', '/');
        cy.get('[data-cy=profile]').should('be.visible');

        cy.contains('a.nav-link', 'Your Feed').should('have.class', 'nav-link active');
        cy.contains('a.nav-link', 'Global Feed').should('not.have.class', 'nav-link active');

        cy.contains('a.nav-link', 'Global Feed').click();
        cy.contains('a.nav-link', 'Global Feed').should('have.class', 'nav-link active');
    });
});