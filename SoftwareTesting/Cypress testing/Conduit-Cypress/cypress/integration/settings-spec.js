/// <reference types="Cypress"/>

describe('Settings on Conduit', ()=>{
    beforeEach(()=>{
        cy.task('cleanDatabase');
        cy.registerUserIfNeeded();
        cy.login();
    });

    it('settings happy flow', () => {
        cy.get('[data-cy=profile]').click();
        cy.get('[data-cy=edit-profile-settings]').click();
        cy.get('[data-cy=username]').clear().type('updated user name');
        cy.get('[data-cy=bio]').clear().type('another lenghty bio...');
        cy.get('form').submit();
    });
});